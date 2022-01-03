using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class RolesController : BaseController<Role, RoleRepository, string>
    {
        private readonly RoleRepository roleRepository;

        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpPost("AssignManager")]
        [Authorize(Roles = "Director")]
        public ActionResult AssignManager(AccountRole accountRole)
        {
            var response = roleRepository.AssignManager(accountRole);
            switch (response)
            {
                case 0:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "NIK Belum Terdaftar, Silahkan Melakukan Pendaftaran" });
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, response, message = $"NIK {accountRole.NIK} Berhasil di Tambahkan Menjadi Manager" });
                case 2:
                    return Ok(new { status = HttpStatusCode.OK, response, message = "Sudah Menjadi Manager" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Menambahkan Manager" });
            }
        }
    }
}
