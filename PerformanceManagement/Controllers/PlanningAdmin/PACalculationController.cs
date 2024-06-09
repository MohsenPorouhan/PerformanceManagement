using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.Services;
using System;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "PlanningAdmin")]
    public class PACalculationController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public PACalculationController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult Calculation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FinalCalculation()
        {
            var priodDefinitoion = applicationDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault();
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            var finalScoreCalculation = applicationDbContext.FinalScoreCalculation.Where(c => c.PeriodDefinitoionId == priodDefinitoion.PeriodDefinitoionId && c.CoacherType == roleId).FirstOrDefault();

            FinalizeCalculation finalizeCalculation = applicationDbContext.FinalizeCalculation.Where(c => c.CocherId == personId && c.PeriodDefinitoionId == priodDefinitoion.PeriodDefinitoionId).SingleOrDefault();
            if (finalizeCalculation != null && finalizeCalculation.IsFinalization || (priodDefinitoion.DateTo < DateTime.Now))
            {
                return Json(-1);
            }
            else if (finalScoreCalculation != null)
            {
                return Json(-2);
            }

            PACalculationService paCalculationService = new PACalculationService(applicationDbContext, connProvider);
            paCalculationService.FinalCalculation(personId, roleId);
            int result = applicationDbContext.SaveChanges();
            return Json(result);
        }
        public IActionResult GetCalculationScoreList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            int? employeeId = null;

            int? periodDefinitionId = null;
            if (Convert.ToInt32(Request.Form["periodDefinitionIdDT2"]) != 0)
            {
                periodDefinitionId = int.Parse(Request.Form["periodDefinitionIdDT2"]);
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

            PACalculationService pacalculationService = new PACalculationService(null, connProvider);
            var result = pacalculationService.GetCalculationScoreList(dataTableParameter, employeeId, periodDefinitionId, roleId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult RollBack()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult RollBackCalculation()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var priodDefinitoion = applicationDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault();
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            FinalizeCalculation finalizeCalculation = applicationDbContext.FinalizeCalculation.Where(c => c.RoleId == roleId && c.PeriodDefinitoionId == priodDefinitoion.PeriodDefinitoionId).SingleOrDefault();
            if (finalizeCalculation != null && finalizeCalculation.IsFinalization || (priodDefinitoion.DateTo < DateTime.Now))
            {
                return Json(-1);
            }

            PARollBackCalculationService parollBackCalculationService = new PARollBackCalculationService(applicationDbContext, null);
            int result = parollBackCalculationService.RollBackCalculation(roleId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult FinalizeCalculationForm()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult FinalizeCalculation()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var coacherId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            PACalculationService pacalculationService = new PACalculationService(applicationDbContext, null);
            int result = pacalculationService.FinalizeCalc(coacherId, roleId);
            return Json(result);
        }
    }
}
