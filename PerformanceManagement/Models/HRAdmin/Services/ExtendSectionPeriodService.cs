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
    public class ExtendSectionPeriodService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ExtendSectionPeriodService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }


        public Dictionary<object, object> AddExtendSectionPeriod(int periodDefinitionId, string[] employee, int personId
            , DateTime? periodDefinitionInitialDateFrom2, DateTime? periodDefinitionInitialDateTo2
            , DateTime? periodDefinitionRunTimeDateFrom2, DateTime? periodDefinitionRunTimeDateTo2
            , DateTime? periodDefinitionFinalDateFrom2, DateTime? periodDefinitionFinalDateTo2
            , DateTime? periodDefinitionProtestDateFrom2, DateTime? periodDefinitionProtestDateTo2
            , DateTime? selfScoreDateFrom2, DateTime? selfScoreDateTo2, DateTime? participantScoreDateFrom2, DateTime? participantScoreDateTo2, DateTime? coacher1ScoreDateFrom2
            , DateTime? coacher1ScoreDateTo2, DateTime? coacher2ScoreDateFrom2, DateTime? coacher2ScoreDateTo2)
        {
            Dictionary<object, object> dictionary = new Dictionary<object, object>();

            PeriodDefinitoion periodDefinitoion = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            int sectionInitialId = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 1).SingleOrDefault().SectionPeriodId;
            int sectionRunTimeId = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 2).SingleOrDefault().SectionPeriodId;
            int sectionFinalId = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 3).SingleOrDefault().SectionPeriodId;
            int sectionProtestId = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 4).SingleOrDefault().SectionPeriodId;

            bool isTrue = false;

            if (periodDefinitionInitialDateFrom2 != null && periodDefinitionInitialDateTo2 != null)
            {
                if (periodDefinitoion != null && periodDefinitoion.DateTo >= periodDefinitionInitialDateTo2 && periodDefinitoion.DateFrom <= periodDefinitionInitialDateFrom2 && periodDefinitionInitialDateFrom2 < periodDefinitionInitialDateTo2)
                {

                    isTrue = true;
                    ExtendSectionPeriod extendSectionPeriod = new ExtendSectionPeriod();
                    extendSectionPeriod.SectionPeriodId = sectionInitialId;
                    extendSectionPeriod.DateFrom = periodDefinitionInitialDateFrom2;
                    extendSectionPeriod.DateTo = periodDefinitionInitialDateTo2;
                    extendSectionPeriod.CreatedBy = personId;
                    extendSectionPeriod.CreatedDate = DateTime.Now;
                    List<ExtendSectionPeriodWithPeople> extendSectionPeriodWithPeople = new List<ExtendSectionPeriodWithPeople>();
                    foreach (var item in employee)
                    {
                        extendSectionPeriodWithPeople.Add(new ExtendSectionPeriodWithPeople
                        {
                            EvaluationHierarchyId = int.Parse(item.Split('-')[0]),
                            PeopleId = int.Parse(item.Split('-')[1]),
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now
                        });
                    }
                    extendSectionPeriod.ExtendSectionPeriodWithPeople = extendSectionPeriodWithPeople;
                    appDbContext.Add(extendSectionPeriod);

                }
                else
                {
                    isTrue = false;
                }
            }
            if (periodDefinitionRunTimeDateFrom2 != null && periodDefinitionRunTimeDateTo2 != null)
            {
                if (periodDefinitoion != null && periodDefinitoion.DateTo >= periodDefinitionRunTimeDateTo2 && periodDefinitoion.DateFrom <= periodDefinitionRunTimeDateFrom2 && periodDefinitionRunTimeDateFrom2 < periodDefinitionRunTimeDateTo2)
                {
                    isTrue = true;
                    ExtendSectionPeriod extendSectionPeriod = new ExtendSectionPeriod();
                    extendSectionPeriod.SectionPeriodId = sectionRunTimeId;
                    extendSectionPeriod.DateFrom = periodDefinitionRunTimeDateFrom2;
                    extendSectionPeriod.DateTo = periodDefinitionRunTimeDateTo2;
                    //extendSectionPeriod.EvaluationHierarchyId = int.Parse(item.Split('-')[0]);
                    //extendSectionPeriod.PeopleId = int.Parse(item.Split('-')[1]);
                    extendSectionPeriod.CreatedBy = personId;
                    extendSectionPeriod.CreatedDate = DateTime.Now;
                    List<ExtendSectionPeriodWithPeople> extendSectionPeriodWithPeople = new List<ExtendSectionPeriodWithPeople>();
                    foreach (var item in employee)
                    {
                        extendSectionPeriodWithPeople.Add(new ExtendSectionPeriodWithPeople
                        {
                            EvaluationHierarchyId = int.Parse(item.Split('-')[0]),
                            PeopleId = int.Parse(item.Split('-')[1]),
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now
                        });
                    }
                    extendSectionPeriod.ExtendSectionPeriodWithPeople = extendSectionPeriodWithPeople;
                    appDbContext.Add(extendSectionPeriod);
                }
                else
                {
                    isTrue = false;
                }
            }
            if (periodDefinitionFinalDateFrom2 != null && periodDefinitionFinalDateTo2 != null)
            {
                if (periodDefinitoion != null && periodDefinitoion.DateTo >= periodDefinitionFinalDateTo2 && periodDefinitoion.DateFrom <= periodDefinitionFinalDateFrom2
                    && periodDefinitionFinalDateFrom2 < periodDefinitionFinalDateTo2 &&
                    periodDefinitionFinalDateTo2 >= selfScoreDateFrom2 && periodDefinitionFinalDateFrom2 <= selfScoreDateFrom2 && selfScoreDateFrom2 < selfScoreDateTo2 && periodDefinitionFinalDateTo2 >= selfScoreDateTo2 && periodDefinitionFinalDateFrom2 <= selfScoreDateTo2
                && periodDefinitionFinalDateTo2 >= participantScoreDateFrom2 && periodDefinitionFinalDateFrom2 <= participantScoreDateFrom2 && participantScoreDateFrom2 < participantScoreDateTo2 && periodDefinitionFinalDateTo2 >= participantScoreDateTo2 && periodDefinitionFinalDateFrom2 <= participantScoreDateTo2
                && periodDefinitionFinalDateTo2 >= coacher1ScoreDateFrom2 && periodDefinitionFinalDateFrom2 <= coacher1ScoreDateFrom2 && coacher1ScoreDateFrom2 < coacher1ScoreDateTo2 && periodDefinitionFinalDateTo2 >= coacher1ScoreDateTo2 && periodDefinitionFinalDateFrom2 <= participantScoreDateTo2
                && periodDefinitionFinalDateTo2 >= coacher2ScoreDateFrom2 && periodDefinitionFinalDateFrom2 <= coacher2ScoreDateFrom2 && coacher2ScoreDateFrom2 < coacher2ScoreDateTo2 && periodDefinitionFinalDateTo2 >= coacher2ScoreDateTo2 && periodDefinitionFinalDateFrom2 <= coacher2ScoreDateTo2)
                {

                    isTrue = true;
                    ExtendSectionPeriod extendSectionPeriod = new ExtendSectionPeriod();
                    extendSectionPeriod.SectionPeriodId = sectionFinalId;
                    extendSectionPeriod.DateFrom = periodDefinitionFinalDateFrom2;
                    extendSectionPeriod.DateTo = periodDefinitionFinalDateTo2;
                    //extendSectionPeriod.EvaluationHierarchyId = int.Parse(item.Split('-')[0]);
                    //extendSectionPeriod.PeopleId = int.Parse(item.Split('-')[1]);
                    extendSectionPeriod.CreatedBy = personId;
                    extendSectionPeriod.CreatedDate = DateTime.Now;
                    List<ExtendSectionPeriodWithPeople> extendSectionPeriodWithPeople = new List<ExtendSectionPeriodWithPeople>();
                    foreach (var item in employee)
                    {
                        extendSectionPeriodWithPeople.Add(new ExtendSectionPeriodWithPeople
                        {
                            EvaluationHierarchyId = int.Parse(item.Split('-')[0]),
                            PeopleId = int.Parse(item.Split('-')[1]),
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now
                        });
                    }
                    extendSectionPeriod.ExtendSectionPeriodWithPeople = extendSectionPeriodWithPeople;

                    List<ExtendScoreSchedule> extendScoreSchedules = new List<ExtendScoreSchedule>();

                    ExtendScoreSchedule extendScoreSchedule = new ExtendScoreSchedule();
                    extendScoreSchedule.ScoreScheduleTypeId = 1;
                    extendScoreSchedule.DateFrom = selfScoreDateFrom2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule.DateTo = selfScoreDateTo2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule.CreatedBy = personId;
                    extendScoreSchedule.CreatedDate = DateTime.Now;
                    extendScoreSchedules.Add(extendScoreSchedule);

                    ExtendScoreSchedule extendScoreSchedule2 = new ExtendScoreSchedule();
                    extendScoreSchedule2.ScoreScheduleTypeId = 2;
                    extendScoreSchedule2.DateFrom = participantScoreDateFrom2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule2.DateTo = participantScoreDateTo2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule2.CreatedBy = personId;
                    extendScoreSchedule2.CreatedDate = DateTime.Now;
                    extendScoreSchedules.Add(extendScoreSchedule2);

                    ExtendScoreSchedule extendScoreSchedule3 = new ExtendScoreSchedule();
                    extendScoreSchedule3.ScoreScheduleTypeId = 3;
                    extendScoreSchedule3.DateFrom = coacher1ScoreDateFrom2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule3.DateTo = coacher1ScoreDateTo2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule3.CreatedBy = personId;
                    extendScoreSchedule3.CreatedDate = DateTime.Now;
                    extendScoreSchedules.Add(extendScoreSchedule3);

                    ExtendScoreSchedule extendScoreSchedule4 = new ExtendScoreSchedule();
                    extendScoreSchedule4.ScoreScheduleTypeId = 4;
                    extendScoreSchedule4.DateFrom = coacher2ScoreDateFrom2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule4.DateTo = coacher2ScoreDateTo2 ?? throw new NoNullAllowedException();
                    extendScoreSchedule4.CreatedBy = personId;
                    extendScoreSchedule4.CreatedDate = DateTime.Now;
                    extendScoreSchedules.Add(extendScoreSchedule4);

                    extendSectionPeriod.ExtendScoreSchedules = extendScoreSchedules;

                    appDbContext.Add(extendSectionPeriod);
                }
                else
                {
                    isTrue = false;
                }
            }
            if (periodDefinitionProtestDateFrom2 != null && periodDefinitionProtestDateTo2 != null)
            {
                if (periodDefinitoion != null && periodDefinitoion.DateTo >= periodDefinitionProtestDateTo2 && periodDefinitoion.DateFrom <= periodDefinitionProtestDateFrom2 && periodDefinitionProtestDateFrom2 < periodDefinitionProtestDateTo2)
                {

                    isTrue = true;
                    ExtendSectionPeriod extendSectionPeriod = new ExtendSectionPeriod();
                    extendSectionPeriod.SectionPeriodId = sectionProtestId;
                    extendSectionPeriod.DateFrom = periodDefinitionProtestDateFrom2;
                    extendSectionPeriod.DateTo = periodDefinitionProtestDateTo2;
                    //extendSectionPeriod.EvaluationHierarchyId = int.Parse(item.Split('-')[0]);
                    //extendSectionPeriod.PeopleId = int.Parse(item.Split('-')[1]);
                    extendSectionPeriod.CreatedBy = personId;
                    extendSectionPeriod.CreatedDate = DateTime.Now;
                    List<ExtendSectionPeriodWithPeople> extendSectionPeriodWithPeople = new List<ExtendSectionPeriodWithPeople>();
                    foreach (var item in employee)
                    {
                        extendSectionPeriodWithPeople.Add(new ExtendSectionPeriodWithPeople
                        {
                            EvaluationHierarchyId = int.Parse(item.Split('-')[0]),
                            PeopleId = int.Parse(item.Split('-')[1]),
                            CreatedBy = personId,
                            CreatedDate = DateTime.Now
                        });
                    }
                    extendSectionPeriod.ExtendSectionPeriodWithPeople = extendSectionPeriodWithPeople;
                    appDbContext.Add(extendSectionPeriod);

                }
                else
                {
                    isTrue = false;
                }
            }
            int result = 0;
            if (isTrue)
            {
                result = appDbContext.SaveChanges();
                dictionary.Add("validation", "valid");

            }
            else
            {
                dictionary.Add("validation", "notValid");
            }

            dictionary.Add("result", result);
            return dictionary;
        }
        public Dictionary<object, object> GetRelatedPeriodDefinitionWithExtendSectionPeriodList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "DateFrom", "DateTo" };
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
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",DateFrom" +
                ",DateTo " +
                "from " +
                "(select " +
                "(ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId asc))  indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.DateFrom" +
                ", pd.DateTo " +
                "from " +
                "PeriodDefinitoion pd " +
                "where exists" +
                "( " +
                "select " +
                "* " +
                "from " +
                "PeriodDefinitoion pd2 " +
                "join SectionPeriod sp on pd2.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "join ExtendSectionPeriod esp on sp.SectionPeriodId = esp.SectionPeriodId " +
                "))tbl where 1 = 1 ";
            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",DateFrom" +
                ",DateTo " +
                "from " +
                "(select " +
                "(ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId asc))  indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.DateFrom" +
                ", pd.DateTo " +
                "from " +
                "PeriodDefinitoion pd " +
                "where exists" +
                "( " +
                "select " +
                "* " +
                "from " +
                "PeriodDefinitoion pd2 " +
                "join SectionPeriod sp on pd2.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "join ExtendSectionPeriod esp on sp.SectionPeriodId = esp.SectionPeriodId " +
                "))tbl where 1 = 1 " +
                where;
            string sQuery = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",DateFrom" +
                ",DateTo " +
                "from " +
                "(select " +
                "(ROW_NUMBER() OVER(ORDER BY pd.PeriodDefinitoionId asc))  indexx" +
                ", pd.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", pd.DateFrom" +
                ", pd.DateTo " +
                "from " +
                "PeriodDefinitoion pd " +
                "where exists" +
                "( " +
                "select " +
                "* " +
                "from " +
                "PeriodDefinitoion pd2 " +
                "join SectionPeriod sp on pd2.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "join ExtendSectionPeriod esp on sp.SectionPeriodId = esp.SectionPeriodId " +
                "))tbl where 1 = 1 " +
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

        public List<ExtendScoreSchedule> GetExtendScoreSchedule(int extendSectionPeriodId)
        {
            return appDbContext.ExtendScoreSchedule.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).ToList();
        }

        public IEnumerable<RelatedExtendSectionView> GetRelatedExtendSectionPeriod(int periodDefinitionId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "esp.ExtendSectionPeriodId" +
                ",esp.SectionPeriodId" +
                ",esp.DateFrom" +
                ",esp.DateTo" +
                ",sp.DateFrom PrimarySectionDateFrom" +
                ", sp.DateTo PrimarySectionDateTo" +
                ", sp.StatusCode" +
                ",sp.PeriodDefinitoionId " +
                ",pd.PeriodTitle " +
                "from PeriodDefinitoion pd join SectionPeriod sp on pd.PeriodDefinitoionId = sp.PeriodDefinitoionId " +
                "join ExtendSectionPeriod esp on sp.SectionPeriodId = esp.SectionPeriodId " +
                "where " +
                "1 = 1 " +
                "and pd.PeriodDefinitoionId = @periodDefinitionIdd";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<RelatedExtendSectionView> query = null;

            query = conn.Query<RelatedExtendSectionView>(sQuery, new
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
        public IEnumerable<GetRelatedPeopleWithExtendSectionPeriodView> GetRelatedPeopleWithExtendSectionPeriod(int ExtendSectionPeriodId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "espwp.ExtendSectionPeriodWithPeopleId" +
                ",espwp.ExtendSectionPeriodId" +
                ",espwp.PeopleId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = espwp.PeopleId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ",espwp.EvaluationHierarchyId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = espwp.EvaluationHierarchyId)EmployeeDepartmentName " +
                "from " +
                "ExtendSectionPeriodWithPeople espwp " +
                "where " +
                "1 = 1 " +
                "and espwp.ExtendSectionPeriodId = @ExtendSectionPeriodIdd ";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<GetRelatedPeopleWithExtendSectionPeriodView> query = null;

            query = conn.Query<GetRelatedPeopleWithExtendSectionPeriodView>(sQuery, new
            {
                ExtendSectionPeriodIdd = ExtendSectionPeriodId,
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
