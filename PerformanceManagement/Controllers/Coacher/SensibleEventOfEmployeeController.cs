using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Hosting;
using PerformanceManagement.Models.Coacher.Services;
using PerformanceManagement.Models.HRAdmin.Services;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class SensibleEventOfEmployeeController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public SensibleEventOfEmployeeController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult EmployeeSensibleEvent()
        {
            return View();
        }
        public IActionResult GetSensibleEventOfEmployeeList()
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
            int coacherId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            int? coacherDepartmentId = null;
            if (Request.Form["departmentIdDT"] != "" && Convert.ToInt32(Request.Form["departmentIdDT"]) != 0)
            {
                coacherDepartmentId = int.Parse(Request.Form["departmentIdDT"]);
            }
            else
            {
                //coacherDepartmentId = applicationDbContext.People.Where(c => c.PeopleId == coacherId && c.EffectiveEndDate == null && c.PositionType == 1).SingleOrDefault().EvaluationHierarchyID;
                var query = from i in applicationDbContext.evaluationHierarchies
                            join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                            join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                            //select new{ id2=i,dep=d.}
                            where (d.EffectiveEndDate == null && i.EffectiveEndDate == null &&
                            p.PositionType == 1 && p.EffectiveEndDate == null && p.PeopleId == coacherId && i.SupervisorId == coacherId)
                            select new { i.EvaluationHierarchyId };
                if (query.SingleOrDefault() != null)
                {
                    coacherDepartmentId = query.SingleOrDefault().EvaluationHierarchyId;
                }
            }

            int? periodDefinitionId = null;
            if (Convert.ToInt32(Request.Form["periodDefinitionIdDT"]) != 0)
            {
                periodDefinitionId = int.Parse(Request.Form["periodDefinitionIdDT"]);
            }
            else
            {
                ShareService shareService = new ShareService(applicationDbContext, null);
                periodDefinitionId = shareService.GetPeriodDefinitionId();
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

            SensibleEventOfEmployeeService sensibleEventOfEmployeeService = new SensibleEventOfEmployeeService(null, connProvider);
            var result = sensibleEventOfEmployeeService.GetSensibleEventOfCoacherList(dataTableParameter, coacherDepartmentId, coacherId, roleId, periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetRelatedTaskCompetencyList(int sensibleEventId)
        {
            SensibleEventOfEmployeeService sensibleEventOfEmployeeService = new SensibleEventOfEmployeeService(null, connProvider);
            return View(sensibleEventOfEmployeeService.GetRelatedTaskCompetencyList(sensibleEventId));
        }
    }
}
