using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class BehaviouralCompetencyService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public BehaviouralCompetencyService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public IQueryable GetJobList()
        {
            var model = appDbContext.Job.Select(c => new { c.JobId, c.Title });
            return model;
        }

        public int AddBehaviouralCompetencyDefinition(string title, string[] tagCompetency, int[] job, int personId, string roleId)
        {
            BehaviouralCompetency behaviouralCompetency = new BehaviouralCompetency();
            List<Truth> truths = new List<Truth>();
            List<CorrespondentJob> correspondentJobs = new List<CorrespondentJob>();
            behaviouralCompetency.Title = title;
            foreach (var item in tagCompetency)
            {
                truths.Add(new Truth() { Title = item, CreatedBy = personId, CreatedDate = DateTime.Now });
            }
            behaviouralCompetency.Truths = truths;
            foreach (var item in job)
            {
                correspondentJobs.Add(new CorrespondentJob() { JobId = item, CreatedBy = personId, CreatedDate = DateTime.Now });
            }
            behaviouralCompetency.CreatedBy = personId;
            behaviouralCompetency.CreatedDate = DateTime.Now;
            behaviouralCompetency.RoleId = roleId;
            behaviouralCompetency.IsActive = true;
            behaviouralCompetency.CorrespondentJobs = correspondentJobs;
            appDbContext.BehaviouralCompetency.Add(behaviouralCompetency);
            var result = appDbContext.SaveChanges();
            return result;
        }
        public int EditBehaviouralCompetencyDefinition(string title, string[] tagCompetency, int[] job, int personId, string roleId,int behaviouralCompetencyId)
        {
            BehaviouralCompetency behaviouralCompetency = appDbContext.BehaviouralCompetency.Where(c=>c.BehaviouralCompetencyId== behaviouralCompetencyId).SingleOrDefault();
            List<Truth> truths = new List<Truth>();
            List<CorrespondentJob> correspondentJobs = new List<CorrespondentJob>();
            behaviouralCompetency.Title = title;
            appDbContext.RemoveRange(appDbContext.Truth.Where(c => c.BehaviouralCompetencyId == behaviouralCompetencyId).ToList());
            foreach (var item in tagCompetency)
            {
                truths.Add(new Truth() { Title = item, CreatedBy = personId, CreatedDate = DateTime.Now });
            }
            behaviouralCompetency.Truths = truths;
            appDbContext.RemoveRange(appDbContext.CorrespondentJob.Where(c => c.BehaviouralCompetencyId == behaviouralCompetencyId).ToList());
            foreach (var item in job)
            {
                correspondentJobs.Add(new CorrespondentJob() { JobId = item, CreatedBy = personId, CreatedDate = DateTime.Now });
            }
            behaviouralCompetency.LastUpdatedBy = personId;
            behaviouralCompetency.LastUpdatedDate = DateTime.Now;
            behaviouralCompetency.CorrespondentJobs = correspondentJobs;
            appDbContext.BehaviouralCompetency.Update(behaviouralCompetency);
            var result = appDbContext.SaveChanges();
            return result;
        }
        public Dictionary<object, object> GetBehaviouralList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "BehaviouralCompetencyId", "Title", "jobTitle" };
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
                ",BehaviouralCompetencyId" +
                ",Title" +
                ",jobTitle " +
                ",jobId " +
                ",IsActive " +
                "from " +
                "(select " +
                "ROW_NUMBER() OVER(ORDER BY bc.BehaviouralCompetencyId desc) As indexx" +
                ", bc.BehaviouralCompetencyId" +
                ", bc.Title" +
                ", STRING_AGG(j.Title, ' - ')jobTitle " +
                ", STRING_AGG(j.JobId, ' - ')jobId " +
                ",bc.IsActive " +
                "from " +
                "BehaviouralCompetency bc," +
                "CorrespondentJob cb," +
                "Job j " +
                "where " +
                "1 = 1 " +
                "and cb.JobId = j.JobId " +
                "and bc.BehaviouralCompetencyId = cb.BehaviouralCompetencyId " +
                "group by bc.BehaviouralCompetencyId, bc.Title,bc.IsActive)BehaviouralCompetency where 1 = 1 ";
            string queryFilteredTotal = "select " +
                "indexx" +
                ",BehaviouralCompetencyId" +
                ",Title" +
                ",jobTitle " +
                ",jobId " +
                ",IsActive " +
                "from " +
                "(select " +
                "ROW_NUMBER() OVER(ORDER BY bc.BehaviouralCompetencyId desc) As indexx" +
                ", bc.BehaviouralCompetencyId" +
                ", bc.Title" +
                ", STRING_AGG(j.Title, ' - ')jobTitle " +
                ", STRING_AGG(j.JobId, ' - ')jobId " +
                ",bc.IsActive " +
                "from " +
                "BehaviouralCompetency bc," +
                "CorrespondentJob cb," +
                "Job j " +
                "where " +
                "1 = 1 " +
                "and cb.JobId = j.JobId " +
                "and bc.BehaviouralCompetencyId = cb.BehaviouralCompetencyId " +
                "group by bc.BehaviouralCompetencyId, bc.Title,bc.IsActive)BehaviouralCompetency where 1 = 1 "  +
                where;
            string sQuery = "select " +
                "indexx" +
                ",BehaviouralCompetencyId" +
                ",Title" +
                ",jobTitle " +
                ",jobId " +
                ",IsActive " +
                "from " +
                "(select " +
                "ROW_NUMBER() OVER(ORDER BY bc.BehaviouralCompetencyId desc) As indexx" +
                ", bc.BehaviouralCompetencyId" +
                ", bc.Title" +
                ", STRING_AGG(j.Title, ' - ')jobTitle " +
                ", STRING_AGG(j.JobId, ',')jobId " +
                ",bc.IsActive " +
                "from " +
                "BehaviouralCompetency bc," +
                "CorrespondentJob cb," +
                "Job j " +
                "where " +
                "1 = 1 " +
                "and cb.JobId = j.JobId " +
                "and bc.BehaviouralCompetencyId = cb.BehaviouralCompetencyId " +
                "group by bc.BehaviouralCompetencyId, bc.Title,bc.IsActive)BehaviouralCompetency where 1 = 1 " +
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

        public int ActivationPublicTaskDefinition(int behaviouralCompetencyId, bool isActivation, int personId)
        {
            BehaviouralCompetency behaviouralCompetency = appDbContext.BehaviouralCompetency.Where(c => c.BehaviouralCompetencyId == behaviouralCompetencyId).SingleOrDefault();
            behaviouralCompetency.IsActive = isActivation;
            behaviouralCompetency.LastUpdatedBy = personId;
            behaviouralCompetency.LastUpdatedDate = DateTime.Now;
            appDbContext.Update(behaviouralCompetency);

            int result = appDbContext.SaveChanges();

            return result;
        }
        public List<Truth> GetTruthDetails(int behaviouralCompetencyId)
        {
            var query = appDbContext.Truth.Where(c => c.BehaviouralCompetencyId == behaviouralCompetencyId);
            if (query != null)
            {
                return query.OrderBy(c => c.BehaviouralCompetencyId).ToList();
            }
            return (null);
        }
        public List<BehaviouralCompetency> BehaviouralCompetencyList()
        {
            var query = appDbContext.BehaviouralCompetency.Where(c => c.IsActive == true);
            if (query != null)
            {
                return query.OrderBy(c => c.BehaviouralCompetencyId).ToList();
            }
            return (null);
        }
        public string BehaviouralCompetencyDeletion(int behaviouralCompetencyId)
        {
            try
            {
                appDbContext.RemoveRange(appDbContext.CorrespondentJob.Where(c=>c.BehaviouralCompetencyId== behaviouralCompetencyId).ToList());
                appDbContext.RemoveRange(appDbContext.Truth.Where(c=>c.BehaviouralCompetencyId== behaviouralCompetencyId).ToList());
                appDbContext.Remove(appDbContext.BehaviouralCompetency.Where(c=>c.BehaviouralCompetencyId== behaviouralCompetencyId).SingleOrDefault());
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل اختصاص شایستگی رفتاری به کارمند/کارمندان یا ارتباط آن با وقایع حساس امکان حذف وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }

    }
}
