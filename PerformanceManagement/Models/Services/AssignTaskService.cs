using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class AssignTaskService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public AssignTaskService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public IEnumerable<object> GetDepartment(int levell)
        {
            IDbConnection conn = connProvider.Connection;
            string departmentIdParam = "ParentEvaluationHierarchyId";
            string sQuery = BuildDepartmentQuery(departmentIdParam);
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { level = levell }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public string BuildDepartmentQuery(string departmentIdParam)
        {
            string sQuery = @"WITH EmpsCTE AS ( 
               select
               eh.EvaluationHierarchyId
               ,p.PeopleId
               ,eh.SupervisorId
               ,eh.ParentEvaluationHierarchyId
               ,CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ' - ', anu.UserName, ')')ShortName
               ,0 Levell
               from
               evaluationHierarchies eh
               , Departments d
               ,People p
               , AspNetUsers anu
                where 1 = 1
               and eh.DepartmentId = d.DepartmentId
               and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
               and anu.PeopleId = p.PeopleId
               and eh.EffectiveEndDate is null
               and d.EffectiveEndDate is null
               and p.EffectiveEndDate is null
               and eh.SupervisorId = p.PeopleId
               and eh.ParentEvaluationHierarchyId is null
               UNION ALL
               SELECT
               C.EvaluationHierarchyId
               ,C.PeopleId
               ,C.SupervisorId
               ,C.ParentEvaluationHierarchyId
               ,C.ShortName
               ,Levell + 1 levell
               FROM EmpsCTE AS P
               JOIN(select
               eh.EvaluationHierarchyId
               , p.PeopleId
               , eh.SupervisorId
               , eh.ParentEvaluationHierarchyId
               , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ' - ', anu.UserName, ')')ShortName
               from
               evaluationHierarchies eh
               , Departments d
               , People p
               , AspNetUsers anu
               where 1 = 1
               and eh.DepartmentId = d.DepartmentId
               and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
               and anu.PeopleId = p.PeopleId
               and eh.EffectiveEndDate is null
               and d.EffectiveEndDate is null
               and p.EffectiveEndDate is null
               and eh.SupervisorId = p.PeopleId) AS C
               ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) 
               SELECT
               [EvaluationHierarchyId]
               ,[ShortName]
               ,ParentEvaluationHierarchyId
               ,Levell
               FROM EmpsCTE
               where 
               1=1 
               and(ParentEvaluationHierarchyId in ( " + departmentIdParam + @" )) 
               and Levell = @level ";
            return sQuery;
        }
        public IEnumerable<object> GetDepartment(int[] departmentId, int levell)
        {
            string departmentIdParam = "0";
            foreach (var item in departmentId)
            {
                if (item == 0)
                {
                    departmentIdParam = "ParentEvaluationHierarchyId";
                }
                else if (item == -1)
                {
                    departmentIdParam = "0";

                }
                else
                {
                    departmentIdParam = string.Join(",", departmentId);
                }
            }
            IDbConnection conn = connProvider.Connection;
            string sQuery = BuildDepartmentQuery(departmentIdParam);
            //if (conn.State == ConnectionState.Closed)
            //{
            conn.Open();
            //}

            List<object> query = null;

            query = conn.Query<object>(sQuery, new { level = levell }).ToList();
            // if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return query;
        }
        public IQueryable GetTaskList()
        {
            var roleId = appDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            var maxPeriodDefinitionId = appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId);
            var task = appDbContext.Task.Where(c => c.IsActive == true && c.RoleId == roleId && c.ResourceType == 1 && c.PeriodDefinitoionId == maxPeriodDefinitionId).Select(c => new { c.TaskId, c.Title, c.Type });
            return task;
        }
        public Dictionary<string, object> AssignTask(int[] assistant, int[] management, int[] chairmanship, int[] taskList, int personId, int periodDefinitionId, int ceoAssignment)
        {
            var roleId = appDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;
            ArrayList assistantDuplicationList = new ArrayList();
            ArrayList managementDuplicationList = new ArrayList();
            ArrayList chairmanshipDuplicationList = new ArrayList();
            ArrayList ceoDuplicationList = new ArrayList();


            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var taskItem in taskList)
            {
                foreach (var assistantItem in assistant)
                {
                    if (assistantItem == -1)
                    {

                    }
                    else
                    {
                        int recieverAllocationPersonId = appDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == assistantItem && c.EffectiveEndDate == null).SingleOrDefault().SupervisorId;

                        var eval = appDbContext.Evaluation.Where(c => c.TaskId == taskItem && c.RecieverAllocationEvaluationHierarchyId == assistantItem && c.PeriodDefinitoionId == periodDefinitionId && c.RecieverAllocationPersonId == recieverAllocationPersonId).SingleOrDefault();

                        if (eval == null)
                        {
                            appDbContext.Evaluation.Add(new Evaluation { TaskId = taskItem, RecieverAllocationEvaluationHierarchyId = assistantItem, AllocatorPersonId = personId, AllocatorRoleId = roleId, RecieverAllocationPersonId = recieverAllocationPersonId, PeriodDefinitoionId = periodDefinitionId, CreatedBy = personId, CreatedDate = DateTime.Now, EvaluationAcceptanceStatusId = 1 });
                        }
                        else
                        {
                            //To prevent to duplicate a task for a person in a period definition
                            var task = appDbContext.Task.Where(c => c.TaskId == taskItem).SingleOrDefault();
                            var evaluationHierarchy = appDbContext.evaluationHierarchies.Include(c => c.Department).Where(c => c.EvaluationHierarchyId == assistantItem && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault();

                            assistantDuplicationList.Add(string.Format("هدف {0} به معاونت  {1} در دوره زمانی مورد نظر تکراری می باشد", task.Title, evaluationHierarchy.Department.ShortName));
                        }

                    }
                }
                foreach (var managementItem in management)
                {
                    if (managementItem == -1)
                    {

                    }
                    else
                    {
                        int recieverAllocationPersonId = appDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == managementItem && c.EffectiveEndDate == null).SingleOrDefault().SupervisorId;

                        var eval = appDbContext.Evaluation.Where(c => c.TaskId == taskItem && c.RecieverAllocationEvaluationHierarchyId == managementItem && c.PeriodDefinitoionId == periodDefinitionId && c.RecieverAllocationPersonId == recieverAllocationPersonId).SingleOrDefault();

                        if (eval == null)
                        {
                            appDbContext.Evaluation.Add(new Evaluation { TaskId = taskItem, RecieverAllocationEvaluationHierarchyId = managementItem, AllocatorPersonId = personId, AllocatorRoleId = roleId, RecieverAllocationPersonId = recieverAllocationPersonId, PeriodDefinitoionId = periodDefinitionId, CreatedBy = personId, CreatedDate = DateTime.Now, EvaluationAcceptanceStatusId = 1 });
                        }
                        else
                        {
                            //To prevent to duplicate a task for a person in a period definition
                            var task = appDbContext.Task.Where(c => c.TaskId == taskItem).SingleOrDefault();
                            var evaluationHierarchy = appDbContext.evaluationHierarchies.Include(c => c.Department).Where(c => c.EvaluationHierarchyId == managementItem && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault();

                            managementDuplicationList.Add(string.Format("هدف {0} به مدیریت  {1} در دوره زمانی مورد نظر تکراری می باشد", task.Title, evaluationHierarchy.Department.ShortName));
                        }
                    }
                }
                foreach (var chairmanshipItem in chairmanship)
                {
                    if (chairmanshipItem == -1)
                    {

                    }
                    else
                    {
                        int recieverAllocationPersonId = appDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == chairmanshipItem && c.EffectiveEndDate == null).SingleOrDefault().SupervisorId;

                        var eval = appDbContext.Evaluation.Where(c => c.TaskId == taskItem && c.RecieverAllocationEvaluationHierarchyId == chairmanshipItem && c.PeriodDefinitoionId == periodDefinitionId && c.RecieverAllocationPersonId == recieverAllocationPersonId).SingleOrDefault();

                        if (eval == null)
                        {
                            appDbContext.Evaluation.Add(new Evaluation { TaskId = taskItem, RecieverAllocationEvaluationHierarchyId = chairmanshipItem, AllocatorPersonId = personId, AllocatorRoleId = roleId, RecieverAllocationPersonId = recieverAllocationPersonId, PeriodDefinitoionId = periodDefinitionId, CreatedBy = personId, CreatedDate = DateTime.Now, EvaluationAcceptanceStatusId = 1 });
                        }
                        else
                        {
                            //To prevent to duplicate a task for a person in a period definition
                            var task = appDbContext.Task.Where(c => c.TaskId == taskItem).SingleOrDefault();
                            var evaluationHierarchy = appDbContext.evaluationHierarchies.Include(c => c.Department).Where(c => c.EvaluationHierarchyId == chairmanshipItem && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault();

                            chairmanshipDuplicationList.Add(string.Format("هدف {0} به ریاست  {1} در دوره زمانی مورد نظر تکراری می باشد", task.Title, evaluationHierarchy.Department.ShortName));
                        }
                    }
                }
                if (ceoAssignment == 1)
                {
                    var recieverAllocation = appDbContext.evaluationHierarchies.Where(c => c.ParentEvaluationHierarchyId == null && c.EffectiveEndDate == null).SingleOrDefault();

                    var eval = appDbContext.Evaluation.Where(c => c.TaskId == taskItem && c.RecieverAllocationEvaluationHierarchyId == recieverAllocation.EvaluationHierarchyId && c.PeriodDefinitoionId == periodDefinitionId && c.RecieverAllocationPersonId == recieverAllocation.SupervisorId).SingleOrDefault();

                    if (eval == null)
                    {
                        appDbContext.Evaluation.Add(new Evaluation { TaskId = taskItem, RecieverAllocationEvaluationHierarchyId = recieverAllocation.EvaluationHierarchyId, AllocatorPersonId = personId, AllocatorRoleId = roleId, RecieverAllocationPersonId = recieverAllocation.SupervisorId, PeriodDefinitoionId = periodDefinitionId, CreatedBy = personId, CreatedDate = DateTime.Now, EvaluationAcceptanceStatusId = 1 });
                    }
                    else
                    {
                        //To prevent to duplicate a task for a person in a period definition
                        var task = appDbContext.Task.Where(c => c.TaskId == taskItem).SingleOrDefault();

                        ceoDuplicationList.Add(string.Format("هدف {0} به مدیر عامل در دوره زمانی مورد نظر تکراری می باشد", task.Title));
                    }
                }
            }

            dictionary.Add("assistantDuplicationList", assistantDuplicationList);
            dictionary.Add("managementDuplicationList", managementDuplicationList);
            dictionary.Add("chairmanshipDuplicationList", chairmanshipDuplicationList);
            dictionary.Add("ceoDuplicationList", ceoDuplicationList);

            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("saveChangeResult", saveChangeResult.ToString());
            return dictionary;
        }

        public EvaluationScore GetEvaluationScore(int evaluationId, string roleId)
        {
            EvaluationScore evaluationScore = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationId && c.RoleId == roleId).SingleOrDefault();
            return evaluationScore;
        }
        public CriteriaWeightScore GetCriteriaWeightScore(int criteriaWeightId, string roleId)
        {
            var criteriaWeightScore = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.RoleId == roleId).SingleOrDefault();
            return criteriaWeightScore;
        }

        public string DeleteTaskAssign(int periodDefinitoionId, int evaluationId)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                try
                {
                    Evaluation evaluation = appDbContext.Evaluation.Where(c => c.EvaluationId == evaluationId).SingleOrDefault();

                    bool anyCriteriaWeight = appDbContext.CriteriaWeight.Where(c => c.EvaluationId == evaluation.EvaluationId).Any();

                    if (anyCriteriaWeight)
                    {
                        appDbContext.RemoveRange(appDbContext.CriteriaWeight.Where(c => c.EvaluationId == evaluation.EvaluationId).ToList());
                    }
                    appDbContext.Remove(evaluation);

                    int result = appDbContext.SaveChanges();
                    transaction.Commit();
                    return result.ToString();
                }
                catch (SqlException se)
                {
                    transaction.Rollback();
                    return se.Message;
                }
                catch (DbUpdateException due)
                {
                    transaction.Rollback();
                    DbUpdateException du2 = due;
                    string message = "در صورتی که برای هدف اختصاصی مورد نظر نمره دهی نموده اید امکان حذف آن وجود نخواهد داشت." + Environment.NewLine + Environment.NewLine + due.InnerException.Message;

                    return message;
                }
                catch (Exception e2)
                {
                    transaction.Rollback();
                    return e2.Message;
                }
            }
        }

        public Dictionary<object, object> GetTaskAssignmentList(DataTableParameter dataTableParameter, string departmentId2, string periodDefinitionIdDT2)
        {
            var rowNumber = "";
            if (departmentId2 == "" || periodDefinitionIdDT2 == "" || (departmentId2 == null && periodDefinitionIdDT2 == null))
            {
                departmentId2 = null;
                periodDefinitionIdDT2 = null;
                rowNumber = "ROW_NUMBER() OVER(ORDER BY e.PeriodDefinitoionId desc) As indexx";
            }
            else
            {
                rowNumber = "ROW_NUMBER() OVER(partition by d.DepartmentId ORDER BY e.PeriodDefinitoionId desc) As indexx";
            }
            String[] aColumns = { "PeriodDefinitoionId", "PeriodCode", "PeriodTitle", "RecieverAllocationPersonId", "fullName", "RecieverAllocationEvaluationHierarchyId", "DepartmentId", "ShortName", "TaskId", "Type", "Title", "allocatorRoleName", "TaskWeight" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            int exactOrder = dataTableParameter.orderColumn + 1;
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
                ",RecieverAllocationPersonId" +
                ",fullName" +
                ",RecieverAllocationEvaluationHierarchyId" +
                ",DepartmentId" +
                ",ShortName" +
                ",TaskId" +
                ",Type" +
                ",Title " +
                ",allocatorRoleName" +
                ",TaskWeight " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY e.PeriodDefinitoionId desc) As indexx" +
                ", e.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ",CONCAT((select distinct p.FirstName from People p where e.RecieverAllocationPersonId = p.PeopleId),' ',(select distinct p.LastName from People p where e.RecieverAllocationPersonId = p.PeopleId))fullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",d.DepartmentId" +
                ",d.ShortName" +
                ",e.TaskId" +
                ",t.Type" +
                ",t.Title " +
                ",(select ar.Name from AspNetRoles ar where ar.Id=e.AllocatorRoleId)allocatorRoleName" +
                ", e.TaskWeight " +
                "from  " +
                "Evaluation e" +
                ", Task t" +
                ",evaluationHierarchies eh" +
                ", Departments d" +
                ",PeriodDefinitoion pd " +
                ",AspNetRoles anr " +
                "where " +
                "1 = 1 " +
                "and t.TaskId = e.TaskId " +
                "and eh.EvaluationHierarchyId = e.RecieverAllocationEvaluationHierarchyId " +
                "and eh.EffectiveEndDate is null " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and anr.Id=e.AllocatorRoleId " +
                "and anr.Name in('PlanningAdmin','HRAdmin') " +
                "and d.EffectiveEndDate is null " +
                "and pd.PeriodDefinitoionId = e.PeriodDefinitoionId)taskAssignment where 1=1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",RecieverAllocationPersonId" +
                ",fullName" +
                ",RecieverAllocationEvaluationHierarchyId" +
                ",DepartmentId" +
                ",ShortName" +
                ",TaskId" +
                ",Type" +
                ",Title " +
                ",allocatorRoleName" +
                ",TaskWeight " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY e.PeriodDefinitoionId desc) As indexx" +
                ", e.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ",CONCAT((select distinct p.FirstName from People p where e.RecieverAllocationPersonId = p.PeopleId),' ',(select distinct p.LastName from People p where e.RecieverAllocationPersonId = p.PeopleId))fullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",d.DepartmentId" +
                ",d.ShortName" +
                ",e.TaskId" +
                ",t.Type" +
                ",t.Title " +
                ",(select ar.Name from AspNetRoles ar where ar.Id=e.AllocatorRoleId)allocatorRoleName" +
                ", e.TaskWeight " +
                "from  " +
                "Evaluation e" +
                ", Task t" +
                ",evaluationHierarchies eh" +
                ", Departments d" +
                ",PeriodDefinitoion pd " +
                ",AspNetRoles anr " +
                "where " +
                "1 = 1 " +
                "and t.TaskId = e.TaskId " +
                "and eh.EvaluationHierarchyId = e.RecieverAllocationEvaluationHierarchyId " +
                "and eh.EffectiveEndDate is null " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and anr.Id=e.AllocatorRoleId " +
                "and anr.Name in('PlanningAdmin','HRAdmin') " +
                "and d.EffectiveEndDate is null " +
                "and pd.PeriodDefinitoionId = e.PeriodDefinitoionId)taskAssignment where 1=1 " +
                "and PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt2,PeriodDefinitoionId) " +
                "and DepartmentId=ISNULL(@departmentIdd2,DepartmentId) " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",EvaluationId" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",RecieverAllocationPersonId" +
                ",fullName" +
                ",RecieverAllocationEvaluationHierarchyId" +
                ",DepartmentId" +
                ",ShortName" +
                ",TaskId" +
                ",Type" +
                ",Title " +
                ",allocatorRoleName" +
                ",TaskWeight " +
                "from( " +
                "select " +
                rowNumber +
                ", e.EvaluationId" +
                ", e.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ",CONCAT((select distinct p.FirstName from People p where e.RecieverAllocationPersonId = p.PeopleId),' ',(select distinct p.LastName from People p where e.RecieverAllocationPersonId = p.PeopleId))fullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",d.DepartmentId" +
                ",d.ShortName" +
                ",e.TaskId" +
                ",t.Type" +
                ",t.Title " +
                ",(select ar.Name from AspNetRoles ar where ar.Id=e.AllocatorRoleId)allocatorRoleName" +
                ", e.TaskWeight " +
                "from  " +
                "Evaluation e" +
                ", Task t" +
                ",evaluationHierarchies eh" +
                ", Departments d" +
                ",PeriodDefinitoion pd " +
                ",AspNetRoles anr " +
                "where " +
                "1 = 1 " +
                "and t.TaskId = e.TaskId " +
                "and eh.EvaluationHierarchyId = e.RecieverAllocationEvaluationHierarchyId " +
                "and eh.EffectiveEndDate is null " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and anr.Id=e.AllocatorRoleId " +
                "and anr.Name in('PlanningAdmin','HRAdmin') " +
                "and d.EffectiveEndDate is null " +
                "and pd.PeriodDefinitoionId = e.PeriodDefinitoionId)taskAssignment where 1=1 " +
                "and PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt2,PeriodDefinitoionId) " +
                "and DepartmentId=ISNULL(@departmentIdd2,DepartmentId) " +
                where +
                limit +
                order;

            conn.Open();
            List<object> query = null;
            if (dataTableParameter.length != -1 && dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2 }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new { sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2 }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2 }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new { sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2 }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }

        public IQueryable GetDepartmentList(int periodDefinitionId, string roleId)
        {
            var query = from i in appDbContext.Evaluation
                        join eh in appDbContext.evaluationHierarchies on i.RecieverAllocationEvaluationHierarchyId equals eh.EvaluationHierarchyId
                        join d in appDbContext.Departments on eh.DepartmentId equals d.DepartmentId
                        //select new{ id2=i,dep=d.}
                        where (d.EffectiveEndDate == null && eh.EffectiveEndDate == null && i.PeriodDefinitoionId == periodDefinitionId && i.AllocatorRoleId == roleId)
                        select new { i.RecieverAllocationEvaluationHierarchyId, d.ShortName };

            return query.Distinct();
        }
        public List<ScoreView> GetCriteriaScore(int evaluationId)
        {
            var sQuery = "select " +
                "tbl.EvaluationId" +
                ",t.Title" +
                ",tbl.CriteriaWeightId" +
                ",tbl.CriteriaId" +
                ",c.Title CriteriaTitle" +
                ",sum(planningAdminScore)planningAdminScore " +
                "from( " +
                "select " +
                "cw.EvaluationId" +
                ", cws.CoacherId" +
                ", cws.CoacherLevel" +
                ", cws.CriteriaWeightId" +
                ", cw.CriteriaId" +
                ", cws.RoleId" +
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
                ",sum(planningAdminScore)planningAdminScore " +
                "from( " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", es.Score as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
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
