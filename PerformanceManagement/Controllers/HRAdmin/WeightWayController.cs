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
    public class WeightWayController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public WeightWayController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }

        public IActionResult WeightWay()
        {
            return View();
        }
        public IActionResult LikertDefinitionPartial()
        {
            return View();
        }
        public IActionResult DetermineWeightPartial()
        {
            return View();
        }
        public IActionResult AddLikert(string tagEditorCompetency, string competencyName, string tagEditorTask, string taskName, string tagEditorScore, string scoreName)
        {
            string[] tagCompetency = tagEditorCompetency.Split(",");
            string[] tagTask = tagEditorTask.Split(",");
            string[] tagScore = tagEditorScore.Split(",");

            WeightWayService weightWayService = new WeightWayService(applicationDbContext, null);
            int finalResult = weightWayService.AddLikertScale(tagCompetency, competencyName, tagTask, taskName, tagScore, scoreName);
            return Json(finalResult);
        }
        public IActionResult GetCompetencyLikert()
        {
            WeightWayService weightWayService = new WeightWayService(applicationDbContext, null);
            var model = weightWayService.GetCompetencyLikert();
            return Json(model);
        }
        public IActionResult GetTaskLikert()
        {
            WeightWayService weightWayService = new WeightWayService(applicationDbContext, null);
            var model = weightWayService.GetTaskLikert();
            return Json(model);
        }
        public IActionResult GetScoreLikert()
        {
            WeightWayService weightWayService = new WeightWayService(applicationDbContext, null);
            var model = weightWayService.GetScoreLikert();
            return Json(model);
        }
        public IActionResult GetPeriodDefinition()
        {
            WeightWayService weightWayService = new WeightWayService(applicationDbContext, null);
            var model = weightWayService.GetPeriodDefinition();
            return Json(model);
        }
        public IActionResult AssignWeightWayToPeriod(int competencyWeight, int taskWeight, int scoreWeight, int periodDefinitionId)
        {
            WeightWayService weightWayService = new WeightWayService(applicationDbContext, null);
            int finalResult = weightWayService.AssignWeightWayToPeriod(competencyWeight, taskWeight, scoreWeight, periodDefinitionId);
            return Json(finalResult);
        }
        public IActionResult GetWeightWayList()
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
            WeightWayService weightWayService = new WeightWayService(null, connProvider);
            var result = weightWayService.WeightWayList(dataTableParameter);
            return Json(result);
        }
    }
}
