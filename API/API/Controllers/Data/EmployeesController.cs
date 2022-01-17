using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
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
                    //return Ok("Berhasil Input Data");

                case 4:
                    return Ok(new { status = HttpStatusCode.BadRequest, response, message = "Email Sudah Digunakan" });
                    //return Ok("Email Sudah Digunakan");

                case 5:
                    return Ok(new { status = HttpStatusCode.BadRequest, response, message = "Phone Number Sudah Digunakan" });
                    //return Ok("Phone Number Sudah Digunakan");

                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Memasukkan Data" });
                    //return BadRequest("Gagal Memasukkan Data");
            }
        }

        [HttpGet("Register/Get")]
        //[Authorize(Roles = "Director, Manager")]

        public ActionResult<GetRegisterVM> GetRegisterData()
        {
            var result = employeeRepository.GetRegister();
            return Ok(result);
        }

        [HttpGet("Register/Get/{Nik}")]
        public ActionResult<GetRegisterVM> GetRegisterID(string Nik)
        {
            var response = employeeRepository.GetRegisterId(Nik);
            if (response == null)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data Tidak Tersedia" });
            }
            return Ok(response);
        }

        [HttpPut("Register/Update")]
        public ActionResult UpdateRegister(RegisterVM registerVM)
        {
            var response = employeeRepository.UpdateRegister(registerVM);

            switch (response)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Berhasil Update Data" });

                case 4:
                    return Ok("Email Sudah Digunakan");

                case 5:
                    return Ok("Phone Number Sudah Digunakan");

                default:
                    return BadRequest("Gagal Memasukkan Data");
            }
        }

        [HttpGet("Register/Get2")]
        public ActionResult Get2()
        {
            var result = employeeRepository.GetRegister2();
            return Ok(result);
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS() {
            return Ok("Tes CORS Berhasil");
        }
    }
}
