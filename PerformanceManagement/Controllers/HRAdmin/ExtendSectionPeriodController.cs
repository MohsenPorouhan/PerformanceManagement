using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using System.Security.Claims;
using System;
using PerformanceManagement.Models.HRAdmin;
using System.Collections.Generic;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class ExtendSectionPeriodController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public ExtendSectionPeriodController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }

        [HttpGet]
        public IActionResult ExtendSectionPeriod()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddExtendSectionPeriod()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddExtendSectionPeriod(int periodDefinitionId, string[] employee, string periodDefinitionInitialDateFrom, string periodDefinitionInitialDateTo
            , string periodDefinitionRunTimeDateFrom, string periodDefinitionRunTimeDateTo, string periodDefinitionFinalDateFrom, string periodDefinitionFinalDateTo
            , string periodDefinitionProtestDateFrom, string periodDefinitionProtestDateTo, string selfScoreDateFrom, string selfScoreDateTo, string participantScoreDateFrom
            , string participantScoreDateTo, string coacher1ScoreDateFrom, string coacher1ScoreDateTo, string coacher2ScoreDateFrom, string coacher2ScoreDateTo)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            DateTime? periodDefinitionInitialDateFrom2 = null;
            DateTime? periodDefinitionInitialDateTo2 = null;
            DateTime? periodDefinitionRunTimeDateFrom2 = null;
            DateTime? periodDefinitionRunTimeDateTo2 = null;
            DateTime? periodDefinitionFinalDateFrom2 = null;
            DateTime? periodDefinitionFinalDateTo2 = null;
            DateTime? periodDefinitionProtestDateFrom2 = null;
            DateTime? periodDefinitionProtestDateTo2 = null;

            DateTime? selfScoreDateFrom2 = null;
            DateTime? selfScoreDateTo2 = null;
            DateTime? participantScoreDateFrom2 = null;
            DateTime? participantScoreDateTo2 = null;
            DateTime? coacher1ScoreDateFrom2 = null;
            DateTime? coacher1ScoreDateTo2 = null;
            DateTime? coacher2ScoreDateFrom2 = null;
            DateTime? coacher2ScoreDateTo2 = null;

            if (periodDefinitionInitialDateFrom != null && periodDefinitionInitialDateTo != null)
            {
                periodDefinitionInitialDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateFrom) + timeSpan;
                periodDefinitionInitialDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionInitialDateTo) + timeSpan;
            }
            if (periodDefinitionRunTimeDateFrom != null && periodDefinitionRunTimeDateTo != null)
            {
                periodDefinitionRunTimeDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionRunTimeDateFrom) + timeSpan;
                periodDefinitionRunTimeDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionRunTimeDateTo) + timeSpan;
            }
            if (periodDefinitionFinalDateFrom != null && periodDefinitionFinalDateTo != null)
            {
                periodDefinitionFinalDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateFrom) + timeSpan;
                periodDefinitionFinalDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionFinalDateTo) + timeSpan;

                selfScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateFrom) + timeSpan;
                selfScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateTo) + timeSpan;

                participantScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateFrom) + timeSpan;
                participantScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateTo) + timeSpan;


                coacher1ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateFrom) + timeSpan;
                coacher1ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateTo) + timeSpan;


                coacher2ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateFrom) + timeSpan;
                coacher2ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateTo) + timeSpan;
            }
            if (periodDefinitionProtestDateFrom != null && periodDefinitionProtestDateTo != null)
            {
                periodDefinitionProtestDateFrom2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionProtestDateFrom) + timeSpan;
                periodDefinitionProtestDateTo2 = dateTimeCustom.Shamsi2Miladi(periodDefinitionProtestDateTo) + timeSpan;
            }

            ExtendSectionPeriodService extendSectionPeriodService = new ExtendSectionPeriodService(applicationDbContext, null);
            var result = extendSectionPeriodService.AddExtendSectionPeriod(periodDefinitionId, employee, personId, periodDefinitionInitialDateFrom2, periodDefinitionInitialDateTo2
                , periodDefinitionRunTimeDateFrom2, periodDefinitionRunTimeDateTo2, periodDefinitionFinalDateFrom2, periodDefinitionFinalDateTo2, periodDefinitionProtestDateFrom2
                , periodDefinitionProtestDateTo2, selfScoreDateFrom2, selfScoreDateTo2, participantScoreDateFrom2, participantScoreDateTo2, coacher1ScoreDateFrom2
                , coacher1ScoreDateTo2, coacher2ScoreDateFrom2, coacher2ScoreDateTo2);
            return Json(result);
        }
        public IActionResult GetRelatedPeriodDefinitionWithExtendSectionPeriodList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

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
            ExtendSectionPeriodService extendSectionPeriodService = new ExtendSectionPeriodService(null, connProvider);
            var result = extendSectionPeriodService.GetRelatedPeriodDefinitionWithExtendSectionPeriodList(dataTableParameter);
            return Json(result);
        }
        public IActionResult GetRelatedExtendSectionPeriod(int periodDefinitionId)
        {
            ExtendSectionPeriodService extendSectionPeriodService = new ExtendSectionPeriodService(null, connProvider);
            return View(extendSectionPeriodService.GetRelatedExtendSectionPeriod(periodDefinitionId));
        }
        public IActionResult GetRelatedPeopleWithExtendSectionPeriod(int ExtendSectionPeriodId)
        {
            ExtendSectionPeriodService extendSectionPeriodService = new ExtendSectionPeriodService(null, connProvider);
            return Json(extendSectionPeriodService.GetRelatedPeopleWithExtendSectionPeriod(ExtendSectionPeriodId));
        }
        [HttpGet]
        public IActionResult ExtendSectionEdition(int extendSectionPeriodId, string dateFrom, string dateTo, string sectionType
            , string periodTitle, int statusCode)
        {

            ExtendSectionPeriodView extendSectionPeriodView = new ExtendSectionPeriodView();
            extendSectionPeriodView.ExtendSectionPeriodId = extendSectionPeriodId;
            extendSectionPeriodView.SectionName = sectionType;
            extendSectionPeriodView.DateFrom = dateFrom;
            extendSectionPeriodView.DateTo = dateTo;
            extendSectionPeriodView.PeriodTitle = periodTitle;
            extendSectionPeriodView.StatusCode = statusCode;
            if (statusCode == 3)
            {
                List<ExtendScoreSchedule> extendScoreSchedule = applicationDbContext.ExtendScoreSchedule.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).ToList();
                List<ExtendScoreScheduleView> extendScoreScheduleViews = new List<ExtendScoreScheduleView>();
                foreach (var item in extendScoreSchedule)
                {
                    extendScoreScheduleViews.Add(new ExtendScoreScheduleView
                    {
                        ExtendScoreScheduleId = item.ExtendScoreScheduleId,
                        ScoreScheduleTypeId = item.ScoreScheduleTypeId,
                        DateFrom = item.DateFrom,
                        DateTo = item.DateTo
                    });
                }
                extendSectionPeriodView.ExtendScoreScheduleViews = extendScoreScheduleViews;
            }
            return PartialView(extendSectionPeriodView);
        }
        [HttpPost]
        public IActionResult ExtendSectionEdition(int extendSectionPeriodId, string dateFrom, string dateTo, string selfScoreDateFrom, string selfScoreDateTo
            , string participantScoreDateFrom, string participantScoreDateTo, string coacher1ScoreDateFrom, string coacher1ScoreDateTo, string coacher2ScoreDateFrom
            , string coacher2ScoreDateTo)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            PeriodDefinitoion periodDefinitoion = applicationDbContext.PeriodDefinitoion.LastOrDefault();

            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

            DateTime? dateFrom2 = null;
            DateTime? dateTo2 = null;

            DateTime selfScoreDateFrom2;
            DateTime selfScoreDateTo2;
            DateTime participantScoreDateFrom2;
            DateTime participantScoreDateTo2;
            DateTime coacher1ScoreDateFrom2;
            DateTime coacher1ScoreDateTo2;
            DateTime coacher2ScoreDateFrom2;
            DateTime coacher2ScoreDateTo2;

            dateFrom2 = dateTimeCustom.Shamsi2Miladi(dateFrom) + timeSpan;
            dateTo2 = dateTimeCustom.Shamsi2Miladi(dateTo) + timeSpan;

            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);

            if (selfScoreDateFrom != null)
            {
                selfScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateFrom) + timeSpan;
                selfScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateTo) + timeSpan;

                participantScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateFrom) + timeSpan;
                participantScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateTo) + timeSpan;


                coacher1ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateFrom) + timeSpan;
                coacher1ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateTo) + timeSpan;


                coacher2ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateFrom) + timeSpan;
                coacher2ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateTo) + timeSpan;

                if (periodDefinitoion != null && periodDefinitoion.DateTo >= dateTo2 && periodDefinitoion.DateFrom <= dateFrom2
                    && dateFrom2 < dateTo2 &&
                    dateTo2 >= selfScoreDateFrom2 && dateFrom2 <= selfScoreDateFrom2 && selfScoreDateFrom2 < selfScoreDateTo2 && dateTo2 >= selfScoreDateTo2 && dateFrom2 <= selfScoreDateTo2
                && dateTo2 >= participantScoreDateFrom2 && dateFrom2 <= participantScoreDateFrom2 && participantScoreDateFrom2 < participantScoreDateTo2 && dateTo2 >= participantScoreDateTo2 && dateFrom2 <= participantScoreDateTo2
                && dateTo2 >= coacher1ScoreDateFrom2 && dateFrom2 <= coacher1ScoreDateFrom2 && coacher1ScoreDateFrom2 < coacher1ScoreDateTo2 && dateTo2 >= coacher1ScoreDateTo2 && dateFrom2 <= participantScoreDateTo2
                && dateTo2 >= coacher2ScoreDateFrom2 && dateFrom2 <= coacher2ScoreDateFrom2 && coacher2ScoreDateFrom2 < coacher2ScoreDateTo2 && dateTo2 >= coacher2ScoreDateTo2 && dateFrom2 <= coacher2ScoreDateTo2)
                {
                    var result = sectionPeriodService.ExtendFinalSectionEdition(extendSectionPeriodId, dateFrom2, dateTo2, selfScoreDateFrom2, selfScoreDateTo2, participantScoreDateFrom2
                , participantScoreDateTo2, coacher1ScoreDateFrom2, coacher1ScoreDateTo2, coacher2ScoreDateFrom2, coacher2ScoreDateTo2, personId);
                    return Json(result);
                }
                else
                {
                    return Json("invalidDateRange");

                }
            }
            else if (periodDefinitoion != null && periodDefinitoion.DateTo >= dateTo2 && periodDefinitoion.DateFrom <= dateFrom2)
            {
                var result = sectionPeriodService.ExtendSectionEdition(extendSectionPeriodId, dateFrom2, dateTo2);
                return Json(result);
            }
            else
            {
                return Json("invalidDateRange");
            }
        }
        [HttpGet]
        public IActionResult ExtendSectionDeletion(int extendSectionPeriodId, string sectionType
            , string periodTitle)
        {
            ExtendSectionPeriodView extendSectionPeriodView = new ExtendSectionPeriodView();
            extendSectionPeriodView.ExtendSectionPeriodId = extendSectionPeriodId;
            extendSectionPeriodView.SectionName = sectionType;
            extendSectionPeriodView.PeriodTitle = periodTitle;

            return PartialView(extendSectionPeriodView);
        }
        [HttpPost]
        public IActionResult ExtendSectionDeletion(int extendSectionPeriodId)
        {
            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);

            var result = sectionPeriodService.ExtendSectionDeletion(extendSectionPeriodId);

            return Json(result);
        }
        [HttpGet]
        public IActionResult RelatedPeopleWithSectionDeletionn(int ExtendSectionPeriodWithPeopleId)
        {
            ViewBag.ExtendSectionPeriodWithPeopleId = ExtendSectionPeriodWithPeopleId;
            return PartialView();
        }
        [HttpPost]
        public IActionResult RelatedPeopleWithSectionDeletion(int ExtendSectionPeriodWithPeopleId)
        {
            SectionPeriodService sectionPeriodService = new SectionPeriodService(applicationDbContext);

            var result = sectionPeriodService.RelatedPeopleWithSectionDeletion(ExtendSectionPeriodWithPeopleId);

            return Json(result);
        }
        [HttpGet]
        public IActionResult GetExtendScoreSchedule(int ExtendSectionPeriodId)
        {
            ExtendSectionPeriodService extendSectionPeriodService = new ExtendSectionPeriodService(applicationDbContext, null);

            return PartialView(extendSectionPeriodService.GetExtendScoreSchedule(ExtendSectionPeriodId));
        }
    }
}
