using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class ParticipantCompetencyAssignmentService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ParticipantCompetencyAssignmentService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> GetAssignedCompetencyList(DataTableParameter dataTableParameter, int personId, string departmentIdDT = null, string periodDefinitionIdDT = null)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "AllocatorFullName", "AllocatorDepartmentName", "CompetencyTitle", "EvalAcceptanceTitle" };
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
                ",LongName" +
                ",ShortName" +
                ",AllocatorPersonId" +
                ",AllocatorFullName" +
                ",AllocatorEvaluationBehaviouralHierarchyId" +
                ",AllocatorDepartmentName" +
                ",AllocatorRoleId" +
                ",RoleName" +
                ",EvaluationBehaviouralCompetencyId" +
                ",RecieverAllocationPersonId" +
                ",RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ",BehaviouralCompetencyId" +
                ",BehaviouralCompetencyWeight" +
                ",CompetencyTitle" +
                ",EvaluationAcceptanceStatusId" +
                ",EvalAcceptanceTitle" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",ParticipantId" +
                ",ParticipantEvaluationBehaviouralHierarchyId" +
                ",Confirmation" +
                ",Score " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY bc.BehaviouralCompetencyId desc) As indexx" +
                ", p.PeopleId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", p.PositionType" +
                ", eh.SupervisorId" +
                ", eh.EvaluationHierarchyId" +
                ", d.DepartmentId" +
                ", d.LongName" +
                ", d.ShortName" +
                ", ebc.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = ebc.AllocatorEvaluationBehaviouralHierarchyId and p.PeopleId = ebc.AllocatorPersonId and p.EffectiveEndDate is null)AllocatorFullName" +
                ", ebc.AllocatorEvaluationBehaviouralHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = ebc.AllocatorEvaluationBehaviouralHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName" +
                ", ebc.AllocatorRoleId" +
                ", anr.Name RoleName" +
                ", ebc.EvaluationBehaviouralCompetencyId" +
                ", ebc.RecieverAllocationPersonId" +
                ", ebc.RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", ebc.BehaviouralCompetencyId" +
                ", ebc.BehaviouralCompetencyWeight" +
                ", bc.Title CompetencyTitle" +
                ", ebc.EvaluationAcceptanceStatusId" +
                ", eas.Title EvalAcceptanceTitle" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ",ebp.ParticipantId" +
                ",ebp.ParticipantEvaluationBehaviouralHierarchyId" +
                ",ebp.Confirmation" +
                ",ebp.Score " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationPersonId = p.PeopleId " +
                "and ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId " +
                "join AspNetRoles anr on anr.Id = ebc.AllocatorRoleId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = ebc.PeriodDefinitoionId " +
                "join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = ebc.EvaluationAcceptanceStatusId " +
                "join EvaluationBehaviouralParticipant ebp on ebp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                //"and(ebp.Confirmation != 0 or  ebp.Confirmation is null) " +
                "and eh.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and ebp.ParticipantEvaluationBehaviouralHierarchyId = ISNULL(@departmentIdd, ebp.ParticipantEvaluationBehaviouralHierarchyId) " +
                "and ebp.ParticipantId = @personIdd " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ") as tbl where 1 = 1  ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeopleId" +
                ",FullName" +
                ",PositionType" +
                ",SupervisorId" +
                ",EvaluationHierarchyId" +
                ",DepartmentId" +
                ",LongName" +
                ",ShortName" +
                ",AllocatorPersonId" +
                ",AllocatorFullName" +
                ",AllocatorEvaluationBehaviouralHierarchyId" +
                ",AllocatorDepartmentName" +
                ",AllocatorRoleId" +
                ",RoleName" +
                ",EvaluationBehaviouralCompetencyId" +
                ",RecieverAllocationPersonId" +
                ",RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ",BehaviouralCompetencyId" +
                ",BehaviouralCompetencyWeight" +
                ",CompetencyTitle" +
                ",EvaluationAcceptanceStatusId" +
                ",EvalAcceptanceTitle" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",ParticipantId" +
                ",ParticipantEvaluationBehaviouralHierarchyId" +
                ",Confirmation" +
                ",Score " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY bc.BehaviouralCompetencyId desc) As indexx" +
                ", p.PeopleId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", p.PositionType" +
                ", eh.SupervisorId" +
                ", eh.EvaluationHierarchyId" +
                ", d.DepartmentId" +
                ", d.LongName" +
                ", d.ShortName" +
                ", ebc.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = ebc.AllocatorEvaluationBehaviouralHierarchyId and p.PeopleId = ebc.AllocatorPersonId and p.EffectiveEndDate is null)AllocatorFullName" +
                ", ebc.AllocatorEvaluationBehaviouralHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = ebc.AllocatorEvaluationBehaviouralHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName" +
                ", ebc.AllocatorRoleId" +
                ", anr.Name RoleName" +
                ", ebc.EvaluationBehaviouralCompetencyId" +
                ", ebc.RecieverAllocationPersonId" +
                ", ebc.RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", ebc.BehaviouralCompetencyId" +
                ", ebc.BehaviouralCompetencyWeight" +
                ", bc.Title CompetencyTitle" +
                ", ebc.EvaluationAcceptanceStatusId" +
                ", eas.Title EvalAcceptanceTitle" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ",ebp.ParticipantId" +
                ",ebp.ParticipantEvaluationBehaviouralHierarchyId" +
                ",ebp.Confirmation" +
                ",ebp.Score " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationPersonId = p.PeopleId " +
                "and ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId " +
                "join AspNetRoles anr on anr.Id = ebc.AllocatorRoleId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = ebc.PeriodDefinitoionId " +
                "join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = ebc.EvaluationAcceptanceStatusId " +
                "join EvaluationBehaviouralParticipant ebp on ebp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                //"and(ebp.Confirmation != 0 or  ebp.Confirmation is null) " +
                "and eh.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and ebp.ParticipantEvaluationBehaviouralHierarchyId = ISNULL(@departmentIdd, ebp.ParticipantEvaluationBehaviouralHierarchyId) " +
                "and ebp.ParticipantId = @personIdd " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ") as tbl where 1 = 1  " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeopleId" +
                ",FullName" +
                ",PositionType" +
                ",SupervisorId" +
                ",EvaluationHierarchyId" +
                ",DepartmentId" +
                ",LongName" +
                ",ShortName" +
                ",AllocatorPersonId" +
                ",AllocatorFullName" +
                ",AllocatorEvaluationBehaviouralHierarchyId" +
                ",AllocatorDepartmentName" +
                ",AllocatorRoleId" +
                ",RoleName" +
                ",EvaluationBehaviouralCompetencyId" +
                ",RecieverAllocationPersonId" +
                ",RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ",BehaviouralCompetencyId" +
                ",BehaviouralCompetencyWeight" +
                ",CompetencyTitle" +
                ",EvaluationAcceptanceStatusId" +
                ",EvalAcceptanceTitle" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",ParticipantId" +
                ",ParticipantEvaluationBehaviouralHierarchyId" +
                ",Confirmation" +
                ",Score " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY bc.BehaviouralCompetencyId desc) As indexx" +
                ", p.PeopleId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", p.PositionType" +
                ", eh.SupervisorId" +
                ", eh.EvaluationHierarchyId" +
                ", d.DepartmentId" +
                ", d.LongName" +
                ", d.ShortName" +
                ", ebc.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.EvaluationHierarchyID = ebc.AllocatorEvaluationBehaviouralHierarchyId and p.PeopleId = ebc.AllocatorPersonId and p.EffectiveEndDate is null)AllocatorFullName" +
                ", ebc.AllocatorEvaluationBehaviouralHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId and eh.EvaluationHierarchyId = ebc.AllocatorEvaluationBehaviouralHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)AllocatorDepartmentName" +
                ", ebc.AllocatorRoleId" +
                ", anr.Name RoleName" +
                ", ebc.EvaluationBehaviouralCompetencyId" +
                ", ebc.RecieverAllocationPersonId" +
                ", ebc.RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", ebc.BehaviouralCompetencyId" +
                ", ebc.BehaviouralCompetencyWeight" +
                ", bc.Title CompetencyTitle" +
                ", ebc.EvaluationAcceptanceStatusId" +
                ", eas.Title EvalAcceptanceTitle" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ",ebp.ParticipantId" +
                ",ebp.ParticipantEvaluationBehaviouralHierarchyId" +
                ",ebp.Confirmation" +
                ",ebp.Score " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationPersonId = p.PeopleId " +
                "and ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on d.DepartmentId = eh.DepartmentId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId " +
                "join AspNetRoles anr on anr.Id = ebc.AllocatorRoleId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = ebc.PeriodDefinitoionId " +
                "join EvaluationAcceptanceStatus eas on eas.EvaluationAcceptanceStatusId = ebc.EvaluationAcceptanceStatusId " +
                "join EvaluationBehaviouralParticipant ebp on ebp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                //"and(ebp.Confirmation != 0 or  ebp.Confirmation is null) " +
                "and eh.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and ebp.ParticipantEvaluationBehaviouralHierarchyId = ISNULL(@departmentIdd, ebp.ParticipantEvaluationBehaviouralHierarchyId) " +
                "and ebp.ParticipantId = @personIdd " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ") as tbl where 1 = 1  " +
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
        public int ParticipantPerformCompetencyConfirmation(PerformCompetencyConfirmationView evaluation, int personId, string roleId)
        {
            List<Notification> notificationList = new List<Notification>();

            foreach (var evaluationItem in evaluation.EvaluationCompetencyId)
            {
                var fetchEval = appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationItem).Include(c => c.EvaluationBehaviouralCompetency).SingleOrDefault();
                if (fetchEval != null)
                {
                    Notification notification = new Notification();
                    if (evaluation.EvaluationCompetencyAcceptanceStatusId == 1)
                    {
                        fetchEval.Confirmation = true;
                        notification.Title = "پذیرش";
                    }
                    else if (evaluation.EvaluationCompetencyAcceptanceStatusId == 0)
                    {
                        fetchEval.Confirmation = false;
                        notification.Title = "عدم پذیرش";
                    }
                    fetchEval.ResponseDate = DateTime.Now;
                    fetchEval.LastUpdatedBy = personId;
                    fetchEval.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(fetchEval);
                    notification.AllocatorHierarchyId = fetchEval.EvaluationBehaviouralCompetency.RecieverAllocationEvaluationBehaviouralHierarchyId;
                    notification.AllocatorPersonId = personId;
                    notification.AllocatorRoleId = roleId;
                    notification.ReceiverPersonId = fetchEval.EvaluationBehaviouralCompetency.AllocatorPersonId;
                    notification.ReceiverHierarchtId = fetchEval.EvaluationBehaviouralCompetency.AllocatorEvaluationBehaviouralHierarchyId;
                    notification.ReceiverRoleId = fetchEval.EvaluationBehaviouralCompetency.AllocatorRoleId;
                    notification.NotificationTitleId = 4;
                    notification.Visibility = false;
                    notification.PeriodDefinitionId = fetchEval.EvaluationBehaviouralCompetency.PeriodDefinitoionId;
                    notification.EvaluationId = fetchEval.EvaluationBehaviouralCompetencyId;
                    notification.CreatedDate = DateTime.Now;
                    notificationList.Add(notification);
                }
            }
            appDbContext.AddRange(notificationList);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public Dictionary<string, object> BehaviouralScoreAssignment(List<EvaluationCompetencyView> listOfScores, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "Employee").SingleOrDefault().Id;

            int? scoreWayRealization = null;

            foreach (var evaluationItem in listOfScores)
            {
                var scoreWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == evaluationItem.PeriodDefinitionId).SingleOrDefault();

                scoreWayRealization = scoreWay.WeightWayScore;
                var result = appDbContext.EvaluationBehaviourCompetencyScore.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationItem.EvaluationBehaviouralCompetencyId && c.CoacherLevel == -1);
                if (result.Count() != 0)
                {
                    dictionary.Add("result", "اطلاعات مورد نظر قبلا ثبت شده است.");
                    return dictionary;
                }
                int[] evalScore = new int[1];
                evalScore[0] = evaluationItem.CompetencyScore ?? 0;
                //There is always one item in this loop 
                foreach (var evaluationScoreItem in evalScore)
                {
                    if (scoreWay.WeightWayScore == 1)//percent
                    {
                        if (evaluationScoreItem == 0)
                        {
                            dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                            return dictionary;
                        }
                    }
                    else if ((evaluationScoreItem < NumberScaleMinMax(scoreWay, 0, 3) || evaluationScoreItem > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2)//likert
                    {
                        return NumberScaleMinMax(dictionary, scoreWay, 3);
                    }
                    EvaluationBehaviourCompetencyScore evaluationScore = new EvaluationBehaviourCompetencyScore();
                    evaluationScore.Score = evaluationItem.CompetencyScore ?? 0;
                    evaluationScore.CoacherLevel = -1;
                    evaluationScore.EvaluationBehaviouralCompetencyId = evaluationItem.EvaluationBehaviouralCompetencyId;
                    evaluationScore.RoleId = roleId;
                    evaluationScore.CoacherId = personId;
                    evaluationScore.CreatedBy = personId;
                    evaluationScore.CreatedDate = DateTime.Now;
                    appDbContext.EvaluationBehaviourCompetencyScore.Add(evaluationScore);
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
        public List<ScoreView> GetCompetencyScore(int evaluationBehaviouralCompetencyId)
        {
            var sQuery = "select " +
                "tbl.EvaluationBehaviouralCompetencyId" +
                ",bc.Title" +
                ",sum(participantScore)participantScore " +
                "from( " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", ebcs.Score as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = -1 " +
                ") as tbl join EvaluationBehaviouralCompetency ebc on ebc.EvaluationBehaviouralCompetencyId = tbl.EvaluationBehaviouralCompetencyId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId " +
                "group by tbl.EvaluationBehaviouralCompetencyId,bc.Title";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<ScoreView> scoreView = null;

            scoreView = conn.Query<ScoreView>(sQuery, new { evaluationBehaviouralCompetencyIdd = evaluationBehaviouralCompetencyId }).ToList();

            //conn.Close();
            conn.Dispose();
            return scoreView;
        }
    }
}
