using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class HRAdminCalculationService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public HRAdminCalculationService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> FinalizeCalc(int[] supervisorId, int creatorId, string roleId)
        {
            int result = 0;
            ShareService shareService = new ShareService(appDbContext, connProvider);
            int periodDefinitionId = shareService.GetPeriodDefinitionId();
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            ArrayList finalizedCalc = new ArrayList();
            ArrayList notExistCalc = new ArrayList();

            foreach (var coacherId in supervisorId)
            {
                FinalizeCalculation finalizeCalculationQuery = appDbContext.FinalizeCalculation.Where(c => c.CocherId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
                FinalScoreCalculation finalScoreCalculation = appDbContext.FinalScoreCalculation.Where(c => c.AllocatorPersonId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).FirstOrDefault();
                FinalScoreCompetencyCalculation finalScoreCompetencyCalculation = appDbContext.FinalScoreCompetencyCalculation.Where(c => c.AllocatorPersonId == coacherId && c.PeriodDefinitoionId == periodDefinitionId).FirstOrDefault();

                if ((finalScoreCalculation != null || finalScoreCompetencyCalculation != null) && finalizeCalculationQuery == null)
                {
                    FinalizeCalculation finalizeCalculation = new FinalizeCalculation();
                    finalizeCalculation.CocherId = coacherId;
                    finalizeCalculation.IsFinalization = true;
                    finalizeCalculation.PeriodDefinitoionId = periodDefinitionId;
                    finalizeCalculation.CreatedBy = creatorId;
                    finalizeCalculation.CreatedDate = DateTime.Now;
                    finalizeCalculation.RoleId = roleId;
                    appDbContext.Add(finalizeCalculation);
                }
                else if (finalizeCalculationQuery != null)
                {
                    var people = appDbContext.People.Where(c => c.PeopleId == coacherId && c.EffectiveEndDate == null).FirstOrDefault();
                    finalizedCalc.Add(string.Format("{0} {1}", people.FirstName, people.LastName));
                }
                else if (finalScoreCalculation == null && finalScoreCompetencyCalculation == null)
                {
                    var people = appDbContext.People.Where(c => c.PeopleId == coacherId && c.EffectiveEndDate == null).FirstOrDefault();
                    notExistCalc.Add(string.Format("{0} {1}", people.FirstName, people.LastName));
                }
            }
            result = appDbContext.SaveChanges();
            dictionary.Add("finalizedCalc", finalizedCalc);
            dictionary.Add("notExistCalc", notExistCalc);
            dictionary.Add("result", result);
            return dictionary;
        }

        public Dictionary<object, object> GetFinalizationCalcList(DataTableParameter dataTableParameter, int? periodDefinitionId)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "UserName", "FullName" };
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
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",UserName" +
                ",CocherId" +
                ",FullName" +
                ",CreatedDate " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId asc))  indexx " +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", anu.UserName" +
                ", fc.CocherId" +
                ", (select distinct CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = fc.CocherId and p.EffectiveEndDate is null)FullName" +
                ",fc.CreatedDate " +
                "from " +
                "FinalizeCalculation fc join PeriodDefinitoion pd on fc.PeriodDefinitoionId = pd.PeriodDefinitoionId " +
                "join AspNetUsers anu on anu.PeopleId = fc.CocherId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = @periodDefinitionIdDTt)tbl where 1 = 1  ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",UserName" +
                ",CocherId" +
                ",FullName" +
                ",CreatedDate " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId asc))  indexx " +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", anu.UserName" +
                ", fc.CocherId" +
                ", (select distinct CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = fc.CocherId and p.EffectiveEndDate is null)FullName" +
                ",fc.CreatedDate " +
                "from " +
                "FinalizeCalculation fc join PeriodDefinitoion pd on fc.PeriodDefinitoionId = pd.PeriodDefinitoionId " +
                "join AspNetUsers anu on anu.PeopleId = fc.CocherId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = @periodDefinitionIdDTt)tbl where 1 = 1  " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",UserName" +
                ",CocherId" +
                ",FullName" +
                ",CreatedDate " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId asc))  indexx " +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", anu.UserName" +
                ", fc.CocherId" +
                ", (select distinct CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = fc.CocherId and p.EffectiveEndDate is null)FullName" +
                ",fc.CreatedDate " +
                "from " +
                "FinalizeCalculation fc join PeriodDefinitoion pd on fc.PeriodDefinitoionId = pd.PeriodDefinitoionId " +
                "join AspNetUsers anu on anu.PeopleId = fc.CocherId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = @periodDefinitionIdDTt)tbl where 1 = 1  " +
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
                    periodDefinitionIdDTt = periodDefinitionId,
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    periodDefinitionIdDTt = periodDefinitionId,
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    periodDefinitionIdDTt = periodDefinitionId,
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                periodDefinitionIdDTt = periodDefinitionId,
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                periodDefinitionIdDTt = periodDefinitionId,
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
