using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PerformanceManagement.Models.Employee.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Models.HRAdmin.Services;
using System.Threading.Tasks;
using PerformanceManagement.ViewModels;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class ProtestAddresseeController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly IAuthorizationService authService;
        private int personIdd;
        //private readonly IConfiguration config;

        public ProtestAddresseeController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, IAuthorizationService authService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.authService = authService;
        }
        [HttpGet]
        public IActionResult ProtestAddressee()
        {
            return View();
        }
        public IActionResult GetProtestAddresseeList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var coacherId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            //string roleId = applicationDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            int? employeeDepartmentId = null;
            if (Convert.ToInt32(Request.Form["departmentIdDT"]) != 0)
            {
                employeeDepartmentId = int.Parse(Request.Form["departmentIdDT"]);
            }
            else
            {
                employeeDepartmentId = applicationDbContext.People.Where(c => c.PeopleId == coacherId && c.EffectiveEndDate == null && c.PositionType == 1).SingleOrDefault().EvaluationHierarchyID;
            }

            int? periodDefinitionId = null;
            if (Convert.ToInt32(Request.Form["periodDefinitionIdDT"]) != 0)
            {
                periodDefinitionId = int.Parse(Request.Form["periodDefinitionIdDT"]);
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

            ProtestAddresseeService protestAddresseeService = new ProtestAddresseeService(null, connProvider);
            var result = protestAddresseeService.GetProtestAddresseeList(dataTableParameter, employeeDepartmentId, coacherId, periodDefinitionId);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddAddresseeResponsePartial([FromBody]ProtestResponse protestResponse)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ShareService shareService = new ShareService(applicationDbContext, null);
            AccessControlDecisionViewModel accessControlDecisionViewModel = new AccessControlDecisionViewModel();
            accessControlDecisionViewModel.AppDbContext = applicationDbContext;
            accessControlDecisionViewModel.DepartmentId = protestResponse.PersonDepartmentId;
            accessControlDecisionViewModel.PeopleId = personId;
            accessControlDecisionViewModel.PeriodDefinitionId = shareService.GetPeriodDefinitionId();
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, accessControlDecisionViewModel, "ProtestationOfPeriod").ConfigureAwait(false);
            if (authorized.Succeeded)
            {
                return PartialView(protestResponse);
            }
            else
            {
                return PartialView("AccessDenied");
            }

        }
        [HttpPost]
        public IActionResult AddAddresseeResponse(int protestId, int CoacherLevel, int PersonDepartmentId, string protestResponse)
        {
            string roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            ProtestAddresseeService protestAddresseeService = new ProtestAddresseeService(applicationDbContext, null);
            int result = protestAddresseeService.AddAddresseeResponse(protestId, CoacherLevel, PersonDepartmentId, protestResponse, personId, roleId);
            return Json(result);
        }
        public IActionResult GetProtestationResponseList(int protestId)
        {
            ProtestAddresseeService employeeSensibleEventService = new ProtestAddresseeService(null, connProvider);
            return View(employeeSensibleEventService.GetProtestationResponseList(protestId));
        }
        public IActionResult GetProtestationResponseList2(int protestId2)
        {
            ProtestAddresseeService employeeSensibleEventService = new ProtestAddresseeService(null, connProvider);
            return View(employeeSensibleEventService.GetProtestationResponseList(protestId2));
        }
        public IActionResult SubSetProtestationPartial()
        {
            return PartialView();
        }
    }
}
