using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using PerformanceManagement.Util;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class MultipleCoacherWeightController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public MultipleCoacherWeightController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }

        public IActionResult MultipleCoacherWeight()
        {
            return View();
        }
        public IActionResult GetMultipleCoacherWeightList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            //int? periodDefinitionId = null; int? employeeId = null;

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
            MultipleCoacherWeightService multipleCoacherWeightService = new MultipleCoacherWeightService(null, connProvider);
            var result = multipleCoacherWeightService.GetMultipleCoacherWeightList(dataTableParameter, null, null);
            return Json(result);
        }
        [HttpGet]
        public IActionResult WeightUpTaskOfMultipleCoacher()
        {
            return PartialView();
        }
        public IActionResult GetTaskCoacherList(int periodDefinitionId, int employeeId)
        {
            MultipleCoacherWeightService multipleCoacherWeightService = new MultipleCoacherWeightService(null, connProvider);

            return Json(multipleCoacherWeightService.GetTaskCoacherList(periodDefinitionId, employeeId));
        }
        [HttpPost]
        public IActionResult WeightUpTaskOfMultipleCoacher([FromBody]List<MultipleTaskCoacherView> listOfMultipleTaskCoacher)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            MultipleCoacherWeightService multipleCoacherWeightService = new MultipleCoacherWeightService(applicationDbContext, null);
            var result = multipleCoacherWeightService.WeightUpTaskOfMultipleCoacher(listOfMultipleTaskCoacher, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult WeightUpCompetencyOfMultipleCoacher()
        {
            return PartialView();
        }
        public IActionResult GetCompetencyCoacherList(int periodDefinitionId, int employeeId)
        {
            MultipleCoacherWeightService multipleCoacherWeightService = new MultipleCoacherWeightService(null, connProvider);

            return Json(multipleCoacherWeightService.GetCompetencyCoacherList(periodDefinitionId, employeeId));
        }
        [HttpPost]
        public IActionResult WeightUpCompetencyOfMultipleCoacher([FromBody]List<MultipleCompetencyCoacherView> listOfMultipleCompetencyCoacher)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            MultipleCoacherWeightService multipleCoacherWeightService = new MultipleCoacherWeightService(applicationDbContext, null);
            var result = multipleCoacherWeightService.WeightUpCompetencyOfMultipleCoacher(listOfMultipleCompetencyCoacher, personId);
            return Json(result);
        }

    }
}
