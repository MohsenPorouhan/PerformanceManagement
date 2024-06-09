using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Collections.Generic;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.Services;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Models.Coacher.View;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ParticipantCompetencyAssignmentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public ParticipantCompetencyAssignmentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult ParticipantCompetencyAssignment()
        {
            return View();
        }
        public IActionResult GetAssignedCompetencyList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            string periodDefinitionIdDT = Request.Form["periodDefinitionIdDT"];

            string departmentIdDT = Request.Form["departmentIdDT"];
            if (periodDefinitionIdDT == "")
            {
                periodDefinitionIdDT = null;
            }
            if (departmentIdDT == "")
            {
                departmentIdDT = null;
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
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            ParticipantCompetencyAssignmentService participantCompetencyAssignmentService = new ParticipantCompetencyAssignmentService(null, connProvider);
            var result = participantCompetencyAssignmentService.GetAssignedCompetencyList(dataTableParameter, personId, departmentIdDT, periodDefinitionIdDT);
            return Json(result);
        }

        [HttpGet]
        public IActionResult ParticipantPerformCompetencyConfirmation()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult ParticipantPerformCompetencyConfirmation([FromBody]PerformCompetencyConfirmationView evaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            ParticipantCompetencyAssignmentService participantCompetencyAssignmentService = new ParticipantCompetencyAssignmentService(applicationDbContext, null);
            var result = participantCompetencyAssignmentService.ParticipantPerformCompetencyConfirmation(evaluation, personId, roleId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult BehaviouralScoreAssignment(int CoacherType)
        {
            ViewBag.CoacherType = CoacherType;
            return PartialView();
        }
        [HttpPost]
        public IActionResult BehaviouralScoreAssignment([FromBody]List<EvaluationCompetencyView> listOfEvaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ParticipantCompetencyAssignmentService participantCompetencyAssignmentService = new ParticipantCompetencyAssignmentService(applicationDbContext, null);
            var result = participantCompetencyAssignmentService.BehaviouralScoreAssignment(listOfEvaluation, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult ViewScore()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult GetScore(int evaluationBehaviouralCompetencyId, int type)
        {
            ParticipantCompetencyAssignmentService participantCompetencyAssignmentService = new ParticipantCompetencyAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            scoreView = participantCompetencyAssignmentService.GetCompetencyScore(evaluationBehaviouralCompetencyId);

            ViewBag.type = type;
            return PartialView(scoreView);
        }
    }
}
