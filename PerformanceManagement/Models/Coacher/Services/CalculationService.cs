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
    public class CalculationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public CalculationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public void FinalCalculation(int personId, string roleId)
        {
            ShareService shareService = new ShareService(appDbContext, connProvider);
            var headOfDepartment = appDbContext.evaluationHierarchies.Where(c => c.SupervisorId == personId && c.EffectiveEndDate == null).ToList();
            IQueryable<Evaluation> evaluation;
            List<CriteriaWeight> criteriaWeight;
            List<Evaluation> evalList;
            double? coacherLevel1Score, coacherLevel2Score, participantScore, selfScore;
            EvaluationScore coacher1Score;
            EvaluationScore coacher2Score;
            CriteriaWeightScore coacher1CriteriaScore;
            CriteriaWeightScore coacher2CriteriaScore;
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

            foreach (var headItem in headOfDepartment)
            {
                var subSetEmployee = SubsetEmployeeList(personId, headItem.EvaluationHierarchyId);
                bool isRootAllocator = shareService.IsRootAllocator(headItem.EvaluationHierarchyId, personId);

                foreach (var subSetItem in subSetEmployee)
                {
                    totalTaskScore = 0;  /////////////////////temporary commented
                    lackOfEvaluation = false;//////////////////////////
                    if (subSetItem.Levell == 1)
                    {
                        evaluation = appDbContext.Evaluation.Where(c => c.RecieverAllocationEvaluationHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorEvaluationHierarchyId == headItem.EvaluationHierarchyId && c.PeriodDefinitoionId == periodDefinitionId || (c.RecieverAllocationEvaluationHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorRoleId == hrAdminRoleId && c.PeriodDefinitoionId==periodDefinitionId));
                        if (evaluation.ToList().Count > 0)
                        {
                            lackOfEvaluation = false;////////////////
                            evalList = evaluation.ToList();
                            foreach (var evaluationItem in evalList)
                            {
                                taskScore = 0;
                                if (evaluationItem.EvaluationAcceptanceStatusId == 1 || evaluationItem.EvaluationAcceptanceStatusId == 2)
                                {
                                    if (shareService.CriteiaCount(evaluationItem.TaskId) > 0)
                                    {
                                        criteriaWeight = appDbContext.CriteriaWeight.Where(c => c.EvaluationId == evaluationItem.EvaluationId).ToList();

                                        if (shareService.HasParticipant(evaluationItem.EvaluationId) == 1 && shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == true)
                                        {
                                            foreach (var criteriaWeightItem in criteriaWeight)
                                            {
                                                coacher1CriteriaScore = GetCriteriaWeightScore(criteriaWeightItem.CriteriaWeightId, 1);
                                                coacher2CriteriaScore = GetCriteriaWeightScore(criteriaWeightItem.CriteriaWeightId, 2);

                                                if (coacher1CriteriaScore == null && coacher2CriteriaScore == null && formulationInfo.LackOfScore == 1)
                                                {
                                                    lackOfEvaluation = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    if (!isRootAllocator)
                                                    {
                                                        coacherLevel1Score = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 1, shareService, true, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);
                                                        coacherLevel2Score = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 2, shareService, true, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);
                                                        participantScore = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, -1, shareService, true, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);
                                                        selfScore = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, true, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);
                                                        taskTotalCoacherScore = coacherLevel1Score + coacherLevel2Score + participantScore + selfScore;
                                                    }
                                                    else
                                                    {
                                                        coacherLevel1Score = this.DRootCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 1, shareService, true, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                        participantScore = this.DRootCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, -1, shareService, true, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                        selfScore = this.DRootCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, true, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                        taskTotalCoacherScore = coacherLevel1Score + participantScore + selfScore;
                                                    }
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
                                                            criteriaScore = (Convert.ToDouble(criteriaWeightItem.Weight)/100) * taskTotalCoacherScore;
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
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);
                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskScore;
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
                                                coacher1CriteriaScore = GetCriteriaWeightScore(criteriaWeightItem.CriteriaWeightId, 1);
                                                coacher2CriteriaScore = GetCriteriaWeightScore(criteriaWeightItem.CriteriaWeightId, 2);

                                                if (coacher1CriteriaScore == null && coacher2CriteriaScore == null && formulationInfo.LackOfScore == 1)
                                                {
                                                    lackOfEvaluation = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    if (!isRootAllocator)
                                                    {
                                                        coacherLevel1Score = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 1, shareService, false, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);
                                                        coacherLevel2Score = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 2, shareService, false, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);
                                                        selfScore = this.DCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, false, coacher1CriteriaScore, coacher2CriteriaScore, formulationInfo.LackOfScore);

                                                        taskTotalCoacherScore = coacherLevel1Score + coacherLevel2Score + selfScore;
                                                    }
                                                    else
                                                    {
                                                        coacherLevel1Score = this.DRootCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 1, shareService, false, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                        selfScore = this.DRootCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, false, coacher1CriteriaScore, formulationInfo.LackOfScore);

                                                        taskTotalCoacherScore = coacherLevel1Score + selfScore;
                                                    }
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
                                                            criteriaScore = (Convert.ToDouble(criteriaWeightItem.Weight)/100) * taskTotalCoacherScore;
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
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);
                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskScore;
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
                                        coacher1Score = this.GetEvalScore(evaluationItem.EvaluationId, 1);
                                        coacher2Score = this.GetEvalScore(evaluationItem.EvaluationId, 2);

                                        if (coacher1Score == null && coacher2Score == null && formulationInfo.LackOfScore == 1)
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                        else if (shareService.HasParticipant(evaluationItem.EvaluationId) == 1 && shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == true)
                                        {
                                            if (!isRootAllocator)
                                            {
                                                coacherLevel1Score = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, 1, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                                coacherLevel2Score = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, 2, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                                participantScore = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, -1, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                                selfScore = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, true, coacher1Score, coacher2Score, formulationInfo.LackOfScore);

                                                taskTotalCoacherScore = coacherLevel1Score + coacherLevel2Score + participantScore + selfScore;
                                            }
                                            else
                                            {
                                                coacherLevel1Score = this.DRootCoacherEvalCalculationScore(evaluationItem.EvaluationId, 1, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                                participantScore = this.DRootCoacherEvalCalculationScore(evaluationItem.EvaluationId, -1, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                                selfScore = this.DRootCoacherEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, true, coacher1Score, formulationInfo.LackOfScore);

                                                taskTotalCoacherScore = coacherLevel1Score + participantScore + selfScore;
                                            }
                                            EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                            evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                            evaluationCalculation.CalculatedScore = taskTotalCoacherScore ?? -10000;
                                            evaluationCalculation.CreatedDate = DateTime.Now;
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);
                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskTotalCoacherScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskTotalCoacherScore;
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
                                            if (!isRootAllocator)
                                            {
                                                coacherLevel1Score = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, 1, shareService, false, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                                coacherLevel2Score = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, 2, shareService, false, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                                selfScore = this.DCoacherEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, false, coacher1Score, coacher2Score, formulationInfo.LackOfScore);
                                                taskTotalCoacherScore = coacherLevel1Score + coacherLevel2Score + selfScore;
                                            }
                                            else
                                            {
                                                coacherLevel1Score = this.DRootCoacherEvalCalculationScore(evaluationItem.EvaluationId, 1, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                                selfScore = this.DRootCoacherEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                                taskTotalCoacherScore = coacherLevel1Score + selfScore;
                                            }
                                            EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                            evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                            evaluationCalculation.CalculatedScore = taskTotalCoacherScore ?? -10000;
                                            evaluationCalculation.CreatedDate = DateTime.Now;
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);
                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskTotalCoacherScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskTotalCoacherScore;
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
                            }//End of evalList foreach
                            if (lackOfEvaluation)
                            {
                                FinalScoreCalculation finalScoreCalculation = new FinalScoreCalculation();
                                finalScoreCalculation.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCalculation.AllocatorPersonId = personId;
                                finalScoreCalculation.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCalculation.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCalculation.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCalculation.AllocatorLevel = subSetItem.Levell;
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
                                finalScoreCalculation2.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCalculation2.AllocatorPersonId = personId;
                                finalScoreCalculation2.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCalculation2.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCalculation2.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCalculation2.AllocatorLevel = subSetItem.Levell;
                                finalScoreCalculation2.CoacherType = roleId;
                                finalScoreCalculation2.ApplyPercentToFinalScore = totalTaskScore * (formulationInfo.TaskPercent / 100);
                                finalScoreCalculation2.FinalScore = totalTaskScore;
                                finalScoreCalculation2.CreatedBy = personId;
                                finalScoreCalculation2.CreatedDate = DateTime.Now;
                                appDbContext.Add(finalScoreCalculation2);
                            }

                        }
                    }
                    else if (subSetItem.Levell > 1)
                    {
                        evaluation = appDbContext.Evaluation.Where(c => c.RecieverAllocationEvaluationHierarchyId == subSetItem.EvalHierarchyId && c.RecieverAllocationPersonId == subSetItem.PeopleId && c.AllocatorEvaluationHierarchyId == headItem.EvaluationHierarchyId && c.PeriodDefinitoionId == periodDefinitionId);

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
                                                coacher1CriteriaScore = GetCriteriaWeightScore(criteriaWeightItem.CriteriaWeightId, subSetItem.Levell);

                                                if (coacher1CriteriaScore == null && formulationInfo.LackOfScore == 1)
                                                {
                                                    lackOfEvaluation = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    coacherLevel1Score = this.INDCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, subSetItem.Levell, shareService, true, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                    participantScore = this.INDCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, -1, shareService, true, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                    selfScore = this.INDCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, true, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                    taskTotalCoacherScore = coacherLevel1Score + participantScore + selfScore;

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
                                                            criteriaScore = (Convert.ToDouble(criteriaWeightItem.Weight)/100) * taskTotalCoacherScore;
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
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);

                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskScore;
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
                                                coacher1CriteriaScore = GetCriteriaWeightScore(criteriaWeightItem.CriteriaWeightId, subSetItem.Levell);

                                                if (coacher1CriteriaScore == null && formulationInfo.LackOfScore == 1)
                                                {
                                                    lackOfEvaluation = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    coacherLevel1Score = this.INDCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, subSetItem.Levell, shareService, false, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                    selfScore = this.INDCoacherCriteriaCalculationScore(criteriaWeightItem.CriteriaWeightId, 0, shareService, false, coacher1CriteriaScore, formulationInfo.LackOfScore);
                                                    taskTotalCoacherScore = coacherLevel1Score + selfScore;

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
                                                            criteriaScore = (Convert.ToDouble(criteriaWeightItem.Weight)/100) * taskTotalCoacherScore;
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
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);

                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskScore;
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
                                        coacher1Score = this.GetEvalScore(evaluationItem.EvaluationId, subSetItem.Levell);

                                        if (coacher1Score == null && formulationInfo.LackOfScore == 1)
                                        {
                                            lackOfEvaluation = true;
                                            break;
                                        }
                                        else if (shareService.HasParticipant(evaluationItem.EvaluationId) == 1 && shareService.ParticipantConfirmation(evaluationItem.EvaluationId) == true)
                                        {
                                            coacherLevel1Score = this.INDCoacherEvalCalculationScore(evaluationItem.EvaluationId, subSetItem.Levell, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                            participantScore = this.INDCoacherEvalCalculationScore(evaluationItem.EvaluationId, -1, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                            selfScore = this.INDCoacherEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, true, coacher1Score, formulationInfo.LackOfScore);
                                            taskTotalCoacherScore = coacherLevel1Score + participantScore + selfScore;

                                            EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                            evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                            evaluationCalculation.CalculatedScore = taskTotalCoacherScore ?? -10000;
                                            evaluationCalculation.CreatedDate = DateTime.Now;
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);

                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskTotalCoacherScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskTotalCoacherScore;
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
                                            coacherLevel1Score = this.INDCoacherEvalCalculationScore(evaluationItem.EvaluationId, subSetItem.Levell, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                            selfScore = this.INDCoacherEvalCalculationScore(evaluationItem.EvaluationId, 0, shareService, false, coacher1Score, formulationInfo.LackOfScore);
                                            taskTotalCoacherScore = coacherLevel1Score + selfScore;

                                            EvaluationCalculation evaluationCalculation = new EvaluationCalculation();
                                            evaluationCalculation.EvaluationId = evaluationItem.EvaluationId;
                                            evaluationCalculation.CalculatedScore = taskTotalCoacherScore ?? -10000;
                                            evaluationCalculation.CreatedDate = DateTime.Now;
                                            evaluationCalculation.CoacherId = personId;
                                            evaluationCalculation.CoacherDepartmentId = headItem.EvaluationHierarchyId;
                                            evaluationCalculation.EmployeeId = subSetItem.PeopleId;
                                            evaluationCalculation.EmployeeDepartmentId = subSetItem.EvalHierarchyId;
                                            evaluationCalculation.CreatedBy = personId;
                                            evaluationCalculation.PeriodDefinitionId = periodDefinitionId;
                                            evaluationCalculation.roleId = roleId;

                                            appDbContext.Add(evaluationCalculation);

                                            if (formulationInfo.WeightWayTask == 2)
                                            {
                                                totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight) / EvaluationWeightSummation(evaluation)) * taskTotalCoacherScore;
                                            }
                                            else if (formulationInfo.WeightWayTask == 1)
                                            {
                                                if (EvaluationWeightSummation(evaluation) == 100)
                                                {
                                                    totalTaskScore += (Convert.ToDouble(evaluationItem.TaskWeight)/100) * taskTotalCoacherScore;
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
                                finalScoreCalculation.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCalculation.AllocatorPersonId = personId;
                                finalScoreCalculation.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCalculation.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCalculation.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCalculation.AllocatorLevel = subSetItem.Levell;
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
                                finalScoreCalculation2.AllocatorEvaluationHierarchyId = headItem.EvaluationHierarchyId;
                                finalScoreCalculation2.AllocatorPersonId = personId;
                                finalScoreCalculation2.RecieverAllocationEvaluationHierarchyId = subSetItem.EvalHierarchyId;
                                finalScoreCalculation2.RecieverAllocationPersonId = subSetItem.PeopleId;
                                finalScoreCalculation2.PeriodDefinitoionId = periodDefinitionId;
                                finalScoreCalculation2.AllocatorLevel = subSetItem.Levell;
                                finalScoreCalculation2.CoacherType = roleId;
                                finalScoreCalculation2.ApplyPercentToFinalScore = totalTaskScore * (formulationInfo.TaskPercent / 100);
                                finalScoreCalculation2.FinalScore = totalTaskScore;
                                finalScoreCalculation2.CreatedBy = personId;
                                finalScoreCalculation2.CreatedDate = DateTime.Now;
                                appDbContext.Add(finalScoreCalculation2);
                            }
                        }
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
        private CriteriaWeightScore GetCriteriaWeightScore(int criteriaWeightId, int coacherLevel)
        {
            var criteriaWeightScore = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.CoacherLevel == coacherLevel).SingleOrDefault();
            return criteriaWeightScore;
        }
        private double? DCoacherEvalCalculationScore(int evaluationId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationScore level1Score, EvaluationScore level2Score, int? lackOfScore)
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
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) / 100);
                            }
                            else if (score1 == null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) / 100);
                            }
                            else if (score1 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (score2 != null)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith) / 100);
                            }
                            else if (score2 == null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith) / 100);
                            }
                            else if (score2 == null && lackOfScore == 2)
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
                            else if (participantScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                            }
                            else if (participantScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
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
                            else if (selfScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                            }
                            else if (selfScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
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
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) / 100);
                            }
                            else if (score1 == null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) / 100);
                            }
                            else if (score1 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (score2 != null)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout) / 100);
                            }
                            else if (score2 == null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout) / 100);
                            }
                            else if (score2 == null && lackOfScore == 2)
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
                            else if (selfScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                            }
                            else if (selfScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
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
        private double? DRootCoacherEvalCalculationScore(int evaluationId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationScore level1Score, int? lackOfScore)
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
        private double? DCoacherCriteriaCalculationScore(int criteriaWeightId, int coacherLevel, ShareService shareService, bool hasParticipant, CriteriaWeightScore level1Score, CriteriaWeightScore level2Score, int? lackOfScore)
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
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) / 100);
                            }
                            else if (score1 == null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWith) / 100);
                            }
                            else if (score1 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (score2 != null)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith) / 100);
                            }
                            else if (score2 == null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWith) / 100);
                            }
                            else if (score2 == null && lackOfScore == 2)
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
                            else if (participantScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
                            }
                            else if (participantScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.participantCoefficientT) / 100);
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
                            else if (selfScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
                            }
                            else if (selfScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWith) / 100);
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
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) / 100);
                            }
                            else if (score1 == null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level1CoacherTWithout) / 100);
                            }
                            else if (score1 == null && lackOfScore == 2)
                            {
                                return 0;
                            }
                            break;
                        case 2:
                            if (score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout) / 100);
                            }
                            else if (score2 == null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.level2CoacherTWithout) / 100);
                            }
                            else if (score2 == null && lackOfScore == 2)
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
                            else if (selfScore == null && score1 != null && lackOfScore != 2)
                            {
                                return score1.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
                            }
                            else if (selfScore == null && score2 != null && lackOfScore != 2)
                            {
                                return score2.Score * (Convert.ToDouble(formulationInfo.EvaluationCoefficient.selfEvaluationTWithout) / 100);
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
        private double? DRootCoacherCriteriaCalculationScore(int criteriaWeightId, int coacherLevel, ShareService shareService, bool hasParticipant, CriteriaWeightScore level1Score, int? lackOfScore)
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
                        var participantScore = this.GetCriteriaWeightScore(criteriaWeightId, -1);
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
                        var selfScore = this.GetCriteriaWeightScore(criteriaWeightId, 0);
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
                        else if (level1Score == null && lackOfScore != 2)
                        {
                            return null;
                        }
                        else if (level1Score == null && lackOfScore == 2)
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
        private double? INDCoacherEvalCalculationScore(int evaluationId, int coacherLevel, ShareService shareService, bool hasParticipant, EvaluationScore IndirectCoacherScore, int? lackOfScore)
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
                            var participantScore = this.GetEvalScore(evaluationId, -1);
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
                            var selfScore = this.GetEvalScore(evaluationId, 0);
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
                            var selfScore = this.GetEvalScore(evaluationId, 0);
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
        private double? INDCoacherCriteriaCalculationScore(int criteriaWeightId, int coacherLevel, ShareService shareService, bool hasParticipant, CriteriaWeightScore IndirectCoacherScore, int? lackOfScore)
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
                            var participantScore = this.GetCriteriaWeightScore(criteriaWeightId, -1);
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
                            var selfScore = this.GetCriteriaWeightScore(criteriaWeightId, 0);
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
                            var selfScore = this.GetCriteriaWeightScore(criteriaWeightId, 0);
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
        public Dictionary<object, object> GetCalculationScoreList(DataTableParameter dataTableParameter, int? coacherDepartmentId, int? employeeId, int? periodDefinitionId)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "coacherDepartmentName", "coacherFullName", "employeeDepartmentName", "employeeFullName" };
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
                ",coacherId" +
                ",coacherEvaluationId" +
                ",coacherDepartmentName" +
                ",coacherFullName" +
                ",coacherPositionType" +
                ",employeeHierarchyId" +
                ",employeeDepartmentName" +
                ",employeeId" +
                ",employeeFullName" +
                ",employeePositionType" +
                ",ROUND(finalTaskScore,3)finalTaskScore" +
                ",ROUND(finalCompetencyScore,3)finalCompetencyScore" +
                ",ROUND(applyPercentToFinalTaskScore,3)applyPercentToFinalTaskScore" +
                ",ROUND(applyPercentToFinalCompetencyScore,3)applyPercentToFinalCompetencyScore" +
                ",ROUND(finalScoreDepartment,3)finalScoreDepartment " +
                "from(" +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId asc))  indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", coacherId" +
                ", coacherEvaluationId" +
                ", coacherDepartmentName" +
                ", coacherFullName" +
                ", coacherPositionType" +
                ", employeeHierarchyId" +
                ", employeeDepartmentName" +
                ", employeeId" +
                ", employeeFullName" +
                ", employeePositionType" +
                ", sum(finalTaskScore)finalTaskScore" +
                ", sum(finalCompetencyScore)finalCompetencyScore" +
                ", sum(applyPercentToFinalTaskScore)applyPercentToFinalTaskScore" +
                ", sum(applyPercentToFinalCompetencyScore)applyPercentToFinalCompetencyScore" +
                ", sum(applyPercentToFinalTaskScore) + sum(applyPercentToFinalCompetencyScore) finalScoreDepartment " +
                "from" +
                "(" +
                "select " +
                "fsc.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", p.PeopleId coacherId" +
                ", p.EvaluationHierarchyID coacherEvaluationId" +
                ", d.ShortName coacherDepartmentName" +
                ", CONCAT(p.FirstName, ' ', p.LastName)coacherFullName" +
                ", p.PositionType coacherPositionType" +
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
                "FinalScoreCalculation fsc join People p " +
                "on p.EvaluationHierarchyID = fsc.AllocatorEvaluationHierarchyId " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fsc.PeriodDefinitoionId " +
                "join evaluationHierarchies eh " +
                "on eh.EvaluationHierarchyId = fsc.AllocatorEvaluationHierarchyId and p.PeopleId = fsc.AllocatorPersonId " +
                "join Departments d " +
                "on eh.DepartmentId = d.DepartmentId and eh.EffectiveEndDate is null and d.EffectiveEndDate is null " +
                "where " +
                "1 = 1 " +
                "and AllocatorEvaluationHierarchyId =@coacherDepartmentIdd " +
                "and p.EffectiveEndDate is null " +
                "union " +
                "select " +
                "fscc.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",p.PeopleId coacherId" +
                ", p.EvaluationHierarchyID coacherEvaluationId" +
                ", d.ShortName coacherDepartmentName" +
                ", CONCAT(p.FirstName, ' ', p.LastName)coacherFullName" +
                ",p.PositionType coacherPositionType" +
                ", fscc.FinalScoreCompetencyCalculationId" +
                ",fscc.RecieverAllocationEvaluationHierarchyId employeeHierarchyId" +
                ", fscc.RecieverAllocationPersonId employeeId" +
                ",(select d2.ShortName from evaluationHierarchies eh2 join Departments d2 on eh2.DepartmentId = d2.DepartmentId " +
                "and eh2.EffectiveEndDate is null and d2.EffectiveEndDate is null " +
                "where " +
                "eh2.EvaluationHierarchyId = fscc.RecieverAllocationEvaluationHierarchyId)employeeDepartmentName" +
                ",(select CONCAT(p2.FirstName, ' ', p2.LastName) from People p2 " +
                "where " +
                "p2.EvaluationHierarchyID = fscc.RecieverAllocationEvaluationHierarchyId " +
                "and p2.PeopleId = fscc.RecieverAllocationPersonId " +
                "and p2.EffectiveEndDate is null)employeeFullName" +
                ",(select p2.PositionType from People p2 where p2.PeopleId = fscc.RecieverAllocationPersonId " +
                "and p2.EvaluationHierarchyID = fscc.RecieverAllocationEvaluationHierarchyId and p2.EffectiveEndDate is null)employeePositionType" +
                ",null finalTaskScore" +
                ",fscc.FinalCompetneciesScore finalCompetencyScore" +
                ",null ApplyPercentToFinalTaskScore" +
                ",fscc.ApplyPercentToFinalScore ApplyPercentToFinalCompetencyScore " +
                "from " +
                "FinalScoreCompetencyCalculation fscc join People p " +
                "on p.EvaluationHierarchyID = fscc.AllocatorEvaluationHierarchyId " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fscc.PeriodDefinitoionId " +
                "join evaluationHierarchies eh " +
                "on eh.EvaluationHierarchyId = fscc.AllocatorEvaluationHierarchyId and p.PeopleId = fscc.AllocatorPersonId " +
                "join Departments d " +
                "on eh.DepartmentId = d.DepartmentId and eh.EffectiveEndDate is null and d.EffectiveEndDate is null " +
                "where " +
                "1 = 1 " +
                "and AllocatorEvaluationHierarchyId =@coacherDepartmentIdd " +
                "and p.EffectiveEndDate is null" +
                ") finalScoreTbl " +
                "group by " +
                "PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", coacherId" +
                ", coacherEvaluationId" +
                ", coacherDepartmentName" +
                ", coacherFullName" +
                ", coacherPositionType" +
                ", employeeHierarchyId" +
                ", employeeId" +
                ", employeeDepartmentName" +
                ", employeeFullName" +
                ", coacherPositionType" +
                ", employeePositionType)tbl " +
                "where 1 = 1 " +
                "and PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt,PeriodDefinitoionId) " +
                "and employeeId=ISNULL(@employeeIdd,employeeId) ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",coacherId" +
                ",coacherEvaluationId" +
                ",coacherDepartmentName" +
                ",coacherFullName" +
                ",coacherPositionType" +
                ",employeeHierarchyId" +
                ",employeeDepartmentName" +
                ",employeeId" +
                ",employeeFullName" +
                ",employeePositionType" +
                ",ROUND(finalTaskScore,3)finalTaskScore" +
                ",ROUND(finalCompetencyScore,3)finalCompetencyScore" +
                ",ROUND(applyPercentToFinalTaskScore,3)applyPercentToFinalTaskScore" +
                ",ROUND(applyPercentToFinalCompetencyScore,3)applyPercentToFinalCompetencyScore" +
                ",ROUND(finalScoreDepartment,3)finalScoreDepartment " +
                "from(" +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId asc))  indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", coacherId" +
                ", coacherEvaluationId" +
                ", coacherDepartmentName" +
                ", coacherFullName" +
                ", coacherPositionType" +
                ", employeeHierarchyId" +
                ", employeeDepartmentName" +
                ", employeeId" +
                ", employeeFullName" +
                ", employeePositionType" +
                ", sum(finalTaskScore)finalTaskScore" +
                ", sum(finalCompetencyScore)finalCompetencyScore" +
                ", sum(applyPercentToFinalTaskScore)applyPercentToFinalTaskScore" +
                ", sum(applyPercentToFinalCompetencyScore)applyPercentToFinalCompetencyScore" +
                ", sum(applyPercentToFinalTaskScore) + sum(applyPercentToFinalCompetencyScore) finalScoreDepartment " +
                "from" +
                "(" +
                "select " +
                "fsc.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", p.PeopleId coacherId" +
                ", p.EvaluationHierarchyID coacherEvaluationId" +
                ", d.ShortName coacherDepartmentName" +
                ", CONCAT(p.FirstName, ' ', p.LastName)coacherFullName" +
                ", p.PositionType coacherPositionType" +
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
                "FinalScoreCalculation fsc join People p " +
                "on p.EvaluationHierarchyID = fsc.AllocatorEvaluationHierarchyId " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fsc.PeriodDefinitoionId " +
                "join evaluationHierarchies eh " +
                "on eh.EvaluationHierarchyId = fsc.AllocatorEvaluationHierarchyId and p.PeopleId = fsc.AllocatorPersonId " +
                "join Departments d " +
                "on eh.DepartmentId = d.DepartmentId and eh.EffectiveEndDate is null and d.EffectiveEndDate is null " +
                "where " +
                "1 = 1 " +
                "and AllocatorEvaluationHierarchyId =@coacherDepartmentIdd " +
                "and p.EffectiveEndDate is null " +
                "union " +
                "select " +
                "fscc.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",p.PeopleId coacherId" +
                ", p.EvaluationHierarchyID coacherEvaluationId" +
                ", d.ShortName coacherDepartmentName" +
                ", CONCAT(p.FirstName, ' ', p.LastName)coacherFullName" +
                ",p.PositionType coacherPositionType" +
                ", fscc.FinalScoreCompetencyCalculationId" +
                ",fscc.RecieverAllocationEvaluationHierarchyId employeeHierarchyId" +
                ", fscc.RecieverAllocationPersonId employeeId" +
                ",(select d2.ShortName from evaluationHierarchies eh2 join Departments d2 on eh2.DepartmentId = d2.DepartmentId " +
                "and eh2.EffectiveEndDate is null and d2.EffectiveEndDate is null " +
                "where " +
                "eh2.EvaluationHierarchyId = fscc.RecieverAllocationEvaluationHierarchyId)employeeDepartmentName" +
                ",(select CONCAT(p2.FirstName, ' ', p2.LastName) from People p2 " +
                "where " +
                "p2.EvaluationHierarchyID = fscc.RecieverAllocationEvaluationHierarchyId " +
                "and p2.PeopleId = fscc.RecieverAllocationPersonId " +
                "and p2.EffectiveEndDate is null)employeeFullName" +
                ",(select p2.PositionType from People p2 where p2.PeopleId = fscc.RecieverAllocationPersonId " +
                "and p2.EvaluationHierarchyID = fscc.RecieverAllocationEvaluationHierarchyId and p2.EffectiveEndDate is null)employeePositionType" +
                ",null finalTaskScore" +
                ",fscc.FinalCompetneciesScore finalCompetencyScore" +
                ",null ApplyPercentToFinalTaskScore" +
                ",fscc.ApplyPercentToFinalScore ApplyPercentToFinalCompetencyScore " +
                "from " +
                "FinalScoreCompetencyCalculation fscc join People p " +
                "on p.EvaluationHierarchyID = fscc.AllocatorEvaluationHierarchyId " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fscc.PeriodDefinitoionId " +
                "join evaluationHierarchies eh " +
                "on eh.EvaluationHierarchyId = fscc.AllocatorEvaluationHierarchyId and p.PeopleId = fscc.AllocatorPersonId " +
                "join Departments d " +
                "on eh.DepartmentId = d.DepartmentId and eh.EffectiveEndDate is null and d.EffectiveEndDate is null " +
                "where " +
                "1 = 1 " +
                "and AllocatorEvaluationHierarchyId =@coacherDepartmentIdd " +
                "and p.EffectiveEndDate is null" +
                ") finalScoreTbl " +
                "group by " +
                "PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", coacherId" +
                ", coacherEvaluationId" +
                ", coacherDepartmentName" +
                ", coacherFullName" +
                ", coacherPositionType" +
                ", employeeHierarchyId" +
                ", employeeId" +
                ", employeeDepartmentName" +
                ", employeeFullName" +
                ", coacherPositionType" +
                ", employeePositionType)tbl " +
                "where 1 = 1 " +
                "and PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt,PeriodDefinitoionId) " +
                "and employeeId=ISNULL(@employeeIdd,employeeId) " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",coacherId" +
                ",coacherEvaluationId" +
                ",coacherDepartmentName" +
                ",coacherFullName" +
                ",coacherPositionType" +
                ",employeeHierarchyId" +
                ",employeeDepartmentName" +
                ",employeeId" +
                ",employeeFullName" +
                ",employeePositionType" +
                ",ROUND(finalTaskScore,3)finalTaskScore" +
                ",ROUND(finalCompetencyScore,3)finalCompetencyScore" +
                ",ROUND(applyPercentToFinalTaskScore,3)applyPercentToFinalTaskScore" +
                ",ROUND(applyPercentToFinalCompetencyScore,3)applyPercentToFinalCompetencyScore" +
                ",ROUND(finalScoreDepartment,3)finalScoreDepartment " +
                "from(" +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId asc))  indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", coacherId" +
                ", coacherEvaluationId" +
                ", coacherDepartmentName" +
                ", coacherFullName" +
                ", coacherPositionType" +
                ", employeeHierarchyId" +
                ", employeeDepartmentName" +
                ", employeeId" +
                ", employeeFullName" +
                ", employeePositionType" +
                ", sum(finalTaskScore)finalTaskScore" +
                ", sum(finalCompetencyScore)finalCompetencyScore" +
                ", sum(applyPercentToFinalTaskScore)applyPercentToFinalTaskScore" +
                ", sum(applyPercentToFinalCompetencyScore)applyPercentToFinalCompetencyScore" +
                ", sum(applyPercentToFinalTaskScore) + sum(applyPercentToFinalCompetencyScore) finalScoreDepartment " +
                "from" +
                "(" +
                "select " +
                "fsc.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", p.PeopleId coacherId" +
                ", p.EvaluationHierarchyID coacherEvaluationId" +
                ", d.ShortName coacherDepartmentName" +
                ", CONCAT(p.FirstName, ' ', p.LastName)coacherFullName" +
                ", p.PositionType coacherPositionType" +
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
                "FinalScoreCalculation fsc join People p " +
                "on p.EvaluationHierarchyID = fsc.AllocatorEvaluationHierarchyId " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fsc.PeriodDefinitoionId " +
                "join evaluationHierarchies eh " +
                "on eh.EvaluationHierarchyId = fsc.AllocatorEvaluationHierarchyId and p.PeopleId = fsc.AllocatorPersonId " +
                "join Departments d " +
                "on eh.DepartmentId = d.DepartmentId and eh.EffectiveEndDate is null and d.EffectiveEndDate is null " +
                "where " +
                "1 = 1 " +
                "and AllocatorEvaluationHierarchyId =@coacherDepartmentIdd " +
                "and p.EffectiveEndDate is null " +
                "union " +
                "select " +
                "fscc.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",p.PeopleId coacherId" +
                ", p.EvaluationHierarchyID coacherEvaluationId" +
                ", d.ShortName coacherDepartmentName" +
                ", CONCAT(p.FirstName, ' ', p.LastName)coacherFullName" +
                ",p.PositionType coacherPositionType" +
                ", fscc.FinalScoreCompetencyCalculationId" +
                ",fscc.RecieverAllocationEvaluationHierarchyId employeeHierarchyId" +
                ", fscc.RecieverAllocationPersonId employeeId" +
                ",(select d2.ShortName from evaluationHierarchies eh2 join Departments d2 on eh2.DepartmentId = d2.DepartmentId " +
                "and eh2.EffectiveEndDate is null and d2.EffectiveEndDate is null " +
                "where " +
                "eh2.EvaluationHierarchyId = fscc.RecieverAllocationEvaluationHierarchyId)employeeDepartmentName" +
                ",(select CONCAT(p2.FirstName, ' ', p2.LastName) from People p2 " +
                "where " +
                "p2.EvaluationHierarchyID = fscc.RecieverAllocationEvaluationHierarchyId " +
                "and p2.PeopleId = fscc.RecieverAllocationPersonId " +
                "and p2.EffectiveEndDate is null)employeeFullName" +
                ",(select p2.PositionType from People p2 where p2.PeopleId = fscc.RecieverAllocationPersonId " +
                "and p2.EvaluationHierarchyID = fscc.RecieverAllocationEvaluationHierarchyId and p2.EffectiveEndDate is null)employeePositionType" +
                ",null finalTaskScore" +
                ",fscc.FinalCompetneciesScore finalCompetencyScore" +
                ",null ApplyPercentToFinalTaskScore" +
                ",fscc.ApplyPercentToFinalScore ApplyPercentToFinalCompetencyScore " +
                "from " +
                "FinalScoreCompetencyCalculation fscc join People p " +
                "on p.EvaluationHierarchyID = fscc.AllocatorEvaluationHierarchyId " +
                "join PeriodDefinitoion pd " +
                "on pd.PeriodDefinitoionId = fscc.PeriodDefinitoionId " +
                "join evaluationHierarchies eh " +
                "on eh.EvaluationHierarchyId = fscc.AllocatorEvaluationHierarchyId and p.PeopleId = fscc.AllocatorPersonId " +
                "join Departments d " +
                "on eh.DepartmentId = d.DepartmentId and eh.EffectiveEndDate is null and d.EffectiveEndDate is null " +
                "where " +
                "1 = 1 " +
                "and AllocatorEvaluationHierarchyId =@coacherDepartmentIdd " +
                "and p.EffectiveEndDate is null" +
                ") finalScoreTbl " +
                "group by " +
                "PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", coacherId" +
                ", coacherEvaluationId" +
                ", coacherDepartmentName" +
                ", coacherFullName" +
                ", coacherPositionType" +
                ", employeeHierarchyId" +
                ", employeeId" +
                ", employeeDepartmentName" +
                ", employeeFullName" +
                ", coacherPositionType" +
                ", employeePositionType)tbl " +
                "where 1 = 1 " +
                "and PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt,PeriodDefinitoionId) " +
                "and employeeId=ISNULL(@employeeIdd,employeeId) " +
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
                    coacherDepartmentIdd = coacherDepartmentId,
                    employeeIdd = employeeId,
                    periodDefinitionIdDTt = periodDefinitionId,
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    coacherDepartmentIdd = coacherDepartmentId,
                    employeeIdd = employeeId,
                    periodDefinitionIdDTt = periodDefinitionId,
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    coacherDepartmentIdd = coacherDepartmentId,
                    employeeIdd = employeeId,
                    periodDefinitionIdDTt = periodDefinitionId,
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                coacherDepartmentIdd = coacherDepartmentId,
                employeeIdd = employeeId,
                periodDefinitionIdDTt = periodDefinitionId,
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                coacherDepartmentIdd = coacherDepartmentId,
                employeeIdd = employeeId,
                periodDefinitionIdDTt = periodDefinitionId,
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
                finalizeCalculation.CocherId = coacherId;
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
