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
    public class EmployeeSensibleEventService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        public EmployeeSensibleEventService(AppDbContext appDbContext, IConnProvider connProvider, IHostingEnvironment hostingEnvironment)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IEnumerable<object> GetTaskListOfEmployee(int employeeDepartmentId, int employeeId)
        {
            ShareService shareService = new ShareService(appDbContext, null);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "e.EvaluationId" +
                ",t.TaskId" +
                ",t.Title " +
                "from Task t join Evaluation e on t.TaskId = e.TaskId " +
                "where " +
                "1 = 1 " +
                "and (e.RecieverAllocationEvaluationHierarchyId = @employeeDepartmentIdd or e.AllocatorEvaluationHierarchyId is null) " +
                "and e.RecieverAllocationPersonId = @employeeIdd " +
                "and e.PeriodDefinitoionId = @periodDefinitionIdd";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                employeeDepartmentIdd = employeeDepartmentId,
                employeeIdd = employeeId,
                periodDefinitionIdd = periodDefinitionId
            }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public IEnumerable<object> GetCompetencyListOfEmployee(int employeeDepartmentId, int employeeId)
        {
            ShareService shareService = new ShareService(appDbContext, null);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "ebc.EvaluationBehaviouralCompetencyId" +
                ",bc.BehaviouralCompetencyId" +
                ",bc.Title " +
                "from BehaviouralCompetency bc join EvaluationBehaviouralCompetency ebc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                "and (ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = @employeeDepartmentIdd or ebc.AllocatorEvaluationBehaviouralHierarchyId is null) " +
                "and ebc.RecieverAllocationPersonId = @employeeIdd " +
                "and ebc.PeriodDefinitoionId = @periodDefinitionIdd";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                employeeDepartmentIdd = employeeDepartmentId,
                employeeIdd = employeeId,
                periodDefinitionIdd = periodDefinitionId
            }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public int AddSensibleEvent(string title, int eventType, DateTime sensibleEventDate, string[] behaviourCompetencyId, string[] taskId, string roleId, int personId, string employeeDepartmentId, IFormFile fupload, string description)
        {
            ShareService shareService = new ShareService(appDbContext, null);
            SensibleEvent sensibleEvent = new SensibleEvent();
            string uniqueFileName = null;
            if (fupload != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "employeeUploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fupload.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                fupload.CopyTo(new FileStream(filePath, FileMode.Create));
                sensibleEvent.FilePath = uniqueFileName;
                sensibleEvent.FileName = fupload.FileName;
            }

            sensibleEvent.Title = title;
            sensibleEvent.EventType = eventType;
            sensibleEvent.SensibleEventDate = sensibleEventDate;
            if (behaviourCompetencyId.Length > 0)
            {
                List<RelatedCompetencyWithSensibleEvent> relatedCompetencyWithSensibleEvents = new List<RelatedCompetencyWithSensibleEvent>();
                foreach (var item in behaviourCompetencyId)
                {
                    RelatedCompetencyWithSensibleEvent relatedCompetencyWithSensibleEvent = new RelatedCompetencyWithSensibleEvent();
                    relatedCompetencyWithSensibleEvent.BehaviouralCompetencyId = int.Parse(item.Split("-")[0]);
                    relatedCompetencyWithSensibleEvent.EvaluationBehaviouralCompetencyId = int.Parse(item.Split("-")[1]);
                    relatedCompetencyWithSensibleEvent.CreatedBy = personId;
                    relatedCompetencyWithSensibleEvent.CreatedDate = DateTime.Now;
                    relatedCompetencyWithSensibleEvents.Add(relatedCompetencyWithSensibleEvent);
                }
                sensibleEvent.RelatedCompetencyWithSensibleEvents = relatedCompetencyWithSensibleEvents;
            }
            if (taskId.Length > 0)
            {
                List<RelatedTaskWithSensibleEvent> relatedTaskWithSensibleEvents = new List<RelatedTaskWithSensibleEvent>();
                foreach (var item in taskId)
                {
                    RelatedTaskWithSensibleEvent relatedTaskWithSensibleEvent = new RelatedTaskWithSensibleEvent();
                    relatedTaskWithSensibleEvent.TaskId = int.Parse(item.Split("-")[0]);
                    relatedTaskWithSensibleEvent.EvaluationId = int.Parse(item.Split("-")[1]);
                    relatedTaskWithSensibleEvent.CreatedBy = personId;
                    relatedTaskWithSensibleEvent.CreatedDate = DateTime.Now;
                    relatedTaskWithSensibleEvents.Add(relatedTaskWithSensibleEvent);
                }
                sensibleEvent.RelatedTaskWithSensibleEvents = relatedTaskWithSensibleEvents;
            }
            sensibleEvent.PersonId = personId;
            sensibleEvent.PersonDepartmentId = int.Parse(employeeDepartmentId.Split("-")[0]);
            sensibleEvent.PersonRole = roleId;
            sensibleEvent.Description = description;
            sensibleEvent.PeriodDefinitionId = shareService.GetPeriodDefinitionId();
            sensibleEvent.CreatedBy = personId;
            sensibleEvent.CreatedDate = DateTime.Now;

            appDbContext.Add(sensibleEvent);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public Dictionary<object, object> GetSensibleEventList(DataTableParameter dataTableParameter, int? employeeDepartmentId, int? employeeId, string roleId, int? periodDefinitionId)
        {
            String[] aColumns = { "Title", "Description", "CoacherFullName", "CoacherDepartmentName", "EmployeeDepartmentName" };
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
                "and se.PersonDepartmentId = ISNULL(@personDepartmentIdd, se.PersonDepartmentId) " +
                "and se.PersonId = ISNULL(@personIdd, se.PersonId) " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
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
                "and se.PersonDepartmentId = ISNULL(@personDepartmentIdd, se.PersonDepartmentId) " +
                "and se.PersonId = ISNULL(@personIdd, se.PersonId) " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
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
                "and se.PersonDepartmentId = ISNULL(@personDepartmentIdd, se.PersonDepartmentId) " +
                "and se.PersonId = ISNULL(@personIdd, se.PersonId) " +
                "and se.PeriodDefinitionId = ISNULL(@periodDefinitionIdd, se.PeriodDefinitionId) " +
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
                    personRolee = roleId,
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
                    personRolee = roleId,
                    personDepartmentIdd = employeeDepartmentId,
                    personIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personRolee = roleId,
                personDepartmentIdd = employeeDepartmentId,
                personIdd = employeeId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personRolee = roleId,
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
