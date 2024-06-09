using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.Services;
using PerformanceManagement.ViewModels;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.HRAdmin.Services;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class TaskDivisionController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;

        //private readonly IConfiguration config;

        public TaskDivisionController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult TaskDivision()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddTaskDivision(int periodDefinitionId,int departmentId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = departmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);
            AuthorizationResult authorized2 = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "IntermediateOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded || authorized2.Succeeded)
            {
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult TaskDivision([FromBody]List<SubTaskDivisionView> subTaskDivisionViews)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var result = taskDivisionService.AddSubTask(subTaskDivisionViews, personId, roleId);
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

            string departmentId2 = Request.Form["departmentId2"];
            string periodDefinitionIdDT2 = Request.Form["periodDefinitionIdDT2"];

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
            TaskDivisionService taskDivisionService = new TaskDivisionService(null, connProvider);
            var result = taskDivisionService.GetAssignedTaskList(dataTableParameter, departmentId2, periodDefinitionIdDT2, personId);
            return Json(result);
        }

        public IActionResult GetDepartmentList(int periodDefinitionId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);
            var result = taskDivisionService.GetDepartmentList(periodDefinitionId, roleId, roleId2, personId);
            return Json(result);
        }
        public IActionResult GetSubTaskList(int taskId, int hierarchyId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);

            return View(taskDivisionService.GetSubTaskList(taskId, personId, roleId, hierarchyId));
        }
        [HttpGet]
        public async Task<IActionResult> UpdatetSubTaskAndCriterias(int taskId, int periodDefinitionId, int departmentId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = departmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);
            AuthorizationResult authorized2 = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "IntermediateOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded || authorized2.Succeeded)
            {
                TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);
                var result = taskDivisionService.GetSubTaskAndCriterias(taskId);
                if (result.Count > 0)
                {
                    return PartialView(result);

                }
                else
                {
                    return Json(0);
                }
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult UpdatetSubTaskAndCriteria([FromBody]TaskView taskView)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);
            var result = taskDivisionService.UpdatetSubTaskAndCriterias(taskView, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult GetCriterias(int taskId, int periodDefinitionId, int departmentId)
        {
            ViewBag.periodDefinitionId = periodDefinitionId;
            ViewBag.departmentId = departmentId;
            ShareService shareService = new ShareService(applicationDbContext, null);
            return PartialView(shareService.GetCriteriaList(taskId));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCriteria(int criteriaId, int periodDefinitionId, int departmentId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = departmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);
            AuthorizationResult authorized2 = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "IntermediateOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded || authorized2.Succeeded)
            {
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult DeleteCriteria(int criteriaId)
        {
            TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);
            var result = taskDivisionService.DeleteCriteria(criteriaId);

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSubTask(int taskId, int periodDefinitionId, int departmentId,string taskTitle)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = departmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = periodDefinitionId;
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "BeginningOfPeriod").ConfigureAwait(false);
            AuthorizationResult authorized2 = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "IntermediateOfPeriod").ConfigureAwait(false);

            if (authorized.Succeeded || authorized2.Succeeded)
            {
                ViewBag.taskId = taskId;
                ViewBag.taskTitle = taskTitle;
                return PartialView();
            }
            else
            {
                return PartialView("AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult DeleteSubTask(int taskId)
        {
            TaskDivisionService taskDivisionService = new TaskDivisionService(applicationDbContext, null);
            var result = taskDivisionService.DeleteSubTask(taskId);

            return Json(result);
        }
    }
}
