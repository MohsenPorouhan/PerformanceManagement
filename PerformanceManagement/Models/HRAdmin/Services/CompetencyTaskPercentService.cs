using Dapper;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class CompetencyTaskPercentService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public CompetencyTaskPercentService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int AddCompetencyTaskPercent(float TaskPercent, float CompetencyPercent, int periodDefinitionId)
        {
            PeriodDefinitoion periodDefinitoion = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();

            if (periodDefinitoion != null && TaskPercent + CompetencyPercent == 100)
            {
                periodDefinitoion.TaskPercent = TaskPercent;
                periodDefinitoion.CompetencyPercent = CompetencyPercent;
            }
            int finalResult = appDbContext.SaveChanges();
            return (finalResult);
        }


        public Dictionary<object, object> CompetencyTaskPercentList(DataTableParameter dataTableParameter)
        {
            IDbConnection conn = connProvider.Connection;

            String[] aColumns = { "PeriodDefinitoionId", "PeriodCode", "PeriodTitle", "TaskPercent", "CompetencyPercent" };
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
            string queryTotalResult = "select * " +
                "from (select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",pd.PeriodDefinitoionId " +
                ",pd.PeriodCode " +
                ",pd.PeriodTitle " +
                ",pd.TaskPercent " +
                ",pd.CompetencyPercent " +
                "from PeriodDefinitoion pd where " +
                "1=1 " +
                "and pd.TaskPercent is not null " +
                "and pd.CompetencyPercent is not null)periodDefinition ";

            string queryFilteredTotal = "select * " +
                "from (select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",pd.PeriodDefinitoionId " +
                ",pd.PeriodCode " +
                ",pd.PeriodTitle " +
                ",pd.TaskPercent " +
                ",pd.CompetencyPercent " +
                "from PeriodDefinitoion pd where " +
                "1=1 " +
                "and pd.TaskPercent is not null " +
                "and pd.CompetencyPercent is not null)periodDefinition where 1=1 " +
                where;

            string sQuery = "select " +
                "indexx " +
                ",PeriodDefinitoionId " +
                ",PeriodCode " +
                ",PeriodTitle " +
                ",TaskPercent " +
                ",CompetencyPercent " +
                "from (select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",pd.PeriodDefinitoionId " +
                ",pd.PeriodCode " +
                ",pd.PeriodTitle " +
                ",pd.TaskPercent " +
                ",pd.CompetencyPercent " +
                "from PeriodDefinitoion pd where " +
                "1=1 " +
                "and pd.TaskPercent is not null " +
                "and pd.CompetencyPercent is not null)periodDefinition where 1=1 " +
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
            int totalResult = conn.Query(queryTotalResult).Count();

            int filterTotal = conn.Query(queryFilteredTotal, new { sVal = "%" + dataTableParameter.search + "%" }).Count();
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
