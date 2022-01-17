using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : BaseController<University, UniversityRepository, string>
    {
        private readonly UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository universityRepository) : base(universityRepository)
        {
            this.universityRepository = universityRepository;
        }

        [HttpGet]
        [Route("UniversityCount")]
        public ActionResult UniversityCount()
        {
            var result = universityRepository.UniversityCount();
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data loaded" });
            }
            return Ok(new { status = HttpStatusCode.NotFound, message = "Error data not found" });

        }
    }
}
