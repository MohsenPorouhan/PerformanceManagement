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
    public class RollBackCalculationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public RollBackCalculationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int RollBackCalculation(int coacherId)
        {
            ShareService shareService = new ShareService(appDbContext, connProvider);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            var criteriaCalculation = from ec in appDbContext.EvaluationCalculation
                                      join cc in appDbContext.CriteriaCalculation on ec.EvaluationId equals cc.EvaluationId
                                      //select new{ id2=i,dep=d.}
                                      where (ec.CoacherId == coacherId && ec.PeriodDefinitionId == periodDefinitionId)
                                      select new { cc.CriteriaCalculationId };
            //Because of lackOfEvaluation some criterias are inserted in database without related evaluations so per rollback this query detect orphan criterias
            var orphanCriteriaCalculation = from cc in appDbContext.CriteriaCalculation
                                            join ec in appDbContext.EvaluationCalculation on cc.EvaluationId equals ec.EvaluationId into joined
                                            from j in joined.DefaultIfEmpty()
                                            join e in appDbContext.Evaluation on cc.EvaluationId equals e.EvaluationId
                                            where (j.EvaluationId == null && e.AllocatorPersonId == coacherId && e.PeriodDefinitoionId == periodDefinitionId)
                                            //select new{ id2=i,dep=d.}
                                            select new { cc.CriteriaCalculationId };

            foreach (var item in criteriaCalculation.ToList())
            {
                CriteriaCalculation criteriaCalculationItem = appDbContext.CriteriaCalculation.Where(c => c.CriteriaCalculationId == item.CriteriaCalculationId).SingleOrDefault();
                appDbContext.Remove(criteriaCalculationItem);
            }

            foreach (var item2 in orphanCriteriaCalculation.ToList())
            {
                CriteriaCalculation criteriaCalculationItem = appDbContext.CriteriaCalculation.Where(c => c.CriteriaCalculationId == item2.CriteriaCalculationId).SingleOrDefault();
                appDbContext.Remove(criteriaCalculationItem);
            }

            List<EvaluationCalculation> evaluationCalculation = appDbContext.EvaluationCalculation.Where(c => c.CoacherId == coacherId && c.PeriodDefinitionId == periodDefinitionId).ToList();
            appDbContext.RemoveRange(evaluationCalculation);

            List<FinalScoreCalculation> finalScoreCalculation = appDbContext.FinalScoreCalculation.Where(c => c.AllocatorPersonId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).ToList();
            appDbContext.RemoveRange(finalScoreCalculation);

            List<EvaluationCompetencyCalculation> evaluationCompetencyCalculation = appDbContext.EvaluationCompetencyCalculation.Where(c => c.CoacherId == coacherId && c.PeriodDefinitionId == periodDefinitionId).ToList();
            appDbContext.RemoveRange(evaluationCompetencyCalculation);

            List<FinalScoreCompetencyCalculation> finalScoreCompetencyCalculation = appDbContext.FinalScoreCompetencyCalculation.Where(c => c.AllocatorPersonId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).ToList();
            appDbContext.RemoveRange(finalScoreCompetencyCalculation);

            int result = appDbContext.SaveChanges();
            return result;
        }
    }
}
