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

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class ScoreScheduleController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public ScoreScheduleController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }

        [HttpGet]
        public IActionResult ScoreSchedule()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddScoreSchedule()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddScoreSchedule(string selfScoreDateFrom, string selfScoreDateTo, string participantScoreDateFrom, string participantScoreDateTo, string coacher1ScoreDateFrom, string coacher1ScoreDateTo, string coacher2ScoreDateFrom, string coacher2ScoreDateTo)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            DateTime selfScoreDateFrom2;
            DateTime selfScoreDateTo2;
            DateTime participantScoreDateFrom2;
            DateTime participantScoreDateTo2;
            DateTime coacher1ScoreDateFrom2;
            DateTime coacher1ScoreDateTo2;
            DateTime coacher2ScoreDateFrom2;
            DateTime coacher2ScoreDateTo2;

            selfScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateFrom) + timeSpan;
            selfScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateTo) + timeSpan;

            participantScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateFrom) + timeSpan;
            participantScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateTo) + timeSpan;


            coacher1ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateFrom) + timeSpan;
            coacher1ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateTo) + timeSpan;


            coacher2ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateFrom) + timeSpan;
            coacher2ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateTo) + timeSpan;

            ShareService shareService = new ShareService(applicationDbContext, null);
            int periodDefinitionId = shareService.GetMaxPeriodDefinitionId();

            bool scoreScheduleExistence = applicationDbContext.ScoreSchedule.Where(c => c.PeriodDefinitionId == periodDefinitionId).Any();
            if (scoreScheduleExistence)
            {
                return Json("duplication");
            }
            SectionPeriod endPeriod = applicationDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 3).SingleOrDefault();
            if (endPeriod.DateTo >= selfScoreDateFrom2 && endPeriod.DateFrom <= selfScoreDateFrom2 && selfScoreDateFrom2 < selfScoreDateTo2 && endPeriod.DateTo >= selfScoreDateTo2 && endPeriod.DateFrom <= selfScoreDateTo2
                && endPeriod.DateTo >= participantScoreDateFrom2 && endPeriod.DateFrom <= participantScoreDateFrom2 && participantScoreDateFrom2 < participantScoreDateTo2 && endPeriod.DateTo >= participantScoreDateTo2 && endPeriod.DateFrom <= participantScoreDateTo2
                && endPeriod.DateTo >= coacher1ScoreDateFrom2 && endPeriod.DateFrom <= coacher1ScoreDateFrom2 && coacher1ScoreDateFrom2 < coacher1ScoreDateTo2 && endPeriod.DateTo >= coacher1ScoreDateTo2 && endPeriod.DateFrom <= participantScoreDateTo2
                && endPeriod.DateTo >= coacher2ScoreDateFrom2 && endPeriod.DateFrom <= coacher2ScoreDateFrom2 && coacher2ScoreDateFrom2 < coacher2ScoreDateTo2 && endPeriod.DateTo >= coacher2ScoreDateTo2 && endPeriod.DateFrom <= coacher2ScoreDateTo2)
            {
                ScoreScheduleService scoreScheduleService = new ScoreScheduleService(applicationDbContext, null);
                var result = scoreScheduleService.AddScoreSchedule(selfScoreDateFrom2, selfScoreDateTo2, participantScoreDateFrom2, participantScoreDateTo2, coacher1ScoreDateFrom2
                 , coacher1ScoreDateTo2, coacher2ScoreDateFrom2, coacher2ScoreDateTo2, periodDefinitionId, personId);
                return Json(result);
            }
            else
            {
                return Json("invalidationDate");
            }
        }
        public IActionResult GetRelatedScoreScheduleWithPeriodDefinitionList()
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
            ScoreScheduleService scoreScheduleService = new ScoreScheduleService(null, connProvider);
            var result = scoreScheduleService.GetRelatedScoreScheduleWithPeriodDefinitionList(dataTableParameter);
            return Json(result);
        }
        public IActionResult GetRelatedScoreSchedule(int periodDefinitionId)
        {
            ScoreScheduleService scoreScheduleService = new ScoreScheduleService(null, connProvider);
            return View(scoreScheduleService.GetRelatedScoreSchedule(periodDefinitionId));
        }
        public IActionResult GetRelatedScoreScheduleJsonFormat(int periodDefinitionId)
        {
            ScoreScheduleService scoreScheduleService = new ScoreScheduleService(null, connProvider);
            return Json(scoreScheduleService.GetRelatedScoreSchedule(periodDefinitionId));
        }
        [HttpPost]
        public IActionResult EditScoreSchedule(string selfScoreDateFrom, string selfScoreDateTo, string participantScoreDateFrom, string participantScoreDateTo, string coacher1ScoreDateFrom, string coacher1ScoreDateTo, string coacher2ScoreDateFrom, string coacher2ScoreDateTo, int periodDefinitionId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            DateTimeCustom dateTimeCustom = new DateTimeCustom();
            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            DateTime selfScoreDateFrom2;
            DateTime selfScoreDateTo2;
            DateTime participantScoreDateFrom2;
            DateTime participantScoreDateTo2;
            DateTime coacher1ScoreDateFrom2;
            DateTime coacher1ScoreDateTo2;
            DateTime coacher2ScoreDateFrom2;
            DateTime coacher2ScoreDateTo2;

            selfScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateFrom) + timeSpan;
            selfScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(selfScoreDateTo) + timeSpan;

            participantScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateFrom) + timeSpan;
            participantScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(participantScoreDateTo) + timeSpan;


            coacher1ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateFrom) + timeSpan;
            coacher1ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher1ScoreDateTo) + timeSpan;


            coacher2ScoreDateFrom2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateFrom) + timeSpan;
            coacher2ScoreDateTo2 = dateTimeCustom.Shamsi2Miladi(coacher2ScoreDateTo) + timeSpan;

            SectionPeriod endPeriod = applicationDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 3).SingleOrDefault();
            if (endPeriod.DateTo >= selfScoreDateFrom2 && endPeriod.DateFrom <= selfScoreDateFrom2 && selfScoreDateFrom2 < selfScoreDateTo2 && endPeriod.DateTo >= selfScoreDateTo2 && endPeriod.DateFrom <= selfScoreDateTo2
                && endPeriod.DateTo >= participantScoreDateFrom2 && endPeriod.DateFrom <= participantScoreDateFrom2 && participantScoreDateFrom2 < participantScoreDateTo2 && endPeriod.DateTo >= participantScoreDateTo2 && endPeriod.DateFrom <= participantScoreDateTo2
                && endPeriod.DateTo >= coacher1ScoreDateFrom2 && endPeriod.DateFrom <= coacher1ScoreDateFrom2 && coacher1ScoreDateFrom2 < coacher1ScoreDateTo2 && endPeriod.DateTo >= coacher1ScoreDateTo2 && endPeriod.DateFrom <= participantScoreDateTo2
                && endPeriod.DateTo >= coacher2ScoreDateFrom2 && endPeriod.DateFrom <= coacher2ScoreDateFrom2 && coacher2ScoreDateFrom2 < coacher2ScoreDateTo2 && endPeriod.DateTo >= coacher2ScoreDateTo2 && endPeriod.DateFrom <= coacher2ScoreDateTo2)
            {
                ScoreScheduleService scoreScheduleService = new ScoreScheduleService(applicationDbContext, null);

                var result = scoreScheduleService.EditScoreSchedule(selfScoreDateFrom2, selfScoreDateTo2, participantScoreDateFrom2, participantScoreDateTo2, coacher1ScoreDateFrom2, coacher1ScoreDateTo2, coacher2ScoreDateFrom2, coacher2ScoreDateTo2, periodDefinitionId, personId);
                return Json(result);
            }
            else
            {
                return Json("invalidationDate");
            }

        }
    }
}
