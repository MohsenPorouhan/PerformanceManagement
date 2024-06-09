using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Collections.Generic;
using System.Linq;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.ViewModels;
using System.Threading.Tasks;
using PerformanceManagement.Models.HRAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class TaskAssignmentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;

        //private readonly IConfiguration config;

        public TaskAssignmentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult TaskAssignment()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DirectTaskAssignment()
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
        public async Task<IActionResult> TaskAssignment([FromBody] CoveredEmployee coveredEmployee)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, null);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allocatorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = coveredEmployee.AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = allocatorId;
            accessControlDecisionViewModel.PeriodDefinitionId = coveredEmployee.PeriodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
                var result = taskAssignmentService.AssignTask(coveredEmployee, allocatorId, roleId);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
        public IActionResult GetDepartmentSupervisorList()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetDepartmentSupervisorList(personId);
            return Json(result);
        }
        public IActionResult GetParentDepartmentList(int departmentId, int level)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetParentDepartmentList(departmentId, level, personId);
            return Json(result);
        }
        public IActionResult GetDirectEmployee(int departmentId)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetDirectEmployee(departmentId);
            return Json(result);
        }
        public IActionResult GetParentOfSubTaskList(int departmentId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var supervisorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetParentOfSubTaskList(departmentId, supervisorId);
            return Json(result);
        }
        public IActionResult GetSubTaskList(int parentDepartmentId, int departmentId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var supervisorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetSubTaskList(departmentId, supervisorId, parentDepartmentId);
            return Json(result);
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

            string employee = Request.Form["employeeId2"];

            string periodDefinitionIdDT2 = Request.Form["periodDefinitionIdDT2"];
            ShareService shareService = new ShareService(applicationDbContext, null);
            if (periodDefinitionIdDT2 == null || periodDefinitionIdDT2 == "")
            {
                periodDefinitionIdDT2 = shareService.GetMaxPeriodDefinitionId().ToString();
            }
            string departmentIdDT = Request.Form["departmentIdDT"];


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
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetAssignedTaskList(dataTableParameter, personId, primaryDepartmentId, employee, periodDefinitionIdDT2);
            return Json(result);
        }
        public IActionResult GetDepartmentResponsibiltyList()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var supervisorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
            var result = taskAssignmentService.GetDepartmentResponsibiltyList(supervisorId);
            return Json(result);
        }
        public IActionResult GetSubSetDepartments(int departmentId)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetSubSetDepartments(departmentId);
            return Json(result);
        }
        public IActionResult GetDirectEmployees(int departmentId, bool isSupervisor = false)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetDirectEmployees(departmentId, isSupervisor);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> IndirectTaskAssignment()
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
        public async Task<IActionResult> AssignTextTaskToIndirect([FromBody] CoveredEmployee coveredEmployee)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allocatorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = coveredEmployee.AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = allocatorId;
            accessControlDecisionViewModel.PeriodDefinitionId = coveredEmployee.PeriodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
                var result = taskAssignmentService.AssignTextTaskToIndirect(coveredEmployee, allocatorId, roleId);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
        public IActionResult GetInDirectEmployees(int departmentId, int peopleId)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetInDirectEmployees(departmentId, peopleId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> WeightAssignment()
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
        public async Task<IActionResult> WeightAssignment([FromBody] List<WeightAssignmentView> listOfTasks)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = listOfTasks.FirstOrDefault().AllocatorEvaluationHierarchyId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = listOfTasks.FirstOrDefault().PeriodDefinitoionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
                var result = taskAssignmentService.WeightAssignment(listOfTasks, personId);
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
        public async Task<IActionResult> ScoreAssignment(int CoacherType, int Level)
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
        public IActionResult ScoreAssignment([FromBody] List<Evaluation> listOfTasks)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
            var result = taskAssignmentService.ScoreAssignment(listOfTasks, personId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> RenewContract()
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
        public async Task<IActionResult> RenewContract([FromBody] PerformConfirmationView evaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = evaluation.DepartmnetId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = evaluation.PeriodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
                var result = taskAssignmentService.RenewContract(evaluation, personId, roleId, roleId2);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
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
        public IActionResult GetCriteriaWeightScore(int criteriaWeightId, int level)
        {
            CriteriaService criteriaService = new CriteriaService(applicationDbContext, null);

            return Json(criteriaService.GetCriteriaWeightScore(criteriaWeightId, level));
        }
        public IActionResult GetCriteriaWeightDetails(int taskId, int evaluationId)
        {
            CriteriaService criteriaService = new CriteriaService(null, connProvider);

            return Json(criteriaService.GetCriteriaWeightList(taskId, evaluationId));
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
        [HttpGet]
        public async Task<IActionResult> SubSetViewScore(int periodDefinitionId)
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
        public IActionResult ViewScore2([FromBody] ScoreParameterView scoreParameterView)
        {
            if (scoreParameterView.hasParticipant == null)
            {
                scoreParameterView.hasParticipant = 0;
            }
            if (scoreParameterView.participantConfirmation == null)
            {
                scoreParameterView.participantConfirmation = false;
            }

            return PartialView(scoreParameterView);
        }
        [HttpPost]
        public IActionResult GetScore(int evaluationId, int hasCriteria, int? hasParticipant, bool? participantConfirmation, int type, int? coacherLevel)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            if (hasCriteria > 0)
            {
                scoreView = taskAssignmentService.GetCriteriaScore(evaluationId);
            }
            else if (hasCriteria == 0)
            {
                scoreView = taskAssignmentService.GetTaskScore(evaluationId);

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
        [HttpGet]
        public async Task<IActionResult> EditTaskAssignment(int periodDefinitionId, int departmentId, int allocatorPersonId
            , string title, int resourceType, int taskId, bool? participantConfirmation, int evaluationId, int? parentTaskId)
        {
            applicationDbContext.People.ToList();
            ShareService shareService = new ShareService(applicationDbContext, connProvider);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = departmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
            List<TrainingNeed> trainingNeed = taskAssignmentService.GetTrainingNeed(evaluationId);
            if (authorized.Succeeded)
            {
                bool isTrue = false;
                isTrue = participantConfirmation ?? false;
                if (personId == allocatorPersonId && ((resourceType == 3 && isTrue) || trainingNeed != null || resourceType == 4))
                {
                    EvaluationView evaluationView = new EvaluationView();
                    evaluationView.AllocatorDepartmentId = departmentId;
                    evaluationView.allocatorPersonId = allocatorPersonId;
                    if (resourceType == 4)
                    {
                        evaluationView.Criterias = taskAssignmentService.GetCriteriaList(taskId);
                    }
                    evaluationView.EvaluationId = evaluationId;
                    evaluationView.ParticipantConfirmation = participantConfirmation;
                    evaluationView.PeriodDefinitionId = periodDefinitionId;
                    evaluationView.ResourceType = resourceType;
                    evaluationView.TaskId = taskId;
                    evaluationView.TaskTitle = title;
                    evaluationView.ParentTaskId = parentTaskId;
                    evaluationView.TrainingNeeds = trainingNeed;
                    if (participantConfirmation ?? false)
                    {
                        EvaluationParticipant evaluationParticipant = taskAssignmentService.GetEvaluationParticipant(evaluationId);
                        evaluationView.ParticipantId = evaluationParticipant.ParticipantId;
                        evaluationView.ParticipantDepartmentId = evaluationParticipant.ParticipantEvaluationHierarchyId;
                        evaluationView.EvaluationParticipantId = evaluationParticipant.EvaluationParticipantId;
                        evaluationView.EvaluationParticipants = shareService.ParticipantList2();
                    }

                    return PartialView(evaluationView);
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
        public IActionResult EditTaskAssignment([FromBody] EvaluationEditionView evaluationEditionView)
        {
            applicationDbContext.People.ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);

            return Json(taskAssignmentService.EditTaskAssignment(evaluationEditionView, personId));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTaskAssignmentPartial([FromBody] EvaluationView evaluationView)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = evaluationView.AllocatorDepartmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = evaluationView.PeriodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded)
            {
                if (personId == evaluationView.allocatorPersonId && (evaluationView.ResourceType != 1 && evaluationView.ResourceType != 2))
                {
                    return PartialView(evaluationView);

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
        public IActionResult DeleteTaskAssignment(int evaluationId, int? hasParticipant)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, connProvider);
            return Json(taskAssignmentService.DeleteTaskAssignment(evaluationId, hasParticipant));
        }
        public IActionResult PriorTaskOfPeriod(string term)
        {
            applicationDbContext.People.ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, connProvider);
            var result = taskAssignmentService.PriorTaskOfPeriod(term, personId);
            return Json(result);
        }
        public IActionResult RelatedTaskList(int departmentId)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, connProvider);
            var result = taskAssignmentService.RelatedTaskList(departmentId);
            return Json(result);
        }
        public IActionResult GetEvaluationScore(int evaluationId, int level)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, connProvider);
            var result = taskAssignmentService.GetEvaluationScore(evaluationId, level);
            return Json(result);
        }
        public IActionResult SensibleEventView(int evaluationId, int index)
        {
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);


            ViewBag.index = index;

            return PartialView(taskAssignmentService.GetSensibleEvent(evaluationId));
        }
        public IActionResult SubSetScorePartial()
        {
            return PartialView();
        }
        public IActionResult GetSubSetScoreList()
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
            ShareService shareService = new ShareService(applicationDbContext, null);
            if (periodDefinitionIdDT2 == null || periodDefinitionIdDT2 == "")
            {
                periodDefinitionIdDT2 = shareService.GetMaxPeriodDefinitionId().ToString();
            }
            string departmentIdDT = Request.Form["departmentIdDT"];


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
            TaskAssignmentService taskAssignmentService = new TaskAssignmentService(null, connProvider);
            var result = taskAssignmentService.GetSubSetScoreList(dataTableParameter, personId, primaryDepartmentId, employee, periodDefinitionIdDT2);
            return Json(result);
        }
        public IActionResult GetCriteria(int taskId, int evaluationId)
        {
            CriteriaService criteriaService = new CriteriaService(null, connProvider);
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            ViewBag.RoleId = roleId;
            var hrAdminRoleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            ViewBag.HRAdminRoleId = hrAdminRoleId;

            List<CriteriaDetailsView> criteria = null;
            criteria = criteriaService.GetAllCriteria(taskId, evaluationId);

            return PartialView(criteria);
        }
        public IActionResult RefutationCauseLookAt(string title, int evaluationId)
        {
            ViewBag.Title = title;
            var refutationCause = applicationDbContext.Evaluation.Where(c => c.EvaluationId == evaluationId).SingleOrDefault().RefutationCause;
            ViewBag.RefutationCause = refutationCause;

            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> TransferTaskAssignment()
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
        public async Task<IActionResult> TransferTaskAssignment([FromBody] EmployeeView employeeView)
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
                TaskAssignmentService taskAssignmentService = new TaskAssignmentService(applicationDbContext, null);
                var result = taskAssignmentService.TransferTaskAssignment(employeeView, allocatorId, roleId, priorPeriodDefinitionId,periodDefinitionId);
                return Json(result);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("notAuthorized", "true");
                return Json(dic);
            }
        }
    }
}
