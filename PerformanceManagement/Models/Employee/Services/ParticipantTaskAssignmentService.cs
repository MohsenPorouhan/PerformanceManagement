using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class ParticipantTaskAssignmentService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ParticipantTaskAssignmentService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> GetAssignedTaskList(DataTableParameter dataTableParameter, int personId, string departmentIdDT = null, string periodDefinitionIdDT = null)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "AllocatorFullName", "AllocatorDepartmentName", "TaskTitle", "EvalAcceptanceTitle" };
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
                ",PeopleId" +
                ",FullName" +
                ",PositionType" +
                ",SupervisorId" +
                ",EvaluationHierarchyId" +
                ",DepartmentId" +
                ", LongName" +
                ", ShortName" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ", AllocatorEvaluationHierarchyId" +
                ", AllocatorDepartmentName" +
                ", AllocatorRoleId" +
                ", RoleName" +
                ", EvaluationId" +
                ", RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", TaskId" +
                ", TaskWeight" +
                ", TaskTitle" +
                ", TaskType" +
                ", ResourceType" +
                ", EvaluationAcceptanceStatusId" +
                ", EvalAcceptanceTitle" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", ParticipantId" +
                ", ParticipantEvaluationHierarchyId" +
                ", Confirmation" +
                ", Score " +
                "from(" +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY t.TaskId desc) As indexx" +
                ", p.PeopleId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", p.PositionType" +
                ", eh.SupervisorId" +
                ", eh.EvaluationHierarchyId" +
                ", d.DepartmentId" +
                ", d.LongName" +
                ", d.ShortName" +
                ", e.AllocatorPersonId" +
                ", e.AllocatorEvaluationHierarchyId" +
                ", e.AllocatorRoleId" +
                ", anr.Name RoleName" +
                ", e.EvaluationId" +
                ", e.RecieverAllocationPersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = e.AllocatorEvaluationHierarchyId and p.PeopleId = e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName" +
                ",e.TaskId" +
                ",e.TaskWeight" +
                ",t.Title TaskTitle" +
                ",t.Type TaskType" +
                ",t.ResourceType" +
                ",e.EvaluationAcceptanceStatusId" +
                ",eas.Title EvalAcceptanceTitle" +
                ", pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ep.ParticipantId" +
                ",ep.ParticipantEvaluationHierarchyId" +
                ",ep.Confirmation" +
                ",ep.Score " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join Evaluation e on e.RecieverAllocationPersonId = p.PeopleId " +
                "and e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "join task t on t.TaskId = e.TaskId " +
                "join AspNetRoles anr on anr.Id = e.AllocatorRoleId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = e.EvaluationAcceptanceStatusId " +
                "join EvaluationParticipant ep on ep.EvaluationId = e.EvaluationId " +
                "where " +
                "1 = 1 " +
                //"and(ep.Confirmation != 0 or  ep.Confirmation is null) " +
                "and eh.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and ep.ParticipantId = @personIdd " +
                "and ep.ParticipantEvaluationHierarchyId = ISNULL(@departmentIdd, ep.ParticipantEvaluationHierarchyId) " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")as tbl where 1 = 1  ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeopleId" +
                ",FullName" +
                ",PositionType" +
                ",SupervisorId" +
                ",EvaluationHierarchyId" +
                ",DepartmentId" +
                ", LongName" +
                ", ShortName" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ", AllocatorEvaluationHierarchyId" +
                ", AllocatorDepartmentName" +
                ", AllocatorRoleId" +
                ", RoleName" +
                ", EvaluationId" +
                ", RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", TaskId" +
                ", TaskWeight" +
                ", TaskTitle" +
                ", TaskType" +
                ", ResourceType" +
                ", EvaluationAcceptanceStatusId" +
                ", EvalAcceptanceTitle" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", ParticipantId" +
                ", ParticipantEvaluationHierarchyId" +
                ", Confirmation" +
                ", Score " +
                "from(" +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY t.TaskId desc) As indexx" +
                ", p.PeopleId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", p.PositionType" +
                ", eh.SupervisorId" +
                ", eh.EvaluationHierarchyId" +
                ", d.DepartmentId" +
                ", d.LongName" +
                ", d.ShortName" +
                ", e.AllocatorPersonId" +
                ", e.AllocatorEvaluationHierarchyId" +
                ", e.AllocatorRoleId" +
                ", anr.Name RoleName" +
                ", e.EvaluationId" +
                ", e.RecieverAllocationPersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = e.AllocatorEvaluationHierarchyId and p.PeopleId = e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName" +
                ",e.TaskId" +
                ",e.TaskWeight" +
                ",t.Title TaskTitle" +
                ",t.Type TaskType" +
                ",t.ResourceType" +
                ",e.EvaluationAcceptanceStatusId" +
                ",eas.Title EvalAcceptanceTitle" +
                ", pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ep.ParticipantId" +
                ",ep.ParticipantEvaluationHierarchyId" +
                ",ep.Confirmation" +
                ",ep.Score " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join Evaluation e on e.RecieverAllocationPersonId = p.PeopleId " +
                "and e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "join task t on t.TaskId = e.TaskId " +
                "join AspNetRoles anr on anr.Id = e.AllocatorRoleId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = e.EvaluationAcceptanceStatusId " +
                "join EvaluationParticipant ep on ep.EvaluationId = e.EvaluationId " +
                "where " +
                "1 = 1 " +
                //"and(ep.Confirmation != 0 or  ep.Confirmation is null) " +
                "and eh.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and ep.ParticipantId = @personIdd " +
                "and ep.ParticipantEvaluationHierarchyId = ISNULL(@departmentIdd, ep.ParticipantEvaluationHierarchyId) " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")as tbl where 1 = 1  " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeopleId" +
                ",FullName" +
                ",PositionType" +
                ",SupervisorId" +
                ",EvaluationHierarchyId" +
                ",DepartmentId" +
                ", LongName" +
                ", ShortName" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ", AllocatorEvaluationHierarchyId" +
                ", AllocatorDepartmentName" +
                ", AllocatorRoleId" +
                ", RoleName" +
                ", EvaluationId" +
                ", RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", TaskId" +
                ", TaskWeight" +
                ", TaskTitle" +
                ", TaskType" +
                ", ResourceType" +
                ", EvaluationAcceptanceStatusId" +
                ", EvalAcceptanceTitle" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", ParticipantId" +
                ", ParticipantEvaluationHierarchyId" +
                ", Confirmation" +
                ", Score " +
                "from(" +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY t.TaskId desc) As indexx" +
                ", p.PeopleId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", p.PositionType" +
                ", eh.SupervisorId" +
                ", eh.EvaluationHierarchyId" +
                ", d.DepartmentId" +
                ", d.LongName" +
                ", d.ShortName" +
                ", e.AllocatorPersonId" +
                ", e.AllocatorEvaluationHierarchyId" +
                ", e.AllocatorRoleId" +
                ", anr.Name RoleName" +
                ", e.EvaluationId" +
                ", e.RecieverAllocationPersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = e.AllocatorEvaluationHierarchyId and p.PeopleId = e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName" +
                ",e.TaskId" +
                ",e.TaskWeight" +
                ",t.Title TaskTitle" +
                ",t.Type TaskType" +
                ",t.ResourceType" +
                ",e.EvaluationAcceptanceStatusId" +
                ",eas.Title EvalAcceptanceTitle" +
                ", pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ep.ParticipantId" +
                ",ep.ParticipantEvaluationHierarchyId" +
                ",ep.Confirmation" +
                ",ep.Score " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join Evaluation e on e.RecieverAllocationPersonId = p.PeopleId " +
                "and e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "join task t on t.TaskId = e.TaskId " +
                "join AspNetRoles anr on anr.Id = e.AllocatorRoleId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = e.EvaluationAcceptanceStatusId " +
                "join EvaluationParticipant ep on ep.EvaluationId = e.EvaluationId " +
                "where " +
                "1 = 1 " +
                //"and(ep.Confirmation != 0 or  ep.Confirmation is null) " +
                "and eh.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and ep.ParticipantId = @personIdd " +
                "and ep.ParticipantEvaluationHierarchyId = ISNULL(@departmentIdd, ep.ParticipantEvaluationHierarchyId) " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")as tbl where 1 = 1  " +
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
                    departmentIdd = departmentIdDT,
                    personIdd = personId,
                    periodDefinitionIdd = periodDefinitionIdDT
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    departmentIdd = departmentIdDT,
                    personIdd = personId,
                    periodDefinitionIdd = periodDefinitionIdDT
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    departmentIdd = departmentIdDT,
                    personIdd = personId,
                    periodDefinitionIdd = periodDefinitionIdDT
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                departmentIdd = departmentIdDT,
                personIdd = personId,
                periodDefinitionIdd = periodDefinitionIdDT
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                departmentIdd = departmentIdDT,
                personIdd = personId,
                periodDefinitionIdd = periodDefinitionIdDT
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }

        public int ParticipantPerformTaskConfirmation(PerformConfirmationView evaluation, int personId, string roleId)
        {
            List<Notification> notificationList = new List<Notification>();

            foreach (var evaluationItem in evaluation.EvaluationId)
            {
                var fetchEval = appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluationItem).Include(c => c.Evaluation).SingleOrDefault();
                if (fetchEval != null)
                {
                    Notification notification = new Notification();
                    if (evaluation.EvaluationAcceptanceStatusId == 1)
                    {
                        fetchEval.Confirmation = true;
                        notification.Title = "پذیرش";
                    }
                    else if (evaluation.EvaluationAcceptanceStatusId == 0)
                    {
                        fetchEval.Confirmation = false;
                        notification.Title = "عدم پذیرش";
                    }
                    fetchEval.ResponseDate = DateTime.Now;
                    fetchEval.LastUpdatedBy = personId;
                    fetchEval.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(fetchEval);
                    notification.AllocatorHierarchyId = fetchEval.Evaluation.RecieverAllocationEvaluationHierarchyId;
                    notification.AllocatorPersonId = personId;
                    notification.AllocatorRoleId = roleId;
                    notification.ReceiverPersonId = fetchEval.Evaluation.AllocatorPersonId;
                    notification.ReceiverHierarchtId = fetchEval.Evaluation.AllocatorEvaluationHierarchyId;
                    notification.ReceiverRoleId = fetchEval.Evaluation.AllocatorRoleId;
                    notification.NotificationTitleId = 3;
                    notification.Visibility = false;
                    notification.PeriodDefinitionId = fetchEval.Evaluation.PeriodDefinitoionId;
                    notification.EvaluationId = fetchEval.EvaluationId;
                    notification.CreatedDate = DateTime.Now;
                    notificationList.Add(notification);
                }
            }
            appDbContext.AddRange(notificationList);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public Dictionary<string, object> ScoreAssignment(List<Evaluation> listOfScores, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            //int? taskScoreSummation = 0;
            //var criteriaScoreSummation = 0;
            int? scoreWayRealization = null;

            foreach (var evaluationItem in listOfScores)
            {
                var scoreWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == evaluationItem.PeriodDefinitoionId).SingleOrDefault();

                scoreWayRealization = scoreWay.WeightWayScore;


                //There is always one item in this loop 
                foreach (var evaluationScoreItem in evaluationItem.EvaluationScores)
                {
                    var result = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationItem.EvaluationId && c.CoacherLevel == evaluationScoreItem.CoacherLevel);
                    if (result.Count() != 0)
                    {
                        dictionary.Add("result", "اطلاعات مورد نظر قبلا ثبت شده است.");
                        return dictionary;
                    }
                    if (scoreWay.WeightWayScore == 1)//percent
                    {
                        if (evaluationScoreItem.Score == 0)
                        {
                            dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                            return dictionary;
                        }
                    }
                    else if ((evaluationScoreItem.Score < NumberScaleMinMax(scoreWay, 0, 3) || evaluationScoreItem.Score > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2)//likert
                    {
                        return NumberScaleMinMax(dictionary, scoreWay, 3);
                    }
                    EvaluationScore evaluationScore = new EvaluationScore();
                    evaluationScore.Score = evaluationScoreItem.Score;
                    evaluationScore.CoacherLevel = -1;
                    evaluationScore.CoacherId = personId;
                    evaluationScore.EvaluationId = evaluationItem.EvaluationId;
                    evaluationScore.RoleId = roleId;
                    evaluationScore.CreatedBy = personId;
                    evaluationScore.CreatedDate = DateTime.Now;
                    //taskScoreSummation += evaluationScoreItem.Score;
                    appDbContext.EvaluationScore.Add(evaluationScore);
                }
                foreach (var criteriaWeightItem in evaluationItem.CriteriaWeights)
                {
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
                        var criteriaWeight = appDbContext.CriteriaWeight.Where(c => c.CriteriaId == criteriaWeightItem.CriteriaId && c.EvaluationId == evaluationItem.EvaluationId).SingleOrDefault();
                        if (criteriaWeight == null)
                        {
                            dictionary.Add("giveWeight", "ابتدا وظایف مورد نظر توسط مربی مستقیم وزن دهی باید گردد و سپس  امکان نمره دهی فراهم می گردد.");
                            return dictionary;
                        }
                        else
                        {
                            int criteriaWeightId = criteriaWeight.CriteriaWeightId;
                            CriteriaWeightScore criteriaWeightScore = new CriteriaWeightScore();
                            criteriaWeightScore.Score = criteriaWeightScoreItem.Score;
                            criteriaWeightScore.CoacherLevel = -1;
                            criteriaWeightScore.CoacherId = personId;
                            criteriaWeightScore.CriteriaWeightId = criteriaWeightId;
                            criteriaWeightScore.RoleId = roleId;
                            criteriaWeightScore.CreatedBy = personId;
                            criteriaWeightScore.CreatedDate = DateTime.Now;
                            //criteriaScoreSummation += criteriaWeightScoreItem.Score;
                            appDbContext.CriteriaWeightScore.Add(criteriaWeightScore);
                        }
                    }
                }
            }
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
        public List<ScoreView> GetCriteriaScore(int evaluationId)
        {
            var sQuery = "select " +
                "tbl.EvaluationId" +
                ",t.Title" +
                ",tbl.CriteriaWeightId" +
                ",tbl.CriteriaId" +
                ",c.Title CriteriaTitle" +
                ",sum(participantScore)participantScore " +
                "from( " +
                "select " +
                "cw.EvaluationId" +
                ", cws.CoacherId" +
                ", cws.CoacherLevel" +
                ", cws.CriteriaWeightId" +
                ", cw.CriteriaId" +
                ", cws.RoleId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", cws.Score as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel = -1 " +
                ") as tbl join Evaluation e on e.EvaluationId = tbl.EvaluationId " +
                "join Task t on t.TaskId = e.TaskId " +
                "join Criteria c on c.CriteriaId = tbl.CriteriaId " +
                "group by tbl.EvaluationId,tbl.CriteriaWeightId,t.Title,tbl.CriteriaId,c.Title";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<ScoreView> scoreView = null;

            scoreView = conn.Query<ScoreView>(sQuery, new { evaluationIdd = evaluationId }).ToList();

            //conn.Close();
            conn.Dispose();
            return scoreView;
        }
        public List<ScoreView> GetTaskScore(int evaluationId)
        {
            var sQuery = "select " +
                "tbl.EvaluationId" +
                ",t.Title" +
                ",sum(participantScore)participantScore " +
                "from( " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", es.Score as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = -1 " +
                ") as tbl join Evaluation e on e.EvaluationId = tbl.EvaluationId " +
                "join Task t on t.TaskId = e.TaskId " +
                "group by tbl.EvaluationId,t.Title";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<ScoreView> scoreView = null;

            scoreView = conn.Query<ScoreView>(sQuery, new { evaluationIdd = evaluationId }).ToList();

            //conn.Close();
            conn.Dispose();
            return scoreView;
        }
    }
}
