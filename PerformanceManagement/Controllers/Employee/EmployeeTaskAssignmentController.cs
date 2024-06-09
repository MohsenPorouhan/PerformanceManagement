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
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.ViewModels;
using System.Threading.Tasks;
using PerformanceManagement.Models.HRAdmin.View;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeTaskAssignmentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;

        //private readonly IConfiguration config;

        public EmployeeTaskAssignmentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult EmployeeTaskAssignment()
        {
            return View();
        }

        public IActionResult GetAssignedTaskList()
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

            EmployeeTaskAssignmentService employeeTaskAssignmentService = new EmployeeTaskAssignmentService(null, connProvider);
            var result = employeeTaskAssignmentService.GetAssignedTaskList(dataTableParameter, personId, departmentIdDT, periodDefinitionIdDT);
            return Json(result);
        }

        public IActionResult GetDepartmentList()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            EmployeeTaskAssignmentService employeeTaskAssignmentService = new EmployeeTaskAssignmentService(null, connProvider);
            var result = employeeTaskAssignmentService.GetDepartmentList(personId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> PerformTaskConfirmation()
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
        public IActionResult PerformTaskConfirmation([FromBody]List<PerformConfirmationView> evaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            EmployeeTaskAssignmentService employeeTaskAssignmentService = new EmployeeTaskAssignmentService(applicationDbContext, null);
            var result = employeeTaskAssignmentService.PerformTaskConfirmation(evaluation, personId, roleId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeScoreAssignment()
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
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult ScoreAssignment([FromBody]List<Evaluation> listOfTasks)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            EmployeeTaskAssignmentService employeeTaskAssignmentService = new EmployeeTaskAssignmentService(applicationDbContext, null);
            var result = employeeTaskAssignmentService.ScoreAssignment(listOfTasks, personId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> ViewScore(int periodDefinitionId,int allocatorPersonId)
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
        public IActionResult GetScore(int evaluationId, int hasCriteria, int? hasParticipant, bool? participantConfirmation, int type, int? coacherLevel)
        {
            EmployeeTaskAssignmentService employeeTaskAssignmentService = new EmployeeTaskAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            if (hasCriteria > 0)
            {
                scoreView = employeeTaskAssignmentService.GetCriteriaScore(evaluationId);
            }
            else if (hasCriteria == 0)
            {
                scoreView = employeeTaskAssignmentService.GetTaskScore(evaluationId);

            }
            ViewBag.hasCriteria = hasCriteria;
            ViewBag.hasParticipant = hasParticipant;
            ViewBag.participantConfirmation = participantConfirmation;
            ViewBag.type = type;
            if (coacherLevel != null)
            {
                ViewBag.coacherLevel = coacherLevel;
            }
            return PartialView(scoreView);
        }
        public IActionResult GetCriteriaDetails(int taskId)
        {
            CriteriaService criteriaService = new CriteriaService(applicationDbContext, null);

            return Json(criteriaService.GetCriteriaList(taskId));
        }
        public IActionResult GetCriteriaDetails2(int taskId, int evaluationId)
        {
            CriteriaService criteriaService = new CriteriaService(null, connProvider);

            return Json(criteriaService.GetCriteriaList2(taskId, evaluationId));
        }
        public IActionResult GetCriteriaOfTask(int taskId,int evaluationId)
        {
            //CriteriaService criteriaService = new CriteriaService(applicationDbContext, null);

            //List<Criteria> criteria = null;
            //criteria = criteriaService.GetCriteriaList(taskId);

            //return PartialView(criteria);
            CriteriaService criteriaService = new CriteriaService(null, connProvider);
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            ViewBag.RoleId = roleId;
            var hrAdminRoleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            ViewBag.HRAdminRoleId = hrAdminRoleId;

            List<CriteriaDetailsView> criteria = null;
            criteria = criteriaService.GetAllCriteria(taskId, evaluationId);

            return PartialView(criteria);
        }
        public IActionResult EmployeeRefutationCauseLookAt(string title, int evaluationId)
        {
            ViewBag.Title = title;
            var refutationCause = applicationDbContext.Evaluation.Where(c => c.EvaluationId == evaluationId).SingleOrDefault().RefutationCause;
            ViewBag.RefutationCause = refutationCause;

            return PartialView();
        }
        public IActionResult GetEvaluationScore(int evaluationId, int level)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, connProvider);
            var result = taskAssignmentService.GetEvaluationScore(evaluationId, level);
            return Json(result);
        }
        public IActionResult GetCriteriaWeightScore(int criteriaWeightId, int level)
        {
            CriteriaService criteriaService = new CriteriaService(applicationDbContext, null);

            return Json(criteriaService.GetCriteriaWeightScore(criteriaWeightId, level));
        }
    }
}
