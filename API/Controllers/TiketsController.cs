using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TicketsController : BaseController<Ticket, TicketRepository, int>
    {
        private readonly TicketRepository ticketRepository;
        public TicketsController(TicketRepository ticketRepository) : base(ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        [HttpPost("CreateTicket")]
        public ActionResult CreateTicket(TicketVM ticketVM)
        {
            var create = ticketRepository.CreateTicket(ticketVM);
            if (create > 0)
            {
                return Ok("Tiket Berhasil Ditambahkan");
            }
            else
            {
                return BadRequest("Tiket Gagal Ditambahkan");
            }
        }

        [HttpGet("GetCase")]
        public ActionResult GetProfile()
        {
            var get = ticketRepository.GetCase();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [HttpGet("ViewTicketsByUserId/{employeeId}")]
        public ActionResult ViewTicketsByUserId(string employeeId)
        {
            var get = ticketRepository.ViewTicketsByUserId(employeeId);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data tidak ditemukan");
            }
        }


        [HttpGet("ViewTicketsByStaffId/{employeeId}")]
        public ActionResult ViewTicketsByStaffId(int userId)
        {
            var get = ticketRepository.ViewTicketsByStaffId(userId);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data tidak ditemukan");
            }
        }

        [HttpGet("ViewTicketsByLevel/{level}")]
        public ActionResult ViewTicketsByLevel(int level)
        {
            var get = ticketRepository.ViewTicketsByLevel(level);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Tiket Gagal Ditutup");
            }
        }

        [HttpPost("AskNextLevel")]
        public ActionResult AskNextLevel(CloseTicketVM closeTicketVM)
        {
            var ask = ticketRepository.AskNextLevel(closeTicketVM.CaseId);
            if (ask > 0)
            {
                return Ok("Berhasil meminta bantuan");
            }
            else
            {
                return BadRequest("Gagal meminta bantuan");
            }
        }

        [HttpGet("ViewHistoryTicketsByUserId/{employeeId}")]
        public ActionResult ViewHistoryTicketsByUserId(string employeeId)
        {
            var get = ticketRepository.ViewHistoryTicketsByUserId(employeeId);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data tidak ditemukan");
            }
        }
            [HttpGet("ViewHistoryTicketsByStaffId/{employeeId}")]
            public ActionResult ViewHistoryTicketsByStaffId(int userId)
            {
                var get = ticketRepository.ViewHistoryTicketsByStaffId(userId);
                if (get != null)
                {
                    return Ok(get);
                }
                else
                {
                    return BadRequest("Data tidak ditemukan");
                }
            }

            [Route("HandleTicket")]
        [HttpPost]
        public ActionResult HandleTicket(CloseTicketVM closeTicketVM)
        {
            var create = ticketRepository.HandleTicket(closeTicketVM);
            if (create > 0)
            {
                return Ok("Tiket Berhasil Ditangani");
            }
            else
            {
                return BadRequest("Tiket Gagal Ditangani");
            }
        }

        [HttpPost("CloseTicket")]
        public ActionResult CloseTicketById(CloseTicketVM closeTicketVM)
        {
            var create = ticketRepository.CloseTicketById(closeTicketVM);
            if (create > 0)
            {
                return Ok("Tiket Berhasil Ditutup");
            }
            else
            {
                return BadRequest("Tiket Gagal Ditutup");
            }
        }

        
    }
}











/*[HttpGet("ViewHistoryTicketsByUserId/{employeeId}")]
public ActionResult ViewHistoryTicketsByUserId(string employeeId)
{
    var get = ticketRepository.ViewHistoryTicketsByUserId(employeeId);
    if (get != null)
    {
        return Ok(get);
    }
    else
    {
        return BadRequest("Data tidak ditemukan");
    }

[HttpGet("ViewHistoryTicketsByStaffId/{employeeId}")]
        public ActionResult ViewHistoryTicketsByStaffId(int userId)
        {
            var get = ticketRepository.ViewHistoryTicketsByStaffId(userId);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data tidak ditemukan");
            }
        }


[HttpPost("ReviewTicket")]
        public ActionResult ReviewTicket(ReviewTicketVM reviewTicketVM)
        {
            var create = ticketRepository.ReviewTicket(reviewTicketVM);
            if (create > 0)
            {
                return Ok("Tiket Berhasil Ditutup");
            }
            else
            {
                return BadRequest("Tiket Gagal Ditutup");
            }
        }

}*/