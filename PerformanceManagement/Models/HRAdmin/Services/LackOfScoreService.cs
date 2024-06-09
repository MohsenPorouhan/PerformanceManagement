using Dapper;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class LackOfScoreService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public LackOfScoreService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int LackOfScoreCalculationWay(int lackOfScore, int periodDefinitoionId)
        {
            var periodDefinitoion = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitoionId).SingleOrDefault();
            periodDefinitoion.LackOfScore = lackOfScore;
            appDbContext.PeriodDefinitoion.Update(periodDefinitoion);
            int finalResult = appDbContext.SaveChanges();
            return finalResult;
        }

        public Dictionary<object, object> LackOfScoreList(DataTableParameter dataTableParameter)
        {
            IDbConnection conn = connProvider.Connection;

            conn.Open();

            String[] aColumns = { "PeriodDefinitoionId", "PeriodCode", "PeriodTitle" };
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
                "indexx" +
                ",DateFrom" +
                ",DateTo" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",LackOfScore " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId desc) As indexx" +
                ", pd.DateFrom" +
                ", pd.DateTo" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.LackOfScore " +
                "from " +
                "PeriodDefinitoion pd " +
                "where 1 = 1)tbl where 1=1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",DateFrom" +
                ",DateTo" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",LackOfScore " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId desc) As indexx" +
                ", pd.DateFrom" +
                ", pd.DateTo" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.LackOfScore " +
                "from " +
                "PeriodDefinitoion pd " +
                "where 1 = 1)tbl where 1=1 " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",DateFrom" +
                ",DateTo" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",LackOfScore " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId desc) As indexx" +
                ", pd.DateFrom" +
                ", pd.DateTo" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.LackOfScore " +
                "from " +
                "PeriodDefinitoion pd " +
                "where 1 = 1)tbl where 1=1 " +
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
