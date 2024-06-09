using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin;
using Microsoft.AspNetCore.Http;
using System.Collections;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class PeriodDefinitionController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public PeriodDefinitionController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        public IActionResult PeriodDefinition()
        {
            return View();
        }
        public IActionResult PeriodDefinitionAssign(string priodCode, string title, int assignToAll, string periodDefinitionDateFrom
    , string periodDefinitionDateTo, string periodDefinitionInitialDateFrom, string periodDefinitionInitialDateTo, string periodDefinitionFinalDateFrom, string periodDefinitionFinalDateTo, string periodDefinitionProtestDateFrom, string periodDefinitionProtestDateTo)
        {
            var query = from i in applicationDbContext.evaluationHierarchies
                        join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                        join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                        //select new{ id2=i,dep=d.}
                        where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && p.EffectiveEndDate == null)
                        select new { p.PeopleId, i.EvaluationHierarchyId };
            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            var dateFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitionDateFrom) + timeSpan;
            var dateTo = dateTimeCustom.Shamsi2Miladi(periodDefinitionDateTo) + timeSpan;
            var periodDefinitionInitialDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateFrom) + timeSpan;
            var periodDefinitionInitialDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateTo) + timeSpan;
            var periodDefinitionFinalDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateFrom) + timeSpan;
            var periodDefinitionFinalDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateTo) + timeSpan;
            var periodDefinitionProtestDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionProtestDateFrom) + timeSpan;
            var periodDefinitionProtestDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionProtestDateTo) + timeSpan;
            if (assignToAll == 1 && dateFrom.Date <= periodDefinitionInitialDateFrom2.Date && dateFrom.Date <= periodDefinitionFinalDateFrom2.Date && dateFrom.Date <= periodDefinitionProtestDateFrom2.Date
                && dateTo.Date >= periodDefinitionInitialDateTo2.Date && dateTo.Date >= periodDefinitionFinalDateTo2.Date && dateTo.Date >= periodDefinitionProtestDateTo2.Date)
            {
                PeriodDefinitoion periodDefinitoion = new PeriodDefinitoion();
                periodDefinitoion.PeriodCode = priodCode;
                periodDefinitoion.PeriodTitle = title;
                periodDefinitoion.DateFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitionDateFrom) + timeSpan;
                periodDefinitoion.DateTo = dateTimeCustom.Shamsi2Miladi(periodDefinitionDateTo) + timeSpan;
                periodDefinitoion.SectionPeriods = new List<SectionPeriod>()
                {
                    new SectionPeriod{StatusCode=1, DateFrom=dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateFrom) + timeSpan,DateTo=dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateTo) + timeSpan},
                    new SectionPeriod{StatusCode=2, DateFrom=dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateTo).AddDays(1) + timeSpan,DateTo=dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateFrom).AddDays(-1) + timeSpan},
                    new SectionPeriod{StatusCode=3, DateFrom=dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateFrom) + timeSpan,DateTo=dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateTo) + timeSpan},
                    new SectionPeriod{StatusCode=4, DateFrom=dateTimeCustom.Shamsi2Miladi(periodDefinitionProtestDateFrom) + timeSpan,DateTo=dateTimeCustom.Shamsi2Miladi(periodDefinitionProtestDateTo) + timeSpan}
                };
                if (query != null)
                {
                    List<PersonPeriodEvaluationHierarchy> personPeriodEvaluationHierarchy = new List<PersonPeriodEvaluationHierarchy>();
                    foreach (var item in query.ToList())
                    {
                        personPeriodEvaluationHierarchy.Add(new PersonPeriodEvaluationHierarchy { PeopleId = item.PeopleId, EvaluationHierarchyId = item.EvaluationHierarchyId });
                    }
                    periodDefinitoion.PersonAndPeriods = personPeriodEvaluationHierarchy;
                }
                applicationDbContext.Add(periodDefinitoion);
                var saveChangeResult = applicationDbContext.SaveChanges();
                dictionary.Add("saveChangeResult", saveChangeResult);
            }
            else
            {
            }
            return Json(dictionary);
        }

        public IActionResult GetPeriodDefinition()
        {
            //[FromQuery(Name = "order[0][column]")] int orderColumn
            int start = int.Parse(Request.Query["start"]);
            int length = int.Parse(Request.Query["length"]);
            int draw = int.Parse(Request.Query["draw"]);
            string search = Request.Query["search[value]"];
            int orderColumn = int.Parse(Request.Query["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Query[concatenateOrder]);
            string orderDIR = Request.Query["order[0][dir]"];

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
            PeriodDefinitionService periodDefinitionService = new PeriodDefinitionService(connProvider);
            var result = periodDefinitionService.periodDefinitionList(dataTableParameter);
            return Json(result);
        }
        public IActionResult GetSectionPeriodDetails(int periodDefinitionId)
        {
            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);

            return View(sectionPeriodService.SectionPeriodList(periodDefinitionId));
        }
        [HttpGet]
        public IActionResult EditPeriodDefinition(int periodDefinitoionId)
        {
            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);
            List<SectionPeriod> result = sectionPeriodService.GetSections(periodDefinitoionId);
            return PartialView(result);
        }
        [HttpPost]
        public IActionResult EditPeriodDefinition([FromBody]PeriodDefinitoionView periodDefinitoionView)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);

            var result = sectionPeriodService.EditPeriodDefinition(periodDefinitoionView, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult DeletionPeriodDefinition(int periodDefinitoionId, string periodTitle)
        {
            ViewBag.PeriodDefinitionId = periodDefinitoionId;
            ViewBag.PeriodTitle = periodTitle;

            return PartialView();
        }
        [HttpPost]
        public IActionResult DeletePeriodDefinition(int periodDefinitionId)
        {
            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);

            var result = sectionPeriodService.DeletePeriodDefinition(periodDefinitionId);

            return Json(result);
        }
    }
}
