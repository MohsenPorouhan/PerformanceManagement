using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.Services;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class SensibleEventController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        //private readonly IConfiguration config;

        public SensibleEventController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IHostingEnvironment hostingEnvironment)
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
        public IActionResult GetTaskListOfEmployee(int employeeDepartmentId, int employeeId, int cocherDepartmentId)
        {
            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            SensibleEventService sensibleEventService = new SensibleEventService(applicationDbContext, connProvider, null);
            var result = sensibleEventService.GetTaskListOfEmployee(employeeDepartmentId, employeeId, cocherDepartmentId);
            return Json(result);
        }
        public IActionResult GetCompetencyListOfEmployee(int employeeDepartmentId, int employeeId, int cocherDepartmentId)
        {
            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            SensibleEventService sensibleEventService = new SensibleEventService(applicationDbContext, connProvider, null);
            var result = sensibleEventService.GetCompetencyListOfEmployee(employeeDepartmentId, employeeId, cocherDepartmentId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult AddSensibleEvent()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddSensibleEvent(string title, int eventType, string sensibleEventDate, string[] behaviourCompetencyId, string[] taskId, bool visibility, string departmentSupervisor, string subSetEmployeeId, IFormFile fupload, string description)
        {
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            DateTime sensibleEventDate2 = dateTimeCustom.Shamsi2Miladi(sensibleEventDate) + timeSpan;
            SensibleEventService sensibleEventService = new SensibleEventService(applicationDbContext, null, hostingEnvironment);
            int result = sensibleEventService.AddSensibleEvent(title, eventType, sensibleEventDate2, behaviourCompetencyId, taskId, visibility, departmentSupervisor, roleId, personId, subSetEmployeeId, fupload, description);
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
            var coacherId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;

            int? coacherDepartmentId = null;
            if (Convert.ToInt32(Request.Form["departmentIdDT"]) != 0)
            {
                coacherDepartmentId = int.Parse(Request.Form["departmentIdDT"]);
            }
            else
            {
                coacherDepartmentId = applicationDbContext.People.Where(c => c.PeopleId == coacherId && c.EffectiveEndDate == null && c.PositionType == 1).SingleOrDefault().EvaluationHierarchyID;
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

            SensibleEventService sensibleEventService = new SensibleEventService(null, connProvider, null);
            var result = sensibleEventService.GetSensibleEventList(dataTableParameter, coacherDepartmentId, null, roleId, periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetRelatedTaskCompetencyList(int sensibleEventId)
        {
            SensibleEventService sensibleEventService = new SensibleEventService(null, connProvider, null);
            return View(sensibleEventService.GetRelatedTaskCompetencyList(sensibleEventId));
        }
    }
}
