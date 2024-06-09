using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class TaskDivisionService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public TaskDivisionService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> GetAssignedTaskList(DataTableParameter dataTableParameter, string departmentId2, string periodDefinitionIdDT2, int personId)
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
            String[] aColumns = { "PeriodCode", "PeriodTitle", "fullName", "ShortName", "Type", "Title", "allocatorRoleName", "TaskWeight", "allocatorRoleName", "AllocatorFullName", "AllocatorDepartmentName" };
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
                ",Title" +
                ",TaskWeight" +
                ",allocatorRoleName" +
                ",AllocatorPersonId" +
                ",AllocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",AllocatorDepartmentName " +
                "from(select " +
                rowNumber +
                ",e.EvaluationId" +
                ", e.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ", CONCAT((select distinct p.FirstName from People p where e.RecieverAllocationPersonId = p.PeopleId), ' '" +
                ", (select distinct p.LastName from People p where e.RecieverAllocationPersonId = p.PeopleId))fullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",d.DepartmentId" +
                ",d.ShortName" +
                ",e.TaskId" +
                ",t.Type" +
                ",t.Title" +
                ",(select ar.Name from AspNetRoles ar where ar.Id = e.AllocatorRoleId)allocatorRoleName" +
                ",e.TaskWeight" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d2.ShortName from Departments d2 join evaluationHierarchies eh2 on d2.DepartmentId = eh2.DepartmentId where " +
                "d2.EffectiveEndDate is null and eh2.EffectiveEndDate is null and eh2.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId)AllocatorDepartmentName" +
                ",e.AllocatorPersonId" +
                ",CONCAT((select distinct p.FirstName from People p where e.AllocatorPersonId = p.PeopleId),' '" +
                ",(select distinct p.LastName from People p where e.AllocatorPersonId = p.PeopleId))AllocatorFullName " +
                "from  Evaluation e" +
                ", Task t" +
                ",evaluationHierarchies eh" +
                ", Departments d" +
                ",PeriodDefinitoion pd " +
                "where 1 = 1 " +
                "and t.TaskId = e.TaskId " +
                "and eh.EvaluationHierarchyId = e.RecieverAllocationEvaluationHierarchyId and eh.EffectiveEndDate is null " +
                "and eh.DepartmentId = d.DepartmentId and d.EffectiveEndDate is null " +
                "and pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "and t.ResourceType != 2 " +
                "and eh.SupervisorId = e.RecieverAllocationPersonId " +
                "and e.RecieverAllocationPersonId = @personId2 " +
                ")taskAssignment where 1 = 1 " +
                "and RecieverAllocationPersonId = @personId2 ";

            string queryFilteredTotal = "select " +
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
                ",Title" +
                ",TaskWeight" +
                ",allocatorRoleName" +
                ",AllocatorPersonId" +
                ",AllocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",AllocatorDepartmentName " +
                "from(select " +
                rowNumber +
                ",e.EvaluationId" +
                ", e.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ", CONCAT((select distinct p.FirstName from People p where e.RecieverAllocationPersonId = p.PeopleId), ' '" +
                ", (select distinct p.LastName from People p where e.RecieverAllocationPersonId = p.PeopleId))fullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",d.DepartmentId" +
                ",d.ShortName" +
                ",e.TaskId" +
                ",t.Type" +
                ",t.Title" +
                ",(select ar.Name from AspNetRoles ar where ar.Id = e.AllocatorRoleId)allocatorRoleName" +
                ",e.TaskWeight" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d2.ShortName from Departments d2 join evaluationHierarchies eh2 on d2.DepartmentId = eh2.DepartmentId where " +
                "d2.EffectiveEndDate is null and eh2.EffectiveEndDate is null and eh2.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId)AllocatorDepartmentName" +
                ",e.AllocatorPersonId" +
                ",CONCAT((select distinct p.FirstName from People p where e.AllocatorPersonId = p.PeopleId),' '" +
                ",(select distinct p.LastName from People p where e.AllocatorPersonId = p.PeopleId))AllocatorFullName " +
                "from  Evaluation e" +
                ", Task t" +
                ",evaluationHierarchies eh" +
                ", Departments d" +
                ",PeriodDefinitoion pd " +
                "where 1 = 1 " +
                "and t.TaskId = e.TaskId " +
                "and eh.EvaluationHierarchyId = e.RecieverAllocationEvaluationHierarchyId and eh.EffectiveEndDate is null " +
                "and eh.DepartmentId = d.DepartmentId and d.EffectiveEndDate is null " +
                "and pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "and t.ResourceType != 2 " +
                "and eh.SupervisorId = e.RecieverAllocationPersonId " +
                "and e.RecieverAllocationPersonId = @personId2 " +
                ")taskAssignment where 1 = 1 " +
                "and PeriodDefinitoionId = ISNULL(@periodDefinitionIdDTt2, PeriodDefinitoionId) " +
                "and DepartmentId = ISNULL(@departmentIdd2, DepartmentId) " +
                "and RecieverAllocationPersonId = @personId2 " +
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
                ",Title" +
                ",TaskWeight" +
                ",allocatorRoleName" +
                ",AllocatorPersonId" +
                ",AllocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",AllocatorDepartmentName " +
                "from(select " +
                rowNumber +
                ",e.EvaluationId" +
                ", e.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ", CONCAT((select distinct p.FirstName from People p where e.RecieverAllocationPersonId = p.PeopleId), ' '" +
                ", (select distinct p.LastName from People p where e.RecieverAllocationPersonId = p.PeopleId))fullName" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ",d.DepartmentId" +
                ",d.ShortName" +
                ",e.TaskId" +
                ",t.Type" +
                ",t.Title" +
                ",(select ar.Name from AspNetRoles ar where ar.Id = e.AllocatorRoleId)allocatorRoleName" +
                ",e.TaskWeight" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d2.ShortName from Departments d2 join evaluationHierarchies eh2 on d2.DepartmentId = eh2.DepartmentId where " +
                "d2.EffectiveEndDate is null and eh2.EffectiveEndDate is null and eh2.EvaluationHierarchyId = e.AllocatorEvaluationHierarchyId)AllocatorDepartmentName" +
                ",e.AllocatorPersonId" +
                ",CONCAT((select distinct p.FirstName from People p where e.AllocatorPersonId = p.PeopleId),' '" +
                ",(select distinct p.LastName from People p where e.AllocatorPersonId = p.PeopleId))AllocatorFullName " +
                "from  Evaluation e" +
                ", Task t" +
                ",evaluationHierarchies eh" +
                ", Departments d" +
                ",PeriodDefinitoion pd " +
                "where 1 = 1 " +
                "and t.TaskId = e.TaskId " +
                "and eh.EvaluationHierarchyId = e.RecieverAllocationEvaluationHierarchyId and eh.EffectiveEndDate is null " +
                "and eh.DepartmentId = d.DepartmentId and d.EffectiveEndDate is null " +
                "and pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "and t.ResourceType != 2 " +
                "and eh.SupervisorId = e.RecieverAllocationPersonId " +
                "and e.RecieverAllocationPersonId = @personId2 " +
                ")taskAssignment where 1 = 1 " +
                "and PeriodDefinitoionId = ISNULL(@periodDefinitionIdDTt2, PeriodDefinitoionId) " +
                "and DepartmentId = ISNULL(@departmentIdd2, DepartmentId) " +
                "and RecieverAllocationPersonId = @personId2 " +
                where +
                limit +
                order;

            conn.Open();
            List<object> query = null;
            if (dataTableParameter.length != -1 && dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2, personId2 = personId }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new { sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2, personId2 = personId }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2, personId2 = personId }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new { personId2 = personId }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new { sVal = "%" + dataTableParameter.search + "%", periodDefinitionIdDTt2 = periodDefinitionIdDT2, departmentIdd2 = departmentId2, personId2 = personId }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }

        public IQueryable GetDepartmentList(int periodDefinitionId, string roleId, string roleId2, int personId)
        {
            var query = from i in appDbContext.Evaluation
                        join eh in appDbContext.evaluationHierarchies on i.RecieverAllocationEvaluationHierarchyId equals eh.EvaluationHierarchyId
                        join d in appDbContext.Departments on eh.DepartmentId equals d.DepartmentId
                        //select new{ id2=i,dep=d.}
                        where (d.EffectiveEndDate == null && eh.EffectiveEndDate == null && i.PeriodDefinitoionId == periodDefinitionId && (i.AllocatorRoleId == roleId || i.AllocatorRoleId == roleId2) && i.RecieverAllocationPersonId == personId && eh.SupervisorId == personId)
                        select new { i.RecieverAllocationEvaluationHierarchyId, d.ShortName };

            return query.Distinct();
        }
        public int AddSubTask(List<SubTaskDivisionView> subTaskDivisionViews, int personId, string roleId)
        {
            ShareService shareService = new ShareService(appDbContext, null);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();

            List<Task> tasks = new List<Task>();

            foreach (var item in subTaskDivisionViews)
            {
                List<Criteria> criterias = new List<Criteria>();
                if (item.CriteriaViews != null)
                {
                    foreach (var criteriaItem in item.CriteriaViews)
                    {
                        criterias.Add(new Criteria
                        {
                            Title = criteriaItem.Title,
                            LimitOfAdmission = criteriaItem.limitOfAdmission,
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now,
                            PeriodDefinitionId=periodDefinitionId
                        });
                    }
                }
                Task task = new Task();
                task.ParentTaskId = item.TaskId;
                task.EvaluationHierarchyId = item.HierarchyId;
                task.Title = item.SubTaskTitle;
                if (item.CriteriaViews != null)
                {
                    task.Criterias = criterias;
                }
                task.IsActive = true;
                task.PeriodDefinitoionId = periodDefinitionId;
                task.RoleId = roleId;
                task.ResourceType = 3;
                task.CreatedBy = personId;
                task.CreatedDate = DateTime.Now;
                tasks.Add(task);
            }

            appDbContext.Task.AddRange(tasks);
            var result = appDbContext.SaveChanges();
            return result;
        }
        public List<Task> GetSubTaskList(int taskId, int personId, string roleId, int hierarchyId)
        {
            var query = appDbContext.Task.Where(c => c.ParentTaskId == taskId && c.CreatedBy == personId && c.EvaluationHierarchyId == hierarchyId && c.RoleId == roleId);
            if (query != null)
            {
                return query.OrderBy(c => c.TaskId).ToList();
            }
            return (null);
        }
        public List<Criteria> GetSubTaskAndCriterias(int taskId)
        {
            var query = appDbContext.Criteria.Include(c => c.Task).Where(c => c.TaskId == taskId);
            if (query != null)
            {
                return query.ToList();
            }
            return (null);
        }
        public int UpdatetSubTaskAndCriterias(TaskView taskView, int personId)
        {
            foreach (var item in taskView.CriteriaViews)
            {
                Criteria criteria = appDbContext.Criteria.Where(c => c.CriteriaId == item.CriteriaId).SingleOrDefault();
                if (criteria.Title != item.Title || criteria.LimitOfAdmission != item.limitOfAdmission)
                {
                    criteria.Title = item.Title;
                    criteria.LimitOfAdmission = item.limitOfAdmission;
                    criteria.LastUpdatedBy = personId;
                    criteria.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(criteria);
                }
            }
            Task task = appDbContext.Task.Where(c => c.TaskId == taskView.TaskId).SingleOrDefault();
            if (task.Title != taskView.TaskTitle)
            {
                task.Title = taskView.TaskTitle;
                task.LastUpdatedBy = personId;
                task.LastUpdatedDate = DateTime.Now;
            }
            int result = appDbContext.SaveChanges();
            return result;
        }
        public string DeleteCriteria(int criteriaId)
        {
            try
            {
                Criteria criteria = appDbContext.Criteria.Where(c => c.CriteriaId == criteriaId).SingleOrDefault();
                appDbContext.Remove(criteria);
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل وزن دهی شاخص مورد نظر امکان حذف وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }
        public string DeleteSubTask(int taskId)
        {
            try
            {
                Task task = appDbContext.Task.Where(c => c.TaskId == taskId).SingleOrDefault();
                appDbContext.Remove(task);
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل وجود شاخص یا تخصیص زیر فعالیت مورد نظر امکان حذف وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }
    }
}
