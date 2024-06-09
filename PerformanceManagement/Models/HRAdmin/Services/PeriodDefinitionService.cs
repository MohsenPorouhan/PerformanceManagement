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
    public class PeriodDefinitionService
    {
        private readonly IConnProvider connProvider;

        //private readonly IConfiguration config;

        public PeriodDefinitionService(IConnProvider connProvider)
        {
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> periodDefinitionList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "PeriodDefinitoionId", "PeriodCode", "PeriodTitle", "DateFrom", "DateTo" };
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
            string queryTotalResult = "SELECT PeriodDefinitoionId " +
                "FROM(SELECT " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",PeriodDefinitoionId " +
                ",PeriodCode " +
                ",PeriodTitle " +
                ",DateFrom " +
                ",DateTo " +
                " FROM PeriodDefinitoion " +
                "WHERE 1=1) PeriodDefinitoion where 1=1 ";
            string queryFilteredTotal = "SELECT PeriodDefinitoionId " +
                "FROM(SELECT " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",PeriodDefinitoionId " +
                ",PeriodCode " +
                ",PeriodTitle " +
                ",DateFrom " +
                ",DateTo " +
                " FROM PeriodDefinitoion " +
                "WHERE 1=1) PeriodDefinitoion where 1=1 " +
                where;
            string sQuery = "SELECT " +
                "indexx " +
                ",PeriodDefinitoionId " +
                ",PeriodCode " +
                ",PeriodTitle " +
                ",DateFrom " +
                ",DateTo FROM(SELECT " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx " +
                ",PeriodDefinitoionId " +
                ",PeriodCode " +
                ",PeriodTitle " +
                ",DateFrom " +
                ",DateTo " +
                " FROM PeriodDefinitoion " +
                "WHERE 1=1) PeriodDefinitoion where 1=1 " +
                where +
                limit +
                order;
            conn.Open();
            List<PeriodDefinitoion> query = null;
            if (dataTableParameter.length != -1 && dataTableParameter.search.Equals(""))
            {
                query = conn.Query<PeriodDefinitoion>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%" }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<PeriodDefinitoion>(sQuery, new { sVal = "%" + dataTableParameter.search + "%" }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<PeriodDefinitoion>(sQuery, new { start = dataTableParameter.start + 1, endd = dataTableParameter.length + dataTableParameter.start, sVal = "%" + dataTableParameter.search + "%" }).ToList();
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
