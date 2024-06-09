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
using PerformanceManagement.Models.Coacher.Services;
using System.Security.Claims;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "Coacher")]
    public class ChartConfirmationController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;
        public ChartConfirmationController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        public IActionResult ChartConfirmation()
        {
            return View();
        }
        public IActionResult GetNumberOfChart()
        {
            //int personId = int.Parse(HttpContext.Session.GetString("userId"));
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ChartConfirmationServices chartConfirmationServices = new ChartConfirmationServices(applicationDbContext, connProvider);
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            dictionary = chartConfirmationServices.GetNumberOfChart(personId);

            return Json(dictionary);
        }
        public JsonResult GetRoot(int evaluationHierarchyId)
        {
            //List<Chart> items = GetTree(applicationDbContext);
            //int personId = int.Parse(HttpContext.Session.GetString("userId"));
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ChartConfirmationServices chartConfirmationServices = new ChartConfirmationServices(applicationDbContext, connProvider);
            var result = chartConfirmationServices.GetTree(evaluationHierarchyId, personId);
            return Json(result);
        }

        public IActionResult ChartDenialPartial()
        {
            return PartialView();
        }
        public JsonResult AddChartDenial(int periodDefinitionId2, string causeDescription)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ChartConfirmationServices chartConfirmationServices = new ChartConfirmationServices(applicationDbContext, null);
            int result = chartConfirmationServices.AddChartDenial(personId, periodDefinitionId2, causeDescription);
            return Json(result);
        }
        public JsonResult AddChartConfirmation(int periodDefinitionId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ChartConfirmationServices chartConfirmationServices = new ChartConfirmationServices(applicationDbContext, null);
            int result = chartConfirmationServices.AddChartConfirmation(personId, periodDefinitionId);
            return Json(result);
        }
        public bool GetConfirmation()
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            ChartConfirmationServices chartConfirmationServices = new ChartConfirmationServices(applicationDbContext, null);
            bool result = chartConfirmationServices.GetConfirmation(personId);
            return result;
        }
    }
}
