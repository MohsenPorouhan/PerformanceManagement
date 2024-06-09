using Dapper;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.Employee.Services
{
    public class SensibleEventOfCoacherService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public SensibleEventOfCoacherService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> GetSensibleEventOfCoacherList(DataTableParameter dataTableParameter, int? employeeDepartmentId, int? employeeId, string roleId, int? periodDefinitionId)
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

            string queryTotalResult = "select " +
                "indexx" +
                ",SensibleEventId" +
                ",Title" +
                ",Description" +
                ",EventType" +
                ",PersonId" +
                ",CoacherFullName" +
                ",PersonDepartmentId" +
                ",CoacherDepartmentName" +
                ",EmployeeId" +
                ",EmployeeFullName" +
                ",EmployeeDepartmentId" +
                ",EmployeeDepartmentName" +
                ",PersonRole" +
                ",Visibility " +
                ",FileName" +
                ",FilePath " +
                ",SensibleEventDate " +
                ",PeriodDefinitionId " +
                "from" +
                "(" +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY se.SensibleEventId asc))  indexx" +
                ", se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CoacherFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CoacherDepartmentName" +
                ",se.EmployeeId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.EmployeeId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ",se.EmployeeDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.EmployeeDepartmentId)EmployeeDepartmentName" +
                ",se.PersonRole" +
                ",se.Visibility " +
                ",se.FileName" +
                ",se.FilePath " +
                ",se.SensibleEventDate " +
                ",se.PeriodDefinitionId " +
                "from SensibleEvent se " +
                "where " +
                "1 = 1 " +
                "and se.PersonRole = @personRolee " +
                "and se.EmployeeDepartmentId = ISNULL(@employeeDepartmentIdd, se.EmployeeDepartmentId) " +
                "and se.EmployeeId = ISNULL(@employeeIdd, se.EmployeeId) " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
                "and se.Visibility = 1 " +
                ")tbl where 1 = 1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",SensibleEventId" +
                ",Title" +
                ",Description" +
                ",EventType" +
                ",PersonId" +
                ",CoacherFullName" +
                ",PersonDepartmentId" +
                ",CoacherDepartmentName" +
                ",EmployeeId" +
                ",EmployeeFullName" +
                ",EmployeeDepartmentId" +
                ",EmployeeDepartmentName" +
                ",PersonRole" +
                ",Visibility " +
                ",FileName" +
                ",FilePath " +
                ",SensibleEventDate " +
                ",PeriodDefinitionId " +
                "from" +
                "(" +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY se.SensibleEventId asc))  indexx" +
                ", se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CoacherFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CoacherDepartmentName" +
                ",se.EmployeeId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.EmployeeId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ",se.EmployeeDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.EmployeeDepartmentId)EmployeeDepartmentName" +
                ",se.PersonRole" +
                ",se.Visibility " +
                ",se.FileName" +
                ",se.FilePath " +
                ",se.SensibleEventDate " +
                ",se.PeriodDefinitionId " +
                "from SensibleEvent se " +
                "where " +
                "1 = 1 " +
                "and se.PersonRole = @personRolee " +
                "and se.EmployeeDepartmentId = ISNULL(@employeeDepartmentIdd, se.EmployeeDepartmentId) " +
                "and se.EmployeeId = ISNULL(@employeeIdd, se.EmployeeId) " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
                "and se.Visibility = 1 " +
                ")tbl where 1 = 1 " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",SensibleEventId" +
                ",Title" +
                ",Description" +
                ",EventType" +
                ",PersonId" +
                ",CoacherFullName" +
                ",PersonDepartmentId" +
                ",CoacherDepartmentName" +
                ",EmployeeId" +
                ",EmployeeFullName" +
                ",EmployeeDepartmentId" +
                ",EmployeeDepartmentName" +
                ",PersonRole" +
                ",Visibility " +
                ",FileName" +
                ",FilePath " +
                ",SensibleEventDate " +
                ",PeriodDefinitionId " +
                "from" +
                "(" +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY se.SensibleEventId asc))  indexx" +
                ", se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CoacherFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CoacherDepartmentName" +
                ",se.EmployeeId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.EmployeeId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ",se.EmployeeDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.EmployeeDepartmentId)EmployeeDepartmentName" +
                ",se.PersonRole" +
                ",se.Visibility " +
                ",se.FileName" +
                ",se.FilePath " +
                ",se.SensibleEventDate " +
                ",se.PeriodDefinitionId " +
                "from SensibleEvent se " +
                "where " +
                "1 = 1 " +
                "and se.PersonRole = @personRolee " +
                "and se.EmployeeDepartmentId = ISNULL(@employeeDepartmentIdd, se.EmployeeDepartmentId) " +
                "and se.EmployeeId = ISNULL(@employeeIdd, se.EmployeeId) " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
                "and se.Visibility = 1 " +
                ")tbl where 1 = 1 " +
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
                    employeeDepartmentIdd = employeeDepartmentId,
                    employeeIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personRolee = roleId,
                    employeeDepartmentIdd = employeeDepartmentId,
                    employeeIdd = employeeId,
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
                    employeeDepartmentIdd = employeeDepartmentId,
                    employeeIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personRolee = roleId,
                employeeDepartmentIdd = employeeDepartmentId,
                employeeIdd = employeeId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personRolee = roleId,
                employeeDepartmentIdd = employeeDepartmentId,
                employeeIdd = employeeId,
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
