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
    public class PACalculationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public PACalculationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public void FinalCalculation(int personId, string roleId)
        {
            ShareService shareService = new ShareService(appDbContext, connProvider);
            var coacherReceiveTask = from p in appDbContext.People
                                     join e in appDbContext.Evaluation on p.PeopleId equals e.RecieverAllocationPersonId
                                     where (p.EvaluationHierarchyID == e.RecieverAllocationEvaluationHierarchyId && p.EffectiveEndDate == null && e.AllocatorRoleId == roleId)
                                     select new { p.PeopleId, p.EvaluationHierarchyID };

            IQueryable<Evaluation> evaluation;
            List<CriteriaWeight> criteriaWeight;
            List<Evaluation> evalList;
            double? planningAdminScore, participantScore, selfScore;
            EvaluationScore paTaskScore;
            CriteriaWeightScore paCriteriaScore;
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            double? taskTotalCoacherScore;
            HRAdmin.PeriodDefinitoion formulationInfo = shareService.FormulationInfo();
            var hrAdminRoleId = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            double? taskScore = 0;
            double? criteriaScore;
            double? totalTaskScore = 0;
            bool lackOfEvaluation = false;
            //int[] criteriaCalculation;
            //int[] taskCalculation;


            foreach (var coacherReceiveTaskItem in coacherReceiveTask.ToList().Distinct())
            {
                bool isRootCoacher = shareService.IsRootAllocator(coacherReceiveTaskItem.EvaluationHierarchyID ?? 0, coacherReceiveTaskItem.PeopleId);

                totalTaskScore = 0;  /////////////////////temporary commented
                lackOfEvaluation = false;//////////////////////////

                evaluation = appDbContext.Evaluation.Where(c => c.RecieverAllocationEvaluationHierarchyId == coacherReceiveTaskItem.EvaluationHierarchyID && c.RecieverAllocationPersonId == coacherReceiveTaskItem.PeopleId && c.AllocatorRoleId == roleId && c.PeriodDefinitoionId == periodDefinitionId);


                if (evaluation.ToList().Count > 0)
                {
                    lackOfEvaluation = false;////////////////////////
                    evalList = evaluation.ToList();
                    foreach (var evaluationItem in evalList)
                    {
                        taskScore = 0;
                        if (evaluationItem.EvaluationAcceptanceStatusId == 1 || evaluationItem.EvaluationAcceptanceStatusId == 2)
                        {
                            if (lackOfEvaluation)
                            {
                                break;
                            }
                            if (shareService.CriteiaCount(evaluationItem.TaskId) > 0)
                            {
                                criteriaWeight = appDbContext.CriteriaWeight.Where(c => c.EvaluationId == evaluationItem.EvaluationId).ToList();

                                if (shareService.HasParticipant(evaluationItem.EvaluationId) == 1 && shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == true)
                                {
                                    foreach (var criteriaWeightItem in criteriaWeight)
                                    {
                                        paCriteriaScore = GetCriteriaWeightPaScore(criteriaWeightItem.CriteriaWeightId, roleId);

                                        if (paCriteriaScore == null && formulationInfo.LackOfScore == 1)
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                        else
                                        {
                                            planningAdminScore = this.PACriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 1, shareService, true, paCriteriaScore, formulationInfo.LackOfScore);
                                            participantScore = this.PACriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, -1, shareService, true, paCriteriaScore, formulationInfo.LackOfScore);
                                            selfScore = this.PACriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, true, paCriteriaScore, formulationInfo.LackOfScore);
                                            taskTotalCoacherScore = planningAdminScore + participantScore + selfScore;

                                            CriteriaCalculation criteriaCalculation = new CriteriaCalculation();
                                            criteriaCalculation.EvaluationId = evaluationItem.EvaluationId;
                                            criteriaCalculation.CriteriaWeightId = criteriaWeightItem.CriteriaWeightId;
                                            criteriaCalculation.CalculatedCriteriaScore = taskTotalCoacherScore ?? -10000;
                                            criteriaCalculation.CreatedDate = DateTime.Now;
                                            criteriaCalculation.RoleId = roleId;
                                            appDbContext.Add(criteriaCalculation);

                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                criteriaScore = (Convert.ToDouble(criteriaWeightItem.Weight) / CriteriaWeightSummation(criteriaWeight)) * taskTotalCoacherScore;
                                                taskScore += criteriaScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (CriteriaWeightSummation(criteriaWeight) == 100)
                                                {
                                                    criteriaScore = criteriaWeightItem.Weight * taskTotalCoacherScore;
                                                    taskScore += criteriaScore;
                                                }
                                                else
                                                {
                                                    lackOfEvaluation = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (lackOfEvaluation)
                                    {
                                        break;
                                    }
                                    EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                    evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                    evaluationCalculation.CalculatedScore = taskScore ?? -10000;
                                    evaluationCalculation.CreatedDate = DateTime.Now;
                                    evaluationCalculation.CoacherId = null;
                                    evaluationCalculation.CoacherDepartmentId = null;
                                    evaluationCalculation.EmployeeId = coacherReceiveTaskItem.PeopleId;
                                    evaluationCalculation.EmployeeDepartmentId = coacherReceiveTaskItem.EvaluationHierarchyID ?? 0;
                                    evaluationCalculation.CreatedBy = personId;
                                    evaluationCalculation.PeriodDefinitionId = periodDefinitionId;

                                    appDbContext.Add(evaluationCalculation);

                                    if (formulationInfo.WeightWayTask == 2)
                                    {
                                        totalTaskScore += (evaluationItem.TaskWeight / EvaluationWeightSummation(evaluation)) * taskScore;
                                    }
                                    else if (formulationInfo.WeightWayTask == 1)
                                    {
                                        if (EvaluationWeightSummation(evaluation) == 100)
                                        {
                                            totalTaskScore += evaluationItem.TaskWeight * taskScore;
                                        }
                                        else
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                    }
                                }
                                else if (shareService.HasParticipant(evaluationItem.EvaluationId) == 0 || shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == false || shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == null)
                                {
                                    foreach (var criteriaWeightItem in criteriaWeight)
                                    {
                                        paCriteriaScore = GetCriteriaWeightPaScore(criteriaWeightItem.CriteriaWeightId, roleId);

                                        if (paCriteriaScore == null && formulationInfo.LackOfScore == 1)
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                        else
                                        {
                                            planningAdminScore = this.PACriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 1, shareService, false, paCriteriaScore, formulationInfo.LackOfScore);
                                            selfScore = this.PACriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, false, paCriteriaScore, formulationInfo.LackOfScore);
                                            taskTotalCoacherScore = planningAdminScore + selfScore;

                                            CriteriaCalculation criteriaCalculation = new CriteriaCalculation();
                                            criteriaCalculation.EvaluationId = evaluationItem.EvaluationId;
                                            criteriaCalculation.CriteriaWeightId = criteriaWeightItem.CriteriaWeightId;
                                            criteriaCalculation.CalculatedCriteriaScore = taskTotalCoacherScore ?? -10000;
                                            criteriaCalculation.CreatedDate = DateTime.Now;
                                            criteriaCalculation.RoleId = roleId;
                                            appDbContext.Add(criteriaCalculation);

                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                criteriaScore = (Convert.ToDouble(criteriaWeightItem.Weight) / CriteriaWeightSummation(criteriaWeight)) * taskTotalCoacherScore;
                                                taskScore += criteriaScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (CriteriaWeightSummation(criteriaWeight) == 100)
                                                {
                                                    criteriaScore = criteriaWeightItem.Weight * taskTotalCoacherScore;
                                                    taskScore += criteriaScore;
                                                }
                                                else
                                                {
                                                    lackOfEvaluation = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (lackOfEvaluation)
                                    {
                                        break;
                                    }
                                    EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                    evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                    evaluationCalculation.CalculatedScore = taskScore ?? -10000;
                                    evaluationCalculation.CreatedDate = DateTime.Now;
                                    evaluationCalculation.CoacherId = null;
                                    evaluationCalculation.CoacherDepartmentId = null;
                                    evaluationCalculation.EmployeeId = coacherReceiveTaskItem.PeopleId;
                                    evaluationCalculation.EmployeeDepartmentId = coacherReceiveTaskItem.EvaluationHierarchyID ?? 0;
                                    evaluationCalculation.CreatedBy = personId;
                                    evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                    evaluationCalculation.roleId = roleId;

                                    appDbContext.Add(evaluationCalculation);

                                    if (formulationInfo.WeightWayTask == 2)
                                    {
                                        totalTaskScore += (evaluationItem.TaskWeight / EvaluationWeightSummation(evaluation)) * taskScore;
                                    }
                                    else if (formulationInfo.WeightWayTask == 1)
                                    {
                                        if (EvaluationWeightSummation(evaluation) == 100)
                                        {
                                            totalTaskScore += evaluationItem.TaskWeight * taskScore;
                                        }
                                        else
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                #region execute when there is not criteria
                                ////////////////////////
                                paTaskScore = this.GetEvalPaScore(evaluationItem.EvaluationId, roleId);

                                if (paTaskScore == null && formulationInfo.LackOfScore == 1)
                                {
                                    lackOfEvaluation = true;
                                    break;
                                }
                                else if (shareService.HasParticipant(evaluationItem.EvaluationId) == 1 && shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == true)
                                {
                                    planningAdminScore = this.PAEvalCalculationScore(evaluationItem.EvaluationId, 1, shareService, true, paTaskScore, formulationInfo.LackOfScore);
                                    participantScore = this.PAEvalCalculationScore(evaluationItem.EvaluationId, -1, shareService, true, paTaskScore, formulationInfo.LackOfScore);
                                    selfScore = this.PAEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, true, paTaskScore, formulationInfo.LackOfScore);
                                    taskTotalCoacherScore = planningAdminScore + participantScore + selfScore;

                                    EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                    evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                    evaluationCalculation.CalculatedScore = taskTotalCoacherScore ?? -10000;
                                    evaluationCalculation.CreatedDate = DateTime.Now;
                                    evaluationCalculation.CoacherId = null;
                                    evaluationCalculation.CoacherDepartmentId = null;
                                    evaluationCalculation.EmployeeId = coacherReceiveTaskItem.PeopleId;
                                    evaluationCalculation.EmployeeDepartmentId = coacherReceiveTaskItem.EvaluationHierarchyID ?? 0;
                                    evaluationCalculation.CreatedBy = personId;
                                    evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                    evaluationCalculation.roleId = roleId;

                                    appDbContext.Add(evaluationCalculation);

                                    if (formulationInfo.WeightWayTask == 2)
                                    {
                                        totalTaskScore += (evaluationItem.TaskWeight / EvaluationWeightSummation(evaluation)) * taskTotalCoacherScore;
                                    }
                                    else if (formulationInfo.WeightWayTask == 1)
                                    {
                                        if (EvaluationWeightSummation(evaluation) == 100)
                                        {
                                            totalTaskScore += evaluationItem.TaskWeight * taskTotalCoacherScore;
                                        }
                                        else
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                    }
                                }
                                else if (shareService.HasParticipant(evaluationItem.EvaluationId) == 0 || shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == false || shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == null)
                                {
                                    planningAdminScore = this.PAEvalCalculationScore(evaluationItem.EvaluationId, 1, shareService, false, paTaskScore, formulationInfo.LackOfScore);
                                    selfScore = this.PAEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, false, paTaskScore, formulationInfo.LackOfScore);
                                    taskTotalCoacherScore = planningAdminScore + selfScore;

                                    EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                    evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                    evaluationCalculation.CalculatedScore = taskTotalCoacherScore ?? -10000;
                                    evaluationCalculation.CreatedDate = DateTime.Now;
                                    evaluationCalculation.CoacherId = null;
                                    evaluationCalculation.CoacherDepartmentId = null;
                                    evaluationCalculation.EmployeeId = coacherReceiveTaskItem.PeopleId;
                                    evaluationCalculation.EmployeeDepartmentId = coacherReceiveTaskItem.EvaluationHierarchyID ?? 0;
                                    evaluationCalculation.CreatedBy = personId;
                                    evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                    evaluationCalculation.roleId = roleId;

                                    appDbContext.Add(evaluationCalculation);

                                    if (formulationInfo.WeightWayTask == 2)
                                    {
                                        totalTaskScore += (evaluationItem.TaskWeight / EvaluationWeightSummation(evaluation)) * taskTotalCoacherScore;
                                    }
                                    else if (formulationInfo.WeightWayTask == 1)
                                    {
                                        if (EvaluationWeightSummation(evaluation) == 100)
                                        {
                                            totalTaskScore += evaluationItem.TaskWeight * taskTotalCoacherScore;
                                        }
                                        else
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    if (lackOfEvaluation)
                    {
                        FinalScoreCalculation finalScoreCalculation = new FinalScoreCalculation();
                        finalScoreCalculation.AllocatorEvaluationHierarchyId = null;
                        finalScoreCalculation.AllocatorPersonId = null;
                        finalScoreCalculation.RecieverAllocationEvaluationHierarchyId = coacherReceiveTaskItem.EvaluationHierarchyID ?? 0;
                        finalScoreCalculation.RecieverAllocationPersonId = coacherReceiveTaskItem.PeopleId;
                        finalScoreCalculation.PeriodDefinitoionId = periodDefinitionId;
                        finalScoreCalculation.AllocatorLevel = null;
                        finalScoreCalculation.CoacherType = roleId;
                        finalScoreCalculation.ApplyPercentToFinalScore = null;
                        finalScoreCalculation.FinalScore = null;
                        finalScoreCalculation.CreatedBy = personId;
                        finalScoreCalculation.CreatedDate = DateTime.Now;
                        appDbContext.Add(finalScoreCalculation);
                    }
                    else
                    {
                        FinalScoreCalculation finalScoreCalculation2 = new FinalScoreCalculation();
                        finalScoreCalculation2.AllocatorEvaluationHierarchyId = null;
                        finalScoreCalculation2.AllocatorPersonId = null;
                        finalScoreCalculation2.RecieverAllocationEvaluationHierarchyId = coacherReceiveTaskItem.EvaluationHierarchyID ?? 0;
                        finalScoreCalculation2.RecieverAllocationPersonId = coacherReceiveTaskItem.PeopleId;
                        finalScoreCalculation2.PeriodDefinitoionId = periodDefinitionId;
                        finalScoreCalculation2.AllocatorLevel = null;
                        finalScoreCalculation2.CoacherType = roleId;
                        finalScoreCalculation2.ApplyPercentToFinalScore = null;
                        finalScoreCalculation2.FinalScore = totalTaskScore;
                        finalScoreCalculation2.CreatedBy = personId;
                        finalScoreCalculation2.CreatedDate = DateTime.Now;
                        appDbContext.Add(finalScoreCalculation2);
                    }
                }
            }
            //return 1;
        }
        private EvaluationScore GetEvalScore(int evaluationId, int coacherLevel)
        {
            EvaluationScore evaluationScore = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationId && c.CoacherLevel == coacherLevel).SingleOrDefault();
            return evaluationScore;
        }
        private EvaluationScore GetEvalPaScore(int evaluationId, string roleId)
        {
            EvaluationScore evaluationScore = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationId && c.RoleId == roleId).SingleOrDefault();
            return evaluationScore;
        }
        private CriteriaWeightScore GetCriteriaWeightPaScore(int criteriaWeightId, string roleId)
        {
            var criteriaWeightScore = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.RoleId == roleId).SingleOrDefault();
            return criteriaWeightScore;
        }
        private CriteriaWeightScore GetCriteriaWeightScore(int criteriaWeightId, int level)
        {
            var criteriaWeightScore = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.CoacherLevel == level).SingleOrDefault();
            return criteriaWeightScore;
        }
        private double? PAEvalCalculationScore(int evaluationId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationScore level1Score, int? lackOfScore)
        {
            var formulationInfo = shareService.FormulationInfo();

            if (hasParticipant)
            {
                switch (coacherLevel)
                {
                    case 1:
                        if (level1Score != null)
                        {
                            return level1Score.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith)) / 100);
                        }
                        else if (level1Score == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case -1:
                        var participantScore = this.GetEvalScore(evaluationId, -1);
                        if (participantScore != null)
                        {
                            return participantScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                        }
                        else if (participantScore == null && level1Score != null && lackOfScore != 2)
                        {
                            return level1Score.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                        }
                        else if (participantScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case 0:
                        var selfScore = this.GetEvalScore(evaluationId, 0);
                        if (selfScore != null)
                        {
                            return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                        }
                        else if (selfScore == null && level1Score != null && lackOfScore != 2)
                        {
                            return level1Score.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                        }
                        else if (selfScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (!hasParticipant)
            {
                switch (coacherLevel)
                {
                    case 1:
                        if (level1Score != null)
                        {
                            return level1Score.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout)) / 100);
                        }
                        else if (level1Score == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case 0:
                        var selfScore = this.GetEvalScore(evaluationId, 0);
                        if (selfScore != null)
                        {
                            return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                        }
                        else if (selfScore == null && level1Score != null && lackOfScore != 2)
                        {
                            return level1Score.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                        }
                        else if (selfScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            //Lack of evaluation score
            return null;
        }
        private double? PACriteriaCalculationScore(int criteriaWeightId, int coacherLevel, ShareService shareService, bool hasParticipant, CriteriaWeightScore paScore, int? lackOfScore)
        {
            var formulationInfo = shareService.FormulationInfo();
            if (hasParticipant)
            {
                switch (coacherLevel)
                {
                    case 1:
                        if (paScore != null)
                        {
                            return paScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith)) / 100);
                        }
                        else if (paScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case -1:
                        var participantScore = this.GetCriteriaWeightScore(criteriaWeightId, -1);
                        if (participantScore != null)
                        {
                            return participantScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                        }
                        else if (participantScore == null && paScore != null && lackOfScore != 2)
                        {
                            return paScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                        }
                        else if (participantScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case 0:
                        var selfScore = this.GetCriteriaWeightScore(criteriaWeightId, 0);
                        if (selfScore != null)
                        {
                            return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                        }
                        else if (selfScore == null && paScore != null && lackOfScore != 2)
                        {
                            return paScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                        }
                        else if (selfScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (!hasParticipant)
            {
                switch (coacherLevel)
                {
                    case 1:
                        if (paScore != null)
                        {
                            return paScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout)) / 100);
                        }
                        else if (paScore == null && lackOfScore != 2)
                        {
                            return null;
                        }
                        else if (paScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case 0:
                        var selfScore = this.GetCriteriaWeightScore(criteriaWeightId, 0);
                        if (selfScore != null)
                        {
                            return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                        }
                        else if (selfScore == null && paScore != null && lackOfScore != 2)
                        {
                            return paScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                        }
                        else if (selfScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            //Lack of evaluation score
            return null;
        }
        public double? EvaluationWeightSummation(IQueryable<Evaluation> evaluation)
        {
            double summation = 0;
            foreach (var item in evaluation.ToList())
            {
                if (item.TaskWeight == null)
                {
                    return null;
                }

                if (item.EvaluationAcceptanceStatusId == 1 || item.EvaluationAcceptanceStatusId == 2)
                {
                    summation += item.TaskWeight ?? 0;
                }
            }
            return summation;
        }
        public double CriteriaWeightSummation(IEnumerable<CriteriaWeight> criteriaWeight)
        {
            double summation = 0;

            foreach (var item in criteriaWeight)
            {
                summation += item.Weight;
            }
            return summation;
        }
        public Dictionary<object, object> GetCalculationScoreList(DataTableParameter dataTableParameter, int? employeeId, int? periodDefinitionId, string paRoleId)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "employeeDepartmentName", "employeeFullName" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            int exactOrder = dataTableParameter.orderColumn + 1;
            if (exactOrder == 9)
            {
                exactOrder += 5;
            }
            if (dataTableParameter.orderable == true)
            {
                order = "order by " + exactOrder + " " + dataTableParameter.orderDIR;
            }
            else
            {
                order = "order by 1 asc";
            }
            if (dataTableParameter.length == -1)
            {
                limit = "";
            }
            else
            {
                limit = "and indexx between @start and @endd ";
            }
            if (where != "")
            {
                for (int i = 0; i < aColumns.Length; i++)
                {
                    where = where + aColumns[i] + " Like @sVal or ";

                }
                where = where.Substring(0, where.Length - 3);
                where += ") ";
            }
            else
            {
                where = "";
            }
            IDbConnection conn = connProvider.Connection;

            string queryTotalResult = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",employeeHierarchyId" +
                ",employeeDepartmentName" +
                ",employeeId" +
                ",employeeFullName" +
                ",employeePositionType" +
                ",ROUND(finalTaskScore, 3)finalTaskScore" +
                ",ROUND(finalScoreDepartment, 3)finalScoreDepartment " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId asc))  indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", employeeHierarchyId" +
                ", employeeDepartmentName" +
                ", employeeId" +
                ", employeeFullName" +
                ", employeePositionType" +
                ", sum(finalTaskScore)finalTaskScore" +
                ", sum(finalTaskScore) finalScoreDepartment " +
                "from " +
                "( " +
                "select " +
                "fsc.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", fsc.FinalScoreCalculationId" +
                ", fsc.RecieverAllocationEvaluationHierarchyId employeeHierarchyId" +
                ", fsc.RecieverAllocationPersonId employeeId" +
                ", (select d2.ShortName from evaluationHierarchies eh2 join Departments d2 on eh2.DepartmentId = d2.DepartmentId " +
                "and eh2.EffectiveEndDate is null and d2.EffectiveEndDate is null " +
                "where " +
                "eh2.EvaluationHierarchyId = fsc.RecieverAllocationEvaluationHierarchyId)employeeDepartmentName" +
                ", (select CONCAT(p2.FirstName, ' ', p2.LastName) from People p2 " +
                "where " +
                "p2.EvaluationHierarchyID = fsc.RecieverAllocationEvaluationHierarchyId " +
                "and p2.PeopleId = fsc.RecieverAllocationPersonId " +
                "and p2.EffectiveEndDate is null)employeeFullName" +
                ",(select p2.PositionType from People p2 where p2.PeopleId = fsc.RecieverAllocationPersonId " +
                "and p2.EvaluationHierarchyID = fsc.RecieverAllocationEvaluationHierarchyId and p2.EffectiveEndDate is null)employeePositionType" +
                ",fsc.FinalScore finalTaskScore" +
                ",null finalCompetencyScore" +
                ",fsc.ApplyPercentToFinalScore applyPercentToFinalTaskScore" +
                ",null applyPercentToFinalCompetencyScore " +
                "from " +
                "FinalScoreCalculation fsc " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fsc.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and fsc.CoacherType = @paRoleIdd" +
                ") finalScoreTbl " +
                "group by " +
                "PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", employeeHierarchyId" +
                ", employeeId" +
                ", employeeDepartmentName" +
                ", employeeFullName" +
                ", employeePositionType)tbl " +
                "where 1 = 1 " +
                "and PeriodDefinitoionId = ISNULL(@periodDefinitionIdDTt, PeriodDefinitoionId) " +
                "and employeeId = ISNULL(@employeeIdd, employeeId)  ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",employeeHierarchyId" +
                ",employeeDepartmentName" +
                ",employeeId" +
                ",employeeFullName" +
                ",employeePositionType" +
                ",ROUND(finalTaskScore, 3)finalTaskScore" +
                ",ROUND(finalScoreDepartment, 3)finalScoreDepartment " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId asc))  indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", employeeHierarchyId" +
                ", employeeDepartmentName" +
                ", employeeId" +
                ", employeeFullName" +
                ", employeePositionType" +
                ", sum(finalTaskScore)finalTaskScore" +
                ", sum(finalTaskScore) finalScoreDepartment " +
                "from " +
                "( " +
                "select " +
                "fsc.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", fsc.FinalScoreCalculationId" +
                ", fsc.RecieverAllocationEvaluationHierarchyId employeeHierarchyId" +
                ", fsc.RecieverAllocationPersonId employeeId" +
                ", (select d2.ShortName from evaluationHierarchies eh2 join Departments d2 on eh2.DepartmentId = d2.DepartmentId " +
                "and eh2.EffectiveEndDate is null and d2.EffectiveEndDate is null " +
                "where " +
                "eh2.EvaluationHierarchyId = fsc.RecieverAllocationEvaluationHierarchyId)employeeDepartmentName" +
                ", (select CONCAT(p2.FirstName, ' ', p2.LastName) from People p2 " +
                "where " +
                "p2.EvaluationHierarchyID = fsc.RecieverAllocationEvaluationHierarchyId " +
                "and p2.PeopleId = fsc.RecieverAllocationPersonId " +
                "and p2.EffectiveEndDate is null)employeeFullName" +
                ",(select p2.PositionType from People p2 where p2.PeopleId = fsc.RecieverAllocationPersonId " +
                "and p2.EvaluationHierarchyID = fsc.RecieverAllocationEvaluationHierarchyId and p2.EffectiveEndDate is null)employeePositionType" +
                ",fsc.FinalScore finalTaskScore" +
                ",null finalCompetencyScore" +
                ",fsc.ApplyPercentToFinalScore applyPercentToFinalTaskScore" +
                ",null applyPercentToFinalCompetencyScore " +
                "from " +
                "FinalScoreCalculation fsc " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fsc.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and fsc.CoacherType = @paRoleIdd" +
                ") finalScoreTbl " +
                "group by " +
                "PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", employeeHierarchyId" +
                ", employeeId" +
                ", employeeDepartmentName" +
                ", employeeFullName" +
                ", employeePositionType)tbl " +
                "where 1 = 1 " +
                "and PeriodDefinitoionId = ISNULL(@periodDefinitionIdDTt, PeriodDefinitoionId) " +
                "and employeeId = ISNULL(@employeeIdd, employeeId)  " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",employeeHierarchyId" +
                ",employeeDepartmentName" +
                ",employeeId" +
                ",employeeFullName" +
                ",employeePositionType" +
                ",ROUND(finalTaskScore, 3)finalTaskScore" +
                ",ROUND(finalScoreDepartment, 3)finalScoreDepartment " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId asc))  indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", employeeHierarchyId" +
                ", employeeDepartmentName" +
                ", employeeId" +
                ", employeeFullName" +
                ", employeePositionType" +
                ", sum(finalTaskScore)finalTaskScore" +
                ", sum(finalTaskScore) finalScoreDepartment " +
                "from " +
                "( " +
                "select " +
                "fsc.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", fsc.FinalScoreCalculationId" +
                ", fsc.RecieverAllocationEvaluationHierarchyId employeeHierarchyId" +
                ", fsc.RecieverAllocationPersonId employeeId" +
                ", (select d2.ShortName from evaluationHierarchies eh2 join Departments d2 on eh2.DepartmentId = d2.DepartmentId " +
                "and eh2.EffectiveEndDate is null and d2.EffectiveEndDate is null " +
                "where " +
                "eh2.EvaluationHierarchyId = fsc.RecieverAllocationEvaluationHierarchyId)employeeDepartmentName" +
                ", (select CONCAT(p2.FirstName, ' ', p2.LastName) from People p2 " +
                "where " +
                "p2.EvaluationHierarchyID = fsc.RecieverAllocationEvaluationHierarchyId " +
                "and p2.PeopleId = fsc.RecieverAllocationPersonId " +
                "and p2.EffectiveEndDate is null)employeeFullName" +
                ",(select p2.PositionType from People p2 where p2.PeopleId = fsc.RecieverAllocationPersonId " +
                "and p2.EvaluationHierarchyID = fsc.RecieverAllocationEvaluationHierarchyId and p2.EffectiveEndDate is null)employeePositionType" +
                ",fsc.FinalScore finalTaskScore" +
                ",null finalCompetencyScore" +
                ",fsc.ApplyPercentToFinalScore applyPercentToFinalTaskScore" +
                ",null applyPercentToFinalCompetencyScore " +
                "from " +
                "FinalScoreCalculation fsc " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fsc.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and fsc.CoacherType = @paRoleIdd" +
                ") finalScoreTbl " +
                "group by " +
                "PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", employeeHierarchyId" +
                ", employeeId" +
                ", employeeDepartmentName" +
                ", employeeFullName" +
                ", employeePositionType)tbl " +
                "where 1 = 1 " +
                "and PeriodDefinitoionId = ISNULL(@periodDefinitionIdDTt, PeriodDefinitoionId) " +
                "and employeeId = ISNULL(@employeeIdd, employeeId)  " +
                where +
                limit +
                order;

            conn.Open();
            List<object> query = null;
            if (dataTableParameter.length != -1 && dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    employeeIdd = employeeId,
                    periodDefinitionIdDTt = periodDefinitionId,
                    paRoleIdd = paRoleId
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    employeeIdd = employeeId,
                    periodDefinitionIdDTt = periodDefinitionId,
                    paRoleIdd = paRoleId
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    employeeIdd = employeeId,
                    periodDefinitionIdDTt = periodDefinitionId,
                    paRoleIdd = paRoleId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                employeeIdd = employeeId,
                periodDefinitionIdDTt = periodDefinitionId,
                paRoleIdd = paRoleId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                employeeIdd = employeeId,
                periodDefinitionIdDTt = periodDefinitionId,
                paRoleIdd = paRoleId
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }
        public int FinalizeCalc(int coacherId, string roleId)
        {
            int result = 0;
            ShareService shareService = new ShareService(appDbContext, connProvider);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            FinalizeCalculation finalizeCalculationQuery = appDbContext.FinalizeCalculation.Where(c => c.CocherId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            FinalScoreCalculation finalScoreCalculation = appDbContext.FinalScoreCalculation.Where(c => c.AllocatorPersonId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).FirstOrDefault();
            FinalScoreCompetencyCalculation finalScoreCompetencyCalculation = appDbContext.FinalScoreCompetencyCalculation.Where(c => c.AllocatorPersonId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).FirstOrDefault();

            if ((finalScoreCalculation != null || finalScoreCompetencyCalculation != null) && finalizeCalculationQuery == null)
            {
                FinalizeCalculation finalizeCalculation = new FinalizeCalculation();
                finalizeCalculation.CocherId = null;
                finalizeCalculation.IsFinalization = true;
                finalizeCalculation.PeriodDefinitoionId = periodDefinitionId;
                finalizeCalculation.CreatedBy = coacherId;
                finalizeCalculation.CreatedDate = DateTime.Now;
                finalizeCalculation.RoleId = roleId;
                appDbContext.Add(finalizeCalculation);
                result = appDbContext.SaveChanges();
            }
            else if (finalizeCalculationQuery != null)
            {
                return -1;
            }
            else if (finalScoreCalculation == null && finalScoreCompetencyCalculation == null)
            {
                return -2;
            }

            return result;
        }
    }
}
