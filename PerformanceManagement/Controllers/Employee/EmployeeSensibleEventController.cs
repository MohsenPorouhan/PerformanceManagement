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
    public class EmployeeSensibleEventController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        //private readonly IConfiguration config;

        public EmployeeSensibleEventController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IHostingEnvironment hostingEnvironment)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult SensibleEvent()
        {
            return View();
        }
        public IActionResult GetTaskListOfEmployee(int employeeDepartmentId, int employeeId)
        {
            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            EmployeeSensibleEventService employeeSensibleEventService = new EmployeeSensibleEventService(applicationDbContext, connProvider, null);
            var result = employeeSensibleEventService.GetTaskListOfEmployee(employeeDepartmentId, employeeId);
            return Json(result);
        }
        public IActionResult GetCompetencyListOfEmployee(int employeeDepartmentId, int employeeId)
        {
            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            EmployeeSensibleEventService employeeSensibleEventService = new EmployeeSensibleEventService(applicationDbContext, connProvider, null);
            var result = employeeSensibleEventService.GetCompetencyListOfEmployee(employeeDepartmentId, employeeId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult AddSensibleEvent()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddSensibleEvent(string title, int eventType, string sensibleEventDate, string[] behaviourCompetencyId, string[] taskId, string employeeDepartmentId, IFormFile fupload, string description)
        {
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            DateTime sensibleEventDate2 = dateTimeCustom.Shamsi2Miladi(sensibleEventDate) + timeSpan;
            EmployeeSensibleEventService employeeSensibleEventService = new EmployeeSensibleEventService(applicationDbContext, null, hostingEnvironment);
            int result = employeeSensibleEventService.AddSensibleEvent(title, eventType, sensibleEventDate2, behaviourCompetencyId, taskId, roleId, personId, employeeDepartmentId, fupload, description);
            return Json(result);
        }
        public IActionResult GetSensibleEventList()
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
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

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

            EmployeeSensibleEventService employeeSensibleEventService = new EmployeeSensibleEventService(null, connProvider, null);
            var result = employeeSensibleEventService.GetSensibleEventList(dataTableParameter, employeeDepartmentId, employeeId, roleId, periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetRelatedTaskCompetencyList(int sensibleEventId)
        {
            EmployeeSensibleEventService employeeSensibleEventService = new EmployeeSensibleEventService(null, connProvider, null);
            return View(employeeSensibleEventService.GetRelatedTaskCompetencyList(sensibleEventId));
        }
    }
}
