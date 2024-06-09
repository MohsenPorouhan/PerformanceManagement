using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.PlanningAdmin.View;
using System.Security.Claims;
using Task = PerformanceManagement.Models.Task;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "PlanningAdmin")]
    public class TaskController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly RoleManager<IdentityRole> roleManager;

        //private readonly IConfiguration config;
        public TaskController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, RoleManager<IdentityRole> roleManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.roleManager = roleManager;
        }
        public IActionResult TaskDefinition()
        {
            return View();
        }
        public IActionResult AddTask(int counter, int type, string title, int? relatedWithTask)
        {
            var periodDefinition = applicationDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            var role = applicationDbContext.UserRoles.Where(c => c.UserId == userId && c.RoleId == roleId).SingleOrDefault();
            if (periodDefinition != null && role != null)
            {
                applicationDbContext.People.ToList();
                var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
                Task task = new Task();
                task.Type = type;
                task.ResourceType = 1;
                task.Title = title;
                task.IsActive = true;
                task.RoleId = role.RoleId;
                task.CreatedBy = personId;
                task.CreatedDate = DateTime.Now;
                task.PeriodDefinitoionId = periodDefinition.PeriodDefinitoionId;
                task.ParentTaskId = relatedWithTask;
                if (counter != 0)
                {
                    List<Criteria> criteria = new List<Criteria>();
                    for (int i = 1; i < counter; i++)
                    {
                        if (Convert.ToString(Request.Form["Criteria" + i]) != null && Convert.ToString(Request.Form["limitOfAdmission" + i]) != null && Convert.ToString(Request.Form["criteriaType" + i]) != null && Convert.ToString(Request.Form["calculationWay" + i]) != null)
                        {
                            criteria.Add(new Criteria() { Title = Request.Form["Criteria" + i], LimitOfAdmission = Request.Form["limitOfAdmission" + i], CriteriaType = Convert.ToInt32(Request.Form["criteriaType" + i]), CalculationWay = Request.Form["calculationWay" + i], CreatedBy = personId, CreatedDate = DateTime.Now ,PeriodDefinitionId=periodDefinition.PeriodDefinitoionId});
                        }
                    }
                    task.Criterias = criteria;
                }
                TaskService taskService = new TaskService(applicationDbContext, null);
                int result = taskService.AddTask(task);
                return Json(result);
            }
            else
            {
                return Json("اطلاعاتی موجود نمی باشد");
            }

        }
        [HttpGet]
        public IActionResult EditTaskDefinition(int periodDefinitionId, string title, int type, int taskId, int? parentTaskId)
        {
            TaskDefinitionEditionView taskDefinitionEditionView = new TaskDefinitionEditionView();
            TaskService taskService = new TaskService(applicationDbContext, null);
            taskDefinitionEditionView.PeriodDefinitionId = periodDefinitionId;
            taskDefinitionEditionView.Title = title;
            taskDefinitionEditionView.Type = type;
            taskDefinitionEditionView.TaskId = taskId;
            taskDefinitionEditionView.ParentTaskId = parentTaskId;
            taskDefinitionEditionView.Criterias = taskService.GetCriteriaList(taskId);
            return PartialView(taskDefinitionEditionView);
        }
        [HttpPost]
        public IActionResult EditTaskDefinition([FromBody]TaskDefinitionEditionView taskDefinitionEditionView)
        {
            applicationDbContext.People.ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            TaskService taskService = new TaskService(applicationDbContext, null);
            int result = taskService.EditTaskDefinition(taskDefinitionEditionView, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult DeleteTaskDefinition(int periodDefinitoionId, int taskId, string title)
        {
            ViewBag.PeriodDefinitoionId = periodDefinitoionId;
            ViewBag.TaskId = taskId;
            ViewBag.Title = title;
            return PartialView();
        }
        [HttpPost]
        public IActionResult DeleteTaskDefinition(int periodDefinitoionId, int taskId)
        {
            TaskService taskService = new TaskService(applicationDbContext, connProvider);
            return Json(taskService.DeleteTaskDefinition(periodDefinitoionId, taskId));
        }
        public IActionResult GetTaskList()
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
            TaskService taskService = new TaskService(null, connProvider);
            var result = taskService.GetTaskList(dataTableParameter);
            return Json(result);
        }
        public IActionResult GetCriteriaDetails(int taskId)
        {
            CriteriaService criteriaService = new CriteriaService(applicationDbContext, null);

            return View(criteriaService.GetCriteriaList(taskId));
        }
        public IActionResult TaskList()
        {
            TaskService taskService = new TaskService(applicationDbContext, connProvider);
            return Json(taskService.TaskList());
        }
        [HttpGet]
        public IActionResult ViewRelatedTask()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult GetRelatedTask(int? parentTaskId)
        {
            TaskService taskService = new TaskService(null, connProvider);

            RelatedTaskView relatedTask = null;
            if (parentTaskId != null)
            {
                relatedTask = taskService.GetRelatedTask(parentTaskId);
            }
            ViewBag.parentTaskId = parentTaskId;
            return PartialView(relatedTask);
        }
        [HttpGet]
        public IActionResult AddCriteria(int periodDefinitoionId, int taskId, string title)
        {
            ViewBag.PriodDefinitoionId = periodDefinitoionId;
            ViewBag.TaskId = taskId;
            ViewBag.Title = title;
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddCriteria([FromBody]List<AddCriteriaView> listOfCriteria)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            TaskService taskService = new TaskService(applicationDbContext, null);
            int result = taskService.AddCriteria(listOfCriteria, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult DeleteCriteria(int CriteriaId, string Title, int PeriodDefinitionId)
        {
            ViewBag.CriteriaId = CriteriaId;
            ViewBag.Title = Title;
            ViewBag.PeriodDefinitionId = PeriodDefinitionId;
            return PartialView();
        }
        [HttpPost]
        public IActionResult DeleteCriteria(int CriteriaId, int PeriodDefinitionId)
        {
            TaskService taskService = new TaskService(applicationDbContext, connProvider);
            return Json(taskService.DeleteCriteria(CriteriaId, PeriodDefinitionId));
        }
    }
}
