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
    public class ProtestService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ProtestService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public IEnumerable<object> GetAddressee(int employeeDepartmentId, int employeeId)
        {
            IDbConnection conn = connProvider.Connection;
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
                "and p.PeopleId = @employeeIdd " +
                "and eh.[EvaluationHierarchyId] = @employeeDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",0 Levell " +
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
                "and p.PeopleId = @employeeIdd " +
                "and eh.[EvaluationHierarchyId]= @employeeDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell+1 levell" +
                ", c.EvalHierarchyId " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) " +
                "SELECT[EvaluationHierarchyId] as id" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell " +
                ",PeopleId" +
                ",EvalHierarchyId " +
                "FROM EmpsCTE where Levell between 1 and 2 order by 1";
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                employeeDepartmentIdd = employeeDepartmentId,
                employeeIdd = employeeId
            }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public int AddProtestation(string employeeDepartmentId, string[] addressee, bool visibility, string description, int personId)
        {
            ShareService shareService = new ShareService(appDbContext, null);

            Protest protest = new Protest();
            protest.PeriodDefinitionId = shareService.GetPeriodDefinitionId();
            protest.ProtesterId = int.Parse(employeeDepartmentId.Split("-")[1]);
            protest.ProtesterDepartmentId = int.Parse(employeeDepartmentId.Split("-")[0]);
            protest.VisibilityToHierarchy = visibility;
            protest.Description = description;
            protest.CreatedBy = personId;
            protest.CreatedDate = DateTime.Now;
            List<Addressee> addresseeList = new List<Addressee>();
            foreach (var item in addressee)
            {
                Addressee addresseeObj = new Addressee();

                if (int.Parse(item.Split("-")[0]) != 0 )
                {
                    addresseeObj.CoacherLevel = int.Parse(item.Split("-")[2]);
                    addresseeObj.CoacherId = int.Parse(item.Split("-")[1]);
                    addresseeObj.CoacherDepartmentId = int.Parse(item.Split("-")[0]);
                }
                else if (int.Parse(item.Split("-")[0]) == 0)
                {
                    addresseeObj.CoacherLevel = int.Parse(item.Split("-")[0]);
                }
                addresseeObj.CreatedBy = personId;
                addresseeObj.CreatedDate = DateTime.Now;
                addresseeList.Add(addresseeObj);
            }
            protest.Addressees = addresseeList;
            appDbContext.Add(protest);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public Dictionary<object, object> GetProtestList(DataTableParameter dataTableParameter, int? employeeDepartmentId, int? employeeId, int? periodDefinitionId)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "Description", "FullName" };
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
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",FullName" +
                ",CoacherLevel " +
                "from" +
                "( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ", ProtestId" +
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
                "and pr.ProtesterId = @personIdd " +
                "and pr.ProtesterDepartmentId = @personDepartmentIdd" +
                ")tbl " +
                "where " +
                "1 = 1 " +
                "group by " +
                "ProtestId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ")tbl2 where 1 = 1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",FullName" +
                ",CoacherLevel " +
                "from" +
                "( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ", ProtestId" +
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
                "and pr.ProtesterId = @personIdd " +
                "and pr.ProtesterDepartmentId = @personDepartmentIdd" +
                ")tbl " +
                "where " +
                "1 = 1 " +
                "group by " +
                "ProtestId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ")tbl2 where 1 = 1 " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",ProtestId" +
                ",PeriodDefinitionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",FullName" +
                ",CoacherLevel " +
                "from" +
                "( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY ProtestId asc))  indexx" +
                ", ProtestId" +
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
                "and pr.ProtesterId = @personIdd " +
                "and pr.ProtesterDepartmentId = @personDepartmentIdd" +
                ")tbl " +
                "where " +
                "1 = 1 " +
                "group by " +
                "ProtestId" +
                ",PeriodDefinitionId" +
                ",Description" +
                ",Confirmation" +
                ",VisibilityToHierarchy" +
                ",PeriodCode" +
                ",PeriodTitle" +
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
                    personDepartmentIdd = employeeDepartmentId,
                    personIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personDepartmentIdd = employeeDepartmentId,
                    personIdd = employeeId,
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
                    personIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personDepartmentIdd = employeeDepartmentId,
                personIdd = employeeId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personDepartmentIdd = employeeDepartmentId,
                personIdd = employeeId,
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
                ",(select CONCAT(p.FirstName,' ',p.LastName) from people p " +
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
        public int AddEmployeeResponse(int protestId, int PersonDepartmentId, string protestResponse, int personId, string roleId)
        {
            ProtestResponse protestResponseObj = new ProtestResponse();
            protestResponseObj.PersonDepartmentId = PersonDepartmentId;
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
    }
}
