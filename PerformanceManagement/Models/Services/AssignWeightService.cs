using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.PlanningAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class AssignWeightService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public AssignWeightService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<string, object> AssignWeight(List<WeightAssignmentView2> tasksWeightList, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            int? taskWeightSummation = 0;
            var criteriaWeightSummation = 0;
            int? weightWayRealization = null;
            bool hasCriteria = false;

            foreach (var taskItem in tasksWeightList)
            {
                var weightWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == taskItem.PeriodDefinitoionId).SingleOrDefault();
                weightWayRealization = weightWay.WeightWayTask;

                List<Evaluation> taskWeight = new List<Evaluation>();
                var taskEvaluation = appDbContext.Evaluation.Where(c => c.EvaluationId == taskItem.EvaluationId).SingleOrDefault();
                if (weightWay.WeightWayTask == 1)//percent
                {
                    if (taskItem.TaskWeight == 0)
                    {
                        dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                        return dictionary;
                    }
                }
                else if ((taskItem.TaskWeight < NumberScaleMinMax(weightWay, 0, 1) || taskItem.TaskWeight > NumberScaleMinMax(weightWay, 1, 1)) && weightWay.WeightWayTask == 2)//likert
                {
                    return NumberScaleMinMax(dictionary, weightWay, 1);
                }
                if (taskEvaluation.AllocatorRoleId == roleId && taskEvaluation.TaskWeight != taskItem.TaskWeight)
                {
                    taskEvaluation.TaskWeight = taskItem.TaskWeight;
                    taskEvaluation.LastUpdatedBy = personId;
                    taskEvaluation.LastUpdatedDate = DateTime.Now;
                    appDbContext.Evaluation.Update(taskEvaluation);
                }
                taskWeightSummation += taskItem.TaskWeight;
                foreach (var criteriaItem in taskItem.CriteriaWeightViews2)
                {
                    CriteriaWeight criteriaWeight = appDbContext.CriteriaWeight.Where(c => c.CriteriaWeightId == criteriaItem.CriteriaWeightId && c.EvaluationId == criteriaItem.EvaluationId && c.RoleId==roleId).SingleOrDefault();
                    hasCriteria = true;
                    criteriaWeightSummation += criteriaItem.Weight;
                    if (weightWay.WeightWayTask == 1)//percent
                    {
                        if (criteriaItem.Weight == 0)
                        {
                            dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                            return dictionary;
                        }
                    }
                    else if ((criteriaItem.Weight < NumberScaleMinMax(weightWay, 0, 1) || criteriaItem.Weight > NumberScaleMinMax(weightWay, 1, 1)) && weightWay.WeightWayTask == 2)//likert
                    {
                        return NumberScaleMinMax(dictionary, weightWay, 1);
                    }

                    if (criteriaWeight != null && criteriaWeight.Weight != criteriaItem.Weight)
                    {
                        criteriaWeight.Weight = criteriaItem.Weight;
                        criteriaWeight.LastUpdatedBy = personId;
                        criteriaWeight.LastUpdatedDate = DateTime.Now;
                    }
                    else if (criteriaWeight == null)
                    {
                     appDbContext.CriteriaWeight.Add(new CriteriaWeight { CriteriaId = criteriaItem.CriteriaId, EvaluationId = taskItem.EvaluationId, Weight = criteriaItem.Weight, RoleId = roleId, CreatedBy = personId, CreatedDate = DateTime.Now });
                    }
                }
                if (criteriaWeightSummation != 100 && weightWayRealization == 1 && hasCriteria)
                {
                    dictionary.Add("result", "مجموع شاخص های هر هدف باید برابر با 100 باشد.");
                    return dictionary;
                }
                hasCriteria = false;
                criteriaWeightSummation = 0;
            }
            if (taskWeightSummation != 100 && weightWayRealization == 1)
            {
                dictionary.Add("result", "مجموع اهداف باید برابر با 100 باشد.");
                return dictionary;
            }
            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
        public Dictionary<string, object> AssignScore(List<Evaluation> listOfScores, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            ShareService shareService = new ShareService(appDbContext, null);

            //int? taskScoreSummation = 0;
            //var criteriaScoreSummation = 0;
            int? scoreWayRealization = null;
            bool hasCriteria = false;

            foreach (var evaluationItem in listOfScores)
            {
                var scoreWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == evaluationItem.PeriodDefinitoionId).SingleOrDefault();

                scoreWayRealization = scoreWay.WeightWayScore;

                //There is always one item in this loop 
                foreach (var evaluationScoreItem in evaluationItem.EvaluationScores)
                {
                    EvaluationScore evalScoreResult = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationItem.EvaluationId && c.RoleId == roleId).SingleOrDefault();

                    if (scoreWay.WeightWayScore == 1)//percent
                    {
                        if (evaluationScoreItem.Score == 0)
                        {
                            dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                            return dictionary;
                        }
                    }
                    else if ((evaluationScoreItem.Score < NumberScaleMinMax(scoreWay, 0, 3) || evaluationScoreItem.Score > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2 && shareService.CriteiaCount(evaluationItem.TaskId) == 0)//likert
                    {
                        return NumberScaleMinMax(dictionary, scoreWay, 3);
                    }
                    if (evalScoreResult != null && evalScoreResult.Score != evaluationScoreItem.Score)
                    {
                        evalScoreResult.Score = evaluationScoreItem.Score;
                        evalScoreResult.LastUpdatedBy = personId;
                        evalScoreResult.LastUpdatedDate = DateTime.Now;
                        appDbContext.Update(evalScoreResult);
                    }
                    else if (evalScoreResult == null)
                    {
                        EvaluationScore evaluationScore = new EvaluationScore();
                        evaluationScore.Score = evaluationScoreItem.Score;
                        evaluationScore.EvaluationId = evaluationItem.EvaluationId;
                        evaluationScore.RoleId = roleId;
                        evaluationScore.CreatedBy = personId;
                        evaluationScore.CreatedDate = DateTime.Now;
                        //taskScoreSummation += evaluationScoreItem.Score;
                        appDbContext.EvaluationScore.Add(evaluationScore);
                    }
                    
                }
                foreach (var criteriaWeightItem in evaluationItem.CriteriaWeights)
                {
                    hasCriteria = true;
                    //There is always one item in this loop
                    foreach (var criteriaWeightScoreItem in criteriaWeightItem.CriteriaWeightScores)
                    {
                        if (scoreWay.WeightWayScore == 1)//percent
                        {
                            if (criteriaWeightScoreItem.Score == 0)
                            {
                                dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                                return dictionary;
                            }
                        }
                        else if ((criteriaWeightScoreItem.Score < NumberScaleMinMax(scoreWay, 0, 3) || criteriaWeightScoreItem.Score > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2)//likert
                        {
                            return NumberScaleMinMax(dictionary, scoreWay, 3);
                        }

                        int criteriaWeightId = appDbContext.CriteriaWeight.Where(c => c.CriteriaId == criteriaWeightItem.CriteriaId && c.EvaluationId == evaluationItem.EvaluationId).SingleOrDefault().CriteriaWeightId;
                        CriteriaWeightScore criteriaWeightScoreResult = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.RoleId == roleId).SingleOrDefault();

                        if (criteriaWeightScoreResult != null && criteriaWeightScoreResult.Score != criteriaWeightScoreItem.Score)
                        {
                            criteriaWeightScoreResult.Score = criteriaWeightScoreItem.Score;
                            criteriaWeightScoreResult.LastUpdatedBy = personId;
                            criteriaWeightScoreResult.LastUpdatedDate = DateTime.Now;
                            appDbContext.Update(criteriaWeightScoreResult);
                        }
                        else if (criteriaWeightScoreResult == null)
                        {
                            CriteriaWeightScore criteriaWeightScore = new CriteriaWeightScore();
                            criteriaWeightScore.Score = criteriaWeightScoreItem.Score;
                            criteriaWeightScore.CriteriaWeightId = criteriaWeightId;
                            criteriaWeightScore.RoleId = roleId;
                            criteriaWeightScore.CreatedBy = personId;
                            criteriaWeightScore.CreatedDate = DateTime.Now;
                            //criteriaScoreSummation += criteriaWeightScoreItem.Score;
                            appDbContext.CriteriaWeightScore.Add(criteriaWeightScore);
                        }
                        
                    }
                }
                //if (criteriaScoreSummation != 100 && scoreWayRealization == 1 && hasCriteria)
                //{
                //    dictionary.Add("result", "مجموع شاخص های هر هدف باید برابر با 100 باشد.");
                //    return dictionary;
                //}
                hasCriteria = false;
                //criteriaScoreSummation = 0;
            }
            //if (taskScoreSummation != 100 && scoreWayRealization == 1)
            //{
            //    dictionary.Add("result", "مجموع اهداف باید برابر با 100 باشد.");
            //    return dictionary;
            //}
            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }

        private Dictionary<string, object> NumberScaleMinMax(Dictionary<string, object> dictionary, PeriodDefinitoion weightWay, int whichLikert)
        {
            int? likertWay = 0;
            switch (whichLikert)
            {
                case 1:
                    likertWay = weightWay.LikertWeightWayTaskId;
                    break;
                case 2:
                    likertWay = weightWay.LikertWeightWayBehaiviourId;
                    break;
                case 3:
                    likertWay = weightWay.LikertScoreWayId;
                    break;
                    //default:
            }
            var numberScaleMinMax = from ls in appDbContext.LikertScale
                                    join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                                    //select new{ id2=i,dep=d.}
                                    where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == likertWay)
                                    select new { ls.LikertScaleId, ld.NumberScale };
            dictionary.Add("result", $"بازه مجاز جهت تعیین وزن بین {numberScaleMinMax.Min(c => c.NumberScale)} تا {numberScaleMinMax.Max(c => c.NumberScale)} می باشد");
            return dictionary;
        }
        private int NumberScaleMinMax(PeriodDefinitoion weightWay, int minMax, int whichLikert)
        {
            int? likertWay = 0;
            switch (whichLikert)
            {
                case 1:
                    likertWay = weightWay.LikertWeightWayTaskId;
                    break;
                case 2:
                    likertWay = weightWay.LikertWeightWayBehaiviourId;
                    break;
                case 3:
                    likertWay = weightWay.LikertScoreWayId;
                    break;
                    //default:
            }
            var numberScaleMinMax = from ls in appDbContext.LikertScale
                                    join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                                    //select new{ id2=i,dep=d.}
                                    where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == likertWay)
                                    select new { ls.LikertScaleId, ld.NumberScale };
            if (minMax == 1)
            {
                return numberScaleMinMax.Max(c => c.NumberScale);
            }

            return numberScaleMinMax.Min(c => c.NumberScale);
        }
    }
}
