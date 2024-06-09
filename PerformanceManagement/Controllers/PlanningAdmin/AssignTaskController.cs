using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Collections.Generic;
using System.Linq;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using System.Security.Claims;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.PlanningAdmin.View;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "PlanningAdmin")]
    public class AssignTaskController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly RoleManager<IdentityRole> roleManager;

        //private readonly IConfiguration config;
        public AssignTaskController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, RoleManager<IdentityRole> roleManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult AssignTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AssignTask(int[] assistant, int[] management, int[] chairmanship, int[] taskList, int periodDefinitionId,int ceoAssignment)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            AssignTaskService assignTaskService = new AssignTaskService(applicationDbContext, null);
            var result = assignTaskService.AssignTask(assistant, management, chairmanship, taskList, personId, periodDefinitionId, ceoAssignment);
            return Json(result);
        }
        [HttpGet]
        public IActionResult AssignWeight()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult AssignWeight([FromBody]List<WeightAssignmentView2> listOfTasks)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            AssignWeightService assignWeightService = new AssignWeightService(applicationDbContext, null);
            var result = assignWeightService.AssignWeight(listOfTasks, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult AssignScore()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AssignScore([FromBody]List<Evaluation> listOfScores)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            AssignWeightService assignWeightService = new AssignWeightService(applicationDbContext, null);
            var result = assignWeightService.AssignScore(listOfScores, personId);
            return Json(result);
        }
        public IActionResult GetAssistantDepartment()
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(1);
            return Json(result);
        }
        public IActionResult GetAllManagementDepartment()
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(2);
            return Json(result);
        }
        public IActionResult GetManagementDepartment(int[] departmentId)
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(departmentId, 2);
            return Json(result);
        }
        public IActionResult GetAllChairmanshipDepartment()
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(3);
            return Json(result);
        }
        public IActionResult GetChairmanshipDepartment(int[] departmentId)
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(departmentId, 3);
            return Json(result);
        }
        public IActionResult GetTaskList()
        {
            AssignTaskService taskService = new AssignTaskService(applicationDbContext, null);
            var result = taskService.GetTaskList();
            return Json(result);
        }
        public IActionResult GetTaskAssignmentList()
        {
            //string search2 = Request.Form["search[value]"].FirstOrDefault(); 
            //string search23 = Request.Form["search[value]"];
            string departmentId2 = Request.Form["departmentId2"];
            string periodDefinitionIdDT2 = Request.Form["periodDefinitionIdDT2"];

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
            AssignTaskService assignTaskService = new AssignTaskService(null, connProvider);
            var result = assignTaskService.GetTaskAssignmentList(dataTableParameter, departmentId2, periodDefinitionIdDT2);
            return Json(result);
        }
        public IActionResult GetDepartmentList(int periodDefinitionId)
        {
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            AssignTaskService assignTaskService = new AssignTaskService(applicationDbContext, null);
            var result = assignTaskService.GetDepartmentList(periodDefinitionId, roleId);
            return Json(result);
        }
        public IActionResult GetCriteriaDetails(int taskId)
        {
            CriteriaService criteriaService = new CriteriaService(applicationDbContext, null);

            return Json(criteriaService.GetCriteriaList(taskId));
        }
        [HttpGet]
        public IActionResult ViewScore()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult GetScore(int evaluationId, int hasCriteria, int type)
        {
            AssignTaskService assignTaskService = new AssignTaskService(null, connProvider);
            List<ScoreView> scoreView = null;
            if (hasCriteria > 0)
            {
                scoreView = assignTaskService.GetCriteriaScore(evaluationId);
            }
            else if (hasCriteria == 0)
            {
                scoreView = assignTaskService.GetTaskScore(evaluationId);

            }
            ViewBag.hasCriteria = hasCriteria;
            ViewBag.type = type;
            return PartialView(scoreView);
        }

        [HttpGet]
        public IActionResult DeleteTaskAssign(int periodDefinitoionId, int evaluationId, string title)
        {
            ViewBag.PeriodDefinitoionId = periodDefinitoionId;
            ViewBag.EvaluationId = evaluationId;
            ViewBag.Title = title;
            return PartialView();
        }
        [HttpPost]
        public IActionResult DeleteTaskAssign(int periodDefinitoionId, int evaluationId)
        {
            AssignTaskService assignTaskService = new AssignTaskService(applicationDbContext, connProvider);
            return Json(assignTaskService.DeleteTaskAssign(periodDefinitoionId, evaluationId));
        }

        public IActionResult GetCriteriaWeightScore(int criteriaWeightId)
        {
            AssignTaskService assignTaskService = new AssignTaskService(applicationDbContext, connProvider);

            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            return Json(assignTaskService.GetCriteriaWeightScore(criteriaWeightId, roleId));
        }
        public IActionResult GetEvaluationScore(int evaluationId)
        {
            AssignTaskService assignTaskService = new AssignTaskService(applicationDbContext, connProvider);

            var roleId = applicationDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            var result = assignTaskService.GetEvaluationScore(evaluationId, roleId);
            return Json(result);
        }
    }
}
