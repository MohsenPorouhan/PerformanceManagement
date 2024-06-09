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
using System.IO;
using System.Text;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class EvaluationCoefficientController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public EvaluationCoefficientController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
        }

        public IActionResult EvaluationCoefficient()
        {
            return View();
        }
        public IActionResult AddEvaluationCoefficient(int periodDefinitionId, int level1CoacherTWith, int level2CoacherTWith, int selfEvaluationTWith, int participantCoefficientT, int level1CoacherBWith, int level2CoacherBWith, int selfEvaluationBWith, int participantCoefficientB, int level1CoacherTWithout, int level2CoacherTWithout, int selfEvaluationTWithout, int level1CoacherBWithout, int level2CoacherBWithout, int selfEvaluationBWithout)
        {
            int taskWithSum = level1CoacherTWith + level2CoacherTWith + selfEvaluationTWith + participantCoefficientT;
            int taskWithoutSum = level1CoacherTWithout + level2CoacherTWithout + selfEvaluationTWithout;
            int competencyWithSum = level1CoacherBWith + level2CoacherBWith + selfEvaluationBWith + participantCoefficientB;
            int competencyWithoutSum = level1CoacherBWithout + level2CoacherBWithout + selfEvaluationBWithout;
            if (/*taskWithSum == 100 &&*/ taskWithoutSum == 100 /*&& competencyWithSum == 100*/ && competencyWithoutSum == 100)
            {
                Models.HRAdmin.EvaluationCoefficient evaluationCoefficient = new EvaluationCoefficient();
                evaluationCoefficient.level1CoacherTWith = level1CoacherTWith;
                evaluationCoefficient.level2CoacherTWith = level2CoacherTWith;
                evaluationCoefficient.selfEvaluationTWith = selfEvaluationTWith;
                evaluationCoefficient.participantCoefficientT = participantCoefficientT;
                evaluationCoefficient.level1CoacherBWith = level1CoacherBWith;
                evaluationCoefficient.level2CoacherBWith = level2CoacherBWith;
                evaluationCoefficient.selfEvaluationBWith = selfEvaluationBWith;
                evaluationCoefficient.participantCoefficientB = participantCoefficientB;
                evaluationCoefficient.level1CoacherTWithout = level1CoacherTWithout;
                evaluationCoefficient.level2CoacherTWithout = level2CoacherTWithout;
                evaluationCoefficient.selfEvaluationTWithout = selfEvaluationTWithout;
                evaluationCoefficient.level1CoacherBWithout = level1CoacherBWithout;
                evaluationCoefficient.level2CoacherBWithout = level2CoacherBWithout;
                evaluationCoefficient.selfEvaluationBWithout = selfEvaluationBWithout;
                EvaluationCoefficientService evaluationCoefficientService = new EvaluationCoefficientService(applicationDbContext, connProvider);
                int finalResult = evaluationCoefficientService.AddEvaluationCoefficient(evaluationCoefficient, periodDefinitionId);
                return Json(finalResult);
            }
            return Json("invalidData");
        }

        public IActionResult GetEvaluationCoefficientList()
        {
            Encoding utf8 =new UTF8Encoding(true);
            var reader = new StreamReader(Request.Body,utf8);
            var body = reader.ReadToEnd().Split("&");
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (var item in body)
            {
                dictionary.Add(item.Split("=")[0], item.Split("=")[1]);
            }
            int start = int.Parse(dictionary["start"]);
            int length = int.Parse(dictionary["length"]);
            int draw = int.Parse(dictionary["draw"]);
            string search = dictionary["search%5Bvalue%5D"];
            int orderColumn = int.Parse(dictionary["order%5B0%5D%5Bcolumn%5D"]);
            string concatenateOrder = "columns%5B" + orderColumn + "%5D%5Borderable%5D";
            bool orderable = bool.Parse(dictionary[concatenateOrder]);
            string orderDIR = dictionary["order%5B0%5D%5Bdir%5D"];

            //int start = int.Parse(Request.Query["start"]);
            //int length = int.Parse(Request.Query["length"]);
            //int draw = int.Parse(Request.Query["draw"]);
            //string search = Request.Query["search[value]"];
            //int orderColumn = int.Parse(Request.Query["order[0][column]"]);
            //string concatenateOrder = "columns[" + orderColumn + "][orderable]";
            //bool orderable = bool.Parse(Request.Query[concatenateOrder]);
            //string orderDIR = Request.Query["order[0][dir]"];

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
            EvaluationCoefficientService evaluationCoefficientService = new EvaluationCoefficientService(null, connProvider);
            var result = evaluationCoefficientService.EvaluationCoefficientList(dataTableParameter);
            return Json(result);
        }
    }
}
