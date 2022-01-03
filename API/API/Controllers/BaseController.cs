using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase where Entity : class where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get() {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var response = Ok(repository.Get(key));
            if (response.Value == null)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data Tidak Tersedia" });
            }
            return response;
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            var response = repository.Insert(entity);
            if (response == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, response, message = "Berhasil Input Data" });
            }
                return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Memasukkan Data" });
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            var response = repository.Update(entity);
            if (response == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, response, message = "Data Berhasil di Ubah" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Mengubah Data" });
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var response = repository.Delete(key);
            if (response == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, response, message = "Data Berhasil di Hapus" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, response, message = "Gagal Menghapus Data" });
        }

    }
}
