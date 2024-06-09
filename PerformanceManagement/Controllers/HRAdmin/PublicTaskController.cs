using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin;
using Microsoft.AspNetCore.Http;
using System.Collections;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using PerformanceManagement.Models.HRAdmin.View;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class PublicTaskController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public PublicTaskController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        public IActionResult GetTaskList()
        {
            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);
            var result = publicTaskService.GetTaskList();
            return Json(result);
        }
        [HttpGet]
        public IActionResult PublicTaskDefinition()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PublicTaskDefinition(string title, string description, int[] correspondentTask)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);
            var result = publicTaskService.PublicTaskDefinition(title, description, correspondentTask, userId);
            return Json(result);
        }
        public IActionResult GetPublicTaskList()
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
            PublicTaskService publicTaskService = new PublicTaskService(null, connProvider);
            var result = publicTaskService.GetPuplicTaskList(dataTableParameter);
            return Json(result);
        }
        [HttpGet]
        public IActionResult PublicTaskAssign()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PublicTaskAssignWithoutHierarchy()
        {
            return PartialView();
        }
        [HttpGet]
        public IActionResult PublicTaskAssignWithHierarchy()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult PublicTaskAssign(int periodDefinitionId, string[] employee, int[] publicTask, int taskWeight, string participant)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);
            var result = publicTaskService.PublicAssignTask(periodDefinitionId, employee, publicTask, taskWeight, participant, personId, roleId);
            return Json(result);
        }
        public IActionResult GetAssignmentPublicTaskList()
        {
            string departmentIdDT2 = Request.Form["departmentIdDT"];
            string periodDefinitionIdDT2 = Request.Form["periodDefinitionIdDT"];
            string peopleIdDT2 = Request.Form["peopleIdDT"];

            if (departmentIdDT2=="")
            {
                departmentIdDT2 = null;
            }
            if (periodDefinitionIdDT2 == "")
            {
                periodDefinitionIdDT2 = null;
            }
            if (peopleIdDT2 == "")
            {
                peopleIdDT2 = null;
            }

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
            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, connProvider);
            var result = publicTaskService.GetAssignmentPublicTaskList(dataTableParameter, periodDefinitionIdDT2, departmentIdDT2, peopleIdDT2);
            return Json(result);
        }
        [HttpPost]
        public IActionResult EditPublicTask(int taskId, string title, string description, int[] correspondentTask)
        {
            applicationDbContext.People.ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);
            var result = publicTaskService.EditPublicTask(taskId, title, description, correspondentTask, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult EditPublicTaskAssignment(int evaluationId, int taskWeight, string title, int? evaluationParticipantId
            , int? participantEvaluationHierarchyId, int? participantId, int periodDefinitoionId)
        {
            ShareService shareService = new ShareService(null, connProvider);
            EditPublicTaskAssignmentView editPublicTaskAssignmentView = new EditPublicTaskAssignmentView();
            editPublicTaskAssignmentView.EvaluationId = evaluationId;
            editPublicTaskAssignmentView.TaskWeight = taskWeight;
            editPublicTaskAssignmentView.Title = title;
            editPublicTaskAssignmentView.PeriodDefinitoionId = periodDefinitoionId;
            editPublicTaskAssignmentView.EvaluationParticipants = shareService.ParticipantList2();
            if (evaluationParticipantId != null)
            {
                editPublicTaskAssignmentView.EvaluationParticipantId = evaluationParticipantId;
                editPublicTaskAssignmentView.ParticipantEvaluationHierarchyId = participantEvaluationHierarchyId;
                editPublicTaskAssignmentView.ParticipantId = participantId;
            }

            return PartialView(editPublicTaskAssignmentView);
        }
        [HttpPost]
        public IActionResult EditPublicTaskAssignment(int? evaluationParticipantId, int taskWeight, string participantId, int evaluationId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            int? departmentId = null;
            int? participentIdd = null;
            if (participantId != null)
            {
                departmentId = int.Parse(participantId.Split('-')[0]);
                participentIdd = int.Parse(participantId.Split('-')[1]);
            }

            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);
            var result = publicTaskService.EditPublicTaskAssignment(evaluationParticipantId, taskWeight, departmentId, participentIdd, evaluationId, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult ActivationPublicTaskDefinition(int taskId, string title, bool isActive)
        {
            ViewBag.TaskId = taskId;
            ViewBag.Title = title;
            ViewBag.IsActive = isActive;
            return PartialView();
        }
        [HttpPost]
        public IActionResult ActivationPublicTaskDefinition(int taskId, bool isActivation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);
            var result = publicTaskService.ActivationPublicTaskDefinition(taskId, isActivation, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult PublicTaskAssignmentDeletion(int evaluationId, string title)
        {
            EvaluationTaskDeletionView evaluationTaskDeletionView = new EvaluationTaskDeletionView();
            evaluationTaskDeletionView.EvaluationId = evaluationId;
            evaluationTaskDeletionView.Title = title;

            return PartialView(evaluationTaskDeletionView);
        }
        [HttpPost]
        public IActionResult PublicTaskAssignmentDeletion(int evaluationId)
        {
            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);

            var result = publicTaskService.PublicTaskAssignmentDeletion(evaluationId);

            return Json(result);
        }
        [HttpGet]
        public IActionResult PublicTaskDefinitionDeletion(int taskId, string title)
        {
            ViewBag.TaksId = taskId;
            ViewBag.Title = title;

            return PartialView();
        }
        [HttpPost]
        public IActionResult PublicTaskDefinitionDeletion(int taskId)
        {
            PublicTaskService publicTaskService = new PublicTaskService(applicationDbContext, null);


            var result = publicTaskService.PublicTaskDefinitionDeletion(taskId);

            return Json(result);
        }
        public IActionResult GetPublicTaskDepartment(int periodDefinitionId)
        {
            PublicTaskService publicTaskService = new PublicTaskService(null, connProvider);


            var result = publicTaskService.GetPublicTaskDepartment(periodDefinitionId);

            return Json(result);
        }
        public IActionResult GetPublicTaskReceiver(int departmentId)
        {
            PublicTaskService publicTaskService = new PublicTaskService(null, connProvider);


            var result = publicTaskService.GetPublicTaskReceiver(departmentId);

            return Json(result);
        }
    }
}
