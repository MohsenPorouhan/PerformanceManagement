using Dapper;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class SensibleEventOfEmployeeService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public SensibleEventOfEmployeeService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> GetSensibleEventOfCoacherList(DataTableParameter dataTableParameter, int? coacherDepartmentId, int coacherId, string roleId, int? periodDefinitionId)
        {
            String[] aColumns = { "Title", "Description", "CoacherDepartmentName", "CoacherFullName" };
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

            string queryTotalResult = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell" +
                ",p.EvaluationHierarchyID EvalHierarchyId " +
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
                "and p.PeopleId = @coacherIdd " +
                "and eh.[EvaluationHierarchyId] = @coacherDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",1 Levell" +
                ",p.EvaluationHierarchyID EvalHierarchyId " +
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
                "and p.PeopleId = @coacherIdd " +
                "and eh.[EvaluationHierarchyId]= @coacherDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell+1 levell" +
                ", C.EvalHierarchyId " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ", SensibleEventId" +
                ", Title" +
                ", Description" +
                ", EventType" +
                ", PersonId" +
                ", CoacherFullName" +
                ", PersonDepartmentId" +
                ", CoacherDepartmentName" +
                ", PersonRole" +
                ", FileName" +
                ", FilePath" +
                ", SensibleEventDate" +
                ", PeriodDefinitionId" +
                ", id" +
                ", text" +
                ", parent" +
                ", Levell" +
                ", PeopleId" +
                ", EvalHierarchyId" +
                ", SupervisorId " +
                "from( " +
                "SELECT " +
                "(ROW_NUMBER() OVER(ORDER BY se.SensibleEventId asc))  indexx " +
                ", se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CoacherFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CoacherDepartmentName" +
                ",se.PersonRole" +
                ",se.FileName" +
                ",se.FilePath " +
                ",se.SensibleEventDate " +
                ",se.PeriodDefinitionId " +
                ",[EvaluationHierarchyId] as id" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell" +
                ",PeopleId" +
                ",EvalHierarchyId" +
                ",SupervisorId " +
                "FROM EmpsCTE join SensibleEvent se on EmpsCTE.PeopleId=se.PersonId and EmpsCTE.EvalHierarchyId= se.PersonDepartmentId " +
                "where " +
                "1=1 " +
                "and se.PersonRole = @personRolee " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
                "and Levell!=1 " +
                ")tbl where 1=1 ";

            string queryFilteredTotal = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell" +
                ",p.EvaluationHierarchyID EvalHierarchyId " +
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
                "and p.PeopleId = @coacherIdd " +
                "and eh.[EvaluationHierarchyId] = @coacherDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",1 Levell" +
                ",p.EvaluationHierarchyID EvalHierarchyId " +
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
                "and p.PeopleId = @coacherIdd " +
                "and eh.[EvaluationHierarchyId]= @coacherDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell+1 levell" +
                ", C.EvalHierarchyId " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ", SensibleEventId" +
                ", Title" +
                ", Description" +
                ", EventType" +
                ", PersonId" +
                ", CoacherFullName" +
                ", PersonDepartmentId" +
                ", CoacherDepartmentName" +
                ", PersonRole" +
                ", FileName" +
                ", FilePath" +
                ", SensibleEventDate" +
                ", PeriodDefinitionId" +
                ", id" +
                ", text" +
                ", parent" +
                ", Levell" +
                ", PeopleId" +
                ", EvalHierarchyId" +
                ", SupervisorId " +
                "from( " +
                "SELECT " +
                "(ROW_NUMBER() OVER(ORDER BY se.SensibleEventId asc))  indexx " +
                ", se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CoacherFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CoacherDepartmentName" +
                ",se.PersonRole" +
                ",se.FileName" +
                ",se.FilePath " +
                ",se.SensibleEventDate " +
                ",se.PeriodDefinitionId " +
                ",[EvaluationHierarchyId] as id" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell" +
                ",PeopleId" +
                ",EvalHierarchyId" +
                ",SupervisorId " +
                "FROM EmpsCTE join SensibleEvent se on EmpsCTE.PeopleId=se.PersonId and EmpsCTE.EvalHierarchyId= se.PersonDepartmentId " +
                "where " +
                "1=1 " +
                "and se.PersonRole = @personRolee " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
                "and Levell!=1 " +
                ")tbl where 1=1 " +
                where;

            string sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell" +
                ",p.EvaluationHierarchyID EvalHierarchyId " +
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
                "and p.PeopleId = @coacherIdd " +
                "and eh.[EvaluationHierarchyId] = @coacherDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",1 Levell" +
                ",p.EvaluationHierarchyID EvalHierarchyId " +
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
                "and p.PeopleId = @coacherIdd " +
                "and eh.[EvaluationHierarchyId]= @coacherDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell+1 levell" +
                ", C.EvalHierarchyId " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ", SensibleEventId" +
                ", Title" +
                ", Description" +
                ", EventType" +
                ", PersonId" +
                ", CoacherFullName" +
                ", PersonDepartmentId" +
                ", CoacherDepartmentName" +
                ", PersonRole" +
                ", FileName" +
                ", FilePath" +
                ", SensibleEventDate" +
                ", PeriodDefinitionId" +
                ", id" +
                ", text" +
                ", parent" +
                ", Levell" +
                ", PeopleId" +
                ", EvalHierarchyId" +
                ", SupervisorId " +
                "from( " +
                "SELECT " +
                "(ROW_NUMBER() OVER(ORDER BY se.SensibleEventId asc))  indexx " +
                ", se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CoacherFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CoacherDepartmentName" +
                ",se.PersonRole" +
                ",se.FileName" +
                ",se.FilePath " +
                ",se.SensibleEventDate " +
                ",se.PeriodDefinitionId " +
                ",[EvaluationHierarchyId] as id" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell" +
                ",PeopleId" +
                ",EvalHierarchyId" +
                ",SupervisorId " +
                "FROM EmpsCTE join SensibleEvent se on EmpsCTE.PeopleId=se.PersonId and EmpsCTE.EvalHierarchyId= se.PersonDepartmentId " +
                "where " +
                "1=1 " +
                "and se.PersonRole = @personRolee " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
                "and Levell!=1 " +
                ")tbl where 1=1 " +
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
                    personRolee = roleId,
                    coacherDepartmentIdd = coacherDepartmentId,
                    coacherIdd = coacherId,
                    periodDefinitionIdd = periodDefinitionId,
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personRolee = roleId,
                    coacherDepartmentIdd = coacherDepartmentId,
                    coacherIdd = coacherId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    personRolee = roleId,
                    coacherDepartmentIdd = coacherDepartmentId,
                    coacherIdd = coacherId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personRolee = roleId,
                coacherDepartmentIdd = coacherDepartmentId,
                coacherIdd = coacherId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personRolee = roleId,
                coacherDepartmentIdd = coacherDepartmentId,
                coacherIdd = coacherId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }
        public IEnumerable<RelatedTaskCompetencyView> GetRelatedTaskCompetencyList(int sensibleEventId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "se.SensibleEventId" +
                ",rt.EvaluationId TBEvalId" +
                ",rt.TaskId TBId" +
                ",t.Title" +
                ",N'وظیفه' Type " +
                "from SensibleEvent se join RelatedTaskWithSensibleEvent rt on se.SensibleEventId = rt.SensibleEventId " +
                "join Task t on rt.TaskId = t.TaskId " +
                "where " +
                "1 = 1 " +
                "and rt.SensibleEventId = @sensibleEventIdd " +
                "union " +
                "select " +
                "se.SensibleEventId" +
                ",rc.EvaluationBehaviouralCompetencyId" +
                ",rc.BehaviouralCompetencyId" +
                ",bc.Title" +
                ",N'شایستگی' Type " +
                "from SensibleEvent se join RelatedCompetencyWithSensibleEvent rc on se.SensibleEventId = rc.SensibleEventId " +
                "join BehaviouralCompetency bc on rc.BehaviouralCompetencyId = bc.BehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                "and rc.SensibleEventId = @sensibleEventIdd";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<RelatedTaskCompetencyView> query = null;

            query = conn.Query<RelatedTaskCompetencyView>(sQuery, new
            {
                sensibleEventIdd = sensibleEventId,
            }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
    }
}
