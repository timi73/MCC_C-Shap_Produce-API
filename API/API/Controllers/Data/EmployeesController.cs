using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("{Register}")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var response = employeeRepository.Register(registerVM);

            switch (response)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Berhasil Input Data" });

                case 4:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Email Sudah Digunakan" });

                case 5:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Phone Number Sudah Digunakan" });

                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Memasukkan Data" });
            }
        }

        [HttpGet("Register/Get")]
        [Authorize(Roles = "Director, Manager")]
        public new ActionResult<GetRegisterVM> Get()
        {
            var result = employeeRepository.GetRegister();
            return Ok(result);
        }

        //[HttpGet("Register/Get2")]
        //public ActionResult Get2()
        //{
        //    var result = employeeRepository.GetRegister2();
        //    return Ok(result);
        //}

        [HttpGet("TestCORS")]
        public ActionResult TestCORS() {
            return Ok("Tes CORS Berhasil");
        }
    }
}
