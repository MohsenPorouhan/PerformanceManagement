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
    public class BehaviouralCompetencyAssignController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public BehaviouralCompetencyAssignController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult BehaviouralCompetencyAssign()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BehaviouralCompetencyAssignWithHierarchy()
        {
            return PartialView();
        }
        [HttpGet]
        public IActionResult BehaviouralCompetencyAssignWithoutHierarchy()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult BehaviouralCompetencyAssignment(int periodDefinitionId, string[] employee, int[] behaviourCompetency, int competencyWeight, string participant)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            BehaviouralCompetencyAssignService behaviouralCompetencyAssignService = new BehaviouralCompetencyAssignService(applicationDbContext, null);
            var result = behaviouralCompetencyAssignService.AssignBehaviouralCompetency(periodDefinitionId, employee, behaviourCompetency, competencyWeight, participant, personId, roleId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult EditBehaviouralCompetencyAssignment(int evaluationBehaviouralCompetencyId, int behaviouralCompetencyWeight, string title, int? evaluationBehaviouralParticipantId
            , int? participantEvaluationBehaviouralHierarchyId, int? participantId, int periodDefinitionId)
        {
            ShareService shareService = new ShareService(null, connProvider);
            EditCompetencyAssignmentView editCompetencyAssignmentView = new EditCompetencyAssignmentView();
            editCompetencyAssignmentView.EvaluationBehaviouralCompetencyId = evaluationBehaviouralCompetencyId;
            editCompetencyAssignmentView.BehaviouralCompetencyWeight = behaviouralCompetencyWeight;
            editCompetencyAssignmentView.Title = title;
            editCompetencyAssignmentView.PeriodDefinitionId = periodDefinitionId;
            editCompetencyAssignmentView.EvaluationCompetencyParticipants = shareService.ParticipantList2();
            if (evaluationBehaviouralParticipantId != null)
            {
                editCompetencyAssignmentView.HasParticipant = true;
                editCompetencyAssignmentView.EvaluationBehaviouralParticipantId = evaluationBehaviouralParticipantId;
                editCompetencyAssignmentView.ParticipantEvaluationBehaviouralHierarchyId = participantEvaluationBehaviouralHierarchyId;
                editCompetencyAssignmentView.ParticipantId = participantId;
            }

            return PartialView(editCompetencyAssignmentView);
        }
        [HttpPost]
        public IActionResult EditBehaviouralCompetencyAssignment(int? evaluationBehaviouralParticipantId, int behaviouralCompetencyWeight, string participantId, int evaluationBehaviouralCompetencyId)
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

            BehaviouralCompetencyAssignService behaviouralCompetencyAssignService = new BehaviouralCompetencyAssignService(applicationDbContext, null);
            var result = behaviouralCompetencyAssignService.EditBehaviouralCompetencyAssignment(evaluationBehaviouralParticipantId, behaviouralCompetencyWeight, departmentId, participentIdd, evaluationBehaviouralCompetencyId, personId);
            return Json(result);
        }
        public IActionResult GetAssignmentBehaviouralList()
        {
            string departmentIdDT2 = Request.Form["departmentIdDT"];
            string periodDefinitionIdDT2 = Request.Form["periodDefinitionIdDT"];
            string peopleIdDT2 = Request.Form["peopleIdDT"];

            if (departmentIdDT2 == "")
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
            BehaviouralCompetencyAssignService behaviouralCompetencyAssignService = new BehaviouralCompetencyAssignService(applicationDbContext, connProvider);
            var result = behaviouralCompetencyAssignService.GetAssignmentBehaviouralList(dataTableParameter, periodDefinitionIdDT2, departmentIdDT2, peopleIdDT2);
            return Json(result);
        }
        [HttpGet]
        public IActionResult BehaviouralCompetencyAssignmentDeletion(int evaluationBehaviouralCompetencyId, string title)
        {
            EvaluationBehaviouralDeletionView evaluationBehaviouralDeletionView = new EvaluationBehaviouralDeletionView();
            evaluationBehaviouralDeletionView.EvaluationBehaviouralCompetencyId = evaluationBehaviouralCompetencyId;
            evaluationBehaviouralDeletionView.Title = title;

            return PartialView(evaluationBehaviouralDeletionView);
        }
        [HttpPost]
        public IActionResult BehaviouralCompetencyAssignmentDeletion(int evaluationBehaviouralCompetencyId)
        {
            BehaviouralCompetencyAssignService behaviouralCompetencyAssignService = new BehaviouralCompetencyAssignService(applicationDbContext, null);

            var result = behaviouralCompetencyAssignService.BehaviouralCompetencyAssignmentDeletion(evaluationBehaviouralCompetencyId);

            return Json(result);
        }
        public IActionResult GetPublicCompetencyDepartment(int periodDefinitionId)
        {
            BehaviouralCompetencyAssignService behaviouralCompetencyAssignService = new BehaviouralCompetencyAssignService(null, connProvider);

            var result = behaviouralCompetencyAssignService.GetPublicCompetencyDepartment(periodDefinitionId);

            return Json(result);
        }
        public IActionResult GetPublicCompetencyReceiver(int departmentId)
        {
            BehaviouralCompetencyAssignService behaviouralCompetencyAssignService = new BehaviouralCompetencyAssignService(null, connProvider);

            var result = behaviouralCompetencyAssignService.GetPublicCompetencyReceiver(departmentId);

            return Json(result);
        }
    }
}
