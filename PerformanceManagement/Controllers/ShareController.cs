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

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin,Coacher,PlanningAdmin,Employee")]
    public class ShareController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public ShareController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        public IActionResult GetPeriodDefinition()
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var model = shareService.GetPeriodDefinition();
            return Json(model);
        }
        public IActionResult GetPeriodDefinitionFromEvaluation()
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var model = shareService.GetPeriodDefinitionFromEvaluation();
            return Json(model);
        }
        public IActionResult GetPeriodDefinitionList()
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var model = shareService.GetPeriodDefinitionList();
            return Json(model);
        }
        public IActionResult GetTaskWeightWay(int periodDefinitionId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.GetTaskWeightWay(periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetWeightLikertScale(int periodDefinitionId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.GetWeightLikertScale(periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetScoreWeightWay(int periodDefinitionId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.GetScoreWeightWay(periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetScoreLikertScale(int periodDefinitionId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.GetScoreLikertScale(periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetBehaviourWeightWay(int periodDefinitionId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.GetBehaviourWeightWay(periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetBehaviourLikertScale(int periodDefinitionId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.GetBehaviourLikertScale(periodDefinitionId);
            return Json(result);
        }
        public IActionResult GetAllDepartment()
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(4);
            return Json(result);
        }
        public IActionResult GetDepartment(int[] departmentId)
        {
            AssignTaskService taskService = new AssignTaskService(null, connProvider);
            var result = taskService.GetDepartment(departmentId, 4);
            return Json(result);
        }
        public IActionResult GetEmployee(int[] departmentId)
        {
            //,int[] chairmanshipId,int[] managementId,int[] assistantId
            ShareService shareService = new ShareService(null, connProvider);
            //, chairmanshipId, managementId, assistantId
            var result = shareService.GetEmployee(departmentId);
            return Json(result);
        }
        public IActionResult ParticipantList()
        {
            ShareService shareService = new ShareService(null, connProvider);
            var result = shareService.ParticipantList();
            return Json(result);
        }
        public IActionResult PublicTaskList()
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.PublicTaskList();

            return Json(result);
        }
        public IActionResult CriteiaCount(int periodDefinitionId, int taskId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.CriteiaCount(taskId);

            return Json(result);
        }
        public IActionResult GetCriteriaDetails(int taskId)
        {
            ShareService shareService = new ShareService(applicationDbContext, null);

            return Json(shareService.GetCriteriaList(taskId));
        }
        public IActionResult GetCurrentUserInfo()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var person = applicationDbContext.People.Where(c => c.PeopleId == personId && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault();
            return Json(personId);
        }
        public IActionResult GetCurrentUserInfo2()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var person = applicationDbContext.People.Where(c => c.PeopleId == personId && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault();
            return Json(person);
        }
        public IActionResult GetCurrentUserInfo3()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var person = applicationDbContext.People.Where(c => c.PeopleId == personId && c.EffectiveEndDate == null).FirstOrDefault();
            return Json(person);
        }
        public IActionResult BehaviouralCompetencyList()
        {
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.BehaviouralCompetencyList();

            return Json(result);
        }
        public IActionResult AllocatorLevel(int departmentId, int personId, int coacherId, int coacherDepartmentId)
        {
            ShareService shareService = new ShareService(null, connProvider);
            var result = shareService.AllocatorLevel(departmentId, personId, coacherId, coacherDepartmentId);
            return Json(result);
        }
        public IActionResult TaskList()
        {
            //applicationDbContext.People.ToList();
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var coacherId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ShareService shareService = new ShareService(applicationDbContext, null);
            var result = shareService.TaskList();

            return Json(result);
        }
        public IActionResult SupervisorList()
        {
            ShareService shareService = new ShareService(null, connProvider);
            var result = shareService.SupervisorList();
            return Json(result);
        }

        public IActionResult GetAllEmployeeDepartment()
        {
            ShareService shareService = new ShareService(null, connProvider);
            var result = shareService.GetAllEmployeeDepartment();
            return Json(result);
        }
    }
}
