using Dapper;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class WeightWayService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public WeightWayService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int AddLikertScale(string[] tagCompetency, string competencyName, string[] tagTask, string taskName, string[] tagScore, string scoreName)
        {
            int likertDescriborMaxId = 0;
            int likertScaleMaxId = 0;
            var exist = appDbContext.LikertScale.Count();
            var exist2 = appDbContext.LikertDescribor.Count();
            if (exist == 0)
            {
                likertScaleMaxId = 1;
            }
            else
            {
                likertScaleMaxId = appDbContext.LikertScale.Max(c => c.LikertScaleId) + 1;
            }
            if (exist2 == 0)
            {
                likertDescriborMaxId = 0;//1
            }
            else
            {
                likertDescriborMaxId = appDbContext.LikertDescribor.Max(c => c.LikertDescriborId);
            }

            LikertScale likertScaleCompetency = new LikertScale();
            List<LikertDescribor> likertDescriborCompetency = new List<LikertDescribor>();
            int i = 1;
            foreach (var item in tagCompetency)
            {
                likertDescriborCompetency.Add(new LikertDescribor()
                {
                    LikertDescriborId = likertDescriborMaxId + i,
                    TitleScale = item,
                    NumberScale = i,
                    EffectiveStartDate = DateTime.Now
                });
                i++;
            }

            likertScaleCompetency.LikertScaleId = likertScaleMaxId;
            likertScaleCompetency.EffectiveStartDate = DateTime.Now;
            likertScaleCompetency.Name = competencyName;
            likertScaleCompetency.Type = 1;
            likertScaleCompetency.LikertDescribors = likertDescriborCompetency;
            appDbContext.Add(likertScaleCompetency);

            LikertScale likertScaleTask = new LikertScale();
            List<LikertDescribor> likertDescriborTask = new List<LikertDescribor>();
            int j = 1;
            foreach (var item in tagTask)
            {
                likertDescriborTask.Add(new LikertDescribor()
                {
                    LikertDescriborId = likertDescriborMaxId + i,
                    TitleScale = item,
                    NumberScale = j,
                    EffectiveStartDate = DateTime.Now
                });
                i++;
                j++;
            }
            likertScaleTask.LikertScaleId = likertScaleMaxId + 1;
            likertScaleTask.EffectiveStartDate = DateTime.Now;
            likertScaleTask.Name = taskName;
            likertScaleTask.Type = 2;
            likertScaleTask.LikertDescribors = likertDescriborTask;
            appDbContext.Add(likertScaleTask);

            LikertScale likertScaleScore = new LikertScale();
            List<LikertDescribor> likertDescriborScore = new List<LikertDescribor>();
            int k = 1;
            foreach (var item in tagScore)
            {
                likertDescriborScore.Add(new LikertDescribor()
                {
                    LikertDescriborId = likertDescriborMaxId + i,
                    TitleScale = item,
                    NumberScale = k,
                    EffectiveStartDate = DateTime.Now
                });
                i++;
                k++;
            }
            likertScaleScore.LikertScaleId = likertScaleMaxId + 2;
            likertScaleScore.EffectiveStartDate = DateTime.Now;
            likertScaleScore.Name = scoreName;
            likertScaleScore.Type = 3;
            likertScaleScore.LikertDescribors = likertDescriborScore;
            appDbContext.Add(likertScaleScore);

            int finalResult = appDbContext.SaveChanges();
            return (finalResult);
        }

        public IQueryable GetCompetencyLikert()
        {
            var model = appDbContext.LikertScale.Where(c => c.EffectiveEndDate == null && c.Type == 1).Select(l => new
            {
                l.LikertScaleId,
                l.Name
            });
            return model;
        }
        public IQueryable GetTaskLikert()
        {
            var model = appDbContext.LikertScale.Where(c => c.EffectiveEndDate == null && c.Type == 2).Select(l => new
            {
                l.LikertScaleId,
                l.Name
            });
            return model;
        }
        public IQueryable GetScoreLikert()
        {
            var model = appDbContext.LikertScale.Where(c => c.EffectiveEndDate == null && c.Type == 3).Select(l => new
            {
                l.LikertScaleId,
                l.Name
            });
            return model;
        }


        public int AssignWeightWayToPeriod(int competencyWeight, int taskWeight, int scoreWeight, int periodDefinitoionId)
        {
            var periodDefinitoion = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitoionId).SingleOrDefault();
            if (competencyWeight == 1000001)
            {
                periodDefinitoion.WeightWayBehaviour = 1;//percent
                periodDefinitoion.LikertWeightWayBehaiviourId = null;
            }
            else
            {
                periodDefinitoion.WeightWayBehaviour = 2;//likert
                periodDefinitoion.LikertWeightWayBehaiviourId = competencyWeight;
            }
            if (taskWeight == 1000001)
            {
                periodDefinitoion.WeightWayTask = 1;//percent
                periodDefinitoion.LikertWeightWayTaskId = null;
            }
            else
            {
                periodDefinitoion.WeightWayTask = 2;//likert
                periodDefinitoion.LikertWeightWayTaskId = taskWeight;
            }
            if (scoreWeight == 1000001)
            {
                periodDefinitoion.WeightWayScore = 1;//percent
                periodDefinitoion.LikertScoreWayId = null;
            }
            else
            {
                periodDefinitoion.WeightWayScore = 2;//likert
                periodDefinitoion.LikertScoreWayId = scoreWeight;
            }
            appDbContext.PeriodDefinitoion.Update(periodDefinitoion);
            int finalResult = appDbContext.SaveChanges();
            return finalResult;
        }

        public IQueryable GetPeriodDefinition()
        {
            var model = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).Select(p => new
            {
                p.PeriodDefinitoionId,
                p.PeriodTitle,
                p.PeriodCode
            });
            return model;
        }

        public Dictionary<object, object> WeightWayList(DataTableParameter dataTableParameter)
        {

            String[] aColumns = { "PeriodDefinitoionId", "PeriodCode", "PeriodTitle", "Type", "Name", "TitleScale", "wieghtWay" };
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
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Name" +
                ",Type" +
                ",TitleScale" +
                ",wieghtWay " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Name" +
                ", Type" +
                ", TitleScale" +
                ", wieghtWay " +
                "from( " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertScoreWayId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "select " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode " +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertWeightWayBehaiviourId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "select " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertWeightWayTaskId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle " +
                ",null" +
                ",'1000001'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayBehaviour = 1 " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",null" +
                ",'1000002'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayTask = 1 " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",null" +
                ",'1000003'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayScore = 1 " +
                ")tbl)tbl2 where 1=1 ";

            string queryFilteredTotal = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Name" +
                ",Type" +
                ",TitleScale" +
                ",wieghtWay " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Name" +
                ", Type" +
                ", TitleScale" +
                ", wieghtWay " +
                "from( " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertScoreWayId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "select " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode " +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertWeightWayBehaiviourId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "select " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertWeightWayTaskId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle " +
                ",null" +
                ",'1000001'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayBehaviour = 1 " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",null" +
                ",'1000002'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayTask = 1 " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",null" +
                ",'1000003'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayScore = 1 " +
                ")tbl)tbl2 where 1=1 " +
                where;

            string sQuery = "select " +
                "indexx" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",Name" +
                ",Type" +
                ",TitleScale" +
                ",wieghtWay " +
                "from( " +
                "select " +
                "ROW_NUMBER() OVER(ORDER BY PeriodDefinitoionId desc) As indexx" +
                ", PeriodDefinitoionId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", Name" +
                ", Type" +
                ", TitleScale" +
                ", wieghtWay " +
                "from( " +
                "select " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertScoreWayId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "select " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode " +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertWeightWayBehaiviourId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "select " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type" +
                ",STRING_AGG(ld.TitleScale, '-')TitleScale" +
                ",N'طیف لیکرت'wieghtWay " +
                "from " +
                "PeriodDefinitoion pd join LikertScale ls on pd.LikertWeightWayTaskId = ls.LikertScaleId " +
                "join LikertDescribor ld on ls.LikertScaleId = ld.LikertScaleId " +
                "where " +
                "1 = 1 " +
                "group by " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",ls.Name" +
                ",ls.Type " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle " +
                ",null" +
                ",'1000001'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayBehaviour = 1 " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",null" +
                ",'1000002'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayTask = 1 " +
                "union " +
                "SELECT " +
                "pd.PeriodDefinitoionId " +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",null" +
                ",'1000003'type" +
                ",'---'" +
                ",'درصدی'weightWay " +
                "from PeriodDefinitoion pd " +
                "where " +
                "1 = 1 " +
                "and pd.WeightWayScore = 1 " +
                ")tbl)tbl2 where 1=1 " +
                where +
                limit +
                order;
            IDbConnection conn = connProvider.Connection;

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
