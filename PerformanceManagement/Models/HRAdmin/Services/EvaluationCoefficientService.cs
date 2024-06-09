using Dapper;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class EvaluationCoefficientService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public EvaluationCoefficientService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int AddEvaluationCoefficient(EvaluationCoefficient evaluationCoefficient, int periodDefinitionId)
        {
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            int finalResult = 0;
            if (periodDefinition != null && periodDefinition.EvaluationCoefficientId == null)
            {
                periodDefinition.EvaluationCoefficient = evaluationCoefficient;
                appDbContext.Update(periodDefinition);
                finalResult = appDbContext.SaveChanges();
            }
            return (finalResult);
        }

        public Dictionary<object, object> EvaluationCoefficientList(DataTableParameter dataTableParameter)
        {
            IDbConnection conn = connProvider.Connection;

            conn.Open();

            String[] aColumns = { "PeriodDefinitoionId", "PeriodCode", "PeriodTitle", "EvaluationCoefficientId", "level1CoacherTWith", "level2CoacherTWith", "selfEvaluationTWith", "participantCoefficientT", "assignTo" };
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
            string queryTotalResult = "select " +
                "PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",EvaluationCoefficientId" +
                ",level1CoacherTWith" +
                ",level2CoacherTWith" +
                ",selfEvaluationTWith" +
                ",participantCoefficientT" +
                ",assignTo from (" +
                " select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherTWith" +
                ",ec.level2CoacherTWith" +
                ",ec.selfEvaluationTWith" +
                ",ec.participantCoefficientT" +
                ",N'وظایف' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ", PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherBWith" +
                ",ec.level2CoacherBWith" +
                ",ec.selfEvaluationBWith" +
                ",ec.participantCoefficientB" +
                ",N'رفتاری' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ",PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherBWithout" +
                ",ec.level2CoacherBWithout" +
                ",ec.selfEvaluationBWithout" +
                ",-1 participantCoefficientB" +
                ",N'رفتاری' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ", PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherTWithout" +
                ",ec.level2CoacherTWithout" +
                ",ec.selfEvaluationTWithout" +
                ",-1 participantCoefficientT" +
                ",N'وظایف' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ",PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId) pec";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",EvaluationCoefficientId" +
                ",level1CoacherTWith" +
                ",level2CoacherTWith" +
                ",selfEvaluationTWith" +
                ",participantCoefficientT" +
                ",assignTo " +
                " from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",EvaluationCoefficientId" +
                ",level1CoacherTWith" +
                ",level2CoacherTWith" +
                ",selfEvaluationTWith" +
                ",participantCoefficientT" +
                ",assignTo from (" +
                " select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherTWith" +
                ",ec.level2CoacherTWith" +
                ",ec.selfEvaluationTWith" +
                ",ec.participantCoefficientT" +
                ",N'وظایف' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ", PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherBWith" +
                ",ec.level2CoacherBWith" +
                ",ec.selfEvaluationBWith" +
                ",ec.participantCoefficientB" +
                ",N'رفتاری' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ",PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherBWithout" +
                ",ec.level2CoacherBWithout" +
                ",ec.selfEvaluationBWithout" +
                ",-1 participantCoefficientB" +
                ",N'رفتاری' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ", PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherTWithout" +
                ",ec.level2CoacherTWithout" +
                ",ec.selfEvaluationTWithout" +
                ",-1 participantCoefficientT" +
                ",N'وظایف' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ",PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId) pec )pec2 where 1=1 " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",EvaluationCoefficientId" +
                ",level1CoacherTWith" +
                ",level2CoacherTWith" +
                ",selfEvaluationTWith" +
                ",participantCoefficientT" +
                ",assignTo " +
                " from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",EvaluationCoefficientId" +
                ",level1CoacherTWith" +
                ",level2CoacherTWith" +
                ",selfEvaluationTWith" +
                ",participantCoefficientT" +
                ",assignTo from (" +
                " select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherTWith" +
                ",ec.level2CoacherTWith" +
                ",ec.selfEvaluationTWith" +
                ",ec.participantCoefficientT" +
                ",N'وظایف' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ", PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherBWith" +
                ",ec.level2CoacherBWith" +
                ",ec.selfEvaluationBWith" +
                ",ec.participantCoefficientB" +
                ",N'رفتاری' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ",PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherBWithout" +
                ",ec.level2CoacherBWithout" +
                ",ec.selfEvaluationBWithout" +
                ",-1 participantCoefficientB" +
                ",N'رفتاری' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ", PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId" +
                " union " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ec.EvaluationCoefficientId" +
                ",ec.level1CoacherTWithout" +
                ",ec.level2CoacherTWithout" +
                ",ec.selfEvaluationTWithout" +
                ",-1 participantCoefficientT" +
                ",N'وظایف' assignTo" +
                " from " +
                "EvaluationCoefficient ec" +
                ",PeriodDefinitoion pd" +
                " where " +
                "1 = 1" +
                " and ec.EvaluationCoefficientId = pd.EvaluationCoefficientId) pec )pec2 where 1=1 " +
                where +
                limit +
                order;

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
    }
}
