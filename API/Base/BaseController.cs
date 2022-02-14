using API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base
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
        public ActionResult<Entity> Get()
        {
            var get = repository.Get();
            if (get.Count() > 0)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Tidak ada data");
            }
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var post = repository.Insert(entity);
            if (post > 0)
            {
                return Ok("Data berhasil ditambah");
            }
            else
            {
                return BadRequest("Data gagal ditambah");
            }
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var get = repository.Get(key);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ditemukan");
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            var get = repository.Update(entity);
            if (get > 0)
            {
                return Ok("Data berhasil diubah");
            }
            else
            {
                return BadRequest("Data gagal diubah");
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var get = repository.Delete(key);
            if (get > 0)
            {
                return Ok("Data berhasil dihapus");
            }
            else
            {
                return BadRequest("Data gagal dihapus");
            }
        }
    }
}
