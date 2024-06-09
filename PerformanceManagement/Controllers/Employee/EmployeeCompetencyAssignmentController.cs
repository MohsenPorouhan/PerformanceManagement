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
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.ViewModels;
using System.Threading.Tasks;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeCompetencyAssignmentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;

        //private readonly IConfiguration config;

        public EmployeeCompetencyAssignmentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult EmployeeCompetencyAssignment()
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

            EmployeeCompetencyAssignmentService employeeCompetencyAssignmentService = new EmployeeCompetencyAssignmentService(null, connProvider);
            var result = employeeCompetencyAssignmentService.GetAssignedCompetencyList(dataTableParameter, personId, departmentIdDT, periodDefinitionIdDT);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> PerformCompetencyConfirmation()
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = shareService.GetDepartmentIdForAuthorization(personId);
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = shareService.GetMaxPeriodDefinitionId();
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult PerformCompetencyConfirmation([FromBody]List<PerformCompetencyConfirmationView> evaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            EmployeeCompetencyAssignmentService employeeCompetencyAssignmentService = new EmployeeCompetencyAssignmentService(applicationDbContext, null);
            var result = employeeCompetencyAssignmentService.PerformCompetencyConfirmation(evaluation, personId, roleId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeBehaviouralScoreAssignment(int CoacherType)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = shareService.GetDepartmentIdForAuthorization2(personId);
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = shareService.GetMaxPeriodDefinitionId();
            accessControlDecisionViewModel.ScoreScheduleTypeId = 1;

            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "EndOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                ViewBag.CoacherType = CoacherType;
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }

        }
        [HttpPost]
        public IActionResult BehaviouralScoreAssignment([FromBody]List<EvaluationCompetencyView> listOfEvaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            EmployeeCompetencyAssignmentService employeeCompetencyAssignmentService = new EmployeeCompetencyAssignmentService(applicationDbContext, null);
            var result = employeeCompetencyAssignmentService.BehaviouralScoreAssignment(listOfEvaluation, personId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> ViewScore(int periodDefinitionId, int allocatorPersonId)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = shareService.GetDepartmentIdForAuthorization(allocatorPersonId);
            accessControlDecisionViewModel.PeopleId = allocatorPersonId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "ScoreView").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult GetScore(int evaluationBehaviouralCompetencyId, int? hasParticipant, bool? participantConfirmation, int type, int? coacherLevel)
        {
            EmployeeCompetencyAssignmentService employeeCompetencyAssignmentService = new EmployeeCompetencyAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            scoreView = employeeCompetencyAssignmentService.GetCompetencyScore(evaluationBehaviouralCompetencyId);

            ViewBag.hasParticipant = hasParticipant;
            ViewBag.participantConfirmation = participantConfirmation;
            ViewBag.type = type;
            if (coacherLevel != null)
            {
                ViewBag.coacherLevel = coacherLevel;
            }
            return PartialView(scoreView);
        }
        public IActionResult EmployeeRefutationCauseCompetencyLookAt(string title, int evaluationBehaviouralCompetencyId)
        {
            ViewBag.Title = title;
            var refutationCause = applicationDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault().RefutationCause;
            ViewBag.RefutationCause = refutationCause;

            return PartialView();
        }

        public IActionResult GetCompetencyEvaluationScore(int evaluationBehaviouralCompetencyId, int level)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
            return Json(behaviouralCompetencyAssignmentService.GetCompetencyEvaluationScore(evaluationBehaviouralCompetencyId, level));
        }
    }
}
