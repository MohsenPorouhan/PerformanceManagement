using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Collections.Generic;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.HRAdmin.Services;
using System.Threading.Tasks;
using PerformanceManagement.ViewModels;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class BehaviouralCompetencyAssignmentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;

        //private readonly IConfiguration config;

        public BehaviouralCompetencyAssignmentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult BehaviouralCompetency()
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

            string employee = Request.Form["employeeId2"];

            string periodDefinitionIdDT2 = Request.Form["periodDefinitionIdDT2"];

            string departmentIdDT = Request.Form["departmentIdDT"];

            ShareService shareService = new ShareService(applicationDbContext, null);
            if (periodDefinitionIdDT2 == null || periodDefinitionIdDT2 == "")
            {
                periodDefinitionIdDT2 = shareService.GetMaxPeriodDefinitionId().ToString();
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
            var person = applicationDbContext.People.Where(c => c.PeopleId == personId && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault();
            int primaryDepartmentId = 0;
            if (person != null && (departmentIdDT == "" || departmentIdDT == null))
            {
                primaryDepartmentId = person.EvaluationHierarchyID ?? 0;
            }
            else
            {
                primaryDepartmentId = int.Parse(departmentIdDT);

            }
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(null, connProvider);
            var result = behaviouralCompetencyAssignmentService.GetAssignedCompetencyList(dataTableParameter, personId, primaryDepartmentId, employee, periodDefinitionIdDT2);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> BehaviouralCompetencyAssignment()
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
        public async Task<IActionResult> BehaviouralCompetencyAssignment(int periodDefinitionId, string[] subSetEmployeeId, string departmentSupervisor, int[] behaviourCompetency, string participant, string tagEditorTruth)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = int.Parse(departmentSupervisor.Split('-')[0]);
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
                var result = behaviouralCompetencyAssignmentService.AssignBehaviouralCompetency(periodDefinitionId, departmentSupervisor, subSetEmployeeId, behaviourCompetency, participant, personId, roleId, tagEditorTruth);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
        [HttpGet]
        public async Task<IActionResult> TransferCompetencyAssignment()
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
        [Consumes("application/json")]
        public async Task<IActionResult> TransferCompetencyAssignment([FromBody] EmployeeView employeeView)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allocatorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var priorPeriodDefinitionId = applicationDbContext.PeriodDefinitoion.ToList()[applicationDbContext.PeriodDefinitoion.Count() - 2].PeriodDefinitoionId;
            var periodDefinitionId = shareService.GetMaxPeriodDefinitionId();
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = employeeView.AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = allocatorId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);

                var result = behaviouralCompetencyAssignmentService.TransferCompetencyAssignment(employeeView, allocatorId, roleId, priorPeriodDefinitionId, periodDefinitionId);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
        [HttpGet]
        public async Task<IActionResult> BehaviouralWeightAssignment()
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
        public async Task<IActionResult> BehaviouralWeightAssignment([FromBody]List<EvaluationCompetencyView> listOfEvaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = listOfEvaluation.FirstOrDefault().AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = listOfEvaluation.FirstOrDefault().PeriodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
                var result = behaviouralCompetencyAssignmentService.BehaviouralWeightAssignment(listOfEvaluation, personId);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
        [HttpGet]
        public async Task<IActionResult> BehaviouralScoreAssignment(int CoacherType,int Level)
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
            if (Level == 2 || Level > 3)
            {
                accessControlDecisionViewModel.ScoreScheduleTypeId = 3;
            }
            else if (Level == 3)
            {
                accessControlDecisionViewModel.ScoreScheduleTypeId = 4;
            }
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
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
            var result = behaviouralCompetencyAssignmentService.BehaviouralScoreAssignment(listOfEvaluation, personId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> RenewCompetencyContract()
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
        public async Task<IActionResult> RenewCompetencyContract([FromBody] PerformCompetencyConfirmationView evaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = evaluation.AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = evaluation.PeriodDefinitionId;

            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
                var result = behaviouralCompetencyAssignmentService.RenewCompetencyContract(evaluation, personId, roleId, roleId2);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewScore(int periodDefinitionId)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = shareService.GetDepartmentIdForAuthorization(personId);
            accessControlDecisionViewModel.PeopleId = personId;
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
        public IActionResult ViewAndInputScore([FromBody]ScoreCompetencyParameterView scoreCompetencyParameterView)
        {
            if (scoreCompetencyParameterView.hasParticipant == null)
            {
                scoreCompetencyParameterView.hasParticipant = 0;
            }
            if (scoreCompetencyParameterView.participantConfirmation == null)
            {
                scoreCompetencyParameterView.participantConfirmation = false;
            }

            return PartialView(scoreCompetencyParameterView);
        }
        [HttpPost]
        public IActionResult GetAndInputScore(int evaluationBehaviouralCompetencyId, int? hasParticipant, bool? participantConfirmation, int type, int? coacherLevel)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            scoreView = behaviouralCompetencyAssignmentService.GetCompetencyScore(evaluationBehaviouralCompetencyId);

            ViewBag.hasParticipant = hasParticipant;
            ViewBag.participantConfirmation = participantConfirmation;
            ViewBag.type = type;
            if (coacherLevel != null)
            {
                ViewBag.coacherLevel = coacherLevel;
            }
            return PartialView(scoreView);
        }
        [HttpPost]
        public IActionResult GetScore(int evaluationBehaviouralCompetencyId, int? hasParticipant, bool? participantConfirmation, int type, int? coacherLevel)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            scoreView = behaviouralCompetencyAssignmentService.GetCompetencyScore(evaluationBehaviouralCompetencyId);

            ViewBag.hasParticipant = hasParticipant;
            ViewBag.participantConfirmation = participantConfirmation;
            ViewBag.type = type;
            if (coacherLevel != null)
            {
                ViewBag.coacherLevel = coacherLevel;
            }
            return PartialView(scoreView);
        }
        public IActionResult GetCoacherTruth(int behaviouralCompetencyId, int evaluationBehaviouralCompetencyId)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(null, connProvider);
            List<CoacherTruthView> coacherTruthView = null;
            coacherTruthView = behaviouralCompetencyAssignmentService.GetTruthView(behaviouralCompetencyId, evaluationBehaviouralCompetencyId);

            return PartialView(coacherTruthView);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCompetencyAssignmentPartial([FromBody]DeleteCompetencyView deleteCompetencyView)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = deleteCompetencyView.AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = deleteCompetencyView.PeriodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                if (personId == deleteCompetencyView.AllocatorPersonId)
                {
                    return PartialView(deleteCompetencyView);
                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("notAllowed", "true");
                    return Json(dic);
                }
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult DeleteCompetencyAssignment(int evaluationBehaviouralCompetencyId, int? hasParticipant)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
            return Json(behaviouralCompetencyAssignmentService.DeleteCompetencyAssignment(evaluationBehaviouralCompetencyId, hasParticipant));
        }
        public IActionResult GetCompetencyEvaluationScore(int evaluationBehaviouralCompetencyId, int level)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(applicationDbContext, null);
            return Json(behaviouralCompetencyAssignmentService.GetCompetencyEvaluationScore(evaluationBehaviouralCompetencyId, level));
        }
        public IActionResult SensibleEventView(int evaluationBehaviouralCompetencyId, int index)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(null, connProvider);


            ViewBag.index = index;

            return PartialView(behaviouralCompetencyAssignmentService.GetSensibleEvent(evaluationBehaviouralCompetencyId));
        }
        public IActionResult TruthView(int behaviouralCompetencyId, int evaluationBehaviouralCompetencyId, int index)
        {
            BehaviouralCompetencyAssignmentService behaviouralCompetencyAssignmentService = new BehaviouralCompetencyAssignmentService(null, connProvider);


            ViewBag.index = index;

            return PartialView(behaviouralCompetencyAssignmentService.GetTruthView2(behaviouralCompetencyId, evaluationBehaviouralCompetencyId));
        }
        public IActionResult RefutationCauseCompetencyLookAt(string title, int evaluationBehaviouralCompetencyId)
        {
            ViewBag.Title = title;
            var refutationCause = applicationDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault().RefutationCause;
            ViewBag.RefutationCause = refutationCause;

            return PartialView();
        }
    }
}
