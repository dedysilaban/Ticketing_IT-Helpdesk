using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Base;
using Web.Repository.Data;

namespace Web.Controllers
{
    [Authorize]
    public class PanelController : BaseController<Employee, UserRepository, string>
    {
        private readonly UserRepository userRepository;
        private readonly CaseRepository caseRepository;
        private readonly ConvertationRepository convertationRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly HistoryRepository historyRepository;
   
        private readonly RoleRepository roleRepository;
        private readonly StatusCodeRepository statusCodeRepository;

        public PanelController(
            UserRepository userRepository,
            CaseRepository caseRepository,
            ConvertationRepository convertationRepository,
            CategoryRepository categoryRepository,
            HistoryRepository historyRepository,
     
            RoleRepository roleRepository,
            StatusCodeRepository statusCodeRepository
            ) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.caseRepository = caseRepository;
            this.convertationRepository = convertationRepository;
            this.categoryRepository = categoryRepository;
            this.historyRepository = historyRepository;
    
            this.roleRepository = roleRepository;
            this.statusCodeRepository = statusCodeRepository;
        }

        public void GetSession()
        {
            ViewBag.EmployeeId = HttpContext.Session.GetString("EmployeeId");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.RoleId = HttpContext.Session.GetString("RoleId");

            ViewBag.CurrentPage = "";
            if (ViewBag.Role == "Technical Support")
            {
                ViewBag.Level = 2;
            }
            else
            {
                ViewBag.Level = 0;
            }
        }
        public IActionResult Profile()
        {
            GetSession();
            ViewBag.CurrentPage = "Profile";
            return View();
        }
        public IActionResult Users()
        {
            GetSession();
            ViewBag.CurrentPage = "Users";
            return View();
        }

        public async Task<JsonResult> GetUsers()
        {
            GetSession();
            var result = await userRepository.GetUsers();
            return Json(result);
        }

        public async Task<JsonResult> GetCases()
        {
            GetSession();
            var result = await caseRepository.GetCases();
            return Json(result);
        }

        public async Task<JsonResult> GetTickets()
        {
            GetSession();
            if (ViewBag.EmployeeId != null)
            {
                var result = await caseRepository.GetTicketsByUserId(ViewBag.EmployeeId);
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> GetHistoryTickets()
        {
            GetSession();
            if (ViewBag.EmployeeId != null)
            {
                var result = await caseRepository.GetHistoryTicketsByUserId(ViewBag.EmployeeId);
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> GetConvertations()
        {
            GetSession();
            var result = await convertationRepository.GetConvertations();
            return Json(result);
        }

        public async Task<JsonResult> GetCategories()
        {
            GetSession();
            var result = await categoryRepository.GetCategories();
            return Json(result);
        }

        public async Task<JsonResult> GetHistories()
        {
            GetSession();
            var result = await historyRepository.GetHistories();
            return Json(result);
        }


        public async Task<JsonResult> GetRoles()
        {
            GetSession();
            var result = await roleRepository.GetRoles();
            return Json(result);
        }

        public async Task<JsonResult> GetStatusCodes()
        {
            GetSession();
            var result = await statusCodeRepository.GetStatusCodes();
            return Json(result);
        }

        // Staff
        public IActionResult ManageTickets()
        {
            GetSession();
            ViewBag.CurrentPage = "ManageTickets";
            return View();
        }

        public IActionResult ViewHandleTickets()
        {
            GetSession();
            ViewBag.CurrentPage = "ViewHandleTickets";
            return View();
        }

        public async Task<JsonResult> GetTicketsByLevel()
        {
            GetSession();
            if (ViewBag.Role != null) {
                int level;
                if(ViewBag.Role == "Technical Support")
                {
                    level = 2;
                } 
                else
                {
                    level = 0;
                }
                var result = await caseRepository.GetTicketsByLevel(level);
                return Json(result);
            }
            else
            {
                return Json(null);
            }
        }

        public async Task<JsonResult> GetHandleTickets()
        {
            GetSession();
            if(ViewBag.EmployeeId != null)
            {
                var result = await caseRepository.GetTicketsByStaffId(ViewBag.EmployeeId);
                return Json(result);
            }
            else
            {
                return Json(null);
            }
        }

        public async Task<JsonResult> GetHistoryHandleTickets()
        {
            GetSession();
            if (ViewBag.EmployeeId != null)
            {
                var result = await caseRepository.GetHistoryTicketsByStaffId(ViewBag.EmployeeId);
                return Json(result);
            }
            else
            {
                return Json(null);
            }
        }

        public IActionResult ViewConvertations()
        {
            GetSession();
            ViewBag.CurrentPage = "ViewConvertations";
            return View();
        }

        public IActionResult ViewAttachments()
        {
            GetSession();
            ViewBag.CurrentPage = "ViewAttachments";
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Auth(LoginVM loginVM)
        //{
        //    var jwToken = userRepository.Auth(loginVM);
        //    if (jwToken == null)
        //    {
        //        return RedirectToAction("index");
        //    }
        //    HttpContext.Session.SetString("JWToken", jwToken.Token);
        //    return RedirectToAction("Index", "Panel");
        //}


        // GET: PanelController
        public IActionResult Index()
        {
            GetSession();
            ViewBag.CurrentPage = "PanelIndex";
            return View();
        }

        public IActionResult Cases()
        {
            GetSession();
            ViewBag.CurrentPage = "Cases";
            return View();
        }

        public IActionResult Tickets()
        {
            GetSession();
            ViewBag.CurrentPage = "Tickets";
            return View();
        }

        public IActionResult Convertations()
        {
            GetSession();
            ViewBag.CurrentPage = "Convertations";
            return View();
        }

        public IActionResult Attachments()
        {
            GetSession();
            ViewBag.CurrentPage = "Attachments";
            return View();
        }

        public IActionResult Histories()
        {
            GetSession();
            ViewBag.CurrentPage = "Histories";
            return View();
        }

        public IActionResult Categories()
        {
            GetSession();
            ViewBag.CurrentPage = "Categories";
            return View();
        }

        public IActionResult StatusCodes()
        {
            GetSession();
            ViewBag.CurrentPage = "StatusCodes";
            return View();
        }

        public IActionResult Priorities()
        {
            GetSession();
            ViewBag.CurrentPage = "Priorities";
            return View();
        }

        public IActionResult Roles()
        {
            GetSession();
            ViewBag.CurrentPage = "Roles";
            return View();
        }

        public IActionResult CreateTicket()
        {
            GetSession();
            ViewBag.CurrentPage = "CreateTicket";
            return View();
        }

        //[AllowAnonymous]
        //[Route("ViewConvertationByCaseId")]
        //[HttpGet("ViewConvertationByCaseId/{caseId}")]
        [HttpGet("{caseId}")]
        public async Task<IActionResult> GetConvertationsByCaseId(int caseId)
        {
            var result = await convertationRepository.GetConvertationByCaseId(caseId);
            return Json(result);
        }

            public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
