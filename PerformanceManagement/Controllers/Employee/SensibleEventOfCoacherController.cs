using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Hosting;
using PerformanceManagement.Models.Employee.Services;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class SensibleEventOfCoacherController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public SensibleEventOfCoacherController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult CoacherSensibleEvent()
        {
            return View();
        }
        public IActionResult GetSensibleEventOfCoacherList()
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
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;

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

            SensibleEventOfCoacherService sensibleEventOfCoacherService = new SensibleEventOfCoacherService(null, connProvider);
            var result = sensibleEventOfCoacherService.GetSensibleEventOfCoacherList(dataTableParameter, employeeDepartmentId, employeeId, roleId, periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetRelatedTaskCompetencyList(int sensibleEventId)
        {
            SensibleEventOfCoacherService sensibleEventOfCoacherService = new SensibleEventOfCoacherService(null, connProvider);
            return View(sensibleEventOfCoacherService.GetRelatedTaskCompetencyList(sensibleEventId));
        }
    }
}
