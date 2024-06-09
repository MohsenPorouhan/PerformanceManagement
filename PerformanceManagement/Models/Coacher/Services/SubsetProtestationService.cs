using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace PerformanceManagement.Models.Employee.Services
{
    public class SubsetProtestationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public SubsetProtestationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> GetSubsetProtestationList(DataTableParameter dataTableParameter, int? employeeDepartmentId, int? coacherId, int? periodDefinitionId)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "Description", "FullName", "text", "Description" };
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
                ", 0 Levell" +
                ",eh.EvaluationHierarchyId EvalHierarchyId " +
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
                "and eh.[EvaluationHierarchyId] = @personDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId " +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",0 Levell" +
                ",eh.EvaluationHierarchyId EvalHierarchyId " +
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
                "and eh.[EvaluationHierarchyId]= @personDepartmentIdd " +
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
                ", EmployeeDepartmentName" +
                ", EvalHierarchyId" +
                ", PeopleId" +
                ", text" +
                ", ProtestId" +
                ", PeriodDefinitionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Description" +
                ", Confirmation" +
                ", VisibilityToHierarchy" +
                ", FullName" +
                ", CoacherLevel " +
                ",Levell DistanceLevel " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ",EmployeeDepartmentName" +
                ",EvalHierarchyId" +
                ",PeopleId" +
                ",text" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",STRING_AGG(CONCAT(CoacherFullName,'(', CoacherDepartmentName,')'),'-')FullName" +
                ",STRING_AGG(CoacherLevel2,' - ') CoacherLevel " +
                ",Levell " +
                "from " +
                "( " +
                "SELECT[EvaluationHierarchyId] as id " +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", EmpsCTE.Levell" +
                ", EmpsCTE.PeopleId" +
                ", EmpsCTE.EvalHierarchyId" +
                ", (select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId= eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId= EmpsCTE.EvalHierarchyId) EmployeeDepartmentName " +
                ",pr.ProtestId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pr.PeriodDefinitionId" +
                ",pr.Description" +
                ",pr.Confirmation" +
                ",pr.VisibilityToHierarchy" +
                ",a.CoacherLevel as CoacherLevel" +
                ",case when a.CoacherLevel=0 then null else a.CoacherLevel end as CoacherLevel2" +
                ",case when a.CoacherLevel= 0 then N'ادمین سرمایه اسانی' else " +
                "(select CONCAT(p.FirstName,' ', p.LastName) " +
                "from People p where p.PeopleId=a.CoacherId and p.EffectiveEndDate is null and p.PositionType=1)end CoacherFullName " +
                ",case when a.CoacherLevel=0 then N'ادمین سرمایه اسانی' else " +
                "(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId= eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId= a.CoacherDepartmentId)end CoacherDepartmentName " +
                "FROM EmpsCTE join Protest pr on EmpsCTE.EvalHierarchyId=pr.ProtesterDepartmentId and EmpsCTE.PeopleId=pr.ProtesterId " +
                "join Addressee a on pr.ProtestId=a.ProtestId " +
                "join PeriodDefinitoion pd on pr.PeriodDefinitionId= pd.PeriodDefinitoionId " +
                "where " +
                "1=1 " +
                "and pr.VisibilityToHierarchy=1 " +
                "and pd.PeriodDefinitoionId= ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")tbl " +
                "where " +
                "1=1 " +
                "group by " +
                "EmployeeDepartmentName" +
                ",EvalHierarchyId " +
                ",PeopleId" +
                ",text" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Levell " +
                ")tbl2 where 1=1 ";

            string queryFilteredTotal = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 0 Levell" +
                ",eh.EvaluationHierarchyId EvalHierarchyId " +
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
                "and eh.[EvaluationHierarchyId] = @personDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId " +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",0 Levell" +
                ",eh.EvaluationHierarchyId EvalHierarchyId " +
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
                "and eh.[EvaluationHierarchyId]= @personDepartmentIdd " +
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
                ", EmployeeDepartmentName" +
                ", EvalHierarchyId" +
                ", PeopleId" +
                ", text" +
                ", ProtestId" +
                ", PeriodDefinitionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Description" +
                ", Confirmation" +
                ", VisibilityToHierarchy" +
                ", FullName" +
                ", CoacherLevel " +
                ",Levell DistanceLevel " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ",EmployeeDepartmentName" +
                ",EvalHierarchyId" +
                ",PeopleId" +
                ",text" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",STRING_AGG(CONCAT(CoacherFullName,'(', CoacherDepartmentName,')'),'-')FullName" +
                ",STRING_AGG(CoacherLevel2,' - ') CoacherLevel " +
                ",Levell " +
                "from " +
                "( " +
                "SELECT[EvaluationHierarchyId] as id " +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", EmpsCTE.Levell" +
                ", EmpsCTE.PeopleId" +
                ", EmpsCTE.EvalHierarchyId" +
                ", (select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId= eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId= EmpsCTE.EvalHierarchyId) EmployeeDepartmentName " +
                ",pr.ProtestId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pr.PeriodDefinitionId" +
                ",pr.Description" +
                ",pr.Confirmation" +
                ",pr.VisibilityToHierarchy" +
                ",a.CoacherLevel as CoacherLevel" +
                ",case when a.CoacherLevel=0 then null else a.CoacherLevel end as CoacherLevel2" +
                ",case when a.CoacherLevel= 0 then N'ادمین سرمایه اسانی' else " +
                "(select CONCAT(p.FirstName,' ', p.LastName) " +
                "from People p where p.PeopleId=a.CoacherId and p.EffectiveEndDate is null and p.PositionType=1)end CoacherFullName " +
                ",case when a.CoacherLevel=0 then N'ادمین سرمایه اسانی' else " +
                "(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId= eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId= a.CoacherDepartmentId)end CoacherDepartmentName " +
                "FROM EmpsCTE join Protest pr on EmpsCTE.EvalHierarchyId=pr.ProtesterDepartmentId and EmpsCTE.PeopleId=pr.ProtesterId " +
                "join Addressee a on pr.ProtestId=a.ProtestId " +
                "join PeriodDefinitoion pd on pr.PeriodDefinitionId= pd.PeriodDefinitoionId " +
                "where " +
                "1=1 " +
                "and pr.VisibilityToHierarchy=1 " +
                "and pd.PeriodDefinitoionId= ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")tbl " +
                "where " +
                "1=1 " +
                "group by " +
                "EmployeeDepartmentName" +
                ",EvalHierarchyId " +
                ",PeopleId" +
                ",text" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Levell " +
                ")tbl2 where 1=1 " +
                where;

            string sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 0 Levell" +
                ",eh.EvaluationHierarchyId EvalHierarchyId " +
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
                "and eh.[EvaluationHierarchyId] = @personDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId " +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",0 Levell" +
                ",eh.EvaluationHierarchyId EvalHierarchyId " +
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
                "and eh.[EvaluationHierarchyId]= @personDepartmentIdd " +
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
                ", EmployeeDepartmentName" +
                ", EvalHierarchyId" +
                ", PeopleId" +
                ", text" +
                ", ProtestId" +
                ", PeriodDefinitionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Description" +
                ", Confirmation" +
                ", VisibilityToHierarchy" +
                ", FullName" +
                ", CoacherLevel " +
                ",Levell DistanceLevel " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ",EmployeeDepartmentName" +
                ",EvalHierarchyId" +
                ",PeopleId" +
                ",text" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",STRING_AGG(CONCAT(CoacherFullName,'(', CoacherDepartmentName,')'),'-')FullName" +
                ",STRING_AGG(CoacherLevel2,' - ') CoacherLevel " +
                ",Levell "+
                "from " +
                "( " +
                "SELECT[EvaluationHierarchyId] as id " +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", EmpsCTE.Levell" +
                ", EmpsCTE.PeopleId" +
                ", EmpsCTE.EvalHierarchyId" +
                ", (select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId= eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId= EmpsCTE.EvalHierarchyId) EmployeeDepartmentName " +
                ",pr.ProtestId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pr.PeriodDefinitionId" +
                ",pr.Description" +
                ",pr.Confirmation" +
                ",pr.VisibilityToHierarchy" +
                ",a.CoacherLevel as CoacherLevel" +
                ",case when a.CoacherLevel=0 then null else a.CoacherLevel end as CoacherLevel2" +
                ",case when a.CoacherLevel= 0 then N'ادمین سرمایه اسانی' else " +
                "(select CONCAT(p.FirstName,' ', p.LastName) " +
                "from People p where p.PeopleId=a.CoacherId and p.EffectiveEndDate is null and p.PositionType=1)end CoacherFullName " +
                ",case when a.CoacherLevel=0 then N'ادمین سرمایه اسانی' else " +
                "(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId= eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId= a.CoacherDepartmentId)end CoacherDepartmentName " +
                "FROM EmpsCTE join Protest pr on EmpsCTE.EvalHierarchyId=pr.ProtesterDepartmentId and EmpsCTE.PeopleId=pr.ProtesterId " +
                "join Addressee a on pr.ProtestId=a.ProtestId " +
                "join PeriodDefinitoion pd on pr.PeriodDefinitionId= pd.PeriodDefinitoionId " +
                "where " +
                "1=1 " +
                "and pr.VisibilityToHierarchy=1 " +
                "and pd.PeriodDefinitoionId= ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")tbl " +
                "where " +
                "1=1 " +
                "group by " +
                "EmployeeDepartmentName" +
                ",EvalHierarchyId " +
                ",PeopleId" +
                ",text" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Levell " +
                ")tbl2 where 1=1 " +
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
                    personDepartmentIdd = employeeDepartmentId,
                    personIdd = coacherId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personDepartmentIdd = employeeDepartmentId,
                    personIdd = coacherId,
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
                    personDepartmentIdd = employeeDepartmentId,
                    personIdd = coacherId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personDepartmentIdd = employeeDepartmentId,
                personIdd = coacherId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personDepartmentIdd = employeeDepartmentId,
                personIdd = coacherId,
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
    }
}
