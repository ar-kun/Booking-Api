using Booking_Api.Contracts;
using Booking_Api.DTOs.Employees;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountRepository _accountRepository;

        public EmployeController(IEmployeRepository employeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, IAccountRepository accountRepository)
        {
            _employeRepository = employeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("details")]
        public IActionResult GetDetails()
        {
            var employees = _employeRepository.GetAll();
            var educations = _educationRepository.GetAll();
            var universities = _universityRepository.GetAll();

            if (!(employees.Any() && educations.Any() && universities.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var employeeDetails = from emp in employees
                                  join edu in educations on emp.Guid equals edu.Guid
                                  join unv in universities on edu.UniversityGuid equals unv.Guid
                                  select new EmployeDetailDto
                                  {
                                      Guid = emp.Guid,
                                      Nik = emp.Nik,
                                      FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                      BirthDate = emp.BirthDate,
                                      Gender = emp.Gender.ToString(),
                                      HiringDate = emp.HiringDate,
                                      Email = emp.Email,
                                      PhoneNumber = emp.PhoneNumber,
                                      Major = edu.Major,
                                      Degree = edu.Degree,
                                      Gpa = edu.Gpa,
                                      University = unv.Name
                                  };

            return Ok(new ResponseOKHandler<IEnumerable<EmployeDetailDto>>(employeeDetails));
        }


        // Get: api/Employe
        [HttpGet]
        public IActionResult GetAll()
        {
            var employes = _employeRepository.GetAll();
            if (!employes.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = employes.Select(x => (EmployeeDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
        }

        // Get: api/Employe/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var employe = _employeRepository.GetById(guid);
            if (employe is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)employe));
        }

        // Post: api/Employe
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                // Employe toCreate = createEmployeeDto;
                // toCreate.Nik = GenerateHandler.Nik(_employeRepository.GetLastNik());
                var createdEmploye = _employeRepository.Create(createEmployeeDto);

                return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)createdEmploye));
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

        // Put: api/Employe
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {
                var employe = _employeRepository.GetById(employeeDto.Guid);
                if (employe is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                Employe toUpdate = employeeDto;
                // toUpdate.Nik = employe.Nik;
                toUpdate.CreatedDate = employe.CreatedDate;
                _employeRepository.Update(employe);
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

        // Delete: api/Employe
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var employe = _employeRepository.GetById(guid);
                if (employe is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                _employeRepository.Delete(employe);
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
