using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class PARollBackCalculationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public PARollBackCalculationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int RollBackCalculation(string roleId)
        {
            ShareService shareService = new ShareService(appDbContext, connProvider);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            var criteriaCalculation = from ec in appDbContext.EvaluationCalculation
                                      join cc in appDbContext.CriteriaCalculation on ec.EvaluationId equals cc.EvaluationId
                                      //select new{ id2=i,dep=d.}
                                      where (ec.roleId==roleId && ec.PeriodDefinitionId == periodDefinitionId)
                                      select new { cc.CriteriaCalculationId };
            foreach (var item in criteriaCalculation.ToList())
            {
                CriteriaCalculation criteriaCalculationItem = appDbContext.CriteriaCalculation.Where(c => c.CriteriaCalculationId == item.CriteriaCalculationId).SingleOrDefault();
                appDbContext.Remove(criteriaCalculationItem);
            }

            List<EvaluationCalculation> evaluationCalculation = appDbContext.EvaluationCalculation.Where(c => c.roleId == roleId && c.PeriodDefinitionId == periodDefinitionId).ToList();
            appDbContext.RemoveRange(evaluationCalculation);

            List<FinalScoreCalculation> finalScoreCalculation = appDbContext.FinalScoreCalculation.Where(c => c.CoacherType == roleId && c.PeriodDefinitoionId == periodDefinitionId).ToList();
            appDbContext.RemoveRange(finalScoreCalculation);

            int result = appDbContext.SaveChanges();
            return result;
        }
    }
}
