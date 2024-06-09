using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class CriteriaService
    {
        private readonly IConnProvider connProvider;
        private readonly AppDbContext appDbContext;

        //private readonly IConfiguration config;

        public CriteriaService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public List<Criteria> GetCriteriaList(int taskId)
        {
            var query = appDbContext.Criteria.Where(c => c.TaskId == taskId);
            if (query != null)
            {
                return query.OrderBy(c => c.CriteriaId).ToList();
            }
            return (null);
        }
        public IEnumerable<object> GetCriteriaList2(int taskId, int evaluationId)
        {
            var sQuery = "select " +
                "c.CriteriaId," +
                "c.TaskId," +
                "c.Title," +
                "c.LimitOfAdmission," +
                "cw.CriteriaWeightId," +
                "cw.EvaluationId," +
                "cw.Weight " +
                "from " +
                "Criteria c left join CriteriaWeight cw on c.CriteriaId = cw.CriteriaId " +
                "where " +
                "1 = 1 " +
                "and c.TaskId = @taskIdd " +
                "and cw.EvaluationId = @evaluationIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                taskIdd = taskId,
                evaluationIdd = evaluationId
            }).ToList();

            conn.Close();
            //conn.Dispose();
            if (query != null)
            {
                return query;

            }
            return null;
        }
        public IEnumerable<object> GetCriteriaWeightList(int taskId, int evaluationId)
        {
            var sQuery = "select " +
                "c.CriteriaId" +
                ",c.Title CriteriaTitle" +
                ", c.LimitOfAdmission" +
                ",(select cw.CriteriaWeightId from CriteriaWeight cw where cw.CriteriaId = c.CriteriaId and cw.EvaluationId = @evaluationIdd)CriteriaWeightId" +
                ",(select cw.Weight from CriteriaWeight cw where cw.CriteriaId = c.CriteriaId and cw.EvaluationId = @evaluationIdd)Weight " +
                ",(select cw.EvaluationId from CriteriaWeight cw where cw.CriteriaId = c.CriteriaId and cw.EvaluationId = @evaluationIdd)EvaluationId " +
                "from " +
                "Criteria c " +
                "where " +
                "1 = 1 " +
                "and c.TaskId = @taskIdd";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                taskIdd = taskId,
                evaluationIdd = evaluationId
            }).ToList();

            conn.Close();
            //conn.Dispose();
            return query;
        }

        public CriteriaWeightScore GetCriteriaWeightScore(int criteriaWeightId, int level)
        {
            var criteriaWeightScore = appDbContext.CriteriaWeightScore.Where(c => c.CriteriaWeightId == criteriaWeightId && c.CoacherLevel == level).SingleOrDefault();
            return criteriaWeightScore;
        }
        public List<CriteriaDetailsView> GetAllCriteria(int taskId,int evaluationId)
        {
            var sQuery = @"select 
                        t.TaskId
                        ,t.Description
                        ,t.RoleId
                        ,c.CriteriaId
                        ,c.Title
                        ,e.EvaluationId
                        ,cw.CriteriaWeightId
                        ,cw.Weight
                        ,c.LimitOfAdmission
                        ,c.CalculationWay
                        ,case 
                        when c.CriteriaType=1 then 'KRI' 
                        when c.CriteriaType=2 then 'KPI' 
                        when c.CriteriaType=3 then 'PI' 
                        end CriteriaType
                        ,c.PeriodDefinitionId
                        ,pd.PeriodCode
                        ,pd.PeriodTitle
                        from
                        Task t join Criteria c on t.TaskId=c.TaskId
                        left join Evaluation e on t.TaskId=e.TaskId
                        left join CriteriaWeight cw on c.CriteriaId=cw.CriteriaId and e.EvaluationId=cw.EvaluationId
                        join PeriodDefinitoion pd on c.PeriodDefinitionId=pd.PeriodDefinitoionId
                        where 
                        1=1
                        and t.TaskId=@taskIdd
                        and e.EvaluationId=@evaluationIdd
                        union
                        select 
                        t.TaskId
                        ,t.Description
                        ,t.RoleId
                        ,null
                        ,null
                        ,e.EvaluationId
                        ,null
                        ,null
                        ,null
                        ,null
                        , null CriteriaType
                        ,null
                        ,null
                        ,null
                        from
                        Task t 
                        join Evaluation e on t.TaskId=e.TaskId
                        where 
                        1=1
                        and t.RoleId=(select Id from AspNetRoles where Name='HRAdmin')
                        and t.TaskId=@taskIdd
                        and e.EvaluationId=@evaluationIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<CriteriaDetailsView> query = null;

            query = conn.Query<CriteriaDetailsView>(sQuery, new
            {
                taskIdd = taskId,
                evaluationIdd= evaluationId
            }).ToList();

            conn.Close();
            //conn.Dispose();
            if (query != null)
            {
                return query;

            }
            return null;
        }
    }
}
