using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class PageError : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound404()
        {
            return View();
        }

        public IActionResult Unauthorized401()
        {
            return View();
        }

        public IActionResult Forbiden403()
        {
            return View();
        }
    }
}
