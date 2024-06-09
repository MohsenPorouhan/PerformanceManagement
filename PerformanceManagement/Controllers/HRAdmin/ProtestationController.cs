using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PerformanceManagement.Models.Employee.Services;
using PerformanceManagement.Models.HRAdmin.View;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class ProtestationController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public ProtestationController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult Protestation()
        {
            return View();
        }
        public IActionResult GetProtestationList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var coacherId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;


            int? periodDefinitionId = null;
            if (Convert.ToInt32(Request.Form["periodDefinitionIdDT"]) != 0)
            {
                periodDefinitionId = int.Parse(Request.Form["periodDefinitionIdDT"]);
            }

            DataTableParameter dataTableParameter = new DataTableParameter
            {
                start = start,
                length = length,
                draw = draw,
                orderColumn = orderColumn,
                orderable = orderable,
                orderDIR = orderDIR,
                search = search
            };

            ProtestationService protestationService = new ProtestationService(null, connProvider);
            var result = protestationService.GetProtestationList(dataTableParameter, periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetProtestationResponseList(int protestId)
        {
            ProtestationService protestationService = new ProtestationService(null, connProvider);
            return View(protestationService.GetProtestationResponseList(protestId));
        }
        [HttpPost]
        public IActionResult AddResponsePartial([FromBody]ProtestResponse protestResponse)
        {
            return PartialView(protestResponse);
        }
        [HttpPost]
        public IActionResult AddResponse(int protestId, string protestResponse)
        {
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            ProtestationService protestationService = new ProtestationService(applicationDbContext, null);
            int result = protestationService.AddResponse(protestId, protestResponse, personId, roleId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult ConfirmationPartial(int ProtestId)
        {
            ViewData["protestId"] = ProtestId;
            return PartialView();
        }
        [HttpPost]
        public IActionResult Confirmation(int protestId, bool confirmation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ProtestationService protestationService = new ProtestationService(applicationDbContext, null);
            int result = protestationService.Confirmation(protestId, confirmation, personId);
            return Json(result);
        }
    }
}
