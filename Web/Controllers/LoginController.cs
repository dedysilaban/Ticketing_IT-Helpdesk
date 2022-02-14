using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Repository.Data;

namespace Web.Controllers
{
    public class LoginController : Controller
    {

        UserRepository repository;
        public LoginController(UserRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwToken = await repository.Auth(loginVM);
            var user = await repository.GetUserByEmail(loginVM.Email);
            if (jwToken == null || user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("JWToken", jwToken.Token);
            HttpContext.Session.SetString("EmployeeId", user.EmployeeId.ToString());
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Name", user.Name);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("RoleId", user.RoleId.ToString());

            return RedirectToAction("Index", "Panel");
        }

    }
}
