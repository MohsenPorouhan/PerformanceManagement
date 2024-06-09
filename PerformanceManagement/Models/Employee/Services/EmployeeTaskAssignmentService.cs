using Dapper;
using Microsoft.AspNetCore.Http;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PerformanceManagement.Models.Coacher.View;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class EmployeeTaskAssignmentService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public EmployeeTaskAssignmentService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> GetAssignedTaskList(DataTableParameter dataTableParameter, int personId, string departmentIdDT = null, string periodDefinitionIdDT = null)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "AllocatorFullName", "AllocatorDepartmentName", "RoleName", "TaskTitle", "TaskWeight", "EvalAcceptanceTitle" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            //int exactOrder = dataTableParameter.orderColumn + 1;
            //if (exactOrder == 9)
            //{
            //    exactOrder += 5;
            //}
            if (dataTableParameter.orderable == true)
            {
                order = "order by " + aColumns[dataTableParameter.orderColumn] + " " + dataTableParameter.orderDIR;
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

            string queryTotalResult = @"select 
                indexx
                ,PeopleId
                ,FullName
                ,PositionType
                ,SupervisorId
                ,EvaluationHierarchyId
                ,DepartmentId
                ,LongName
                ,ShortName
                ,AllocatorPersonId
                ,AllocatorFullName
                ,AllocatorEvaluationHierarchyId
                ,AllocatorDepartmentName
                ,AllocatorRoleId
                ,RoleName
                ,EvaluationId
                ,RecieverAllocationPersonId
                ,RecieverAllocationEvaluationHierarchyId
                ,TaskId
                ,TaskWeight
                ,TaskTitle
                ,TaskType
                ,ResourceType
                ,EvaluationAcceptanceStatusId
                ,EvalAcceptanceTitle
                ,PeriodDefinitoionId
                ,PeriodCode
                ,PeriodTitle 
                ,hasParticipant 
                ,participantConfirmation 
                from( 
                select 
                ROW_NUMBER() OVER(ORDER BY t.TaskId desc) As indexx
                , p.PeopleId
                , CONCAT(p.FirstName, ' ', p.LastName)FullName
                , p.PositionType
                , eh.SupervisorId
                , eh.EvaluationHierarchyId
                , d.DepartmentId
                , d.LongName
                , d.ShortName
                , e.AllocatorPersonId
                ,(select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId = e.AllocatorPersonId and p.EffectiveEndDate is null)AllocatorFullName
                , e.AllocatorEvaluationHierarchyId
                ,(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName
                , e.AllocatorRoleId
                , anr.Name RoleName
                , e.EvaluationId
                , e.RecieverAllocationPersonId
                , e.RecieverAllocationEvaluationHierarchyId
                , e.TaskId
                , e.TaskWeight
                , t.Title TaskTitle
                , t.Type TaskType
                , t.ResourceType
                , e.EvaluationAcceptanceStatusId
                , eas.Title EvalAcceptanceTitle
                , pd.PeriodDefinitoionId
                , pd.PeriodCode
                , pd.PeriodTitle 
                ,(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant 
                ,(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation 
                from 
                People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId 
                join Evaluation e on e.RecieverAllocationPersonId = p.PeopleId 
                and e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId 
                join Departments d on d.DepartmentId = eh.DepartmentId 
                join Task t on t.TaskId = e.TaskId 
                join AspNetRoles anr on anr.Id = e.AllocatorRoleId 
                join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId 
                join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = e.EvaluationAcceptanceStatusId 
                where 
                1 = 1 
                and eh.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and eh.EvaluationHierarchyId = ISNULL(@departmentIdd, eh.EvaluationHierarchyId) 
                and p.PeopleId = @personIdd 
                and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId)) as tbl where 1 = 1  ";

            string queryFilteredTotal = @"select 
                indexx
                ,PeopleId
                ,FullName
                ,PositionType
                ,SupervisorId
                ,EvaluationHierarchyId
                ,DepartmentId
                ,LongName
                ,ShortName
                ,AllocatorPersonId
                ,AllocatorFullName
                ,AllocatorEvaluationHierarchyId
                ,AllocatorDepartmentName
                ,AllocatorRoleId
                ,RoleName
                ,EvaluationId
                ,RecieverAllocationPersonId
                ,RecieverAllocationEvaluationHierarchyId
                ,TaskId
                ,TaskWeight
                ,TaskTitle
                ,TaskType
                ,ResourceType
                ,EvaluationAcceptanceStatusId
                ,EvalAcceptanceTitle
                ,PeriodDefinitoionId
                ,PeriodCode
                ,PeriodTitle
                ,hasParticipant
                ,participantConfirmation
                from(
                select
                ROW_NUMBER() OVER(ORDER BY t.TaskId desc) As indexx
                , p.PeopleId
                , CONCAT(p.FirstName, ' ', p.LastName)FullName
                , p.PositionType
                , eh.SupervisorId
                , eh.EvaluationHierarchyId
                , d.DepartmentId
                , d.LongName
                , d.ShortName
                , e.AllocatorPersonId
                , (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = e.AllocatorEvaluationHierarchyId and p.PeopleId = e.AllocatorPersonId and p.EffectiveEndDate is null)AllocatorFullName
                , e.AllocatorEvaluationHierarchyId
                ,(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName
                , e.AllocatorRoleId
                , anr.Name RoleName
                , e.EvaluationId
                , e.RecieverAllocationPersonId
                , e.RecieverAllocationEvaluationHierarchyId
                , e.TaskId
                , e.TaskWeight
                , t.Title TaskTitle
                , t.Type TaskType
                , t.ResourceType
                , e.EvaluationAcceptanceStatusId
                , eas.Title EvalAcceptanceTitle
                , pd.PeriodDefinitoionId
                , pd.PeriodCode
                , pd.PeriodTitle
                ,(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate = (select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant
                ,(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate = (select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation
                 from
                People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId
                join Evaluation e on e.RecieverAllocationPersonId = p.PeopleId
                and e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId
                join Departments d on d.DepartmentId = eh.DepartmentId
                join Task t on t.TaskId = e.TaskId
                join AspNetRoles anr on anr.Id = e.AllocatorRoleId
                join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId
                join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = e.EvaluationAcceptanceStatusId
                where
                1 = 1
                and eh.EffectiveEndDate is null
                and p.EffectiveEndDate is null
                and d.EffectiveEndDate is null
                and eh.EvaluationHierarchyId = ISNULL(@departmentIdd, eh.EvaluationHierarchyId)
                and p.PeopleId = @personIdd
                and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId)) as tbl where 1 = 1  " +
                where;

            string sQuery = @"select 
                indexx
                ,PeopleId
                ,FullName
                ,PositionType
                ,SupervisorId
                ,EvaluationHierarchyId
                ,DepartmentId
                ,LongName
                ,ShortName
                ,AllocatorPersonId
                ,AllocatorFullName
                ,AllocatorEvaluationHierarchyId
                ,AllocatorDepartmentName
                ,AllocatorRoleId
                ,RoleName
                ,EvaluationId
                ,RecieverAllocationPersonId
                ,RecieverAllocationEvaluationHierarchyId
                ,TaskId
                ,TaskWeight
                ,TaskTitle
                ,TaskType
                ,ResourceType
                ,EvaluationAcceptanceStatusId
                ,EvalAcceptanceTitle
                ,PeriodDefinitoionId
                ,PeriodCode
                ,PeriodTitle 
                ,hasParticipant 
                ,participantConfirmation 
				,RefutationCause
                from( 
                select 
                ROW_NUMBER() OVER(ORDER BY t.TaskId desc) As indexx
                , p.PeopleId
                , CONCAT(p.FirstName, ' ', p.LastName)FullName
                , p.PositionType
                , eh.SupervisorId
                , eh.EvaluationHierarchyId
                , d.DepartmentId
                , d.LongName
                , d.ShortName
                , e.AllocatorPersonId
                ,(select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId = e.AllocatorPersonId and p.EffectiveEndDate is null)AllocatorFullName
                , e.AllocatorEvaluationHierarchyId
                ,(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName
                , e.AllocatorRoleId
                , anr.Name RoleName
                , e.EvaluationId
                , e.RecieverAllocationPersonId
                , e.RecieverAllocationEvaluationHierarchyId
                , e.TaskId
                , e.TaskWeight
                , t.Title TaskTitle
                , t.Type TaskType
                , t.ResourceType
                , e.EvaluationAcceptanceStatusId
                , eas.Title EvalAcceptanceTitle
                , pd.PeriodDefinitoionId
                , pd.PeriodCode
                , pd.PeriodTitle 
                ,(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant 
                ,(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation 
				,e.RefutationCause
                from 
                People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId 
                join Evaluation e on e.RecieverAllocationPersonId = p.PeopleId 
                and e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId 
                join Departments d on d.DepartmentId = eh.DepartmentId 
                join Task t on t.TaskId = e.TaskId 
                join AspNetRoles anr on anr.Id = e.AllocatorRoleId 
                join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId 
                join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = e.EvaluationAcceptanceStatusId 
                where 
                1 = 1 
                and eh.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and eh.EvaluationHierarchyId = ISNULL(@departmentIdd, eh.EvaluationHierarchyId) 
                and p.PeopleId = @personIdd 
                and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId)) as tbl where 1 = 1  " +
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
        public IEnumerable<object> GetDepartmentList(int personId)
        {
            var sQuery = "select " +
                "p.PeopleId" +
                ",CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ",eh.EvaluationHierarchyId" +
                ",d.ShortName " +
                ",p.PositionType " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "where " +
                "1 = 1 " +
                "and p.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.PeopleId = @personIdd";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { personIdd = personId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public int PerformTaskConfirmation(List<PerformConfirmationView> evaluation, int personId, string roleId)
        {
            List<Notification> notificationList = new List<Notification>();

            foreach (var evaluationItem in evaluation)
            {
                var fetchEval = appDbContext.Evaluation.Where(c => c.EvaluationId == evaluationItem.EvaluationId2).SingleOrDefault();
                if (fetchEval != null)
                {
                    Notification notification = new Notification();
                    if (evaluationItem.EvaluationAcceptanceStatusId == 1)
                    {
                        fetchEval.EvaluationAcceptanceStatusId = 1;
                        fetchEval.RefutationCause = null;
                        notification.Title = "تایید";
                    }
                    else if (evaluationItem.EvaluationAcceptanceStatusId == 4)
                    {
                        fetchEval.EvaluationAcceptanceStatusId = 4;
                        fetchEval.RefutationCause = evaluationItem.RefutationCause;
                        notification.Title = "عدم تایید";
                    }
                    fetchEval.LastUpdatedBy = personId;
                    fetchEval.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(fetchEval);
                    notification.AllocatorHierarchyId = fetchEval.RecieverAllocationEvaluationHierarchyId;
                    notification.AllocatorPersonId = personId;
                    notification.AllocatorRoleId = roleId;
                    notification.ReceiverPersonId = fetchEval.AllocatorPersonId;
                    notification.ReceiverHierarchtId = fetchEval.AllocatorEvaluationHierarchyId;
                    notification.ReceiverRoleId = fetchEval.AllocatorRoleId;
                    notification.NotificationTitleId = 1;
                    notification.Visibility = false;
                    notification.PeriodDefinitionId = fetchEval.PeriodDefinitoionId;
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
            var roleId2 = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            ShareService shareService = new ShareService(appDbContext, null);
            int? scoreWayRealization = null;

            int numberOFEval = appDbContext.Evaluation.Where(c => c.RecieverAllocationPersonId == listOfScores.FirstOrDefault().RecieverAllocationPersonId &&
              c.RecieverAllocationEvaluationHierarchyId == listOfScores.FirstOrDefault().RecieverAllocationEvaluationHierarchyId
              && c.PeriodDefinitoionId == shareService.GetMaxPeriodDefinitionId() && (c.EvaluationAcceptanceStatusId==1 || c.EvaluationAcceptanceStatusId==2)
              //&& (c.AllocatorEvaluationHierarchyId == listOfScores.FirstOrDefault().AllocatorEvaluationHierarchyId || c.AllocatorRoleId == roleId2)
              ).Count();
            if (numberOFEval >= listOfScores.Count)
            {
                foreach (var evaluationItem in listOfScores)
                {
                    var scoreWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == evaluationItem.PeriodDefinitoionId).SingleOrDefault();

                    scoreWayRealization = scoreWay.WeightWayScore;


                    //There is always one item in this loop 
                    foreach (var evaluationScoreItem in evaluationItem.EvaluationScores)
                    {
                        //var result = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationItem.EvaluationId && c.CoacherLevel == 0);
                        //if (result.Count() != 0)
                        //{
                        //    dictionary.Add("result", "اطلاعات مورد نظر قبلا ثبت شده است.");
                        //    return dictionary;
                        //}
                        EvaluationScore evalScoreResult = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationItem.EvaluationId && c.CoacherLevel == 0).SingleOrDefault();

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
                            evaluationScore.CoacherLevel = 0;
                            evaluationScore.CoacherId = personId;
                            evaluationScore.EvaluationId = evaluationItem.EvaluationId;
                            evaluationScore.RoleId = roleId;
                            evaluationScore.CreatedBy = personId;
                            evaluationScore.CreatedDate = DateTime.Now;
                            appDbContext.EvaluationScore.Add(evaluationScore);
                        }
                    }
                    if (evaluationItem.CriteriaWeights.Count == appDbContext.Criteria.Where(c => c.TaskId == evaluationItem.TaskId).Count())
                    {
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
                                    CriteriaWeightScore criteriaWeightScoreResult = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeight.CriteriaWeightId && c.CoacherLevel == 0).SingleOrDefault();
                                    if (criteriaWeightScoreResult != null && criteriaWeightScoreResult.Score != criteriaWeightScoreItem.Score)
                                    {
                                        criteriaWeightScoreResult.Score = criteriaWeightScoreItem.Score;
                                        criteriaWeightScoreResult.LastUpdatedBy = personId;
                                        criteriaWeightScoreResult.LastUpdatedDate = DateTime.Now;
                                        appDbContext.Update(criteriaWeightScoreResult);
                                    }
                                    else if (criteriaWeightScoreResult == null)
                                    {
                                        int criteriaWeightId = criteriaWeight.CriteriaWeightId;
                                        CriteriaWeightScore criteriaWeightScore = new CriteriaWeightScore();
                                        criteriaWeightScore.Score = criteriaWeightScoreItem.Score;
                                        criteriaWeightScore.CoacherLevel = 0;
                                        criteriaWeightScore.CoacherId = personId;
                                        criteriaWeightScore.CriteriaWeightId = criteriaWeightId;
                                        criteriaWeightScore.RoleId = roleId;
                                        criteriaWeightScore.CreatedBy = personId;
                                        criteriaWeightScore.CreatedDate = DateTime.Now;
                                        appDbContext.CriteriaWeightScore.Add(criteriaWeightScore);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        dictionary.Add("malicious", "true");
                        return dictionary;
                    }
                }
            }
            else
            {
                dictionary.Add("malicious", "true");
                return dictionary;
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
                ",sum(score1)score1" +
                ",sum(score2)score2" +
                ",sum(score3)score3" +
                ",sum(score4)score4" +
                ",sum(participantScore)participantScore" +
                ",sum(selfScore)selfScore" +
                ",sum(planningAdminScore)planningAdminScore " +
                "from( " +
                "select " +
                "cw.EvaluationId" +
                ", cws.CoacherId" +
                ", cws.CoacherLevel" +
                ", cws.CriteriaWeightId" +
                ", cw.CriteriaId" +
                ", cws.RoleId" +
                ", cws.Score as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel = 1 " +
                "union all " +
                "select " +
                "cw.EvaluationId" +
                ", cws.CoacherId" +
                ", cws.CoacherLevel" +
                ", cws.CriteriaWeightId" +
                ", cw.CriteriaId" +
                ", cws.RoleId" +
                ", null as score1" +
                ", cws.Score as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel = 2 " +
                "union all " +
                "select " +
                "cw.EvaluationId" +
                ", cws.CoacherId" +
                ", cws.CoacherLevel" +
                ", cws.CriteriaWeightId" +
                ", cw.CriteriaId" +
                ", cws.RoleId" +
                ", null as score1" +
                ", null as score2" +
                ", cws.Score as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel = 3 " +
                "union all " +
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
                ", cws.Score as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel = 4 " +
                "union all " +
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
                "union all " +
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
                ", null as participantScore" +
                ", cws.Score as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel = 0 " +
                "union all " +
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
                ", null as participantScore" +
                ", null as selfScore" +
                ", cws.Score as planningAdminScore " +
                "from " +
                "CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId " +
                "where " +
                "1 = 1 " +
                "and cw.EvaluationId = @evaluationIdd " +
                "and cws.CoacherLevel is null " +
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
                ",sum(score1)score1" +
                ",sum(score2)score2" +
                ",sum(score3)score3" +
                ",sum(score4)score4" +
                ",sum(participantScore)participantScore" +
                ",sum(selfScore)selfScore" +
                ",sum(planningAdminScore)planningAdminScore " +
                "from( " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", es.Score as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 1 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", es.Score as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 2 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", es.Score as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 3 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", es.Score as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 4 " +
                "union all " +
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
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", es.Score as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 0 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", es.Score as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where 1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel is null " +
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
