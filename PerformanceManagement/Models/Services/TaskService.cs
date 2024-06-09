using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.PlanningAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class TaskService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public TaskService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int AddTask(Task task)
        {
            appDbContext.Task.Add(task);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public Dictionary<object, object> GetTaskList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "TaskId", "Title", "Type", "IsActive", "PeriodDefinitoionId", "PeriodCode", "PeriodTitle" };
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
                "ROW_NUMBER() OVER(ORDER BY TaskId desc) As indexx" +
                ",TaskId" +
                ",Title" +
                ",Type" +
                ",IsActive" +
                ",PeriodDefinitoionId " +
                "from " +
                "Task t" +
                ",AspNetRoles anr " +
                "where " +
                "1 = 1 " +
                "and anr.Id = t.RoleId " +
                "and t.ResourceType=1 " +
                "and Name = 'PlanningAdmin' ";
            string queryFilteredTotal = "select " +
                "indexx" +
                ",TaskId" +
                ",Title" +
                ",Type" +
                ",IsActive" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle from(" +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY TaskId desc) As indexx" +
                ",TaskId" +
                ",Title" +
                ",Type" +
                ",IsActive" +
                ",t.PeriodDefinitoionId " +
                ",PeriodCode " +
                ",PeriodTitle " +
                "from " +
                "Task t" +
                ",AspNetRoles anr " +
                ",PeriodDefinitoion p " +
                "where " +
                "1 = 1 " +
                "and anr.Id = t.RoleId " +
                "and t.ResourceType=1 " +
                "and p.PeriodDefinitoionId=t.PeriodDefinitoionId " +
                "and Name = 'PlanningAdmin')Task where 1 = 1 " +
                where;
            string sQuery = @"select 
                            indexx
                            ,TaskId
                            ,Title
                            ,Type
                            ,IsActive
                            ,PeriodDefinitoionId
                            ,PeriodCode
                            ,PeriodTitle
                            ,ParentTaskId
                            from(
                            select
                            ROW_NUMBER() OVER(ORDER BY TaskId desc) As indexx
                            , TaskId
                            , Title
                            , Type
                            , IsActive
                            , t.PeriodDefinitoionId
                            , PeriodCode
                            , PeriodTitle
                            , ParentTaskId
                            from
                            Task t
                            , AspNetRoles anr
                            , PeriodDefinitoion p
                            where
                            1 = 1
                            and anr.Id = t.RoleId
                            and t.ResourceType = 1
                            and p.PeriodDefinitoionId = t.PeriodDefinitoionId
                            and Name = 'PlanningAdmin')Task where 1 = 1 " +
                where +
                limit +
                order;
            conn.Open();
            List<object> query = null;
            if (dataTableParameter.length != -1 && dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%" }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new { sVal = "%" + dataTableParameter.search + "%" }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%" }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new { sVal = "%" + dataTableParameter.search + "%" }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return (dictionary);
        }

        public string DeleteTaskDefinition(int periodDefinitoionId, int taskId)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                try
                {
                    Task task = appDbContext.Task.Where(c => c.TaskId == taskId).SingleOrDefault();
                    bool anyCriteria = appDbContext.Criteria.Where(c => c.TaskId == taskId).Any();


                    if (anyCriteria)
                    {
                        appDbContext.RemoveRange(appDbContext.Criteria.Where(c => c.TaskId == taskId).ToList());
                    }

                    appDbContext.Remove(task);


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
                    string message = "در صورتی که هدف مورد نظر را به فرد/افرادی اختصاص داده اید، ایتدا تخصیص ها را حذف و همچنین اگر وظایف دیگر به صورت آبشاری در راستای این وظیفه تعریف شده باشند باید آن وظایف را حذف و سپس دوباره سعی نمایید." + Environment.NewLine + Environment.NewLine + due.InnerException.Message;

                    return message;
                }
                catch (Exception e2)
                {
                    transaction.Rollback();
                    return e2.Message;
                }
            }
        }

        public int AddCriteria(List<AddCriteriaView> addCriteriaViews,int personId)
        {
            try
            {
                if (addCriteriaViews.Count>0)
                {
                    List<Criteria> criteria = new List<Criteria>();
                    foreach (var item in addCriteriaViews)
                    {
                        criteria.Add(new Criteria() { Title = item.Criteria, LimitOfAdmission = item.LimitOfAdmission, CriteriaType = item.CriteriaType, CalculationWay = item.CalculationWay, CreatedBy = personId, CreatedDate = DateTime.Now ,TaskId=item.TaskId,PeriodDefinitionId=item.PeriodDefinitoionId});
                    }
                    appDbContext.AddRange(criteria);
                    return appDbContext.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteCriteria(int criteriaId, int periodDefinitionId)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                try
                {
                    
                    Criteria criteria = appDbContext.Criteria.Where(c => c.CriteriaId == criteriaId && c.PeriodDefinitionId==periodDefinitionId).SingleOrDefault();
                    bool anyCriteriaWeight = appDbContext.CriteriaWeight.Where(c => c.CriteriaId == criteriaId).Any();

                    bool anyCriteriaWeightScore= appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == appDbContext.CriteriaWeight.Where(c2 => c2.CriteriaId == criteriaId).SingleOrDefault().CriteriaWeightId).Any();
                    
                    if (appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId) != criteria.PeriodDefinitionId || anyCriteriaWeightScore)
                    {
                        string message2 = " در صورتی که شاخص مورد نظر نمره دهی یا مربوط به دوره های قبل باشد آنگاه امکان حذف وجود ندارد.";
                        return message2;
                    }

                    if (anyCriteriaWeight)
                    {
                        appDbContext.Remove(appDbContext.CriteriaWeight.Where(c => c.CriteriaId == criteriaId).SingleOrDefault());
                    } 

                    if (appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId)==criteria.PeriodDefinitionId && !anyCriteriaWeightScore)
                    {
                        appDbContext.Remove(criteria);
                    }

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
                    string message = "در صورتی که شاخص مورد نظر نمره دهی شده یا مربوط به دوره های قبل باشد آنگاه امکان حذف وجود ندارد.." + Environment.NewLine + Environment.NewLine + due.InnerException.Message;

                    return message;
                }
                catch (Exception e2)
                {
                    transaction.Rollback();
                    return e2.Message;
                }
            }
        }

        public RelatedTaskView GetRelatedTask(int? parentTaskId)
        {
            var sQuery = @"SELECT TaskId
                          , Title
                          , Type
                          FROM Task
                          where TaskId = @parentTaskIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            RelatedTaskView relatedTask = null;

            relatedTask = conn.Query<RelatedTaskView>(sQuery, new { parentTaskIdd = parentTaskId }).SingleOrDefault();

            //conn.Close();
            conn.Dispose();
            return relatedTask;
        }

        public IEnumerable<object> TaskList()
        {
            string roleId = appDbContext.Roles.Where(c => c.Name == "PlanningAdmin").SingleOrDefault().Id;

            IDbConnection conn = connProvider.Connection;
            string sQuery = @"SELECT TaskId
                            ,Title
                            FROM Task
                            where
							1=1
							and PeriodDefinitoionId=(select MAX(PeriodDefinitoionId) from PeriodDefinitoion)
							and RoleId=@roleIdd";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                roleIdd = roleId,
            }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public int EditTaskDefinition(TaskDefinitionEditionView taskDefinitionEditionView, int personId)
        {
            Task task = appDbContext.Task.Where(c => c.TaskId == taskDefinitionEditionView.TaskId).SingleOrDefault();
            if (task.Title != taskDefinitionEditionView.Title || task.Type != taskDefinitionEditionView.Type || task.ParentTaskId!=taskDefinitionEditionView.ParentTaskId)
            {
                task.Title = taskDefinitionEditionView.Title;
                task.Type = taskDefinitionEditionView.Type;
                task.ParentTaskId = taskDefinitionEditionView.ParentTaskId;
                task.LastUpdatedBy = personId;
                task.LastUpdatedDate = DateTime.Now;
                appDbContext.Update(task);
            }

            if (taskDefinitionEditionView.Criterias != null)
            {
                foreach (var item in taskDefinitionEditionView.Criterias)
                {
                    Criteria criteria = appDbContext.Criteria.Where(c => c.CriteriaId == item.CriteriaId).SingleOrDefault();
                    if (criteria.Title != item.Title || criteria.LimitOfAdmission != item.LimitOfAdmission || criteria.CriteriaType != item.CriteriaType || criteria.CalculationWay != item.CalculationWay)
                    {
                        criteria.Title = item.Title;
                        criteria.LimitOfAdmission = item.LimitOfAdmission;
                        criteria.CriteriaType = item.CriteriaType;
                        criteria.CalculationWay = item.CalculationWay;
                        criteria.LastUpdatedBy = personId;
                        criteria.LastUpdatedDate = DateTime.Now;
                        appDbContext.Update(criteria);
                    }
                }
            }
            int result = appDbContext.SaveChanges();
            return result;
        }

        public List<Criteria> GetCriteriaList(int taskId)
        {
            var query = appDbContext.Criteria.Where(c => c.TaskId == taskId);
            List<Criteria> criteria = null;
            if (query.Count() > 0)
            {
                criteria = query.ToList();
            }
            return criteria;
        }
    }
}
