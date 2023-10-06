using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.DTOs.Accounts;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeRepository _employeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;

        private readonly BookingManagementDbContext _context;
        private readonly IEmailHandler _emailHandler;

        public AccountController(IAccountRepository accountRepository, IEmployeRepository employeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, BookingManagementDbContext context, IEmailHandler emailHandler)
        {
            _accountRepository = accountRepository;
            _employeRepository = employeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _context = context;
            _emailHandler = emailHandler;
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(string email)
        {
            var employees = _employeRepository.GetAll();
            var employee = employees.FirstOrDefault(e => e.Email == email);
            if (employee is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });
            }
            var account = _accountRepository.GetById(employee.Guid);
            if (account is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });
            }

            var GenerateHandler = new GenerateHandler();
            int otp = GenerateHandler.GenerateRandomNumber();

            account.Otp = otp;
            account.ExpiredTime = GenerateHandler.ExpireTime();
            account.IsUsed = false;
            _accountRepository.Update(account);

            _emailHandler.Send("Forgot password", $"Your OTP verification is {account.Otp}", email);

            return Ok(new ResponseOKHandler<string>("OTP has been send to your email"));
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {

                var employees = _employeRepository.GetAll();
                var employee = employees.FirstOrDefault(e => e.Email == changePasswordDto.Email);
                var account = _accountRepository.GetById(employee.Guid);

                if (account == null || account.Otp != changePasswordDto.Otp)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP"
                    });
                }

                if (DateTime.Now > account.ExpiredTime)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has expired"
                    });
                }

                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "New password and confirm password do not match"
                    });
                }

                Account toChangePassword = changePasswordDto;

                toChangePassword.Guid = account.Guid;
                toChangePassword.CreatedDate = account.CreatedDate;
                toChangePassword.Password = HashingHandler.HashPassword(changePasswordDto.NewPassword);

                _accountRepository.Update(toChangePassword);
                return Ok(new ResponseOKHandler<string>("Password updated successfully"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to change password",
                    Error = ex.Message
                });
            }
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var employees = _employeRepository.GetAll();
            var account = _accountRepository.GetAll();
            try
            {
                var checkEmail = from emp in employees
                                 join acc in account on emp.Guid equals acc.Guid
                                 where emp.Email == loginDto.Email
                                 select new LoginDto
                                 {
                                     Email = emp.Email,
                                 };

                var checkPassword = from emp in employees
                                    join acc in account on emp.Guid equals acc.Guid
                                    where HashingHandler.VerifyPassword(loginDto.Password, acc.Password)
                                    select new LoginDto
                                    {
                                        Password = acc.Password,
                                    };

                if (checkEmail is null && checkPassword is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Email or Password is wrong"
                    });
                }

                return Ok(new ResponseOKHandler<string>("Login Success"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var createdUniversity = _universityRepository.Create(registerDto.University);
                    var createdEmployee = _employeRepository.Create(registerDto.Employee);
                    if (createdEmployee != null && createdUniversity != null)
                    {
                        registerDto.Education.UniversityGuid = createdUniversity.Guid;
                        registerDto.Account.Guid = createdEmployee.Guid;

                        var createdEducation = _educationRepository.Create(registerDto.Education);
                        var createdAccount = _accountRepository.Create(registerDto.Account);

                        if (createdEducation is null && createdAccount is null)
                        {
                            return NotFound(new ResponseErrorHandler
                            {
                                Code = StatusCodes.Status404NotFound,
                                Status = HttpStatusCode.NotFound.ToString(),
                                Message = "Data Not Found"
                            });
                        }
                    }

                    transaction.Commit();

                    return Ok(new ResponseOKHandler<string>("Register Success"));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Status = HttpStatusCode.InternalServerError.ToString(),
                        Message = "Failed to create data",
                        Error = ex.Message
                    });
                }
            }
        }

        // Get: api/Account
        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = accounts.Select(x => (AccountDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }

        // Get: api/Account/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var account = _accountRepository.GetById(guid);
            if (account is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)account));
        }

        // Post: api/Account
        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try
            {
                Account toCreate = createAccountDto;
                toCreate.Password = HashingHandler.HashPassword(createAccountDto.Password);

                var createdAccount = _accountRepository.Create(createAccountDto);

                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)createdAccount));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        // Put: api/Account
        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var account = _accountRepository.GetById(accountDto.Guid);
                if (account is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                Account toUpdate = accountDto;
                toUpdate.CreatedDate = account.CreatedDate;
                toUpdate.Password = HashingHandler.HashPassword(accountDto.Password);
                _accountRepository.Update(account);

                return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        // Delete: api/Account
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var account = _accountRepository.GetById(guid);
                if (account is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _accountRepository.Delete(account);

                return Ok(new ResponseOKHandler<string>("Data has been Deleted successfully"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

    }
}
