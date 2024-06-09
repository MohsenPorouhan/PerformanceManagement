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
    [Authorize(Roles = "HRAdmin")]
    public class BehaviouralCompetencyController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public BehaviouralCompetencyController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }
        [HttpGet]
        public IActionResult BehaviouralCompetencyDefinition()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BehaviouralCompetencyDefinition(string title, string tagEditorCompetency, int[] job)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            string[] tagCompetency = tagEditorCompetency.Split(",");
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.AddBehaviouralCompetencyDefinition(title, tagCompetency, job, personId, roleId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult EditBehaviouralCompetency(string title, string tagEditorCompetency, int[] job,int behaviouralCompetencyId)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            string[] tagCompetency = tagEditorCompetency.Split(",");
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.EditBehaviouralCompetencyDefinition(title, tagCompetency, job, personId, roleId, behaviouralCompetencyId);
            return Json(result);
        }
        public IActionResult GetJobList()
        {
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.GetJobList();
            return Json(result);
        }
        public IActionResult GetBehaviouralList()
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
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, connProvider);
            var result = behaviouralCompetencyService.GetBehaviouralList(dataTableParameter);
            return Json(result);
        }

        public IActionResult GetTruthDetails(int behaviouralCompetencyId)
        {
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.GetTruthDetails(behaviouralCompetencyId);

            return View(result);
        }
        public IActionResult GetTruthDetails2(int behaviouralCompetencyId)
        {
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.GetTruthDetails(behaviouralCompetencyId);

            return Json(result);
        }
        public IActionResult BehaviouralCompetencyList()
        {
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.BehaviouralCompetencyList();

            return Json(result);
        }
        [HttpGet]
        public IActionResult ActivationCompetencyDefinition(int behaviouralCompetencyId, string title, bool isActive)
        {
            ViewBag.BehaviouralCompetencyId = behaviouralCompetencyId;
            ViewBag.Title = title;
            ViewBag.IsActive = isActive;
            return PartialView();
        }
        [HttpPost]
        public IActionResult ActivationCompetencyDefinition(int behaviouralCompetencyId, bool isActivation)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);
            var result = behaviouralCompetencyService.ActivationPublicTaskDefinition(behaviouralCompetencyId, isActivation, personId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult BehaviouralCompetencyDeletion(int behaviouralCompetencyId, string title)
        {
            ViewBag.BehaviouralCompetencyId = behaviouralCompetencyId;
            ViewBag.Title = title;

            return PartialView();
        }
        [HttpPost]
        public IActionResult BehaviouralCompetencyDeletion(int behaviouralCompetencyId)
        {
            BehaviouralCompetencyService behaviouralCompetencyService = new BehaviouralCompetencyService(applicationDbContext, null);

            var result = behaviouralCompetencyService.BehaviouralCompetencyDeletion(behaviouralCompetencyId);

            return Json(result);
        }
    }
}
