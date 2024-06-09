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

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ProtestController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        //private readonly IConfiguration config;

        public ProtestController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IHostingEnvironment hostingEnvironment)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Protest()
        {
            return View();
        }
        public IActionResult GetAddressee(int employeeDepartmentId, int employeeId)
        {
            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ProtestService protestService = new ProtestService(null, connProvider);
            var result = protestService.GetAddressee(employeeDepartmentId, employeeId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult AddProtestation()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddProtestation(string employeeDepartmentId, string[] addressee, bool visibility, string description)
        {
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            ProtestService protestService = new ProtestService(applicationDbContext, null);
            int result = protestService.AddProtestation(employeeDepartmentId, addressee, visibility, description, personId);
            return Json(result);
        }
        public IActionResult GetProtestList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employeeId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            int? employeeDepartmentId = null;
            if (Convert.ToInt32(Request.Form["departmentIdDT"]) != 0)
            {
                employeeDepartmentId = int.Parse(Request.Form["departmentIdDT"]);
            }
            else
            {
                employeeDepartmentId = applicationDbContext.People.Where(c => c.PeopleId == employeeId && c.EffectiveEndDate == null && c.PositionType == 1).SingleOrDefault().EvaluationHierarchyID;
            }

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

            ProtestService protestService = new ProtestService(null, connProvider);
            var result = protestService.GetProtestList(dataTableParameter, employeeDepartmentId, employeeId, periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetProtestationResponseList(int protestId)
        {
            ProtestService protestService = new ProtestService(null, connProvider);
            return View(protestService.GetProtestationResponseList(protestId));
        }
        [HttpPost]
        public IActionResult AddEmployeeResponsePartial([FromBody]ProtestResponse protestResponse)
        {
            return PartialView(protestResponse);
        }
        [HttpPost]
        public IActionResult AddEmployeeResponse(int protestId, int PersonDepartmentId, string protestResponse)
        {
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            ProtestService protestService = new ProtestService(applicationDbContext, null);
            int result = protestService.AddEmployeeResponse(protestId, PersonDepartmentId, protestResponse, personId, roleId);
            return Json(result);
        }
    }
}
