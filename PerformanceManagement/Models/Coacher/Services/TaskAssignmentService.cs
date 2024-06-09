using Dapper;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PerformanceManagement.Models.Coacher.View;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class TaskAssignmentService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public TaskAssignmentService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public IEnumerable<object> GetDepartmentSupervisorList(int personIdd)
        {
            IDbConnection conn = connProvider.Connection;
            string departmentIdParam = "ParentEvaluationHierarchyId";
            string sQuery = BuildDepartmentQuery(departmentIdParam);
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { personId = personIdd }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public string BuildDepartmentQuery(string departmentIdParam)
        {
            string sQuery = @"
WITH EmpsCTE AS ( 
               select 
               eh.EvaluationHierarchyId
               ,p.PeopleId
               ,eh.SupervisorId
               ,eh.ParentEvaluationHierarchyId
               ,CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
               ,0 Levell 
               ,p.PositionType 
               from 
               evaluationHierarchies eh
               ,Departments d
               ,People p 
               where 1 = 1 
               and eh.DepartmentId = d.DepartmentId 
               and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
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
               ,C.PositionType 
               FROM EmpsCTE AS P 
               JOIN(select 
               eh.EvaluationHierarchyId
               ,p.PeopleId
               ,eh.SupervisorId
               ,eh.ParentEvaluationHierarchyId
               ,CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName 
               ,p.PositionType 
               from 
               evaluationHierarchies eh
               ,Departments d
               ,People p 
               where 1 = 1 
               and eh.DepartmentId = d.DepartmentId 
               and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
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
               ,PeopleId 
               ,PositionType 
               FROM EmpsCTE 
               where 
               1=1 
               -- and(ParentEvaluationHierarchyId in (  departmentIdParam   )) 
               and PeopleId = @personId
               order by 1";
            return sQuery;
        }

        public IEnumerable<object> GetParentDepartmentList(int departmentId, int level, int personIdd)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", @levelll Levell " +
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
                "and p.PeopleId = @personId " +
                "and eh.[EvaluationHierarchyId] = @departmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1   EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",@levelll Levell " +
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
                "and p.PeopleId = @personId " +
                "and eh.[EvaluationHierarchyId]= @departmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell-1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) " +
                "SELECT[EvaluationHierarchyId] as id" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell " +
                "FROM EmpsCTE order by 1";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { levelll = level, personId = personIdd, departmentIdd = departmentId }).ToList();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public IEnumerable<object> GetDirectEmployee(int departmentId)
        {
            var sQuery = "select " +
                "e.ParentEvaluationHierarchyId" +
                ",e.EvaluationHierarchyId" +
                ",d.ShortName" +
                ",p.PeopleId" +
                ",concat(p.FirstName, ' ', p.LastName, ' (', d.ShortName, ')')fullname" +
                ",e.SupervisorId " +
                "from " +
                "evaluationHierarchies e" +
                ", Departments d" +
                ",People p " +
                "where e.EvaluationHierarchyId = @departmentIdd " +
                "and d.DepartmentId = e.DepartmentId " +
                "and p.EvaluationHierarchyID = e.EvaluationHierarchyId " +
                "and p.PeopleId != e.SupervisorId " +
                "and p.EffectiveEndDate is null " +
                "and e.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "union " +
                "select " +
                "e.ParentEvaluationHierarchyId" +
                ",e.EvaluationHierarchyId" +
                ",d.ShortName" +
                ",p.PeopleId" +
                ",concat(p.FirstName, ' ', p.LastName, ' (', d.ShortName, ')')fullname" +
                ",e.SupervisorId " +
                "from " +
                "evaluationHierarchies e" +
                ", Departments d" +
                ",People p " +
                "where " +
                "1 = 1 " +
                "and e.ParentEvaluationHierarchyId = @departmentIdd " +
                "and d.DepartmentId = e.DepartmentId " +
                "and p.PeopleId = e.SupervisorId " +
                "and p.EvaluationHierarchyID = e.EvaluationHierarchyId " +
                "and p.EffectiveEndDate is null " +
                "and e.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> GetParentOfSubTaskList(int departmentId, int supervicorId)
        {
            var sQuery = "select " +
                "t.TaskId" +
                ",t.Title" +
                ",t.EvaluationHierarchyId" +
                ",t.CreatedBy" +
                ",e.EvaluationId " +
                "from " +
                "Task t join Evaluation e on t.TaskId = e.TaskId " +
                "where " +
                "1 = 1 " +
                "and e.RecieverAllocationEvaluationHierarchyId = @departmentIdd " +
                "and e.RecieverAllocationPersonId = @supervisorIdd " +
                "and t.ResourceType in(1,3,4)";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId, supervisorIdd = supervicorId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> GetSubTaskList(int departmentId, int supervicorId, int parentDepartmentId)
        {
            var sQuery = "select " +
                "t.TaskId" +
                ",t.ParentTaskId" +
                ",t.Title" +
                ",t.EvaluationHierarchyId" +
                ",t.CreatedBy " +
                "from " +
                "Task t " +
                "where " +
                "1 = 1 " +
                "and t.ParentTaskId = @parentDepartmentIdd " +
                "and t.CreatedBy = @supervisorIdd " +
                "and t.EvaluationHierarchyId = @departmentIdd " +
                "and t.ResourceType = 3";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId, supervisorIdd = supervicorId, parentDepartmentIdd = parentDepartmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public Dictionary<object, object> AssignTask(CoveredEmployee coveredEmployee, int allocatorId, string roleId)
        {
            List<Evaluation> evalList = new List<Evaluation>();
            List<Evaluation> evalTextList = new List<Evaluation>();
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            List<object> duplicateList = new List<object>();
            if (coveredEmployee.subTaskViews.Count() > 0)
            {
                foreach (var item2 in coveredEmployee.subTaskViews)
                {
                    foreach (var item in coveredEmployee.Receiver)
                    {
                        int receiverId = int.Parse(item.Split("-")[0]);
                        int receiverDepartmentId = int.Parse(item.Split("-")[1]);
                        var taskQueryDuplicate = from i in appDbContext.evaluationHierarchies
                                                 join d in appDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                                 join e in appDbContext.Evaluation on i.EvaluationHierarchyId equals e.RecieverAllocationEvaluationHierarchyId
                                                 join t in appDbContext.Task on e.TaskId equals t.TaskId
                                                 join p in appDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                                                 //select new{ id2=i,dep=d.}
                                                 where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && p.EffectiveEndDate == null && t.TaskId == item2.SubTaskId && p.PeopleId == receiverId && e.RecieverAllocationEvaluationHierarchyId == receiverDepartmentId)
                                                 select new { d.ShortName, p.FirstName, p.LastName, t.Title };
                        if (taskQueryDuplicate.SingleOrDefault() != null)
                        {
                            var r = taskQueryDuplicate.SingleOrDefault();
                            duplicateList.Add(string.Format("{0} به آقای {1} مربوط به واحد {2} در دوره مورد نظر تخصیص داده شده است.", r.Title, r.FirstName + " " + r.LastName, r.ShortName));
                        }
                        else
                        {
                            Evaluation evaluation = new Evaluation();
                            evaluation.RecieverAllocationPersonId = receiverId;
                            evaluation.RecieverAllocationEvaluationHierarchyId = receiverDepartmentId;
                            evaluation.PeriodDefinitoionId = coveredEmployee.PeriodDefinitionId;
                            evaluation.AllocatorPersonId = allocatorId;
                            evaluation.AllocatorEvaluationHierarchyId = coveredEmployee.AllocatorDepartmentId;
                            evaluation.TaskId = item2.SubTaskId;
                            evaluation.EvaluationAcceptanceStatusId = 1;
                            evaluation.AllocatorRoleId = roleId;
                            evaluation.CreatedBy = allocatorId;
                            evaluation.CreatedDate = DateTime.Now;
                            if (item2.ParticipantId != null)
                            {
                                List<EvaluationParticipant> evalParticipants = new List<EvaluationParticipant>();
                                EvaluationParticipant evaluationParticipant = new EvaluationParticipant();
                                evaluationParticipant.ParticipantId = item2.ParticipantId ?? 0;
                                evaluationParticipant.ParticipantEvaluationHierarchyId = item2.ParticipantDepartmentId ?? 0;
                                evaluationParticipant.RequestDate = DateTime.Now;
                                evaluationParticipant.CreatedBy = allocatorId;
                                evaluationParticipant.CreatedDate = DateTime.Now;
                                evalParticipants.Add(evaluationParticipant);
                                evaluation.EvaluationParticipants = evalParticipants;
                            }
                            if (item2.TrainingNeed != "")
                            {
                                List<TrainingNeed> tNeeds = new List<TrainingNeed>();
                                foreach (var item3 in item2.TrainingNeed.Split(","))
                                {
                                    TrainingNeed trainingNeed = new TrainingNeed();
                                    trainingNeed.Title = item3;
                                    trainingNeed.CreatedBy = allocatorId;
                                    trainingNeed.CreatedDate = DateTime.Now;
                                    tNeeds.Add(trainingNeed);
                                }
                                evaluation.TrainingNeeds = tNeeds;
                            }
                            evalList.Add(evaluation);
                        }

                    }
                }
                dictionary.Add("duplicate", duplicateList);
                appDbContext.AddRange(evalList);
            }

            if (coveredEmployee.textTaskViews.Count() > 0)
            {
                foreach (var item2 in coveredEmployee.textTaskViews)
                {
                    List<Criteria> criteriaList = new List<Criteria>();
                    Task task = new Task();
                    task.Title = item2.TextTask;
                    task.PeriodDefinitoionId = coveredEmployee.PeriodDefinitionId;
                    task.RoleId = roleId;
                    task.IsActive = true;
                    task.ParentTaskId = item2.ParentTaskId;
                    task.CreatedBy = allocatorId;
                    task.CreatedDate = DateTime.Now;
                    task.ResourceType = 4;
                    task.EvaluationHierarchyId = coveredEmployee.AllocatorDepartmentId;
                    if (item2.CriteriaViews.Count > 0)
                    {
                        foreach (var criteriaItem in item2.CriteriaViews)
                        {

                            Criteria criteria = new Criteria();
                            criteria.Title = criteriaItem.Title;
                            criteria.LimitOfAdmission = criteriaItem.limitOfAdmission;
                            criteria.CalculationWay = criteriaItem.CalculationWay;
                            criteria.CreatedBy = allocatorId;
                            criteria.CreatedDate = DateTime.Now;
                            criteria.PeriodDefinitionId = appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId);
                            criteria.IsProcessingCriteria = criteriaItem.IsProcessingCriteria;
                            criteriaList.Add(criteria);
                        }
                    }
                    else
                    {
                        dictionary.Add("criteriaRestriction", "وجود حداقل یک شاخص برای هر وظیه الزامی می باشد");
                        return dictionary;
                    }
                    task.Criterias = criteriaList;
                    foreach (var item in coveredEmployee.Receiver)
                    {
                        int receiverId = int.Parse(item.Split("-")[0]);
                        int receiverDepartmentId = int.Parse(item.Split("-")[1]);
                        Evaluation evaluation = new Evaluation();
                        evaluation.RecieverAllocationPersonId = receiverId;
                        evaluation.RecieverAllocationEvaluationHierarchyId = receiverDepartmentId;
                        evaluation.PeriodDefinitoionId = coveredEmployee.PeriodDefinitionId;
                        evaluation.AllocatorPersonId = allocatorId;
                        evaluation.AllocatorEvaluationHierarchyId = coveredEmployee.AllocatorDepartmentId;
                        evaluation.Task = task;
                        evaluation.AllocatorRoleId = roleId;
                        evaluation.EvaluationAcceptanceStatusId = 1;
                        evaluation.CreatedBy = allocatorId;
                        evaluation.CreatedDate = DateTime.Now;
                        if (item2.ParticipantId != null)
                        {
                            List<EvaluationParticipant> evalParticipants = new List<EvaluationParticipant>();
                            EvaluationParticipant evaluationParticipant = new EvaluationParticipant();
                            evaluationParticipant.ParticipantId = item2.ParticipantId ?? 0;
                            evaluationParticipant.ParticipantEvaluationHierarchyId = item2.ParticipantDepartmentId ?? 0;
                            evaluationParticipant.RequestDate = DateTime.Now;
                            evaluationParticipant.CreatedBy = allocatorId;
                            evaluationParticipant.CreatedDate = DateTime.Now;
                            evalParticipants.Add(evaluationParticipant);
                            evaluation.EvaluationParticipants = evalParticipants;
                        }
                        if (item2.TrainingNeed != "")
                        {
                            List<TrainingNeed> tNeeds = new List<TrainingNeed>();
                            foreach (var item3 in item2.TrainingNeed.Split(","))
                            {
                                TrainingNeed trainingNeed = new TrainingNeed();
                                trainingNeed.Title = item3;
                                trainingNeed.CreatedBy = allocatorId;
                                trainingNeed.CreatedDate = DateTime.Now;
                                tNeeds.Add(trainingNeed);
                            }
                            evaluation.TrainingNeeds = tNeeds;
                        }
                        evalTextList.Add(evaluation);
                    }
                }
                appDbContext.AddRange(evalTextList);
            }
            int result = appDbContext.SaveChanges();
            dictionary.Add("result", result);
            return dictionary;
        }
        public Dictionary<object, object> TransferTaskAssignment(EmployeeView employeeView, int allocatorId, string roleId, int priorPriodDefinitionId, int periodDefinitionId)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                Dictionary<object, object> dictionary = new Dictionary<object, object>();

                try
                {
                    int result = 0;
                    List<string> evalDuplicateList = new List<string>();

                    if (employeeView.Receiver.Count() > 0)
                    {
                        foreach (var item in employeeView.Receiver)
                        {

                            int receiverId = int.Parse(item.Split("-")[0]);
                            int receiverDepartmentId = int.Parse(item.Split("-")[1]);

                            List<Evaluation> priorEvaluationList = appDbContext.Evaluation.Where(c =>
                                c.RecieverAllocationPersonId == receiverId &&
                                c.RecieverAllocationEvaluationHierarchyId == receiverDepartmentId &&
                                c.AllocatorPersonId == allocatorId &&
                                c.PeriodDefinitoionId == priorPriodDefinitionId &&
                                (c.RefutationCause != "3" && c.RefutationCause != "4" || c.RefutationCause == null)).ToList();
                            foreach (var priorEvaluation in priorEvaluationList)
                            {
                                bool isDuplicateTransition = appDbContext.Evaluation.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.PriorEvaluationId == priorEvaluation.EvaluationId).Any();
                                if (!isDuplicateTransition)
                                {
                                    Task priorTask = appDbContext.Task.Where(c => c.TaskId == priorEvaluation.TaskId).SingleOrDefault();
                                    List<Criteria> priorCriteriaList = appDbContext.Criteria.Where(c => c.TaskId == priorTask.TaskId).ToList();
                                    List<TrainingNeed> trainingNeeds = appDbContext.TrainingNeed.Where(c => c.EvaluationId == priorEvaluation.EvaluationId).ToList();
                                    List<Criteria> criteriaList = new List<Criteria>();
                                    List<CriteriaWeight> criteriaWeights = new List<CriteriaWeight>();
                                    Evaluation evaluation = new Evaluation();

                                    Task task = new Task();
                                    task.Title = priorTask.Title;
                                    task.PeriodDefinitoionId = periodDefinitionId;
                                    task.RoleId = roleId;
                                    task.IsActive = true;
                                    task.ParentTaskId = priorTask.ParentTaskId;
                                    task.CreatedBy = allocatorId;
                                    task.CreatedDate = DateTime.Now;
                                    task.ResourceType = 4;
                                    task.EvaluationHierarchyId = employeeView.AllocatorDepartmentId;
                                    if (priorCriteriaList.Count > 0)
                                    {
                                        foreach (var prioCriteriaItem in priorCriteriaList)
                                        {

                                            Criteria criteria = new Criteria();
                                            criteria.Title = prioCriteriaItem.Title;
                                            criteria.LimitOfAdmission = prioCriteriaItem.LimitOfAdmission;
                                            criteria.CalculationWay = prioCriteriaItem.CalculationWay;
                                            criteria.CreatedBy = allocatorId;
                                            criteria.CreatedDate = DateTime.Now;
                                            criteria.PeriodDefinitionId = periodDefinitionId;
                                            criteria.IsProcessingCriteria = prioCriteriaItem.IsProcessingCriteria;
                                            criteria.PriorCriteriaId = prioCriteriaItem.CriteriaId;
                                            criteriaList.Add(criteria);
                                        }

                                    }
                                    else
                                    {
                                        dictionary.Add("criteriaRestriction", "وجود حداقل یک شاخص برای هر وظیه الزامی می باشد");
                                        return dictionary;
                                    }
                                    task.Criterias = criteriaList;

                                    evaluation.RecieverAllocationPersonId = receiverId;
                                    evaluation.RecieverAllocationEvaluationHierarchyId = receiverDepartmentId;
                                    evaluation.PeriodDefinitoionId = periodDefinitionId;
                                    evaluation.AllocatorPersonId = allocatorId;
                                    evaluation.AllocatorEvaluationHierarchyId = employeeView.AllocatorDepartmentId;
                                    evaluation.Task = task;
                                    evaluation.AllocatorRoleId = roleId;
                                    evaluation.EvaluationAcceptanceStatusId = 1;
                                    evaluation.CreatedBy = allocatorId;
                                    evaluation.CreatedDate = DateTime.Now;
                                    evaluation.IsPriorPeriodTransition = true;
                                    evaluation.PriorPeriodDefinitionId = priorPriodDefinitionId;
                                    evaluation.PriorEvaluationId = priorEvaluation.EvaluationId;
                                    evaluation.TaskWeight = priorEvaluation.TaskWeight;

                                    //if (item.ParticipantId != null)
                                    //{
                                    //    List<EvaluationParticipant> evalParticipants = new List<EvaluationParticipant>();
                                    //    EvaluationParticipant evaluationParticipant = new EvaluationParticipant();
                                    //    evaluationParticipant.ParticipantId = item2.ParticipantId ?? 0;
                                    //    evaluationParticipant.ParticipantEvaluationHierarchyId = item2.ParticipantDepartmentId ?? 0;
                                    //    evaluationParticipant.RequestDate = DateTime.Now;
                                    //    evaluationParticipant.CreatedBy = allocatorId;
                                    //    evaluationParticipant.CreatedDate = DateTime.Now;
                                    //    evalParticipants.Add(evaluationParticipant);
                                    //    evaluation.EvaluationParticipants = evalParticipants;
                                    //}
                                    if (trainingNeeds.Count() > 0)
                                    {
                                        List<TrainingNeed> tNeeds = new List<TrainingNeed>();
                                        foreach (var item3 in trainingNeeds)
                                        {
                                            TrainingNeed trainingNeed = new TrainingNeed();
                                            trainingNeed.Title = item3.Title;
                                            trainingNeed.CreatedBy = allocatorId;
                                            trainingNeed.CreatedDate = DateTime.Now;
                                            tNeeds.Add(trainingNeed);
                                        }
                                        evaluation.TrainingNeeds = tNeeds;
                                    }

                                    appDbContext.Add(evaluation);
                                    result = appDbContext.SaveChanges();

                                    foreach (var newCriteriaItem in criteriaList)
                                    {

                                        CriteriaWeight priorCriteriaWeight = appDbContext.CriteriaWeight.Where(c => c.EvaluationId == priorEvaluation.EvaluationId &&
                                            c.CriteriaId == newCriteriaItem.PriorCriteriaId).SingleOrDefault();

                                        CriteriaWeight criteriaWeight = new CriteriaWeight();
                                        criteriaWeight.CriteriaId = newCriteriaItem.CriteriaId;
                                        criteriaWeight.EvaluationId = evaluation.EvaluationId;
                                        criteriaWeight.Weight = priorCriteriaWeight.Weight;
                                        criteriaWeight.RoleId = roleId;
                                        criteriaWeight.CreatedBy = allocatorId;
                                        criteriaWeight.CreatedDate = DateTime.Now;
                                        criteriaWeights.Add(criteriaWeight);

                                    }
                                    appDbContext.AddRange(criteriaWeights);
                                    appDbContext.SaveChanges();
                                }

                                else
                                {
                                    evalDuplicateList.Add(appDbContext.Task.Where(c => c.TaskId == priorEvaluation.TaskId).SingleOrDefault().Title);
                                }

                            }

                        }
                        dictionary.Add("duplicate", evalDuplicateList);
                    }
                    transaction.Commit();
                    dictionary.Add("result", result);
                    return dictionary;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    dictionary.Add("exception", e.Message);
                    return dictionary;
                }

            }

        }
        public Dictionary<object, object> GetAssignedTaskList(DataTableParameter dataTableParameter, int personId, int primaryDepartmentId, string employee = null, string periodDefinitionIdDT2 = null)
        {
            int? personIdDT = null;
            int? departmentIdDT = null;
            //int? periodDefinitionIdDT2Param = null;
            int? periodDefinitionIdDT2Param = int.Parse(periodDefinitionIdDT2);
            if (employee != null && employee != "")
            {
                personIdDT = int.Parse(employee.Split("-")[0]);
                departmentIdDT = int.Parse(employee.Split("-")[1]);
            }
            else
            {
                //periodDefinitionIdDT2Param = null;
            }
            String[] aColumns = { "PeriodCode", "PeriodTitle", "allocatorFullName", "allocatorRoleName", "allocatorDepartmentName", "text", "Title", "TaskWeight", "ResourceType" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            //int exactOrder = dataTableParameter.orderColumn + 1;
            //if (exactOrder == 9)
            //{
            //    exactOrder += 5;
            //}
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

            string queryTotalResult = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
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
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId " +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ",id" +
                ",text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationId" +
                ", TaskId" +
                ", Title" +
                ",RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",allocatorDepartmentName" +
                ",allocatorRoleName" +
                ",AllocatorRoleId" +
                ",ResourceType" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",PeriodDefinitoionId" +
                ",TaskWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx" +
                ",EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", e.EvaluationId" +
                ", e.TaskId" +
                ", t.Title" +
                ",e.RecieverAllocationPersonId" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ", e.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId=e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",e.AllocatorRoleId" +
                ",t.ResourceType" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",e.TaskWeight " +
                ",e.EvaluationAcceptanceStatusId " +
                ",(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant " +
                ",(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=[EvalHierarchyId] " +
                "and e.RecieverAllocationPersonId= PeopleId " +
                "join Task t on t.TaskId= e.TaskId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= e.PeriodDefinitoionId " +
                "where Levell!=1" +
                "and e.PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt22,e.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2,RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationHierarchyId = ISNULL(@departmentIdDT2,RecieverAllocationEvaluationHierarchyId) " +
                "and (Levell in(2,3) or AllocatorPersonId=@personIdd) " +
                ")dd where 1=1 and allocatorRoleName!='PlanningAdmin' ";

            string queryFilteredTotal = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
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
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId " +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ",id" +
                ",text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationId" +
                ", TaskId" +
                ", Title" +
                ",RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",allocatorDepartmentName" +
                ",allocatorRoleName" +
                ",AllocatorRoleId" +
                ",ResourceType" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",PeriodDefinitoionId" +
                ",TaskWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx" +
                ",EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", e.EvaluationId" +
                ", e.TaskId" +
                ", t.Title" +
                ",e.RecieverAllocationPersonId" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ", e.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId=e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",e.AllocatorRoleId" +
                ",t.ResourceType" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",e.TaskWeight " +
                ",e.EvaluationAcceptanceStatusId " +
                ",(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant " +
                ",(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=[EvalHierarchyId] " +
                "and e.RecieverAllocationPersonId= PeopleId " +
                "join Task t on t.TaskId= e.TaskId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= e.PeriodDefinitoionId " +
                "where Levell!=1" +
                "and e.PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt22,e.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2,RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationHierarchyId = ISNULL(@departmentIdDT2,RecieverAllocationEvaluationHierarchyId) " +
                "and (Levell in(2,3) or AllocatorPersonId=@personIdd) " +
                ")dd where 1=1 and allocatorRoleName!='PlanningAdmin' " +
                where;

            string sQuery = @"WITH EmpsCTE AS(
                select 
                eh.EvaluationHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                ,eh.EvaluationHierarchyId[EvalHierarchyId]
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 1 Levell 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and eh.SupervisorId = p.PeopleId 
                and p.PeopleId = @personIdd 
                and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd 
                union 
                select 
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , eh.EvaluationHierarchyId[EvalHierarchyId]
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,4 Levell 
                from 
                evaluationHierarchies eh
                ,Departments d
                ,People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and p.EvaluationHierarchyID is not null 
                and eh.SupervisorId != p.PeopleId 
                and p.PeopleId = @personIdd 
                and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd 
                UNION ALL 
                SELECT C.EvaluationHierarchyId 
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.[EvalHierarchyId]
                , C.ShortName
                , Levell+1 levell 
                FROM EmpsCTE AS P 
                JOIN dbo.ChartConfirmationn AS C 
                ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) 
                select 
                indexx
                ,id
                ,text
                , parent
                , [EvalHierarchyId]
                , Levell
                , EvaluationId
                , TaskId
                , Title
				,ParentTaskId
                ,RecieverAllocationPersonId
                , RecieverAllocationEvaluationHierarchyId
                , AllocatorPersonId
                , allocatorFullName
                ,AllocatorEvaluationHierarchyId
                ,allocatorDepartmentName
                ,allocatorRoleName
                ,AllocatorRoleId
                ,ResourceType
                ,PeriodCode
                ,PeriodTitle
                ,PeriodDefinitoionId
                ,TaskWeight 
                ,EvaluationAcceptanceStatusId 
                ,hasParticipant 
                ,participantConfirmation 
                from( 
                SELECT 
                ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx
                ,EmpsCTE.[EvaluationHierarchyId] as id
                , [ShortName] as text
                ,case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent
                , [EvalHierarchyId]
                , Levell
                , e.EvaluationId
                , e.TaskId
                , t.Title
				,t.ParentTaskId
                ,e.RecieverAllocationPersonId
                ,e.RecieverAllocationEvaluationHierarchyId
                , e.AllocatorPersonId
                , (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId=e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName
                ,e.AllocatorEvaluationHierarchyId
                ,(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName
                ,(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName
                ,e.AllocatorRoleId
                ,t.ResourceType
                ,pd.PeriodCode
                ,pd.PeriodTitle
                ,pd.PeriodDefinitoionId
                ,e.TaskWeight 
                ,e.EvaluationAcceptanceStatusId 
                ,(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant 
                ,(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation 
                FROM EmpsCTE 
                join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=[EvalHierarchyId] 
                and e.RecieverAllocationPersonId= PeopleId 
                join Task t on t.TaskId= e.TaskId 
                join PeriodDefinitoion pd on pd.PeriodDefinitoionId= e.PeriodDefinitoionId 
                where Levell!=1
                and e.PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt22,e.PeriodDefinitoionId) 
                and RecieverAllocationPersonId = ISNULL(@personIdDT2,RecieverAllocationPersonId) 
                and RecieverAllocationEvaluationHierarchyId = ISNULL(@departmentIdDT2,RecieverAllocationEvaluationHierarchyId) 
                and (Levell in(2,3) or AllocatorPersonId=@personIdd) 
                )dd where 1=1 and allocatorRoleName!='PlanningAdmin' " +
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
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT,
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personIdd = personId,
                primaryDepartmentIdd = primaryDepartmentId,
                periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                personIdDT2 = personIdDT,
                departmentIdDT2 = departmentIdDT
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personIdd = personId,
                primaryDepartmentIdd = primaryDepartmentId,
                periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                personIdDT2 = personIdDT,
                departmentIdDT2 = departmentIdDT
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }

        public EvaluationScore GetEvaluationScore(int evaluationId, int level)
        {
            EvaluationScore evaluationScore = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationId && c.CoacherLevel == level).SingleOrDefault();
            return evaluationScore;
        }

        public IEnumerable<Task> RelatedTaskList(int departmentId)
        {
            var sQuery = @"select 
                distinct
                t.TaskId
                ,t.Title
                from
                Evaluation e
                join Task t on e.TaskId = t.TaskId
                where
                1 = 1
                and t.ResourceType in(1, 3, 4)

                and e.PeriodDefinitoionId = (select MAX(PeriodDefinitoionId) from PeriodDefinitoion)
                and e.RecieverAllocationEvaluationHierarchyId = @departmentIdd";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<Task> query = null;

            query = conn.Query<Task>(sQuery, new { departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public List<PriorTaskOfPeriodView> PriorTaskOfPeriod(string term, int personId)
        {
            var sQuery = "select distinct " +
                "t.TaskId id" +
                ", t.Title label" +
                ", t.Title value " +
                "from " +
                "Task t " +
                "where " +
                "1 = 1 " +
                "and t.CreatedBy = @personIdd " +
                "and t.ResourceType = 4 " +
                "and t.IsActive = 1 " +
                "and t.Title like N'%" + term.Trim() + "%'";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<PriorTaskOfPeriodView> query = null;

            query = conn.Query<PriorTaskOfPeriodView>(sQuery, new { personIdd = personId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IQueryable GetDepartmentResponsibiltyList(int personId)
        {
            var query = from eh in appDbContext.evaluationHierarchies
                        join d in appDbContext.Departments on eh.DepartmentId equals d.DepartmentId
                        join p in appDbContext.People on eh.EvaluationHierarchyId equals p.EvaluationHierarchyID
                        //select new{ id2=i,dep=d.}
                        where (d.EffectiveEndDate == null && eh.EffectiveEndDate == null && eh.SupervisorId == personId && p.EffectiveEndDate == null
                        && eh.SupervisorId == p.PeopleId)
                        select new { eh.EvaluationHierarchyId, d.ShortName, p.PositionType };

            return query.Distinct();
        }

        public IEnumerable<object> GetSubSetDepartments(int departmentId)
        {
            var sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ",CONCAT(p.FirstName, ' ', p.LastName)fullName" +
                ", 0 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and eh.SupervisorId = p.PeopleId " +
                "--and p.PeopleId = 1 \n" +
                "and eh.[EvaluationHierarchyId] = @departmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", pp.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', pp.FirstName, ' ', pp.LastName, ')')ShortName" +
                ",CONCAT(pp.FirstName,' ', pp.LastName)" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN evaluationHierarchies AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId] " +
                "join People pp on pp.EvaluationHierarchyID=C.EvaluationHierarchyId " +
                "and C.SupervisorId= pp.PeopleId " +
                "join Departments d on d.DepartmentId= C.DepartmentId " +
                "and d.EffectiveEndDate is null " +
                "and C.EffectiveEndDate is null " +
                "and pp.EffectiveEndDate is null) " +
                "SELECT " +
                "[EvaluationHierarchyId]" +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, EvaluationHierarchyId) = @departmentIdd then '' " +
                //" when convert(nvarchar, ParentEvaluationHierarchyId) = @departmentIdd then 0 " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell" +
                ",fullName " +
                "FROM EmpsCTE " +
                "where 1=1 " +
                "order by 1";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> GetDirectEmployees(int departmentId, bool isSupervisor = false)
        {
            var condition = "";
            if (isSupervisor)
            {
                condition = "and eh.SupervisorId!=PeopleId";
            }
            var sQuery = "select " +
                "p.PeopleId" +
                ",CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ",eh.SupervisorId" +
                ",case when eh.SupervisorId = p.PeopleId then 'true' else 'false' end IsSupervisor " +
                ",p.EvaluationHierarchyID " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "where " +
                "1 = 1 " +
                "and p.EvaluationHierarchyID = @departmentIdd " +
                "and p.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null " +
                condition;

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> GetInDirectEmployees(int departmentId, int peopleId)
        {
            var sQuery = "WITH EmpsCTE AS( select " +
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
                "and p.PeopleId = @peopleIdd  " +
                "and eh.[EvaluationHierarchyId] = @departmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
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
                "and p.PeopleId = @peopleIdd  " +
                "and eh.[EvaluationHierarchyId]= @departmentIdd " +
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
                "SELECT " +
                "pp.[EvaluationHierarchyId]" +
                ", pp.[ShortName] as text" +
                ",case when convert(nvarchar, pp.EvaluationHierarchyId) = @departmentIdd then '' " +
                "else convert(nvarchar, pp.ParentEvaluationHierarchyId) end as parent" +
                ", pp.Levell" +
                ", pp.PeopleId" +
                ", pp.EvalHierarchyId " +
                "FROM EmpsCTE as pp " +
                "where 1=1 " +
                "order by 1 ";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId, peopleIdd = peopleId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public Dictionary<object, object> AssignTextTaskToIndirect(CoveredEmployee coveredEmployee, int allocatorId, string roleId)
        {
            List<Evaluation> evalList = new List<Evaluation>();
            List<Evaluation> evalTextList = new List<Evaluation>();
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            List<object> duplicateList = new List<object>();

            if (coveredEmployee.textTaskViews.Count() > 0)
            {
                foreach (var item2 in coveredEmployee.textTaskViews)
                {
                    List<Criteria> criteriaList = new List<Criteria>();
                    Task task = new Task();
                    task.Title = item2.TextTask;
                    task.PeriodDefinitoionId = coveredEmployee.PeriodDefinitionId;
                    task.RoleId = roleId;
                    task.IsActive = true;
                    task.ParentTaskId = item2.ParentTaskId;
                    task.CreatedBy = allocatorId;
                    task.CreatedDate = DateTime.Now;
                    task.ResourceType = 4;
                    task.EvaluationHierarchyId = coveredEmployee.AllocatorDepartmentId;
                    foreach (var criteriaItem in item2.CriteriaViews)
                    {
                        Criteria criteria = new Criteria();
                        criteria.Title = criteriaItem.Title;
                        criteria.CalculationWay = criteriaItem.CalculationWay;
                        criteria.LimitOfAdmission = criteriaItem.limitOfAdmission;
                        criteria.CreatedBy = allocatorId;
                        criteria.IsProcessingCriteria = criteriaItem.IsProcessingCriteria;
                        criteria.CreatedDate = DateTime.Now;
                        criteria.PeriodDefinitionId = appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId);
                        criteriaList.Add(criteria);
                    }
                    task.Criterias = criteriaList;
                    foreach (var item in coveredEmployee.Receiver)
                    {
                        int receiverId = int.Parse(item.Split("-")[1]);
                        int receiverDepartmentId = int.Parse(item.Split("-")[0]);
                        Evaluation evaluation = new Evaluation();
                        evaluation.RecieverAllocationPersonId = receiverId;
                        evaluation.RecieverAllocationEvaluationHierarchyId = receiverDepartmentId;
                        evaluation.PeriodDefinitoionId = coveredEmployee.PeriodDefinitionId;
                        evaluation.AllocatorPersonId = allocatorId;
                        evaluation.AllocatorEvaluationHierarchyId = coveredEmployee.AllocatorDepartmentId;
                        evaluation.Task = task;
                        evaluation.AllocatorRoleId = roleId;
                        evaluation.EvaluationAcceptanceStatusId = 1;
                        evaluation.CreatedBy = allocatorId;
                        evaluation.CreatedDate = DateTime.Now;
                        if (item2.ParticipantId != null)
                        {
                            List<EvaluationParticipant> evalParticipants = new List<EvaluationParticipant>();
                            EvaluationParticipant evaluationParticipant = new EvaluationParticipant();
                            evaluationParticipant.ParticipantId = item2.ParticipantId ?? 0;
                            evaluationParticipant.ParticipantEvaluationHierarchyId = item2.ParticipantDepartmentId ?? 0;
                            evaluationParticipant.RequestDate = DateTime.Now;
                            evaluationParticipant.CreatedBy = allocatorId;
                            evaluationParticipant.CreatedDate = DateTime.Now;
                            evalParticipants.Add(evaluationParticipant);
                            evaluation.EvaluationParticipants = evalParticipants;
                        }
                        if (item2.TrainingNeed != "")
                        {
                            List<TrainingNeed> tNeeds = new List<TrainingNeed>();
                            foreach (var item3 in item2.TrainingNeed.Split(","))
                            {
                                TrainingNeed trainingNeed = new TrainingNeed();
                                trainingNeed.Title = item3;
                                trainingNeed.CreatedBy = allocatorId;
                                trainingNeed.CreatedDate = DateTime.Now;
                                tNeeds.Add(trainingNeed);
                            }
                            evaluation.TrainingNeeds = tNeeds;
                        }
                        evalTextList.Add(evaluation);
                    }
                }
                appDbContext.AddRange(evalTextList);
            }
            int result = appDbContext.SaveChanges();
            dictionary.Add("result", result);
            return dictionary;
        }
        public Dictionary<string, object> WeightAssignment(List<WeightAssignmentView> tasksWeightList, int personId)
        {
            ShareService shareService = new ShareService(appDbContext, null);

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;


            int? taskWeightSummation = 0;
            var criteriaWeightSummation = 0;
            int? weightWayRealization = null;
            bool hasCriteria = false;
            int numberOFEval = appDbContext.Evaluation.Where(c => c.RecieverAllocationPersonId == tasksWeightList.FirstOrDefault().RecieverAllocationPersonId &&
              c.RecieverAllocationEvaluationHierarchyId == tasksWeightList.FirstOrDefault().RecieverAllocationEvaluationHierarchyId
              && c.PeriodDefinitoionId == shareService.GetMaxPeriodDefinitionId() && (c.AllocatorEvaluationHierarchyId == tasksWeightList.FirstOrDefault().AllocatorEvaluationHierarchyId
              || c.AllocatorRoleId == roleId2)
              ).Count();
            if (numberOFEval == tasksWeightList.Count)
            {
                foreach (var taskItem in tasksWeightList)
                {
                    var weightWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == taskItem.PeriodDefinitoionId).SingleOrDefault();
                    weightWayRealization = weightWay.WeightWayTask;

                    List<Evaluation> taskWeight = new List<Evaluation>();
                    var taskEvaluation = appDbContext.Evaluation.Where(c => c.EvaluationId == taskItem.EvaluationId).SingleOrDefault();
                    if (weightWay.WeightWayTask == 1)//percent
                    {
                        if (taskItem.TaskWeight == 0)
                        {
                            dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                            return dictionary;
                        }
                    }
                    else if ((taskItem.TaskWeight < NumberScaleMinMax(weightWay, 0, 1) || taskItem.TaskWeight > NumberScaleMinMax(weightWay, 1, 1)) && weightWay.WeightWayTask == 2)//likert
                    {
                        return NumberScaleMinMax(dictionary, weightWay, 1);
                    }
                    if (taskEvaluation.AllocatorRoleId == roleId && taskEvaluation.TaskWeight != taskItem.TaskWeight)
                    {
                        taskEvaluation.TaskWeight = taskItem.TaskWeight;
                        taskEvaluation.LastUpdatedBy = personId;
                        taskEvaluation.LastUpdatedDate = DateTime.Now;
                        appDbContext.Evaluation.Update(taskEvaluation);
                    }
                    taskWeightSummation += taskItem.TaskWeight;

                    if (taskItem.CriteriaWeightViews.Count == appDbContext.Criteria.Where(c => c.TaskId == taskItem.TaskId).Count())
                    {
                        foreach (var criteriaItem in taskItem.CriteriaWeightViews)
                        {
                            CriteriaWeight criteriaWeight = appDbContext.CriteriaWeight.Where(c => c.CriteriaId == criteriaItem.CriteriaId && c.EvaluationId == criteriaItem.EvaluationId).SingleOrDefault();
                            hasCriteria = true;
                            criteriaWeightSummation += criteriaItem.Weight;
                            if (weightWay.WeightWayTask == 1)//percent
                            {
                                if (criteriaItem.Weight == 0)
                                {
                                    dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                                    return dictionary;
                                }
                            }
                            else if ((criteriaItem.Weight < NumberScaleMinMax(weightWay, 0, 1) || criteriaItem.Weight > NumberScaleMinMax(weightWay, 1, 1)) && weightWay.WeightWayTask == 2)//likert
                            {
                                return NumberScaleMinMax(dictionary, weightWay, 1);
                            }
                            if (criteriaWeight != null && criteriaWeight.Weight != criteriaItem.Weight)
                            {
                                criteriaWeight.Weight = criteriaItem.Weight;
                                criteriaWeight.LastUpdatedBy = personId;
                                criteriaWeight.LastUpdatedDate = DateTime.Now;
                                appDbContext.CriteriaWeight.Update(criteriaWeight);
                            }
                            else if (criteriaWeight == null)
                            {
                                appDbContext.CriteriaWeight.Add(new CriteriaWeight { CriteriaId = criteriaItem.CriteriaId, EvaluationId = taskItem.EvaluationId, Weight = criteriaItem.Weight, RoleId = roleId, CreatedBy = personId, CreatedDate = DateTime.Now });
                            }

                        }
                        if (criteriaWeightSummation != 100 && weightWayRealization == 1 && hasCriteria)
                        {
                            dictionary.Add("result", "مجموع شاخص های هر هدف باید برابر با 100 باشد.");
                            return dictionary;
                        }
                        hasCriteria = false;
                        criteriaWeightSummation = 0;
                    }
                    else
                    {
                        dictionary.Add("malicious", "true");
                        return dictionary;
                    }

                }
                if (taskWeightSummation != 100 && weightWayRealization == 1)
                {
                    dictionary.Add("result", "مجموع اهداف باید برابر با 100 باشد.");
                    return dictionary;
                }
            }
            else
            {
                dictionary.Add("malicious", "true");
                return dictionary;
            }

            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
        private Dictionary<string, object> NumberScaleMinMax(Dictionary<string, object> dictionary, PeriodDefinitoion weightWay, int whichLikert)
        {
            int? likertWay = 0;
            switch (whichLikert)
            {
                case 1:
                    likertWay = weightWay.LikertWeightWayTaskId;
                    break;
                case 2:
                    likertWay = weightWay.LikertWeightWayBehaiviourId;
                    break;
                case 3:
                    likertWay = weightWay.LikertScoreWayId;
                    break;
                    //default:
            }
            var numberScaleMinMax = from ls in appDbContext.LikertScale
                                    join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                                    //select new{ id2=i,dep=d.}
                                    where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == likertWay)
                                    select new { ls.LikertScaleId, ld.NumberScale };
            dictionary.Add("result", $"بازه مجاز جهت تعیین وزن بین {numberScaleMinMax.Min(c => c.NumberScale)} تا {numberScaleMinMax.Max(c => c.NumberScale)} می باشد");
            return dictionary;
        }
        private int NumberScaleMinMax(PeriodDefinitoion weightWay, int minMax, int whichLikert)
        {
            int? likertWay = 0;
            switch (whichLikert)
            {
                case 1:
                    likertWay = weightWay.LikertWeightWayTaskId;
                    break;
                case 2:
                    likertWay = weightWay.LikertWeightWayBehaiviourId;
                    break;
                case 3:
                    likertWay = weightWay.LikertScoreWayId;
                    break;
                    //default:
            }
            var numberScaleMinMax = from ls in appDbContext.LikertScale
                                    join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                                    //select new{ id2=i,dep=d.}
                                    where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == likertWay)
                                    select new { ls.LikertScaleId, ld.NumberScale };
            if (minMax == 1)
            {
                return numberScaleMinMax.Max(c => c.NumberScale);
            }

            return numberScaleMinMax.Min(c => c.NumberScale);
        }
        public Dictionary<string, object> ScoreAssignment(List<Evaluation> listOfScores, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            ShareService shareService = new ShareService(appDbContext, null);
            //int? taskScoreSummation = 0;
            //var criteriaScoreSummation = 0;
            int? scoreWayRealization = null;
            bool hasCriteria = false;
            int numberOFEval = appDbContext.Evaluation.Where(c => c.RecieverAllocationPersonId == listOfScores.FirstOrDefault().RecieverAllocationPersonId &&
              c.RecieverAllocationEvaluationHierarchyId == listOfScores.FirstOrDefault().RecieverAllocationEvaluationHierarchyId
              && c.PeriodDefinitoionId == shareService.GetMaxPeriodDefinitionId() && (c.AllocatorEvaluationHierarchyId == listOfScores.FirstOrDefault().AllocatorEvaluationHierarchyId
              || c.AllocatorRoleId == roleId2) && (c.EvaluationAcceptanceStatusId != 3 && c.EvaluationAcceptanceStatusId != 4)
              ).Count();
            if (numberOFEval >= listOfScores.Count)
            {
                foreach (var evaluationItem in listOfScores)
                {
                    var scoreWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == evaluationItem.PeriodDefinitoionId).SingleOrDefault();

                    scoreWayRealization = scoreWay.WeightWayScore;


                    //There is always one item in this loop 
                    foreach (var evaluationScoreItem in evaluationItem.EvaluationScores)
                    {
                        EvaluationScore evalScoreResult = appDbContext.EvaluationScore.Where(c => c.EvaluationId == evaluationItem.EvaluationId && c.CoacherLevel == evaluationScoreItem.CoacherLevel).SingleOrDefault();

                        if (scoreWay.WeightWayScore == 1)//percent
                        {
                            if (evaluationScoreItem.Score == 0)
                            {
                                dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                                return dictionary;
                            }
                        }
                        else if ((evaluationScoreItem.Score < NumberScaleMinMax(scoreWay, 0, 3) || evaluationScoreItem.Score > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2 && shareService.CriteiaCount(evaluationItem.TaskId) == 0)//likert
                        {
                            return NumberScaleMinMax(dictionary, scoreWay, 3);
                        }

                        if (evalScoreResult != null && evalScoreResult.Score != evaluationScoreItem.Score)
                        {
                            evalScoreResult.Score = evaluationScoreItem.Score;
                            evalScoreResult.LastUpdatedBy = personId;
                            evalScoreResult.LastUpdatedDate = DateTime.Now;
                            appDbContext.Update(evalScoreResult);
                        }
                        else if (evalScoreResult == null)
                        {
                            EvaluationScore evaluationScore = new EvaluationScore();
                            evaluationScore.Score = evaluationScoreItem.Score;
                            evaluationScore.CoacherLevel = evaluationScoreItem.CoacherLevel;
                            evaluationScore.CoacherId = personId;
                            evaluationScore.EvaluationId = evaluationItem.EvaluationId;
                            evaluationScore.RoleId = roleId;
                            evaluationScore.CreatedBy = personId;
                            evaluationScore.CreatedDate = DateTime.Now;
                            //taskScoreSummation += evaluationScoreItem.Score;
                            appDbContext.EvaluationScore.Add(evaluationScore);
                        }

                    }
                    if (evaluationItem.CriteriaWeights.Count == appDbContext.Criteria.Where(c => c.TaskId == evaluationItem.TaskId).Count())
                    {
                        foreach (var criteriaWeightItem in evaluationItem.CriteriaWeights)
                        {
                            hasCriteria = true;
                            //There is always one item in this loop
                            foreach (var criteriaWeightScoreItem in criteriaWeightItem.CriteriaWeightScores)
                            {
                                if (scoreWay.WeightWayScore == 1)//percent
                                {
                                    if (criteriaWeightScoreItem.Score == 0)
                                    {
                                        dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                                        return dictionary;
                                    }
                                }
                                else if ((criteriaWeightScoreItem.Score < NumberScaleMinMax(scoreWay, 0, 3) || criteriaWeightScoreItem.Score > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2)//likert
                                {
                                    return NumberScaleMinMax(dictionary, scoreWay, 3);
                                }
                                int criteriaWeightId = appDbContext.CriteriaWeight.Where(c => c.CriteriaId == criteriaWeightItem.CriteriaId && c.EvaluationId == evaluationItem.EvaluationId).SingleOrDefault().CriteriaWeightId;
                                CriteriaWeightScore criteriaWeightScoreResult = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.CoacherLevel == criteriaWeightScoreItem.CoacherLevel).SingleOrDefault();

                                if (criteriaWeightScoreResult != null && criteriaWeightScoreResult.Score != criteriaWeightScoreItem.Score)
                                {
                                    criteriaWeightScoreResult.Score = criteriaWeightScoreItem.Score;
                                    criteriaWeightScoreResult.LastUpdatedBy = personId;
                                    criteriaWeightScoreResult.LastUpdatedDate = DateTime.Now;
                                    appDbContext.Update(criteriaWeightScoreResult);
                                }
                                else if (criteriaWeightScoreResult == null)
                                {
                                    CriteriaWeightScore criteriaWeightScore = new CriteriaWeightScore();
                                    criteriaWeightScore.Score = criteriaWeightScoreItem.Score;
                                    criteriaWeightScore.CoacherLevel = criteriaWeightScoreItem.CoacherLevel;
                                    criteriaWeightScore.CoacherId = personId;
                                    criteriaWeightScore.CriteriaWeightId = criteriaWeightId;
                                    criteriaWeightScore.RoleId = roleId;
                                    criteriaWeightScore.CreatedBy = personId;
                                    criteriaWeightScore.CreatedDate = DateTime.Now;
                                    //criteriaScoreSummation += criteriaWeightScoreItem.Score;
                                    appDbContext.CriteriaWeightScore.Add(criteriaWeightScore);
                                }
                            }
                        }
                        //if (criteriaScoreSummation != 100 && scoreWayRealization == 1 && hasCriteria)
                        //{
                        //    dictionary.Add("result", "مجموع شاخص های هر هدف باید برابر با 100 باشد.");
                        //    return dictionary;
                        //}
                        hasCriteria = false;
                        //criteriaScoreSummation = 0;
                    }
                    else
                    {
                        dictionary.Add("malicious", "true");
                        return dictionary;
                    }
                }
                //if (taskScoreSummation != 100 && scoreWayRealization == 1)
                //{
                //    dictionary.Add("result", "مجموع اهداف باید برابر با 100 باشد.");
                //    return dictionary;
                //}
            }
            else
            {
                dictionary.Add("malicious", "true");
                return dictionary;
            }

            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
        public int RenewContract(PerformConfirmationView evaluation, int personId, string roleId, string roleId2)
        {
            List<Notification> notificationList = new List<Notification>();

            foreach (var evaluationItem in evaluation.EvaluationId)
            {
                var fetchEval = appDbContext.Evaluation.Where(c => c.EvaluationId == evaluationItem).SingleOrDefault();
                if (fetchEval != null)
                {
                    Notification notification = new Notification();
                    if (evaluation.EvaluationAcceptanceStatusId == 2)
                    {
                        fetchEval.EvaluationAcceptanceStatusId = 2;
                        notification.Title = "دستوری";
                    }
                    else if (evaluation.EvaluationAcceptanceStatusId == 3)
                    {
                        fetchEval.EvaluationAcceptanceStatusId = 3;
                        notification.Title = "صرف نظر";
                    }
                    fetchEval.LastUpdatedBy = personId;
                    fetchEval.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(fetchEval);
                    notification.AllocatorHierarchyId = fetchEval.AllocatorEvaluationHierarchyId;
                    notification.AllocatorPersonId = personId;
                    notification.AllocatorRoleId = roleId;
                    notification.ReceiverPersonId = fetchEval.RecieverAllocationPersonId;
                    notification.ReceiverHierarchtId = fetchEval.RecieverAllocationEvaluationHierarchyId;
                    notification.ReceiverRoleId = roleId2;
                    notification.NotificationTitleId = 1;
                    notification.Visibility = false;
                    notification.PeriodDefinitionId = fetchEval.PeriodDefinitoionId;
                    notification.EvaluationId = fetchEval.EvaluationId;
                    notification.CreatedDate = DateTime.Now;
                    notificationList.Add(notification);
                }
            }
            appDbContext.AddRange(notificationList);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public List<ScoreView> GetCriteriaScore(int evaluationId)
        {
            var sQuery = @"select 
                tbl.EvaluationId
                ,t.Title
                ,tbl.CriteriaWeightId
                ,tbl.CriteriaId
                ,c.Title CriteriaTitle
                ,c.LimitOfAdmission
				,c.CalculationWay
                ,sum(score1)score1
                ,sum(score2)score2
                ,sum(score3)score3
                ,sum(score4)score4
                ,sum(participantScore)participantScore
                ,sum(selfScore)selfScore
                ,sum(planningAdminScore)planningAdminScore 
                from( 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , cws.Score as score1
                , null as score2
                , null as score3
                , null as score4
                , null as participantScore
                , null as selfScore
                , null as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel = 1 
                union all 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , null as score1
                , cws.Score as score2
                , null as score3
                , null as score4
                , null as participantScore
                , null as selfScore
                , null as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel = 2 
                union all 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , null as score1
                , null as score2
                , cws.Score as score3
                , null as score4
                , null as participantScore
                , null as selfScore
                , null as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel = 3 
                union all 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , null as score1
                , null as score2
                , null as score3
                , cws.Score as score4
                , null as participantScore
                , null as selfScore
                , null as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel = 4 
                union all 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , null as score1
                , null as score2
                , null as score3
                , null as score4
                , cws.Score as participantScore
                , null as selfScore
                , null as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel = -1 
                union all 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , null as score1
                , null as score2
                , null as score3
                , null as score4
                , null as participantScore
                , cws.Score as selfScore
                , null as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel = 0 
                union all 
                select 
                cw.EvaluationId
                , cws.CoacherId
                , cws.CoacherLevel
                , cws.CriteriaWeightId
                , cw.CriteriaId
                , cws.RoleId
                , null as score1
                , null as score2
                , null as score3
                , null as score4
                , null as participantScore
                , null as selfScore
                , cws.Score as planningAdminScore 
                from 
                CriteriaWeightScore cws join CriteriaWeight cw on cws.CriteriaWeightId = cw.CriteriaWeightId 
                where 
                1 = 1 
                and cw.EvaluationId = @evaluationIdd 
                and cws.CoacherLevel is null 
                ) as tbl join Evaluation e on e.EvaluationId = tbl.EvaluationId 
                join Task t on t.TaskId = e.TaskId 
                join Criteria c on c.CriteriaId = tbl.CriteriaId 
                group by tbl.EvaluationId,tbl.CriteriaWeightId,t.Title,tbl.CriteriaId,c.Title,c.LimitOfAdmission,c.CalculationWay";

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
                ",sum(score1)score1" +
                ",sum(score2)score2" +
                ",sum(score3)score3" +
                ",sum(score4)score4" +
                ",sum(participantScore)participantScore" +
                ",sum(selfScore)selfScore" +
                ",sum(planningAdminScore)planningAdminScore " +
                "from( " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", es.Score as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 1 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", es.Score as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 2 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", es.Score as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 3 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", es.Score as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 4 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", es.Score as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = -1 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", es.Score as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where " +
                "1 = 1 " +
                "and es.EvaluationId = @evaluationIdd " +
                "and es.CoacherLevel = 0 " +
                "union all " +
                "select " +
                "es.CoacherId" +
                ", es.CoacherLevel" +
                ", es.EvaluationId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", es.Score as planningAdminScore " +
                "from " +
                "EvaluationScore es " +
                "where 1 = 1 " +
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
        public EvaluationParticipant GetEvaluationParticipant(int evaluationId)
        {
            return appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluationId).SingleOrDefault();
        }
        public List<TrainingNeed> GetTrainingNeed(int evaluationId)
        {
            List<TrainingNeed> trainingNeed = null;
            var query = appDbContext.TrainingNeed.Where(c => c.EvaluationId == evaluationId);
            if (query.Count() > 0)
            {
                trainingNeed = query.ToList();
            }
            return trainingNeed;
        }
        public Dictionary<object, object> EditTaskAssignment(EvaluationEditionView evaluationEditionView, int personId)
        {
            Dictionary<object, object> dictionary = new Dictionary<object, object>();

            try
            {

                if (evaluationEditionView.TaskId != null)
                {
                    Task task = appDbContext.Task.Where(c => c.TaskId == evaluationEditionView.TaskId).SingleOrDefault();
                    if (task.Title != evaluationEditionView.TaskTitle || task.ParentTaskId != evaluationEditionView.ParentTaskId)
                    {
                        task.Title = evaluationEditionView.TaskTitle;
                        task.ParentTaskId = evaluationEditionView.ParentTaskId;
                        task.LastUpdatedBy = personId;
                        task.LastUpdatedDate = DateTime.Now;
                        appDbContext.Update(task);
                    }
                }

                if (evaluationEditionView.EvaluationParticipantId != null)
                {
                    EvaluationParticipant evaluationParticipant = appDbContext.EvaluationParticipant.Where(c => c.EvaluationParticipantId == evaluationEditionView.EvaluationParticipantId).SingleOrDefault();

                    if (evaluationEditionView.ParticipantId != evaluationParticipant.ParticipantId || evaluationEditionView.ParticipantDepartmentId != evaluationParticipant.ParticipantEvaluationHierarchyId)
                    {
                        evaluationParticipant.ParticipantId = evaluationEditionView.ParticipantId ?? throw new NullReferenceException();
                        evaluationParticipant.ParticipantEvaluationHierarchyId = evaluationEditionView.ParticipantDepartmentId ?? throw new NullReferenceException();
                        evaluationParticipant.ResponseDate = DateTime.Now;
                        evaluationParticipant.Confirmation = null;
                        evaluationParticipant.LastUpdatedBy = personId;
                        evaluationParticipant.LastUpdatedDate = DateTime.Now;
                        appDbContext.Update(evaluationParticipant);
                    }
                }

                if (evaluationEditionView.CriteriaViews != null)
                {
                    foreach (var item in evaluationEditionView.CriteriaViews)
                    {
                        Criteria criteria = appDbContext.Criteria.Where(c => c.CriteriaId == item.CriteriaId).SingleOrDefault();
                        if (criteria.Title != item.Title || criteria.LimitOfAdmission != item.limitOfAdmission || criteria.CalculationWay != item.CalculationWay || criteria.IsProcessingCriteria != item.IsProcessingCriteria)
                        {
                            criteria.Title = item.Title;
                            criteria.LimitOfAdmission = item.limitOfAdmission;
                            criteria.CalculationWay = item.CalculationWay;
                            criteria.IsProcessingCriteria = item.IsProcessingCriteria;
                            criteria.LastUpdatedBy = personId;
                            criteria.LastUpdatedDate = DateTime.Now;
                            appDbContext.Update(criteria);
                        }
                    }
                }

                if (evaluationEditionView.TrainingNeeds != null)
                {
                    foreach (var item2 in evaluationEditionView.TrainingNeeds)
                    {
                        TrainingNeed trainingNeed = appDbContext.TrainingNeed.Where(c => c.TrainingNeedId == item2.TrainingNeedId).SingleOrDefault();
                        if (trainingNeed.Title != item2.Title)
                        {
                            trainingNeed.Title = item2.Title;
                            trainingNeed.LastUpdatedBy = personId;
                            trainingNeed.LastUpdatedDate = DateTime.Now;
                            appDbContext.Update(trainingNeed);
                        }
                    }
                }

                if (evaluationEditionView.TrainingNeedDeletion != null && evaluationEditionView.TrainingNeedDeletion.Length > 0)
                {
                    foreach (var item in evaluationEditionView.TrainingNeedDeletion)
                    {
                        appDbContext.Remove(appDbContext.TrainingNeed.Where(c => c.TrainingNeedId == item).SingleOrDefault());
                    }
                }

                if (evaluationEditionView.CriteriaInsertionViews.Count() > 0)
                {
                    List<Criteria> criteriaList = new List<Criteria>();

                    foreach (var criteriaItem in evaluationEditionView.CriteriaInsertionViews)
                    {
                        Criteria criteria = new Criteria();
                        criteria.Title = criteriaItem.Title;
                        criteria.LimitOfAdmission = criteriaItem.LimitOfAdmission;
                        criteria.CalculationWay = criteriaItem.CalculationWay;
                        criteria.TaskId = evaluationEditionView.TaskId ?? 0;
                        criteria.IsProcessingCriteria = criteriaItem.IsProcessingCriteria;
                        criteria.CreatedBy = personId;
                        criteria.CreatedDate = DateTime.Now;
                        criteria.PeriodDefinitionId = appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId);
                        criteriaList.Add(criteria);
                    }
                    appDbContext.AddRange(criteriaList);
                }
                if (evaluationEditionView.TrainingNeedInsertion != "")
                {
                    List<TrainingNeed> tNeeds = new List<TrainingNeed>();
                    foreach (var item3 in evaluationEditionView.TrainingNeedInsertion.Split(","))
                    {
                        TrainingNeed trainingNeed = new TrainingNeed();
                        trainingNeed.Title = item3;
                        trainingNeed.EvaluationId = evaluationEditionView.EvaluationId ?? 0;
                        trainingNeed.CreatedBy = personId;
                        trainingNeed.CreatedDate = DateTime.Now;
                        tNeeds.Add(trainingNeed);
                    }
                    appDbContext.AddRange(tNeeds);
                }

                int result = appDbContext.SaveChanges();
                dictionary.Add("success", result);
                if (evaluationEditionView.CriteriaDeletion != null && evaluationEditionView.CriteriaDeletion.Length > 0)
                {
                    int criteriaExistenceCount = appDbContext.Criteria.Where(c => c.TaskId == evaluationEditionView.TaskId).Count();
                    int criteriaInsertion = evaluationEditionView.CriteriaInsertionViews.Count();
                    int criteriaDeletion = evaluationEditionView.CriteriaDeletion.Length;
                    int criteriaCount = criteriaExistenceCount + criteriaInsertion - criteriaDeletion;
                    if (criteriaCount > 0)
                    {
                        foreach (var item in evaluationEditionView.CriteriaDeletion)
                        {

                            if (appDbContext.CriteriaWeight.Where(c => c.CriteriaId == item).Any())
                            {
                                appDbContext.Remove(appDbContext.CriteriaWeight.Where(c => c.CriteriaId == item).SingleOrDefault());
                            }
                            appDbContext.Remove(appDbContext.Criteria.Where(c => c.CriteriaId == item).SingleOrDefault());
                            result = appDbContext.SaveChanges();
                            dictionary.Add("successDeletion", result);
                        }
                    }
                    else
                    {
                        dictionary.Add("requirement", -1);
                    }
                }
                return dictionary;
            }
            catch (SqlException)
            {
                //return se.Message;
            }
            catch (DbUpdateException due)
            {
                DbUpdateException du2 = due;
                dictionary.Add("DbUpdateException", "در صورتی که برای شاخص/شاخص ها اختصاصی مورد نظر، نمره دهی یا محاسبات پایان دوره انجام شده باشد، قابل حذف نمی باشد" + due.InnerException.Message);
                //return dictionary;
            }
            catch (Exception e2)
            {
                dictionary.Add("exception", e2.Message);
                // return dictionary;
            }
            return dictionary;
        }
        public string DeleteTaskAssignment(int evaluationId, int? hasParticipant)
        {
            try
            {
                ShareService shareService = new ShareService(appDbContext, null);
                Evaluation evaluation = appDbContext.Evaluation.Where(c => c.EvaluationId == evaluationId).SingleOrDefault();
                Task task = appDbContext.Task.Where(c => c.TaskId == evaluation.TaskId).SingleOrDefault();
                int evaluationCount = appDbContext.Evaluation.Where(c => c.TaskId == task.TaskId).Count();
                bool anyCriteria = appDbContext.Criteria.Where(c => c.TaskId == evaluation.TaskId).Any();
                bool anyCriteriaWeight = appDbContext.CriteriaWeight.Where(c => c.EvaluationId == evaluation.EvaluationId).Any();
                bool anyTrainingNeed = appDbContext.TrainingNeed.Where(c => c.EvaluationId == evaluation.EvaluationId).Any();

                if (hasParticipant == 1)
                {
                    appDbContext.Remove(appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluation.EvaluationId).SingleOrDefault());
                }
                if (anyTrainingNeed)
                {
                    appDbContext.RemoveRange(appDbContext.TrainingNeed.Where(c => c.EvaluationId == evaluation.EvaluationId).ToList());
                }
                if (anyCriteriaWeight)
                {
                    appDbContext.RemoveRange(appDbContext.CriteriaWeight.Where(c => c.EvaluationId == evaluation.EvaluationId).ToList());
                }
                appDbContext.Remove(evaluation);
                if (task.PeriodDefinitoionId == shareService.GetMaxPeriodDefinitionId() && task.ResourceType == 4 && evaluationCount == 1)
                {
                    if (anyCriteria)
                    {
                        appDbContext.RemoveRange(appDbContext.Criteria.Where(c => c.TaskId == evaluation.TaskId).ToList());
                    }
                    appDbContext.Remove(task);
                }

                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                DbUpdateException du2 = due;
                string message = "در صورتی که برای وظیفه اختصاصی مورد نظر، محاسبه یا وقایع حساس ثبت کرده اید، قابل حذف نمی باشد" + Environment.NewLine + Environment.NewLine + due.InnerException.Message;

                return message;
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }
        public List<SensibleEventTableView> GetSensibleEvent(int evaluationId)
        {
            var sQuery = "select " +
                "se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ",t.Title TaskTitle" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CreatedPersonFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CreatedDepartmentName" +
                ",se.EmployeeId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.EmployeeId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ",se.EmployeeDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.EmployeeDepartmentId)EmployeeDepartmentName" +
                ",se.PersonRole" +
                ",anr.Name RoleName" +
                ", se.Visibility" +
                ",se.FileName" +
                ",se.FilePath" +
                ",se.SensibleEventDate" +
                ",se.PeriodDefinitionId " +
                "from SensibleEvent se join RelatedTaskWithSensibleEvent rt on se.SensibleEventId = rt.SensibleEventId " +
                "join Task t on rt.TaskId = t.TaskId " +
                "join AspNetRoles anr on se.PersonRole = anr.Id " +
                "where " +
                "1 = 1 " +
                "and rt.EvaluationId = @evaluationIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<SensibleEventTableView> SensibleEventTableView = null;

            SensibleEventTableView = conn.Query<SensibleEventTableView>(sQuery, new { evaluationIdd = evaluationId }).ToList();

            //conn.Close();
            conn.Dispose();
            return SensibleEventTableView;
        }
        public Dictionary<object, object> GetSubSetScoreList(DataTableParameter dataTableParameter, int personId, int primaryDepartmentId, string employee = null, string periodDefinitionIdDT2 = null)
        {
            int? personIdDT = null;
            int? departmentIdDT = null;
            //int? periodDefinitionIdDT2Param = null;
            int? periodDefinitionIdDT2Param = int.Parse(periodDefinitionIdDT2);
            if (employee != null && employee != "")
            {
                personIdDT = int.Parse(employee.Split("-")[0]);
                departmentIdDT = int.Parse(employee.Split("-")[1]);
            }
            else
            {
                //periodDefinitionIdDT2Param = null;
            }
            String[] aColumns = { "text", "Title", "allocatorFullName", "allocatorDepartmentName", "allocatorRoleName", "ResourceType", "PeriodCode", "PeriodTitle", "TaskWeight" };
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

            string queryTotalResult = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
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
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId " +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ",id" +
                ",text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationId" +
                ", TaskId" +
                ", Title" +
                ",RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",allocatorDepartmentName" +
                ",allocatorRoleName" +
                ",AllocatorRoleId" +
                ",ResourceType" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",PeriodDefinitoionId" +
                ",TaskWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx" +
                ",EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", e.EvaluationId" +
                ", e.TaskId" +
                ", t.Title" +
                ",e.RecieverAllocationPersonId" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ", e.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId=e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",e.AllocatorRoleId" +
                ",t.ResourceType" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",e.TaskWeight " +
                ",e.EvaluationAcceptanceStatusId " +
                ",(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant " +
                ",(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=[EvalHierarchyId] " +
                "and e.RecieverAllocationPersonId= PeopleId " +
                "join Task t on t.TaskId= e.TaskId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= e.PeriodDefinitoionId " +
                "where Levell!=1" +
                "and e.PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt22,e.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2,RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationHierarchyId = ISNULL(@departmentIdDT2,RecieverAllocationEvaluationHierarchyId) " +
                ")dd where 1=1 ";

            string queryFilteredTotal = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
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
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId " +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ",id" +
                ",text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationId" +
                ", TaskId" +
                ", Title" +
                ",RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",allocatorDepartmentName" +
                ",allocatorRoleName" +
                ",AllocatorRoleId" +
                ",ResourceType" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",PeriodDefinitoionId" +
                ",TaskWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx" +
                ",EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", e.EvaluationId" +
                ", e.TaskId" +
                ", t.Title" +
                ",e.RecieverAllocationPersonId" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ", e.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId=e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",e.AllocatorRoleId" +
                ",t.ResourceType" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",e.TaskWeight " +
                ",e.EvaluationAcceptanceStatusId " +
                ",(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant " +
                ",(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=[EvalHierarchyId] " +
                "and e.RecieverAllocationPersonId= PeopleId " +
                "join Task t on t.TaskId= e.TaskId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= e.PeriodDefinitoionId " +
                "where Levell!=1" +
                "and e.PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt22,e.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2,RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationHierarchyId = ISNULL(@departmentIdDT2,RecieverAllocationEvaluationHierarchyId) " +
                ")dd where 1=1 " +
                where;

            string sQuery = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
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
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId " +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ",id" +
                ",text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationId" +
                ", TaskId" +
                ", Title" +
                ",RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ",AllocatorEvaluationHierarchyId" +
                ",allocatorDepartmentName" +
                ",allocatorRoleName" +
                ",AllocatorRoleId" +
                ",ResourceType" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",PeriodDefinitoionId" +
                ",TaskWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY e.EvaluationId desc) As indexx" +
                ",EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", e.EvaluationId" +
                ", e.TaskId" +
                ", t.Title" +
                ",e.RecieverAllocationPersonId" +
                ",e.RecieverAllocationEvaluationHierarchyId" +
                ", e.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=e.AllocatorEvaluationHierarchyId and p.PeopleId=e.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",e.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=e.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",e.AllocatorRoleId" +
                ",t.ResourceType" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",e.TaskWeight " +
                ",e.EvaluationAcceptanceStatusId " +
                ",(select case when ep.ParticipantId > 0 then 1 end from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))hasParticipant " +
                ",(select ep.Confirmation from EvaluationParticipant ep where ep.EvaluationId = e.EvaluationId and ep.RequestDate =(select MAX(ep2.RequestDate) from EvaluationParticipant ep2 where ep2.EvaluationId = e.EvaluationId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join Evaluation e on e.RecieverAllocationEvaluationHierarchyId=[EvalHierarchyId] " +
                "and e.RecieverAllocationPersonId= PeopleId " +
                "join Task t on t.TaskId= e.TaskId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= e.PeriodDefinitoionId " +
                "where Levell!=1" +
                "and e.PeriodDefinitoionId=ISNULL(@periodDefinitionIdDTt22,e.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2,RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationHierarchyId = ISNULL(@departmentIdDT2,RecieverAllocationEvaluationHierarchyId) " +
                ")dd where 1=1 " +
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
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT,
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personIdd = personId,
                primaryDepartmentIdd = primaryDepartmentId,
                periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                personIdDT2 = personIdDT,
                departmentIdDT2 = departmentIdDT
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personIdd = personId,
                primaryDepartmentIdd = primaryDepartmentId,
                periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                personIdDT2 = personIdDT,
                departmentIdDT2 = departmentIdDT
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
