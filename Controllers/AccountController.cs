using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.DTOs.Accounts;
using Booking_Api.Models;
using Booking_Api.Repositories;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeRepository _employeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly BookingManagementDbContext _context;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;

        public AccountController(IAccountRepository accountRepository, IEmployeRepository employeRepository, IUniversityRepository  universityRepository, IEducationRepository educationRepository, BookingManagementDbContext context, IEmailHandler emailHandler, ITokenHandler tokenHandler, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _employeRepository = employeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _context = context;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login(LoginDto loginDto)
        {
            var employees = _employeRepository.GetAll();
            var employee = employees.FirstOrDefault(e => e.Email == loginDto.Email);
            var account = _accountRepository.GetAll();
            var acount = _accountRepository.GetById(employee.Guid);
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

                var claims = new List<Claim>();
                claims.Add(new Claim("Email", loginDto.Email));
                claims.Add(new Claim("FullName", string.Concat(employee.FirstName + " " + employee.LastName)));
                var getRoleName = from ar in _accountRoleRepository.GetAll()
                                  join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                  where ar.AccountGuid == acount.Guid
                                  select r.Name;
                foreach (var roleName in getRoleName)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }
                var generateToken = _tokenHandler.Generate(claims);

                return Ok(new ResponseOKHandler<string>("Login Success", new { Token = generateToken }));
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
        [AllowAnonymous]
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
