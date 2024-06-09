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

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class CompetencyTaskPercentController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public CompetencyTaskPercentController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        public IActionResult CompetencyTaskPercent()
        {
            return View();
        }
        public IActionResult AddCompetencyTaskPercent(float TaskPercent, float CompetencyPercent, int periodDefinitionId)
        {
            CompetencyTaskPercentService competencyTaskPercentService = new CompetencyTaskPercentService(applicationDbContext, null);
            int finalResult = competencyTaskPercentService.AddCompetencyTaskPercent(TaskPercent, CompetencyPercent, periodDefinitionId);
            return Json(finalResult);
        }
        public IActionResult GetCompetencyTaskPercentList()
        {
            int start = int.Parse(Request.Query["start"]);
            int length = int.Parse(Request.Query["length"]);
            int draw = int.Parse(Request.Query["draw"]);
            string search = Request.Query["search[value]"];
            int orderColumn = int.Parse(Request.Query["order[0][column]"]);
            string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            bool orderable = bool.Parse(Request.Query[concatenateOrder]);
            string orderDIR = Request.Query["order[0][dir]"];

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
            CompetencyTaskPercentService competencyTaskPercentService = new CompetencyTaskPercentService(null, connProvider);
            var finalResult = competencyTaskPercentService.CompetencyTaskPercentList(dataTableParameter);
            return Json(finalResult);
        }
    }
}
