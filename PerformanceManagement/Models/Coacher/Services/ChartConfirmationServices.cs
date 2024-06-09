using Dapper;
using Microsoft.AspNetCore.Http;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class ChartConfirmationServices
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ChartConfirmationServices(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> GetNumberOfChart(int personId)
        {
            IDbConnection conn = connProvider.Connection;
            Dictionary<object, object> dictionary = new Dictionary<object, object>();

            string sQuery = "select " +
                "p.FirstName" +
                ",p.LastName" +
                ",p.PeopleId" +
                ",eh.EvaluationHierarchyId" +
                ",eh.SupervisorId" +
                ",eh.ParentEvaluationHierarchyId" +
                ",p.PositionType" +
                ",d.ShortName " +
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
                "and p.PeopleId = @personIdd ";
            List<object> query = null;
            conn.Open();
            query = conn.Query<object>(sQuery, new { personIdd = personId }).ToList();
            int totalResult = query.Count;
            //conn.Close();
            conn.Dispose();

            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("chartsInformation", query);
            return (dictionary);
        }
        public List<object> GetTree(int evaluationHierarchyId, int personId)
        {
            IDbConnection conn = connProvider.Connection;
            var sQuery = "WITH EmpsCTE AS(" +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 0 Levell " +
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
                "and p.PeopleId = @peopleId " +
                "and eh.[EvaluationHierarchyId] = @evaluationHierarchyIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId" +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",0 Levell " +
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
                "and p.PeopleId = @peopleId " +
                "and eh.[EvaluationHierarchyId]=@evaluationHierarchyIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "SELECT[EvaluationHierarchyId] as id " +
                ",[ShortName] as text " +
                ",case when convert(nvarchar,ParentEvaluationHierarchyId) is null then '#' " +
                "when convert(nvarchar, EvaluationHierarchyId) = @evaluationHierarchyIdd then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                "FROM EmpsCTE order by 1 ";
            conn.Open();
            List<object> query = null;
            query = conn.Query<object>(sQuery, new { peopleId = personId, evaluationHierarchyIdd = evaluationHierarchyId }).ToList();
            conn.Close();
            //conn.Dispose();
            return query;
        }

        public int AddChartDenial(int personId, int periodDefinitionId, string causeDescription)
        {
            var person = appDbContext.People.Where(c => c.PeopleId == personId && c.EffectiveEndDate == null).FirstOrDefault();
            var chartConfirmation2 = appDbContext.ChartConfirmation.Where(c => c.CreatedBy == personId && c.PeriodDefinitionId == periodDefinitionId).ToList();
            if (person != null)
            {
                var evalHierarchy = appDbContext.evaluationHierarchies.Where(c => c.SupervisorId == person.PeopleId && c.EffectiveEndDate == null).ToList();
                List<ChartConfirmation> chartConfirmation = new List<ChartConfirmation>();
                if (chartConfirmation2.Count > 0)
                {
                    foreach (var item in chartConfirmation2)
                    {
                        item.Confirmation = false;
                        item.LastUpdatedBy = personId;
                        item.LastUpdatedDate = DateTime.Now;
                        item.CauseDescription = causeDescription;
                        item.SupervisorId = personId;
                    }
                    appDbContext.UpdateRange(chartConfirmation2);
                }
                else
                {
                    foreach (var item in evalHierarchy)
                    {
                        chartConfirmation.Add(new ChartConfirmation
                        {
                            EvaluationHierarchyId = item.EvaluationHierarchyId,
                            PeriodDefinitionId = periodDefinitionId,
                            Confirmation = false,
                            CauseDescription = causeDescription,
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now,
                            SupervisorId=personId
                        });
                    }

                    appDbContext.AddRange(chartConfirmation);
                }
            }
            int result = appDbContext.SaveChanges();
            return result;
        }

        public int AddChartConfirmation(int personId, int periodDefinitionId)
        {
            var person = appDbContext.People.Where(c => c.PeopleId == personId && c.EffectiveEndDate == null).FirstOrDefault();
            var chartConfirmation2 = appDbContext.ChartConfirmation.Where(c => c.SupervisorId == personId && c.PeriodDefinitionId == periodDefinitionId).ToList();

            if (person != null)
            {
                var evalHierarchy = appDbContext.evaluationHierarchies.Where(c => c.SupervisorId == person.PeopleId && c.EffectiveEndDate == null).ToList();
                List<ChartConfirmation> chartConfirmation = new List<ChartConfirmation>();
                if (chartConfirmation2.Count > 0)
                {
                    foreach (var item in chartConfirmation2)
                    {
                        item.Confirmation = true;
                        item.LastUpdatedBy = personId;
                        item.LastUpdatedDate = DateTime.Now;
                        item.SupervisorId = personId;
                    }
                    appDbContext.UpdateRange(chartConfirmation2);
                }
                else
                {
                    foreach (var item in evalHierarchy)
                    {
                        chartConfirmation.Add(new ChartConfirmation
                        {
                            EvaluationHierarchyId = item.EvaluationHierarchyId,
                            PeriodDefinitionId = periodDefinitionId,
                            Confirmation = true,
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now,
                            SupervisorId=personId
                        });
                    }

                    appDbContext.AddRange(chartConfirmation);
                }
            }
            int result = appDbContext.SaveChanges();
            return result;
        }
        public bool GetConfirmation(int personId)
        {
            var periodDefinitionId = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom >= DateTime.Now && c.DateTo <= DateTime.Now).SingleOrDefault().PeriodDefinitoionId;
            var confirmation = appDbContext.ChartConfirmation.Where(c => c.CreatedBy == personId && c.PeriodDefinitionId == periodDefinitionId).FirstOrDefault().Confirmation;

            return confirmation;
        }
    }
}
