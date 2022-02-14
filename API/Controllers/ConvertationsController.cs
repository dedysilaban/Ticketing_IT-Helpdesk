using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ConvertationsController : BaseController<Convertation, ConvertationRepository, int>
    {
        private readonly ConvertationRepository convertationRepository;
        public ConvertationsController(ConvertationRepository convertationRepository) : base(convertationRepository)
        {
            this.convertationRepository = convertationRepository;
        }

        [HttpGet("ViewConvertations")]
        public ActionResult ViewConvertations()
        {
            var get = convertationRepository.ViewConvertations();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [HttpGet("GetConvertation")]
        public ActionResult GetConvertation()
        {
            var get = convertationRepository.GetConvertation();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [HttpPost("CreateConvertations")]
        public ActionResult CreateConvertation(CreateConvertationVM createConvertationVM)
        {

            var post = convertationRepository.CreateConvertation(createConvertationVM);
            if (post > 0)
            {
                    return Ok("Berhasil mengirim pesan");
            }

            else
            {
                return BadRequest("Gagal mengirim pesan");
            }

        }

        [HttpGet("ViewConvertationsByCaseId/{id}")]
        public ActionResult ViewConvertationsByCaseId(int id)
        {
            var get = convertationRepository.ViewConvertationsByCaseId(id);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [HttpGet("ViewConvertationsByUserId/{id}")]
        public ActionResult ViewConvertationsByUserId(string id)
        {
            var get = convertationRepository.ViewConvertationsByUserId(id);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }
    }
}