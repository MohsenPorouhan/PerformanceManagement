using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models;
using System.Linq;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.HRAdmin.Services;
using System;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class FinalizeController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public FinalizeController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult Finalize()
        {
            return View();
        }
        [HttpGet]
        public IActionResult FinalizeCalculationForm()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult FinalizeCalculation(int[] supervisorId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creatorId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            HRAdminCalculationService hrAdminCalculationService = new HRAdminCalculationService(applicationDbContext, null);
            var result = hrAdminCalculationService.FinalizeCalc(supervisorId, creatorId, roleId);
            return Json(result);
        }
        public IActionResult GetFinalizationCalcList()
        {
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int draw = int.Parse(Request.Form["draw"]);
            string search = Request.Form["search[value]"];
            int orderColumn = int.Parse(Request.Form["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Form[concatenateOrder]);
            string orderDIR = Request.Form["order[0][dir]"];

            int? periodDefinitionId = null;
            if (Convert.ToInt32(Request.Form["periodDefinitionIdDT2"]) != 0)
            {
                periodDefinitionId = int.Parse(Request.Form["periodDefinitionIdDT2"]);
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

            HRAdminCalculationService hrAdminCalculationService = new HRAdminCalculationService(null, connProvider);
            var result = hrAdminCalculationService.GetFinalizationCalcList(dataTableParameter, periodDefinitionId);
            return Json(result);
        }
    }
}
