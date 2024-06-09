using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class ScoreScheduleService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ScoreScheduleService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public int AddScoreSchedule(DateTime selfScoreDateFrom2, DateTime selfScoreDateTo2, DateTime participantScoreDateFrom2,
                                    DateTime participantScoreDateTo2, DateTime coacher1ScoreDateFrom2, DateTime coacher1ScoreDateTo2,
                                    DateTime coacher2ScoreDateFrom2, DateTime coacher2ScoreDateTo2, int periodDefinitionId, int personId)
        {
            List<ScoreSchedule> scoreSchedule = new List<ScoreSchedule>();

            scoreSchedule.Add(new ScoreSchedule
            {
                PeriodDefinitionId = periodDefinitionId,
                ScoreScheduleTypeId = 1,
                DateFrom = selfScoreDateFrom2,
                DateTo = selfScoreDateTo2,
                CreatedBy = personId,
                CreatedDate = DateTime.Now
            });
            scoreSchedule.Add(new ScoreSchedule
            {
                PeriodDefinitionId = periodDefinitionId,
                ScoreScheduleTypeId = 2,
                DateFrom = participantScoreDateFrom2,
                DateTo = participantScoreDateTo2,
                CreatedBy = personId,
                CreatedDate = DateTime.Now
            });
            scoreSchedule.Add(new ScoreSchedule
            {
                PeriodDefinitionId = periodDefinitionId,
                ScoreScheduleTypeId = 3,
                DateFrom = coacher1ScoreDateFrom2,
                DateTo = coacher1ScoreDateTo2,
                CreatedBy = personId,
                CreatedDate = DateTime.Now
            });
            scoreSchedule.Add(new ScoreSchedule
            {
                PeriodDefinitionId = periodDefinitionId,
                ScoreScheduleTypeId = 4,
                DateFrom = coacher2ScoreDateFrom2,
                DateTo = coacher2ScoreDateTo2,
                CreatedBy = personId,
                CreatedDate = DateTime.Now
            });

            appDbContext.AddRange(scoreSchedule);

            return appDbContext.SaveChanges();
        }
        public Dictionary<object, object> GetRelatedScoreScheduleWithPeriodDefinitionList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "DateFrom", "DateTo", "EndSectionDateFrom", "EndSectionDateTo" };
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
            string queryTotalResult = "SELECT " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",DateFrom" +
                ",DateTo" +
                ",SectionPeriodId" +
                ",StatusCode" +
                ",EndSectionDateFrom" +
                ",EndSectionDateTo " +
                "FROM(SELECT " +
                "ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId desc) As indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.DateFrom" +
                ", pd.DateTo" +
                ", sp.SectionPeriodId" +
                ", sp.StatusCode" +
                ", sp.DateFrom EndSectionDateFrom" +
                ", sp.DateTo EndSectionDateTo " +
                "FROM PeriodDefinitoion pd join SectionPeriod sp on pd.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "WHERE 1 = 1 " +
                "and sp.StatusCode = 3" +
                "and exists(select * from ScoreSchedule sc where sc.PeriodDefinitionId=pd.PeriodDefinitoionId) " +
                ") PeriodDefinition where 1 = 1 ";
            string queryFilteredTotal = "SELECT " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",DateFrom" +
                ",DateTo" +
                ",SectionPeriodId" +
                ",StatusCode" +
                ",EndSectionDateFrom" +
                ",EndSectionDateTo " +
                "FROM(SELECT " +
                "ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId desc) As indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.DateFrom" +
                ", pd.DateTo" +
                ", sp.SectionPeriodId" +
                ", sp.StatusCode" +
                ", sp.DateFrom EndSectionDateFrom" +
                ", sp.DateTo EndSectionDateTo " +
                "FROM PeriodDefinitoion pd join SectionPeriod sp on pd.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "WHERE 1 = 1 " +
                "and sp.StatusCode = 3" +
                "and exists(select * from ScoreSchedule sc where sc.PeriodDefinitionId=pd.PeriodDefinitoionId) " +
                ") PeriodDefinition where 1 = 1 " +
                where;
            string sQuery = "SELECT " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",DateFrom" +
                ",DateTo" +
                ",SectionPeriodId" +
                ",StatusCode" +
                ",EndSectionDateFrom" +
                ",EndSectionDateTo " +
                "FROM(SELECT " +
                "ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId desc) As indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.DateFrom" +
                ", pd.DateTo" +
                ", sp.SectionPeriodId" +
                ", sp.StatusCode" +
                ", sp.DateFrom EndSectionDateFrom" +
                ", sp.DateTo EndSectionDateTo " +
                "FROM PeriodDefinitoion pd join SectionPeriod sp on pd.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "WHERE 1 = 1 " +
                "and sp.StatusCode = 3" +
                "and exists(select * from ScoreSchedule sc where sc.PeriodDefinitionId=pd.PeriodDefinitoionId) " +
                ") PeriodDefinition where 1 = 1 " +
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
                    sVal = "%" + dataTableParameter.search + "%"
                }
                ).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%"
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%"
                }
                ).ToList();
            }
            object totalResult = conn.Query(queryTotalResult).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%"
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return (dictionary);
        }

        public int EditScoreSchedule(DateTime selfScoreDateFrom, DateTime selfScoreDateTo, DateTime participantScoreDateFrom, DateTime participantScoreDateTo, DateTime coacher1ScoreDateFrom, DateTime coacher1ScoreDateTo, DateTime coacher2ScoreDateFrom, DateTime coacher2ScoreDateTo, int periodDefinitoionId, int personId)
        {
            List<ScoreSchedule> scoreSchedule = appDbContext.ScoreSchedule.Where(c => c.PeriodDefinitionId == periodDefinitoionId).ToList();

            foreach (var item in scoreSchedule)
            {
                switch (item.ScoreScheduleTypeId)
                {
                    case 1:
                        if (item.DateFrom.Date != selfScoreDateFrom.Date || item.DateTo.Date != selfScoreDateTo.Date)
                        {
                            item.DateFrom = selfScoreDateFrom;
                            item.DateTo = selfScoreDateTo;
                            item.LastUpdatedDate = DateTime.Now;
                            item.LastUpdatedBy = personId;
                            appDbContext.Update(item);
                        }
                        break;
                    case 2:
                        if (item.DateFrom.Date != participantScoreDateFrom.Date || item.DateTo.Date != participantScoreDateTo.Date)
                        {
                            item.DateFrom = participantScoreDateFrom;
                            item.DateTo = participantScoreDateTo;
                            item.LastUpdatedDate = DateTime.Now;
                            item.LastUpdatedBy = personId;
                            appDbContext.Update(item);
                        }
                        break;
                    case 3:
                        if (item.DateFrom.Date != coacher1ScoreDateFrom.Date || item.DateTo.Date != coacher1ScoreDateTo.Date)
                        {
                            item.DateFrom = coacher1ScoreDateFrom;
                            item.DateTo = coacher1ScoreDateTo;
                            item.LastUpdatedDate = DateTime.Now;
                            item.LastUpdatedBy = personId;
                            appDbContext.Update(item);
                        }
                        break;
                    case 4:
                        if (item.DateFrom.Date != coacher2ScoreDateFrom.Date || item.DateTo.Date != coacher2ScoreDateTo.Date)
                        {
                            item.DateFrom = coacher2ScoreDateFrom;
                            item.DateTo = coacher2ScoreDateTo;
                            item.LastUpdatedDate = DateTime.Now;
                            item.LastUpdatedBy = personId;
                            appDbContext.Update(item);
                        }
                        break;
                    default:
                        break;
                }
            }
            int result = appDbContext.SaveChanges();
            return result;
        }

        public IEnumerable<RelatedScoreScheduleView> GetRelatedScoreSchedule(int periodDefinitionId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "ss.ScoreScheduleId" +
                ",ss.PeriodDefinitionId" +
                ",ss.DateFrom" +
                ",ss.DateTo" +
                ",sst.ScoreScheduleTypeId" +
                ",sst.Title " +
                "from ScoreSchedule ss join ScoreScheduleType sst on ss.ScoreScheduleTypeId = sst.ScoreScheduleTypeId " +
                "where " +
                "1 = 1 " +
                "and ss.PeriodDefinitionId = @periodDefinitionIdd";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<RelatedScoreScheduleView> query = null;

            query = conn.Query<RelatedScoreScheduleView>(sQuery, new
            {
                periodDefinitionIdd = periodDefinitionId,
            }).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
    }
}
