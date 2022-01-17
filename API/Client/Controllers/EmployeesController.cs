using API.Models;
using API.ViewModel;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeRepository, string>
    {
        private readonly EmployeRepository employeRepository;
        public EmployeesController(EmployeRepository repository) : base(repository)
        {
            this.employeRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> RegisterGetData() {
            var result = await employeRepository.RegisterGet();
            return Json(result);
        }

        [HttpGet("Employees/RegisterDetailData/{NIK}")]
        public async Task<JsonResult> RegisterDetailData(String NIK)
        {
            var result = await employeRepository.RegisterDetail(NIK);
            return Json(result);
        }

        [HttpPost]
        public JsonResult AddRegisterData(RegisterVM registerVM)
        {
            var result = employeRepository.AddRegister(registerVM);
            return Json(result);
        }

        [HttpPut]
        public JsonResult UpdateRegister(RegisterVM registerVM)
        {
            var result = employeRepository.UpdateRegisterData(registerVM);
            return Json(result);
        }
    }
}
