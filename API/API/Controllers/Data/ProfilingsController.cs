﻿using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
    {
        private readonly ProfilingRepository profilingRepository;
        public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
        {
            this.profilingRepository = profilingRepository;
        }
    }
}
