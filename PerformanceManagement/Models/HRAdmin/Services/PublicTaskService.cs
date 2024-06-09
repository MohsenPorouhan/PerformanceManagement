using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public class PublicTaskService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public PublicTaskService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public IQueryable GetTaskList()
        {
            var tasks = appDbContext.Task.Where(c => c.ParentTaskId == null && c.ResourceType == 1).Select(c => new { c.TaskId, c.Title });
            return tasks;
        }
        public int PublicTaskDefinition(string title, string description, int[] correspondentTask, string userId)
        {
            var personId = appDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            string roleId = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;
            var role = appDbContext.UserRoles.Where(c => c.UserId == userId && c.RoleId == roleId).SingleOrDefault();
            Task task = new Task();
            task.ResourceType = 2;
            task.Title = title;
            task.Description = description;
            task.IsActive = true;
            task.RoleId = role.RoleId;
            task.CreatedBy = personId;
            task.CreatedDate = DateTime.Now;
            List<PublicTask> publicTask = new List<PublicTask>();
            foreach (var item in correspondentTask)
            {
                publicTask.Add(new PublicTask() { CorrespondentTaskId = item, CreatedBy = personId, CreatedDate = DateTime.Now });
            }
            task.PublicTasks = publicTask;
            appDbContext.Add(task);
            var result = appDbContext.SaveChanges();
            return result;
        }

        public Dictionary<object, object> GetPuplicTaskList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "indexx", "TaskId", "Title", "Description", "correspondentTask" };
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
                ",TaskId" +
                ",Title" +
                ",Description" +
                ",correspondentTask " +
                ",CorrespondentTaskId " +
                ",IsActive " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY TaskId desc) As indexx" +
                ", TaskId" +
                ", Title" +
                ", Description" +
                ", STRING_AGG(correspondentTask, ' - ')correspondentTask " +
                ",STRING_AGG(CorrespondentTaskId,',')CorrespondentTaskId " +
                ",IsActive " +
                "from( " +
                "select " +
                "t.TaskId" +
                ", t.Title" +
                ", t.Description" +
                ", (select tt.Title from Task tt where tt.TaskId = pt.CorrespondentTaskId)correspondentTask " +
                ",pt.CorrespondentTaskId " +
                ",t.IsActive " +
                " from " +
                "Task t left " +
                "join PublicTask pt on t.TaskId = pt.TaskId " +
                "where " +
                "1 = 1 " +
                "and t.ResourceType = 2)PT " +
                "group by TaskId, Title, Description,IsActive)publicTask where 1=1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",TaskId" +
                ",Title" +
                ",Description" +
                ",correspondentTask " +
                ",CorrespondentTaskId " +
                ",IsActive " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY TaskId desc) As indexx" +
                ", TaskId" +
                ", Title" +
                ", Description" +
                ", STRING_AGG(correspondentTask, ' - ')correspondentTask " +
                ",STRING_AGG(CorrespondentTaskId,',')CorrespondentTaskId " +
                ",IsActive " +
                "from( " +
                "select " +
                "t.TaskId" +
                ", t.Title" +
                ", t.Description" +
                ", (select tt.Title from Task tt where tt.TaskId = pt.CorrespondentTaskId)correspondentTask " +
                ",pt.CorrespondentTaskId " +
                ",t.IsActive " +
                " from " +
                "Task t left " +
                "join PublicTask pt on t.TaskId = pt.TaskId " +
                "where " +
                "1 = 1 " +
                "and t.ResourceType = 2)PT " +
                "group by TaskId, Title, Description,IsActive)publicTask where 1=1 " +
                where;
            string sQuery = "select " +
                "indexx" +
                ",TaskId" +
                ",Title" +
                ",Description" +
                ",correspondentTask " +
                ",CorrespondentTaskId " +
                ",IsActive " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY TaskId desc) As indexx" +
                ", TaskId" +
                ", Title" +
                ", Description" +
                ", STRING_AGG(correspondentTask, ' - ')correspondentTask " +
                ",STRING_AGG(CorrespondentTaskId,',')CorrespondentTaskId " +
                ",IsActive " +
                "from( " +
                "select " +
                "t.TaskId" +
                ", t.Title" +
                ", t.Description" +
                ", (select tt.Title from Task tt where tt.TaskId = pt.CorrespondentTaskId)correspondentTask " +
                ",pt.CorrespondentTaskId " +
                ",t.IsActive " +
                " from " +
                "Task t left " +
                "join PublicTask pt on t.TaskId = pt.TaskId " +
                "where " +
                "1 = 1 " +
                "and t.ResourceType = 2)PT " +
                "group by TaskId, Title, Description,IsActive)publicTask where 1=1 " +
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

        public string EditPublicTaskAssignment(int? evaluationParticipantId, int taskWeight, int? departmentId, int? participentIdd, int evaluationId, int personId)
        {
            Evaluation evaluation = appDbContext.Evaluation.Where(c => c.EvaluationId == evaluationId).SingleOrDefault();
            if (evaluation.TaskWeight != taskWeight)
            {
                evaluation.TaskWeight = taskWeight;
                evaluation.LastUpdatedDate = DateTime.Now;
                evaluation.LastUpdatedBy = personId;
                appDbContext.Update(evaluation);
            }

            EvaluationParticipant evaluationParticipant = null;
            if (evaluationParticipantId != null)
            {
                evaluationParticipant = appDbContext.EvaluationParticipant.Where(c => c.EvaluationParticipantId == evaluationParticipantId).SingleOrDefault();
            }

            if (evaluationParticipantId != null && departmentId == null && participentIdd == null)
            {
                try
                {
                    appDbContext.Remove(evaluationParticipant);
                }
                catch (DbUpdateException due)
                {
                    string message = due.InnerException.Message;
                    return "notRemovedException";
                }
                catch (Exception e)
                {
                    string message = e.InnerException.Message;
                    return "notRemovedException";
                }
            }
            else if (evaluationParticipantId != null && departmentId != null && participentIdd != null
                && (evaluationParticipant.ParticipantId != participentIdd || evaluationParticipant.ParticipantEvaluationHierarchyId != departmentId))
            {
                evaluationParticipant.ParticipantId = participentIdd ?? throw new NoNullAllowedException();
                evaluationParticipant.ParticipantEvaluationHierarchyId = departmentId ?? throw new NoNullAllowedException();
                evaluationParticipant.LastUpdatedBy = personId;
                evaluationParticipant.LastUpdatedDate = DateTime.Now;
                appDbContext.Update(evaluationParticipant);
            }
            else if (evaluationParticipantId == null && departmentId != null && participentIdd != null)
            {
                EvaluationParticipant evaluationParticipant2 = new EvaluationParticipant();
                evaluationParticipant2.EvaluationId = evaluationId;
                evaluationParticipant2.ParticipantId = participentIdd ?? throw new NoNullAllowedException();
                evaluationParticipant2.ParticipantEvaluationHierarchyId = departmentId ?? throw new NoNullAllowedException();
                evaluationParticipant2.RequestDate = DateTime.Now;
                evaluationParticipant2.CreatedBy = personId;
                evaluationParticipant2.CreatedDate = DateTime.Now;
                appDbContext.Add(evaluationParticipant2);
            }
            int result = appDbContext.SaveChanges();
            return result.ToString();
        }
        public string PublicTaskDefinitionDeletion(int taskId)
        {
            try
            {
                appDbContext.RemoveRange(appDbContext.Criteria.Where(c => c.TaskId == taskId).ToList());
                appDbContext.Remove(appDbContext.Task.Where(c => c.TaskId == taskId).SingleOrDefault());
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل اختصاص وظیفه عمومی مورد نظر به کارمند/کارمندان یا ارتباط آن با وقایع حساس یا تعریف اهداف دیگر که در راستای این هدف می باشند،امکان حذف وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }
        public string PublicTaskAssignmentDeletion(int evaluationId)
        {
            try
            {
                if (appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluationId).Any())
                {
                    appDbContext.Remove(appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluationId).SingleOrDefault());
                }
                appDbContext.Remove(appDbContext.Evaluation.Where(c => c.EvaluationId == evaluationId).SingleOrDefault());
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل نمره دهی یا محاسبات پایان دوره یا ثبت وقایع حساس به وظیفه عمومی مورد نظر امکان حذف وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }

        public int ActivationPublicTaskDefinition(int taskId, bool isActivation, int personId)
        {
            Task task = appDbContext.Task.Where(c => c.TaskId == taskId).SingleOrDefault();
            task.IsActive = isActivation;
            task.LastUpdatedBy = personId;
            task.LastUpdatedDate = DateTime.Now;
            appDbContext.Update(task);

            int result = appDbContext.SaveChanges();

            return result;
        }

        public Dictionary<object, object> PublicAssignTask(int periodDefinitionId, string[] employee, int[] publicTask, int taskWeight, string participant, int personId, string roleId)
        {
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            ArrayList arrList = new ArrayList();

            var employeeId = 0;
            var hierarchyId = 0;
            foreach (var item in employee)
            {
                employeeId = int.Parse(item.Split('-')[1]);
                hierarchyId = int.Parse(item.Split('-')[0]);
                foreach (var itemPublicTask in publicTask)
                {
                    var query = from e in appDbContext.Evaluation
                                join t in appDbContext.Task on e.TaskId equals t.TaskId
                                join p in appDbContext.People on new { a = e.RecieverAllocationPersonId, b = e.RecieverAllocationEvaluationHierarchyId } equals new { a = p.PeopleId, b = p.EvaluationHierarchyID ?? Convert.ToInt32(0) }
                                join eh in appDbContext.evaluationHierarchies on e.RecieverAllocationEvaluationHierarchyId equals eh.EvaluationHierarchyId
                                join d in appDbContext.Departments on eh.DepartmentId equals d.DepartmentId
                                where (p.EffectiveEndDate == null && eh.EffectiveEndDate == null && d.EffectiveEndDate == null && e.RecieverAllocationPersonId == employeeId && e.RecieverAllocationEvaluationHierarchyId == hierarchyId && e.TaskId == itemPublicTask && e.PeriodDefinitoionId == periodDefinitionId && t.ResourceType == 2)
                                select new { t.Title, d.ShortName, p.FirstName, p.LastName };
                    var tableResult = query.SingleOrDefault();

                    if (tableResult != null)
                    {
                        arrList.Add(string.Format("وظیفه عمومی {0} به کارمند  {1} مربوط به واحد سازمانی {2} در دوره زمانی مورد نظر تکراری می باشد", tableResult.Title, tableResult.FirstName + ' ' + tableResult.LastName, tableResult.ShortName));
                    }
                    else
                    {
                        List<EvaluationParticipant> evaluationParticipant = null;
                        if (participant != null)
                        {
                            evaluationParticipant = new List<EvaluationParticipant>();

                            evaluationParticipant.Add(new EvaluationParticipant
                            {
                                ParticipantId = int.Parse(participant.Split('-')[1]),
                                ParticipantEvaluationHierarchyId = int.Parse(participant.Split('-')[0]),
                                RequestDate = DateTime.Now,
                                CreatedBy = personId,
                                CreatedDate = DateTime.Now
                            });
                        }

                        Evaluation evaluation = new Evaluation();
                        evaluation.AllocatorPersonId = personId;
                        evaluation.AllocatorRoleId = roleId;
                        evaluation.PeriodDefinitoionId = periodDefinitionId;
                        evaluation.TaskWeight = taskWeight;
                        evaluation.EvaluationAcceptanceStatusId = 1;
                        evaluation.RecieverAllocationPersonId = int.Parse(item.Split('-')[1]);
                        evaluation.RecieverAllocationEvaluationHierarchyId = int.Parse(item.Split('-')[0]);
                        evaluation.TaskId = itemPublicTask;
                        if (participant != null)
                        {
                            evaluation.EvaluationParticipants = evaluationParticipant;
                        }
                        evaluation.CreatedBy = personId;
                        evaluation.CreatedDate = DateTime.Now;

                        appDbContext.Evaluation.Add(evaluation);

                    }
                }
            }
            var result = appDbContext.SaveChanges();
            dictionary.Add("duplicate", arrList);
            dictionary.Add("result", result);
            return dictionary;
        }

        public Dictionary<object, object> GetAssignmentPublicTaskList(DataTableParameter dataTableParameter, string periodDefinitionIdDT2, string departmentIdDT2, string peopleIdDT2)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "UserName", "FullName", "ShortName", "Title", "Description", "TaskWeight", "participantFullName", "participantDepartmentName" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            //int exactOrder = dataTableParameter.orderColumn + 1;
            if (dataTableParameter.orderable == true)
            {
                order = "order by " + aColumns[dataTableParameter.orderColumn] + " " + dataTableParameter.orderDIR;
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
                ",FullName" +
                ",RecieverAllocationEvaluationHierarchyId" +
                ",ShortName" +
                ",EvaluationId" +
                ",TaskId" +
                ",Title" +
                ",TaskWeight" +
                ",EvaluationParticipantId" +
                ",participantFullName" +
                ",ParticipantEvaluationHierarchyId" +
                ",ParticipantId" +
                ",participantDepartmentName " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", e.RecieverAllocationPersonId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", e.RecieverAllocationEvaluationHierarchyId" +
                ", d.ShortName" +
                ", e.EvaluationId" +
                ", t.TaskId" +
                ", t.Title" +
                ", e.TaskWeight" +
                ", ep.EvaluationParticipantId" +
                ", (select CONCAT(pp.FirstName, ' ', pp.LastName) from People pp where pp.PeopleId = ep.ParticipantId and " +
                "pp.EvaluationHierarchyID = ep.ParticipantEvaluationHierarchyId and pp.EffectiveEndDate is null) participantFullName " +
                ",ep.ParticipantEvaluationHierarchyId" +
                ",ep.ParticipantId" +
                ",(select dd.ShortName " +
                "from People pp, evaluationHierarchies eh2,Departments dd " +
                "where pp.PeopleId = ep.ParticipantId " +
                "and pp.EvaluationHierarchyID = ep.ParticipantEvaluationHierarchyId " +
                "and eh2.EvaluationHierarchyId = ep.ParticipantEvaluationHierarchyId " +
                "and dd.DepartmentId = eh2.DepartmentId " +
                "and dd.EffectiveEndDate is null " +
                "and eh2.EffectiveEndDate is null " +
                "and pp.EffectiveEndDate is null) participantDepartmentName " +
                "from " +
                "Evaluation e join " +
                "Task t on e.TaskId = t.TaskId " +
                "join People p on p.EvaluationHierarchyID = e.RecieverAllocationEvaluationHierarchyId and e.RecieverAllocationPersonId = p.PeopleId " +
                "join evaluationHierarchies eh on e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId " +
                "join Departments d on eh.DepartmentId = d.DepartmentId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId " +
                "left join EvaluationParticipant ep on ep.EvaluationId = e.EvaluationId " +
                "where " +
                "1 = 1 " +
                "and p.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and t.ResourceType = 2)as tb where 1 = 1";
            string queryFilteredTotal = @"select 
                                        indexx
                                        ,PeriodDefinitoionId
                                        ,PeriodCode
                                        ,PeriodTitle
                                        ,RecieverAllocationPersonId
                                        ,UserName
                                        ,FullName
                                        ,RecieverAllocationEvaluationHierarchyId
                                        ,ShortName
                                        ,EvaluationId
                                        ,TaskId
                                        ,Title
                                        ,Description
                                        ,TaskWeight
                                        ,EvaluationParticipantId
                                        ,participantFullName
                                        ,ParticipantEvaluationHierarchyId
                                        ,ParticipantId
                                        ,participantDepartmentName
                                        from(
                                        select
                                        ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx
                                        , pd.PeriodDefinitoionId
                                        , pd.PeriodCode
                                        , pd.PeriodTitle
                                        , e.RecieverAllocationPersonId
                                        , anu.UserName
                                        , CONCAT(p.FirstName, ' ', p.LastName)FullName
                                        , e.RecieverAllocationEvaluationHierarchyId
                                        , d.ShortName
                                        , e.EvaluationId
                                        , t.TaskId
                                        , t.Title
                                        , t.Description
                                        , e.TaskWeight
                                        , ep.EvaluationParticipantId
                                        , (select CONCAT(pp.FirstName, ' ', pp.LastName) from People pp where pp.PeopleId = ep.ParticipantId and
                                        pp.EvaluationHierarchyID = ep.ParticipantEvaluationHierarchyId and pp.EffectiveEndDate is null) participantFullName
                                        ,ep.ParticipantEvaluationHierarchyId
                                        ,ep.ParticipantId
                                        ,(select dd.ShortName
                                        from People pp, evaluationHierarchies eh2,Departments dd
                                        where pp.PeopleId = ep.ParticipantId
                                        and pp.EvaluationHierarchyID = ep.ParticipantEvaluationHierarchyId
                                        and eh2.EvaluationHierarchyId = ep.ParticipantEvaluationHierarchyId
                                        and dd.DepartmentId = eh2.DepartmentId
                                        and dd.EffectiveEndDate is null
                                        and eh2.EffectiveEndDate is null
                                        and pp.EffectiveEndDate is null) participantDepartmentName
                                        from
                                        Evaluation e join
                                        Task t on e.TaskId = t.TaskId
                                        join People p on p.EvaluationHierarchyID = e.RecieverAllocationEvaluationHierarchyId and e.RecieverAllocationPersonId = p.PeopleId
                                        join AspNetUsers anu on p.PeopleId = anu.PeopleId
                                        join evaluationHierarchies eh on e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId
                                        join Departments d on eh.DepartmentId = d.DepartmentId
                                        join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId
                                        left join EvaluationParticipant ep on ep.EvaluationId = e.EvaluationId
                                        where
                                        1 = 1
                                        and p.EffectiveEndDate is null
                                        and eh.EffectiveEndDate is null
                                        and d.EffectiveEndDate is null
                                        and t.ResourceType = 2
                                        and pd.PeriodDefinitoionId=isnull(@periodDefinitoionIdd,pd.PeriodDefinitoionId)
                                        and e.RecieverAllocationEvaluationHierarchyId=isnull(@departmentIdd,e.RecieverAllocationEvaluationHierarchyId)
                                        and e.RecieverAllocationPersonId=isnull(@peopleIdd,e.RecieverAllocationPersonId)
                                        )as tb
                                        where 1 = 1" +
                where;
            string sQuery = @"select 
                            indexx
                            ,PeriodDefinitoionId
                            ,PeriodCode
                            ,PeriodTitle
                            ,RecieverAllocationPersonId
                            ,UserName
                            ,FullName
                            ,RecieverAllocationEvaluationHierarchyId
                            ,ShortName
                            ,EvaluationId
                            ,TaskId
                            ,Title
                            ,Description
                            ,TaskWeight
                            ,EvaluationParticipantId
                            ,participantFullName
                            ,ParticipantEvaluationHierarchyId
                            ,ParticipantId
                            ,participantDepartmentName
                            from(
                            select
                            ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx
                            , pd.PeriodDefinitoionId
                            , pd.PeriodCode
                            , pd.PeriodTitle
                            , e.RecieverAllocationPersonId
                            ,anu.UserName
                            , CONCAT(p.FirstName, ' ', p.LastName)FullName
                            , e.RecieverAllocationEvaluationHierarchyId
                            , d.ShortName
                            , e.EvaluationId
                            , t.TaskId
                            , t.Title
                            , t.Description
                            , e.TaskWeight
                            , ep.EvaluationParticipantId
                            , (select CONCAT(pp.FirstName, ' ', pp.LastName) from People pp where pp.PeopleId = ep.ParticipantId and
                            pp.EvaluationHierarchyID = ep.ParticipantEvaluationHierarchyId and pp.EffectiveEndDate is null) participantFullName
                            ,ep.ParticipantEvaluationHierarchyId
                            ,ep.ParticipantId
                            ,(select dd.ShortName
                            from People pp, evaluationHierarchies eh2,Departments dd
                            where pp.PeopleId = ep.ParticipantId
                            and pp.EvaluationHierarchyID = ep.ParticipantEvaluationHierarchyId
                            and eh2.EvaluationHierarchyId = ep.ParticipantEvaluationHierarchyId
                            and dd.DepartmentId = eh2.DepartmentId
                            and dd.EffectiveEndDate is null
                            and eh2.EffectiveEndDate is null
                            and pp.EffectiveEndDate is null) participantDepartmentName
                            from
                            Evaluation e join
                            Task t on e.TaskId = t.TaskId
                            join People p on p.EvaluationHierarchyID = e.RecieverAllocationEvaluationHierarchyId and e.RecieverAllocationPersonId = p.PeopleId
                            join AspNetUsers anu on p.PeopleId=anu.PeopleId
                            join evaluationHierarchies eh on e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId
                            join Departments d on eh.DepartmentId = d.DepartmentId
                            join PeriodDefinitoion pd on pd.PeriodDefinitoionId = e.PeriodDefinitoionId
                            left join EvaluationParticipant ep on ep.EvaluationId = e.EvaluationId
                            where
                            1 = 1
                            and p.EffectiveEndDate is null
                            and eh.EffectiveEndDate is null
                            and d.EffectiveEndDate is null
                            and t.ResourceType = 2
                            and pd.PeriodDefinitoionId=isnull(@periodDefinitoionIdd,pd.PeriodDefinitoionId)
                            and e.RecieverAllocationEvaluationHierarchyId=isnull(@departmentIdd,e.RecieverAllocationEvaluationHierarchyId)
                            and e.RecieverAllocationPersonId=isnull(@peopleIdd,e.RecieverAllocationPersonId)
                            )as tb 
                            where 1 = 1" +
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
                    periodDefinitoionIdd = periodDefinitionIdDT2,
                    departmentIdd = departmentIdDT2,
                    peopleIdd = peopleIdDT2
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    periodDefinitoionIdd = periodDefinitionIdDT2,
                    departmentIdd = departmentIdDT2,
                    peopleIdd = peopleIdDT2
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    periodDefinitoionIdd = periodDefinitionIdDT2,
                    departmentIdd = departmentIdDT2,
                    peopleIdd = peopleIdDT2
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                periodDefinitoionIdd = periodDefinitionIdDT2,
                departmentIdd = departmentIdDT2,
                peopleIdd = peopleIdDT2
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return (dictionary);
        }

        public int EditPublicTask(int taskId, string title, string description, int[] correspondentTask, int personId)
        {
            Task task = appDbContext.Task.Where(c => c.TaskId == taskId).SingleOrDefault();
            task.Title = title;
            task.Description = description;
            task.LastUpdatedBy = personId;
            task.LastUpdatedDate = DateTime.Now;
            appDbContext.Update(task);

            appDbContext.RemoveRange(appDbContext.PublicTask.Where(c => c.TaskId == taskId).ToList());

            List<PublicTask> publicTask = new List<PublicTask>();

            foreach (var item in correspondentTask)
            {
                publicTask.Add(new PublicTask() { TaskId = taskId, CorrespondentTaskId = item, CreatedBy = personId, CreatedDate = DateTime.Now, LastUpdatedBy = personId, LastUpdatedDate = DateTime.Now });
            }
            appDbContext.AddRange(publicTask);

            var result = appDbContext.SaveChanges();

            return result;
        }
        public List<object> GetPublicTaskDepartment(int periodDefinitionId)
        {
            var sQuery = @"select 
                        distinct
                        eh.EvaluationHierarchyId
                        ,d.DepartmentId
                        ,concat(d.ShortName,' (',p.FirstName,' ',p.LastName,'-',anu.UserName,')')ShortName
                        from
                        Evaluation e join Task t on e.TaskId = t.TaskId
                        join evaluationHierarchies eh on e.RecieverAllocationEvaluationHierarchyId = eh.EvaluationHierarchyId
                        join Departments d on eh.DepartmentId = d.DepartmentId
                        join People p on eh.SupervisorId=p.PeopleId and eh.EvaluationHierarchyId=p.EvaluationHierarchyID
                        join AspNetUsers anu on p.PeopleId=anu.PeopleId
                        where
                        1 = 1
                        and t.ResourceType = 2
                        and e.PeriodDefinitoionId = @periodDefinitionIdd
                        and eh.EffectiveEndDate is null
                        and d.EffectiveEndDate is null
                        and p.EffectiveEndDate is null";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> departmentList = null;

            departmentList = conn.Query<object>(sQuery, new { periodDefinitionIdd = periodDefinitionId }).ToList();

            //conn.Close();
            conn.Dispose();
            return departmentList;
        }
        public List<object> GetPublicTaskReceiver(int departmentId)
        {
            var sQuery = @"select distinct
                        p.PeopleId
                        ,anu.UserName
                        ,CONCAT(p.FirstName,' ',LastName,' (',anu.UserName,')')FullName
                        from 
                        People p join AspNetUsers anu on p.PeopleId=anu.PeopleId
                        join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=p.EvaluationHierarchyID and e.RecieverAllocationPersonId=p.PeopleId
                        join Task t on e.TaskId=t.TaskId
                        where 
                        1=1
                        and t.ResourceType=2
                        and p.EffectiveEndDate is null
                        and p.EvaluationHierarchyID=@departmentIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> receiverList = null;

            receiverList = conn.Query<object>(sQuery, new { departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return receiverList;
        }

    }
}
