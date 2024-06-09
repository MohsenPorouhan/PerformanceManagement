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

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ParticipantTaskAssignmentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;

        //private readonly IConfiguration config;

        public ParticipantTaskAssignmentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult ParticipantTaskAssignment()
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

            ParticipantTaskAssignmentService participantTaskAssignmentService = new ParticipantTaskAssignmentService(null, connProvider);
            var result = participantTaskAssignmentService.GetAssignedTaskList(dataTableParameter, personId, departmentIdDT, periodDefinitionIdDT);
            return Json(result);
        }
        [HttpGet]
        public IActionResult ParticipantPerformTaskConfirmation()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult ParticipantPerformTaskConfirmation([FromBody]PerformConfirmationView evaluation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            ParticipantTaskAssignmentService employeeTaskAssignmentService = new ParticipantTaskAssignmentService(applicationDbContext, null);
            var result = employeeTaskAssignmentService.ParticipantPerformTaskConfirmation(evaluation, personId, roleId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> ScoreAssignment()
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
            accessControlDecisionViewModel.ScoreScheduleTypeId = 2;

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
            ParticipantTaskAssignmentService participantTaskAssignmentService = new ParticipantTaskAssignmentService(applicationDbContext, null);
            var result = participantTaskAssignmentService.ScoreAssignment(listOfTasks, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult ViewScore()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult GetScore(int evaluationId, int hasCriteria, int type)
        {
            ParticipantTaskAssignmentService participantTaskAssignmentService = new ParticipantTaskAssignmentService(null, connProvider);
            List<ScoreView> scoreView = null;
            if (hasCriteria > 0)
            {
                scoreView = participantTaskAssignmentService.GetCriteriaScore(evaluationId);
            }
            else if (hasCriteria == 0)
            {
                scoreView = participantTaskAssignmentService.GetTaskScore(evaluationId);

            }
            ViewBag.hasCriteria = hasCriteria;
            ViewBag.type = type;
            return PartialView(scoreView);
        }
    }
}
