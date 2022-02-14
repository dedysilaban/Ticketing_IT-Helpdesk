using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository userRepository;
        public EmployeesController(EmployeeRepository userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }

/*        [Authorize(Roles = "Admin")]*/
        [HttpPost("Register")]
        public ActionResult Register (RegisterVM registerVM)
        {
            var register = userRepository.Register(registerVM);
            if (register > 0)
            {
                return Ok("Register Berhasil");
            }
            else
            {
                return BadRequest("Register Gagal");
            }
        }

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(Employee user)
        {
            var create = userRepository.CreateUser(user);
            if (create > 0)
            {
                return Ok("Register Berhasil");
            }
            else
            {
                return BadRequest("Register Gagal");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = userRepository.Login(loginVM);
            if (login == 404)
            {
                return BadRequest("Email tidak ditemukan, Silakan gunakan email lain");
            }
            else if (login == 401)
            {
                return BadRequest("Password salah");
            }
            else if (login == 1)
            {
                return Ok(
                    new JWTokenVM
                    {
                        Token = userRepository.GenerateTokenLogin(loginVM),
                        Messages = "Login Success"
                    }
                    );

            }
            else
            {
                return BadRequest("Gagal login");
            }
        }

        [Authorize]
        [HttpGet("GetClients")]
        public ActionResult GetClients()
        {
            var get = userRepository.GetClients();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [Authorize]
        [HttpGet("GetClientbyId/{id}")]
        public ActionResult GetClientbyId(string id)
        {
            var get = userRepository.GetClientById(id);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [Authorize]
        [HttpPost("DeleteClientById/{id}")]
        public ActionResult DeleteClientById(string id)
        {
            var get = userRepository.DeleteClientById(id);
            if (get != 0)
            {
                return Ok("Data Berhasil Dihapus");
            }
            else
            {
                return BadRequest("Data Gagal Dihapus");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetUserByEmail/{email}")]
        public ActionResult GetUserByEmail(string email)
        {
            var get = userRepository.GetUserByEmail(email);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetStaffs")]
        public ActionResult GetStaffs()
        {
            var get = userRepository.GetStaffs();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetStaffbyId/{id}")]
        public ActionResult GetStaffbyId(string id)
        {
            var get = userRepository.GetStaffById(id);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return BadRequest("Data Tidak Ditemukan");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteStaffById/{id}")]
        public ActionResult DeleteStaffById(string id)
        {
            var get = userRepository.DeleteStaffById(id);
            if (get != 0)
            {
                return Ok("Data Berhasil Dihapus");
            }
            else
            {
                return BadRequest("Data Gagal Dihapus");
            }
        }
        [HttpPost("UpdateProfile")]
        public ActionResult UpdateProfile(Employee user)
        {
            var register = userRepository.UpdateProfile(user);
            if (register > 0)
            {
                return Ok("Data Berhasil Diubah");
            }
            else
            {
                return BadRequest("Data Gagal Diubah");
            }
        }

        [HttpGet("GetProfile")]
        public ActionResult GetProfile()
        {
            var get = userRepository.GetProfile();
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


















/*

 

*/
