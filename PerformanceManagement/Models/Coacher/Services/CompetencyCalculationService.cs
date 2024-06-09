using Dapper;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class CompetencyCalculationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public CompetencyCalculationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public void CompetencyFinalCalculation(int personId, string roleId)
        {
            ShareService shareService = new ShareService(appDbContext, connProvider);
            var headOfDepartment = appDbContext.evaluationHierarchies.Where(c => c.SupervisorId == personId && c.EffectiveEndDate == null).ToList();
            IQueryable<EvaluationBehaviouralCompetency> evaluationBehaviouralCompetency;
            List<EvaluationBehaviouralCompetency> competencyList;
            double? coacherLevel1Score, coacherLevel2Score, participantScore, selfScore;
            EvaluationBehaviourCompetencyScore coacher1Score;
            EvaluationBehaviourCompetencyScore coacher2Score;
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            double? competencyTotalCoacherScore;
            HRAdmin.PeriodDefinitoion formulationInfo = shareService.FormulationInfo();
            var hrAdminRoleId = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            double? totalCompetencyScore = 0;
            bool lackOfEvaluation = false;

            foreach (var headItem in headOfDepartment)
            {
                var subSetEmployee = SubsetEmployeeList(personId, headItem.EvaluationHierarchyId);
                bool isRootAllocator = shareService.IsRootAllocator(headItem.EvaluationHierarchyId, personId);

                foreach (var subSetItem in subSetEmployee)
                {
                    totalCompetencyScore = 0;  /////////////////////temporary commented
                    lackOfEvaluation = false;//////////////////////////
                    if (subSetItem.Levell == 1)
                    {
                        evaluationBehaviouralCompetency = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.RecieverAllocationEvaluationBehaviouralHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorEvaluationBehaviouralHierarchyId == headItem.EvaluationHierarchyId && c.PeriodDefinitoionId == shareService.GetPeriodDefinitionId() || (c.RecieverAllocationEvaluationBehaviouralHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorRoleId == hrAdminRoleId && c.PeriodDefinitoionId==periodDefinitionId));
                        if (evaluationBehaviouralCompetency.ToList().Count > 0)
                        {
                            lackOfEvaluation = false;////////////////
                            competencyList = evaluationBehaviouralCompetency.ToList();
                            foreach (var evaluationItem in competencyList)
                            {
                                if (evaluationItem.EvaluationAcceptanceStatusId == 1 || evaluationItem.EvaluationAcceptanceStatusId == 2)
                                {
                                    coacher1Score = this.GetCompetencyEvalScore(evaluationItem.EvaluationBehaviouralCompetencyId, 1);
                                    coacher2Score = this.GetCompetencyEvalScore(evaluationItem.EvaluationBehaviouralCompetencyId, 2);

                                    if (coacher1Score == null && coacher2Score == null && formulationInfo.LackOfScore == 1)
                                    {
                                        lackOfEvaluation = true;
                                        break;
                                    }
                                    else if (shareService.HasCompetencyParticipant(evaluationItem.EvaluationBehaviouralCompetencyId) == 1 && shareService.CompetencyParticipantConfirmation(evaluationItem.EvaluationBehaviouralCompetencyId) == true)
                                    {
                                        if (!isRootAllocator)
                                        {
                                            coacherLevel1Score = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 1, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                            coacherLevel2Score = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 2, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                            participantScore = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, -1, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                            selfScore = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 0, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);

                                            competencyTotalCoacherScore = coacherLevel1Score + coacherLevel2Score + participantScore + selfScore;
                                        }
                                        else
                                        {
                                            coacherLevel1Score = this.DRootCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 1, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                            participantScore = this.DRootCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, -1, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                            selfScore = this.DRootCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 0, shareService, true, coacher1Score, formulationInfo.LackOfScore);

                                            competencyTotalCoacherScore = coacherLevel1Score + participantScore + selfScore;
                                        }
                                        EvaluationCompetencyCalculation evaluationCompetencyCalculation = new EvaluationCompetencyCalculation();
                                        evaluationCompetencyCalculation.EvaluationBehaviouralCompetencyId = evaluationItem.EvaluationBehaviouralCompetencyId;
                                        evaluationCompetencyCalculation.CalculatedCompetencyScore = competencyTotalCoacherScore ?? -10000;
                                        evaluationCompetencyCalculation.CreatedDate = DateTime.Now;
                                        evaluationCompetencyCalculation.CoacherId = personId;
                                        evaluationCompetencyCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                        evaluationCompetencyCalculation.EmployeeId = subSetItem.PeopleId;
                                        evaluationCompetencyCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                        evaluationCompetencyCalculation.CreatedBy = personId;
                                        evaluationCompetencyCalculation.PeriodDefinitionId = periodDefinitionId;
                                        appDbContext.Add(evaluationCompetencyCalculation);
                                        if (formulationInfo.WeightWayTask == 2)
                                        {
                                            totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency)) * competencyTotalCoacherScore;
                                        }
                                        else if (formulationInfo.WeightWayTask == 1)
                                        {
                                            if (EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency) == 100)
                                            {
                                                totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / 100) * competencyTotalCoacherScore;
                                            }
                                            else
                                            {
                                                lackOfEvaluation = true;
                                                break;
                                            }
                                        }
                                    }
                                    else if (shareService.HasCompetencyParticipant(evaluationItem.EvaluationBehaviouralCompetencyId) == 0 || shareService.CompetencyParticipantConfirmation(evaluationItem.EvaluationBehaviouralCompetencyId) == false || shareService.CompetencyParticipantConfirmation(evaluationItem.EvaluationBehaviouralCompetencyId) == null)
                                    {
                                        if (!isRootAllocator)
                                        {
                                            coacherLevel1Score = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 1, shareService, false, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                            coacherLevel2Score = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 2, shareService, false, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                            selfScore = this.DCoacherEvalComoetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 0, shareService, false, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                            competencyTotalCoacherScore = coacherLevel1Score + coacherLevel2Score + selfScore;
                                        }
                                        else
                                        {
                                            coacherLevel1Score = this.DRootCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 1, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                            selfScore = this.DRootCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 0, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                            competencyTotalCoacherScore = coacherLevel1Score + selfScore;
                                        }
                                        EvaluationCompetencyCalculation evaluationCompetencyCalculation = new EvaluationCompetencyCalculation();
                                        evaluationCompetencyCalculation.EvaluationBehaviouralCompetencyId = evaluationItem.EvaluationBehaviouralCompetencyId;
                                        evaluationCompetencyCalculation.CalculatedCompetencyScore = competencyTotalCoacherScore ?? -10000;
                                        evaluationCompetencyCalculation.CreatedDate = DateTime.Now;
                                        evaluationCompetencyCalculation.CoacherId = personId;
                                        evaluationCompetencyCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                        evaluationCompetencyCalculation.EmployeeId = subSetItem.PeopleId;
                                        evaluationCompetencyCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                        evaluationCompetencyCalculation.CreatedBy = personId;
                                        evaluationCompetencyCalculation.PeriodDefinitionId = periodDefinitionId;

                                        appDbContext.Add(evaluationCompetencyCalculation);
                                        if (formulationInfo.WeightWayTask == 2)
                                        {
                                            totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency)) * competencyTotalCoacherScore;
                                        }
                                        else if (formulationInfo.WeightWayTask == 1)
                                        {
                                            if (EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency) == 100)
                                            {
                                                totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / 100) * competencyTotalCoacherScore;
                                            }
                                            else
                                            {
                                                lackOfEvaluation = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }//End of competencyList foreach
                            if (lackOfEvaluation)
                            {
                                FinalScoreCompetencyCalculation finalScoreCompetencyCalculation = new FinalScoreCompetencyCalculation();
                                finalScoreCompetencyCalculation.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCompetencyCalculation.AllocatorPersonId = personId;
                                finalScoreCompetencyCalculation.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCompetencyCalculation.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCompetencyCalculation.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCompetencyCalculation.AllocatorLevel = subSetItem.Levell;
                                finalScoreCompetencyCalculation.CoacherType = roleId;
                                finalScoreCompetencyCalculation.FinalCompetneciesScore = null;
                                finalScoreCompetencyCalculation.ApplyPercentToFinalScore = null;
                                finalScoreCompetencyCalculation.CreatedBy = personId;
                                finalScoreCompetencyCalculation.CreatedDate = DateTime.Now;
                                appDbContext.Add(finalScoreCompetencyCalculation);
                            }
                            else
                            {
                                FinalScoreCompetencyCalculation finalScoreCompetencyCalculation2 = new FinalScoreCompetencyCalculation();
                                finalScoreCompetencyCalculation2.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCompetencyCalculation2.AllocatorPersonId = personId;
                                finalScoreCompetencyCalculation2.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCompetencyCalculation2.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCompetencyCalculation2.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCompetencyCalculation2.AllocatorLevel = subSetItem.Levell;
                                finalScoreCompetencyCalculation2.CoacherType = roleId;
                                finalScoreCompetencyCalculation2.FinalCompetneciesScore = totalCompetencyScore;
                                finalScoreCompetencyCalculation2.ApplyPercentToFinalScore = totalCompetencyScore * (formulationInfo.CompetencyPercent / 100);
                                finalScoreCompetencyCalculation2.CreatedBy = personId;
                                finalScoreCompetencyCalculation2.CreatedDate = DateTime.Now;
                                appDbContext.Add(finalScoreCompetencyCalculation2);
                            }

                        }
                    }
                    else if (subSetItem.Levell > 1)
                    {
                        evaluationBehaviouralCompetency = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.RecieverAllocationEvaluationBehaviouralHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorEvaluationBehaviouralHierarchyId == headItem.EvaluationHierarchyId && c.PeriodDefinitoionId == shareService.GetPeriodDefinitionId() || (c.RecieverAllocationEvaluationBehaviouralHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorRoleId == hrAdminRoleId));
                        if (evaluationBehaviouralCompetency.ToList().Count > 0)
                        {
                            lackOfEvaluation = false;////////////////
                            competencyList = evaluationBehaviouralCompetency.ToList();
                            foreach (var evaluationItem in competencyList)
                            {
                                if (evaluationItem.EvaluationAcceptanceStatusId == 1 || evaluationItem.EvaluationAcceptanceStatusId == 2)
                                {
                                    coacher1Score = this.GetCompetencyEvalScore(evaluationItem.EvaluationBehaviouralCompetencyId, subSetItem.Levell);

                                    if (coacher1Score == null && formulationInfo.LackOfScore == 1)
                                    {
                                        lackOfEvaluation = true;
                                        break;
                                    }
                                    else if (shareService.HasCompetencyParticipant(evaluationItem.EvaluationBehaviouralCompetencyId) == 1 && shareService.CompetencyParticipantConfirmation(evaluationItem.EvaluationBehaviouralCompetencyId) == true)
                                    {

                                        coacherLevel1Score = this.INDCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, subSetItem.Levell, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                        participantScore = this.INDCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, -1, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                        selfScore = this.INDCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 0, shareService, true, coacher1Score, formulationInfo.LackOfScore);

                                        competencyTotalCoacherScore = coacherLevel1Score + participantScore + selfScore;

                                        EvaluationCompetencyCalculation evaluationCompetencyCalculation = new EvaluationCompetencyCalculation();
                                        evaluationCompetencyCalculation.EvaluationBehaviouralCompetencyId = evaluationItem.EvaluationBehaviouralCompetencyId;
                                        evaluationCompetencyCalculation.CalculatedCompetencyScore = competencyTotalCoacherScore ?? -10000;
                                        evaluationCompetencyCalculation.CreatedDate = DateTime.Now;
                                        evaluationCompetencyCalculation.CoacherId = personId;
                                        evaluationCompetencyCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                        evaluationCompetencyCalculation.EmployeeId = subSetItem.PeopleId;
                                        evaluationCompetencyCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                        evaluationCompetencyCalculation.CreatedBy = personId;
                                        evaluationCompetencyCalculation.PeriodDefinitionId = periodDefinitionId;
                                        appDbContext.Add(evaluationCompetencyCalculation);
                                        if (formulationInfo.WeightWayTask == 2)
                                        {
                                            totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency)) * competencyTotalCoacherScore;
                                        }
                                        else if (formulationInfo.WeightWayTask == 1)
                                        {
                                            if (EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency) == 100)
                                            {
                                                totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / 100) * competencyTotalCoacherScore;
                                            }
                                            else
                                            {
                                                lackOfEvaluation = true;
                                                break;
                                            }
                                        }
                                    }
                                    else if (shareService.HasCompetencyParticipant(evaluationItem.EvaluationBehaviouralCompetencyId) == 0 || shareService.CompetencyParticipantConfirmation(evaluationItem.EvaluationBehaviouralCompetencyId) == false || shareService.CompetencyParticipantConfirmation(evaluationItem.EvaluationBehaviouralCompetencyId) == null)
                                    {

                                        coacherLevel1Score = this.INDCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, subSetItem.Levell, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                        selfScore = this.INDCoacherEvalCompetencyCalculationScore(evaluationItem.EvaluationBehaviouralCompetencyId, 0, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                        competencyTotalCoacherScore = coacherLevel1Score + selfScore;

                                        EvaluationCompetencyCalculation evaluationCompetencyCalculation = new EvaluationCompetencyCalculation();
                                        evaluationCompetencyCalculation.EvaluationBehaviouralCompetencyId = evaluationItem.EvaluationBehaviouralCompetencyId;
                                        evaluationCompetencyCalculation.CalculatedCompetencyScore = competencyTotalCoacherScore ?? -10000;
                                        evaluationCompetencyCalculation.CreatedDate = DateTime.Now;
                                        evaluationCompetencyCalculation.CoacherId = personId;
                                        evaluationCompetencyCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                        evaluationCompetencyCalculation.EmployeeId = subSetItem.PeopleId;
                                        evaluationCompetencyCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                        evaluationCompetencyCalculation.CreatedBy = personId;
                                        evaluationCompetencyCalculation.PeriodDefinitionId = periodDefinitionId;

                                        appDbContext.Add(evaluationCompetencyCalculation);
                                        if (formulationInfo.WeightWayTask == 2)
                                        {
                                            totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency)) * competencyTotalCoacherScore;
                                        }
                                        else if (formulationInfo.WeightWayTask == 1)
                                        {
                                            if (EvaluationCompetencyWeightSummation(evaluationBehaviouralCompetency) == 100)
                                            {
                                                totalCompetencyScore += (Convert.ToDouble(evaluationItem.BehaviouralCompetencyWeight) / 100) * competencyTotalCoacherScore;
                                            }
                                            else
                                            {
                                                lackOfEvaluation = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }//End of competencyList foreach
                            if (lackOfEvaluation)
                            {
                                FinalScoreCompetencyCalculation finalScoreCompetencyCalculation = new FinalScoreCompetencyCalculation();
                                finalScoreCompetencyCalculation.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCompetencyCalculation.AllocatorPersonId = personId;
                                finalScoreCompetencyCalculation.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCompetencyCalculation.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCompetencyCalculation.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCompetencyCalculation.AllocatorLevel = subSetItem.Levell;
                                finalScoreCompetencyCalculation.CoacherType = roleId;
                                finalScoreCompetencyCalculation.FinalCompetneciesScore = null;
                                finalScoreCompetencyCalculation.ApplyPercentToFinalScore = null;
                                finalScoreCompetencyCalculation.CreatedBy = personId;
                                finalScoreCompetencyCalculation.CreatedDate = DateTime.Now;
                                appDbContext.Add(finalScoreCompetencyCalculation);
                            }
                            else
                            {
                                FinalScoreCompetencyCalculation finalScoreCompetencyCalculation2 = new FinalScoreCompetencyCalculation();
                                finalScoreCompetencyCalculation2.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCompetencyCalculation2.AllocatorPersonId = personId;
                                finalScoreCompetencyCalculation2.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCompetencyCalculation2.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCompetencyCalculation2.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCompetencyCalculation2.AllocatorLevel = subSetItem.Levell;
                                finalScoreCompetencyCalculation2.CoacherType = roleId;
                                finalScoreCompetencyCalculation2.FinalCompetneciesScore = totalCompetencyScore;
                                finalScoreCompetencyCalculation2.ApplyPercentToFinalScore = totalCompetencyScore * (formulationInfo.CompetencyPercent / 100);
                                finalScoreCompetencyCalculation2.CreatedBy = personId;
                                finalScoreCompetencyCalculation2.CreatedDate = DateTime.Now;
                                appDbContext.Add(finalScoreCompetencyCalculation2);
                            }

                        }
                    }
                }
            }
            //return 1;
        }
        private EvaluationBehaviourCompetencyScore GetCompetencyEvalScore(int evaluationBehaviouralCompetencyId, int coacherLevel)
        {
            EvaluationBehaviourCompetencyScore evaluationBehaviourCompetencyScore = appDbContext.EvaluationBehaviourCompetencyScore.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId && c.CoacherLevel == coacherLevel).SingleOrDefault();
            return evaluationBehaviourCompetencyScore;
        }
        private double? DCoacherEvalComoetencyCalculationScore(int evaluationBehaviouralCompetencyId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationBehaviourCompetencyScore level1Score, EvaluationBehaviourCompetencyScore level2Score, int? lackOfScore)
        {
            var formulationInfo = shareService.FormulationInfo();
            var score1 = level1Score;
            var score2 = level2Score;

            if (score1 != null || score2 != null || lackOfScore != 2 || lackOfScore == 2)
            {
                if (hasParticipant)
                {
                    switch (coacherLevel)
                    {
                        case 1:
                            if (score1 != null)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherBWith) / 100);
                            }
                            else if (score1 == null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherBWith) / 100);
                            }
                            else if (score1 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (score2 != null)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherBWith) / 100);
                            }
                            else if (score2 == null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherBWith) / 100);
                            }
                            else if (score2 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case -1:
                            var participantScore = this.GetCompetencyEvalScore(evaluationBehaviouralCompetencyId, -1);
                            if (participantScore != null)
                            {
                                return participantScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientB) / 100);
                            }
                            else if (participantScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientB) / 100);
                            }
                            else if (participantScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientB) / 100);
                            }
                            else if (participantScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 0:
                            var selfScore = this.GetCompetencyEvalScore(evaluationBehaviouralCompetencyId, 0);
                            if (selfScore != null)
                            {
                                return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWith) / 100);
                            }
                            else if (selfScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWith) / 100);
                            }
                            else if (selfScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWith) / 100);
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
                            if (score1 != null)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherBWithout) / 100);
                            }
                            else if (score1 == null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherBWithout) / 100);
                            }
                            else if (score1 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (score2 != null)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherBWithout) / 100);
                            }
                            else if (score2 == null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherBWithout) / 100);
                            }
                            else if (score2 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 0:
                            var selfScore = this.GetCompetencyEvalScore(evaluationBehaviouralCompetencyId, 0);
                            if (selfScore != null)
                            {
                                return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWithout) / 100);
                            }
                            else if (selfScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWithout) / 100);
                            }
                            else if (selfScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWithout) / 100);
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
            }
            else
            {
                //Lack of evaluation score
                return null;
            }
            //Lack of evaluation score
            return null;
        }
        private double? DRootCoacherEvalCompetencyCalculationScore(int evaluationBehaviouralCompetencyId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationBehaviourCompetencyScore level1Score, int? lackOfScore)
        {
            var formulationInfo = shareService.FormulationInfo();

            if (hasParticipant)
            {
                switch (coacherLevel)
                {
                    case 1:
                        if (level1Score != null)
                        {
                            return level1Score.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherBWith) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherBWith)) / 100);
                        }
                        else if (level1Score == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case -1:
                        var participantScore = this.GetCompetencyEvalScore(evaluationBehaviouralCompetencyId, -1);
                        if (participantScore != null)
                        {
                            return participantScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientB) / 100);
                        }
                        else if (participantScore == null && level1Score != null && lackOfScore != 2)
                        {
                            return level1Score.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientB) / 100);
                        }
                        else if (participantScore == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case 0:
                        var selfScore = this.GetCompetencyEvalScore(evaluationBehaviouralCompetencyId, 0);
                        if (selfScore != null)
                        {
                            return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWith) / 100);
                        }
                        else if (selfScore == null && level1Score != null && lackOfScore != 2)
                        {
                            return level1Score.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWith) / 100);
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
                            return level1Score.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherBWithout) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherBWithout)) / 100);
                        }
                        else if (level1Score == null && lackOfScore == 2)
                        {
                            return 0;
                        }
                        break;
                    case 0:
                        var selfScore = this.GetCompetencyEvalScore(evaluationBehaviouralCompetencyId, 0);
                        if (selfScore != null)
                        {
                            return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWithout) / 100);
                        }
                        else if (selfScore == null && level1Score != null && lackOfScore != 2)
                        {
                            return level1Score.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationBWithout) / 100);
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
        private double? INDCoacherEvalCompetencyCalculationScore(int evaluationId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationBehaviourCompetencyScore IndirectCoacherScore, int? lackOfScore)
        {
            var formulationInfo = shareService.FormulationInfo();

            if (IndirectCoacherScore != null || lackOfScore != 2 || lackOfScore == 2)
            {
                if (hasParticipant)
                {
                    switch (coacherLevel)
                    {
                        case 2:
                            if (IndirectCoacherScore != null)
                            {
                                return IndirectCoacherScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith)) / 100);
                            }
                            else if (IndirectCoacherScore == null && lackOfScore != 2)
                            {
                                return null;
                            }
                            else if (IndirectCoacherScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 3:
                            if (IndirectCoacherScore != null)
                            {
                                return IndirectCoacherScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith)) / 100);
                            }
                            else if (IndirectCoacherScore == null && lackOfScore != 2)
                            {
                                return null;
                            }
                            else if (IndirectCoacherScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 4:
                            if (IndirectCoacherScore != null)
                            {
                                return IndirectCoacherScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith)) / 100);
                            }
                            else if (IndirectCoacherScore == null && lackOfScore != 2)
                            {
                                return null;
                            }
                            else if (IndirectCoacherScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case -1:
                            var participantScore = this.GetCompetencyEvalScore(evaluationId, -1);
                            if (participantScore != null)
                            {
                                return participantScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                            }
                            else if (participantScore == null && IndirectCoacherScore != null && lackOfScore != 2)
                            {
                                return IndirectCoacherScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                            }
                            else if (participantScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 0:
                            var selfScore = this.GetCompetencyEvalScore(evaluationId, 0);
                            if (selfScore != null)
                            {
                                return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                            }
                            else if (selfScore == null && IndirectCoacherScore != null && lackOfScore != 2)
                            {
                                return IndirectCoacherScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
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
                        case 2:
                            if (IndirectCoacherScore != null)
                            {
                                return IndirectCoacherScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout)) / 100);
                            }
                            else if (IndirectCoacherScore == null && lackOfScore != 2)
                            {
                                return null;
                            }
                            else if (IndirectCoacherScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 3:
                            if (IndirectCoacherScore != null)
                            {
                                return IndirectCoacherScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout)) / 100);
                            }
                            else if (IndirectCoacherScore == null && lackOfScore != 2)
                            {
                                return null;
                            }
                            else if (IndirectCoacherScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 4:
                            if (IndirectCoacherScore != null)
                            {
                                return IndirectCoacherScore.Score * ((Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) + Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout)) / 100);
                            }
                            else if (IndirectCoacherScore == null && lackOfScore != 2)
                            {
                                return null;
                            }
                            else if (IndirectCoacherScore == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 0:
                            var selfScore = this.GetCompetencyEvalScore(evaluationId, 0);
                            if (selfScore != null)
                            {
                                return selfScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                            }
                            else if (selfScore == null && IndirectCoacherScore != null && lackOfScore != 2)
                            {
                                return IndirectCoacherScore.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
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
            }
            else
            {
                //Lack of evaluation score
                return null;
            }
            //Lack of evaluation score
            return null;
        }

        public IEnumerable<SubsetEmployeeView> SubsetEmployeeList(int personId, int departmentId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId EvalHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 0 Levell " +
                "from " +
                "evaluationHierarchies eh," +
                "Departments d," +
                "People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and eh.SupervisorId = p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @departmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId EvalHierarchyId" +
                ", p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",0 Levell " +
                "from " +
                "evaluationHierarchies eh," +
                "Departments d," +
                "People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId]= @departmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.EvalHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "SELECT " +
                "[EvaluationHierarchyId] as id" +
                ",EvalHierarchyId" +
                ",PeopleId" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell " +
                "FROM EmpsCTE " +
                "where Levell!=0 " +
                "order by Levell";
            conn.Open();
            List<SubsetEmployeeView> query = null;

            query = conn.Query<SubsetEmployeeView>(sQuery, new { personIdd = personId, departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return (query);
        }
        public double? EvaluationCompetencyWeightSummation(IQueryable<EvaluationBehaviouralCompetency> evaluationBehaviouralCompetency)
        {
            double summation = 0;
            foreach (var item in evaluationBehaviouralCompetency.ToList())
            {
                if (item.BehaviouralCompetencyWeight == null)
                {
                    return null;
                }

                if (item.EvaluationAcceptanceStatusId == 1 || item.EvaluationAcceptanceStatusId == 2)
                {
                    summation += item.BehaviouralCompetencyWeight ?? 0;
                }
            }
            return summation;
        }
    }
}
