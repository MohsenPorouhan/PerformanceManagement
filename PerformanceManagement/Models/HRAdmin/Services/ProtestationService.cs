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
    public class ProtestationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ProtestationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> GetProtestationList(DataTableParameter dataTableParameter, int? periodDefinitionId)
        {
            String[] aColumns = { "EmployeeFullName", "EmployeeDepartmentName", "PeriodCode", "PeriodTitle", "Description", "FullName", "Description" };
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
                ",ProtestId" +
                ",ProtesterId" +
                ",EmployeeFullName" +
                ",ProtesterDepartmentId" +
                ",EmployeeDepartmentName" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",FullName" +
                ",CoacherLevel " +
                "from " +
                "( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ", ProtestId" +
                ", ProtesterId" +
                ", EmployeeFullName" +
                ", ProtesterDepartmentId" +
                ", EmployeeDepartmentName" +
                ", PeriodDefinitionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Description" +
                ", Confirmation" +
                ", VisibilityToHierarchy" +
                ", STRING_AGG(CONCAT(FullName, '(', DepartmentName, ')'), '-')FullName" +
                ", STRING_AGG(CoacherLevel2, ' - ') CoacherLevel " +
                "from " +
                "(select " +
                "pr.ProtestId" +
                ", pr.ProtesterId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) " +
                "from People p where p.PeopleId = pr.ProtesterId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ", pr.ProtesterDepartmentId" +
                ", (select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null " +
                "and eh.EvaluationHierarchyId = pr.ProtesterDepartmentId)EmployeeDepartmentName" +
                ", pr.PeriodDefinitionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pr.Description" +
                ", pr.Confirmation" +
                ", pr.VisibilityToHierarchy" +
                ", a.CoacherLevel as CoacherLevel" +
                ",case when a.CoacherLevel = 0 then null else a.CoacherLevel end as CoacherLevel2" +
                ",case when a.CoacherLevel = 0 then N'ادمین سرمایه انسانی' else " +
                "(select CONCAT(p.FirstName, ' ', p.LastName) " +
                "from People p where p.PeopleId = a.CoacherId and p.EffectiveEndDate is null and p.PositionType = 1)end FullName" +
                ",case when a.CoacherLevel = 0 then N'ادمین سرمایه انسانی' else " +
                "(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId = a.CoacherDepartmentId)end DepartmentName " +
                "from Protest pr join Addressee a on pr.ProtestId = a.ProtestId " +
                "join PeriodDefinitoion pd on pr.PeriodDefinitionId = pd.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")tbl " +
                "where " +
                "1 = 1 " +
                "group by " +
                "ProtestId " +
                ",EmployeeFullName" +
                ",EmployeeDepartmentName" +
                ",ProtesterId" +
                ",ProtesterDepartmentId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle " +
                ")tbl2 where 1 = 1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",ProtestId" +
                ",ProtesterId" +
                ",EmployeeFullName" +
                ",ProtesterDepartmentId" +
                ",EmployeeDepartmentName" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",FullName" +
                ",CoacherLevel " +
                "from " +
                "( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ", ProtestId" +
                ", ProtesterId" +
                ", EmployeeFullName" +
                ", ProtesterDepartmentId" +
                ", EmployeeDepartmentName" +
                ", PeriodDefinitionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Description" +
                ", Confirmation" +
                ", VisibilityToHierarchy" +
                ", STRING_AGG(CONCAT(FullName, '(', DepartmentName, ')'), '-')FullName" +
                ", STRING_AGG(CoacherLevel2, ' - ') CoacherLevel " +
                "from " +
                "(select " +
                "pr.ProtestId" +
                ", pr.ProtesterId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) " +
                "from People p where p.PeopleId = pr.ProtesterId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ", pr.ProtesterDepartmentId" +
                ", (select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null " +
                "and eh.EvaluationHierarchyId = pr.ProtesterDepartmentId)EmployeeDepartmentName" +
                ", pr.PeriodDefinitionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pr.Description" +
                ", pr.Confirmation" +
                ", pr.VisibilityToHierarchy" +
                ", a.CoacherLevel as CoacherLevel" +
                ",case when a.CoacherLevel = 0 then null else a.CoacherLevel end as CoacherLevel2" +
                ",case when a.CoacherLevel = 0 then N'ادمین سرمایه انسانی' else " +
                "(select CONCAT(p.FirstName, ' ', p.LastName) " +
                "from People p where p.PeopleId = a.CoacherId and p.EffectiveEndDate is null and p.PositionType = 1)end FullName" +
                ",case when a.CoacherLevel = 0 then N'ادمین سرمایه انسانی' else " +
                "(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId = a.CoacherDepartmentId)end DepartmentName " +
                "from Protest pr join Addressee a on pr.ProtestId = a.ProtestId " +
                "join PeriodDefinitoion pd on pr.PeriodDefinitionId = pd.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")tbl " +
                "where " +
                "1 = 1 " +
                "group by " +
                "ProtestId " +
                ",EmployeeFullName" +
                ",EmployeeDepartmentName" +
                ",ProtesterId" +
                ",ProtesterDepartmentId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle " +
                ")tbl2 where 1 = 1 " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",ProtestId" +
                ",ProtesterId" +
                ",EmployeeFullName" +
                ",ProtesterDepartmentId" +
                ",EmployeeDepartmentName" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",FullName" +
                ",CoacherLevel " +
                "from " +
                "( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ", ProtestId" +
                ", ProtesterId" +
                ", EmployeeFullName" +
                ", ProtesterDepartmentId" +
                ", EmployeeDepartmentName" +
                ", PeriodDefinitionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Description" +
                ", Confirmation" +
                ", VisibilityToHierarchy" +
                ", STRING_AGG(CONCAT(FullName, '(', DepartmentName, ')'), '-')FullName" +
                ", STRING_AGG(CoacherLevel2, ' - ') CoacherLevel " +
                "from " +
                "(select " +
                "pr.ProtestId" +
                ", pr.ProtesterId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) " +
                "from People p where p.PeopleId = pr.ProtesterId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ", pr.ProtesterDepartmentId" +
                ", (select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null " +
                "and eh.EvaluationHierarchyId = pr.ProtesterDepartmentId)EmployeeDepartmentName" +
                ", pr.PeriodDefinitionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pr.Description" +
                ", pr.Confirmation" +
                ", pr.VisibilityToHierarchy" +
                ", a.CoacherLevel as CoacherLevel" +
                ",case when a.CoacherLevel = 0 then null else a.CoacherLevel end as CoacherLevel2" +
                ",case when a.CoacherLevel = 0 then N'ادمین سرمایه انسانی' else " +
                "(select CONCAT(p.FirstName, ' ', p.LastName) " +
                "from People p where p.PeopleId = a.CoacherId and p.EffectiveEndDate is null and p.PositionType = 1)end FullName" +
                ",case when a.CoacherLevel = 0 then N'ادمین سرمایه انسانی' else " +
                "(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null and eh.EvaluationHierarchyId = a.CoacherDepartmentId)end DepartmentName " +
                "from Protest pr join Addressee a on pr.ProtestId = a.ProtestId " +
                "join PeriodDefinitoion pd on pr.PeriodDefinitionId = pd.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, pd.PeriodDefinitoionId) " +
                ")tbl " +
                "where " +
                "1 = 1 " +
                "group by " +
                "ProtestId " +
                ",EmployeeFullName" +
                ",EmployeeDepartmentName" +
                ",ProtesterId" +
                ",ProtesterDepartmentId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle " +
                ")tbl2 where 1 = 1 " +
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
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
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
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                periodDefinitionIdd = periodDefinitionId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
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
        public IEnumerable<ProtestResponseView> GetProtestationResponseList(int protestId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "SELECT pr.ProtestResponseId" +
                ",pr.ProtestId" +
                ",pr.Response" +
                ",pr.CoacherLevel" +
                ",pr.PersonId" +
                ",(select CONCAT(p.FirstName,' ',p.LastName) from People p " +
                "where p.PeopleId=pr.PersonId and p.EffectiveEndDate is null and p.PositionType=1)CoacherFullName" +
                ",pr.PersonDepartmentId" +
                ",(select d.ShortName " +
                "from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.EvaluationHierarchyId " +
                "where d.EffectiveEndDate is null and eh.EffectiveEndDate is null " +
                "and eh.EvaluationHierarchyId=pr.PersonDepartmentId)CoacherDepartmentName" +
                ",pr.RoleType" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=pr.RoleType)RoleName " +
                "FROM ProtestResponse pr " +
                "where 1=1 " +
                "and ProtestId = @protestIdd ";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<ProtestResponseView> query = null;

            query = conn.Query<ProtestResponseView>(sQuery, new
            {
                protestIdd = protestId,
            }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public int AddResponse(int protestId, string protestResponse, int personId, string roleId)
        {
            ProtestResponse protestResponseObj = new ProtestResponse();
            protestResponseObj.PersonId = personId;
            protestResponseObj.ProtestId = protestId;
            protestResponseObj.Response = protestResponse;
            protestResponseObj.RoleType = roleId;
            protestResponseObj.CreatedBy = personId;
            protestResponseObj.CreatedDate = DateTime.Now;
            appDbContext.Add(protestResponseObj);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public int Confirmation(int protestId, bool confirmation,int personId)
        {
            Protest protest = appDbContext.Protest.Where(c => c.ProtestId == protestId).SingleOrDefault();
            protest.Confirmation = confirmation;
            protest.LastUpdatedBy = personId;
            protest.LastUpdatedDate = DateTime.Now;
            appDbContext.Update(protest);
            int result = appDbContext.SaveChanges();
            return result;
        }
    }
}
