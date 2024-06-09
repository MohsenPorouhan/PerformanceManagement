using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Util;
using PerformanceManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class ShareService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ShareService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
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
        public int GetPeriodDefinitionId()
        {
            var priodDefinitoionId = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault().PeriodDefinitoionId;
            return priodDefinitoionId;
        }
        public IQueryable GetPeriodDefinitionFromEvaluation()
        {
            var model = appDbContext.Evaluation.Include(c => c.PeriodDefinitoion).Where(c => c.PeriodDefinitoion.DateFrom <= DateTime.Now && c.PeriodDefinitoion.DateTo >= DateTime.Now).Select(p => new
            {
                p.PeriodDefinitoion.PeriodDefinitoionId,
                p.PeriodDefinitoion.PeriodTitle,
                p.PeriodDefinitoion.PeriodCode
            }).Distinct().OrderByDescending(o => o.PeriodDefinitoionId);
            return model;
        }
        public int GetMaxPeriodDefinitionId()
        {
            int priodDefinitoionId = appDbContext.PeriodDefinitoion.Max(c => c.PeriodDefinitoionId);
            return priodDefinitoionId;
        }
        public List<PeriodDefinitoion> GetPeriodDefinitionList()
        {
            List<PeriodDefinitoion> priodDefinitoion = appDbContext.PeriodDefinitoion.ToList();
            return priodDefinitoion;
        }
        public int GetTaskWeightWay(int periodDefinitionId)
        {
            if (periodDefinitionId == 0)
            {
                periodDefinitionId = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault().PeriodDefinitoionId;
            }
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            if (periodDefinition.WeightWayTask == 1)
            {
                return 1;//percent
            }
            return 2;//likert
        }
        public IQueryable GetWeightLikertScale(int periodDefinitionId = 0)
        {
            if (periodDefinitionId == 0)
            {
                periodDefinitionId = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault().PeriodDefinitoionId;
            }
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            var likertScale = from ls in appDbContext.LikertScale
                              join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                              //select new{ id2=i,dep=d.}
                              where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == periodDefinition.LikertWeightWayTaskId)
                              select new { ls.LikertScaleId, ld.NumberScale, ld.TitleScale };
            return likertScale;
        }
        public int GetScoreWeightWay(int periodDefinitionId)
        {
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            if (periodDefinition.WeightWayScore == 1)
            {
                return 1;//percent
            }
            return 2;//likert
        }
        public IQueryable GetScoreLikertScale(int periodDefinitionId)
        {
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            var likertScale = from ls in appDbContext.LikertScale
                              join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                              //select new{ id2=i,dep=d.}
                              where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == periodDefinition.LikertScoreWayId)
                              select new { ls.LikertScaleId, ld.NumberScale, ld.TitleScale };
            return likertScale;
        }
        public int GetBehaviourWeightWay(int periodDefinitionId = 0)
        {
            if (periodDefinitionId == 0)
            {
                periodDefinitionId = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault().PeriodDefinitoionId;
            }
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            if (periodDefinition.WeightWayBehaviour == 1)
            {
                return 1;//percent
            }
            return 2;//likert
        }
        public IQueryable GetBehaviourLikertScale(int periodDefinitionId = 0)
        {
            if (periodDefinitionId == 0)
            {
                periodDefinitionId = appDbContext.PeriodDefinitoion.Where(c => c.DateFrom <= DateTime.Now && c.DateTo >= DateTime.Now).SingleOrDefault().PeriodDefinitoionId;
            }
            var periodDefinition = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
            var likertScale = from ls in appDbContext.LikertScale
                              join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                              //select new{ id2=i,dep=d.}
                              where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == periodDefinition.LikertWeightWayBehaiviourId)
                              select new { ls.LikertScaleId, ld.NumberScale, ld.TitleScale };
            return likertScale;
        }
        public IEnumerable<object> GetEmployee(int[] departmentId)
        {
            //, int[] chairmanshipId, int[] managementId, int[] assistantId
            //int[] allDepartment;
            //allDepartment = departmentId.Concat(chairmanshipId).Concat(managementId).Concat(assistantId).ToArray();

            string departmentIdParam = "0";

            //departmentIdParam = string.Join(",", allDepartment);
            departmentIdParam = string.Join(",", departmentId);

            var sQuery = @"select 
                e.ParentEvaluationHierarchyId
                ,e.EvaluationHierarchyId
                ,d.ShortName
                ,p.PeopleId
                ,concat(p.FirstName, ' ', p.LastName, ' (', d.ShortName, ' - ', anu.UserName, ')')fullname
                ,e.SupervisorId
                from
                evaluationHierarchies e
                , Departments d
                ,People p
                , AspNetUsers anu
                 where e.EvaluationHierarchyId in (" + departmentIdParam + @")
                and d.DepartmentId = e.DepartmentId
                and p.EvaluationHierarchyID = e.EvaluationHierarchyId
                and anu.PeopleId = p.PeopleId
                and p.EffectiveEndDate is null
                and e.EffectiveEndDate is null
                and d.EffectiveEndDate is null";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> SupervisorList()
        {
            var sQuery = "select " +
                "distinct " +
                "p.PeopleId" +
                ",CONCAT(p.FirstName, ' ', p.LastName, '-', anu.UserName)FullName " +
                "from " +
                "People p join evaluationHierarchies eh on p.EvaluationHierarchyID = eh.EvaluationHierarchyId " +
                "and p.PeopleId = eh.SupervisorId " +
                "join AspNetUsers anu on anu.PeopleId = p.PeopleId " +
                "where " +
                "1 = 1 " +
                "and p.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null ";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> GetAllEmployeeDepartment()
        {
            var sQuery = @"select
                        EvaluationHierarchyId
                        ,PeopleId
                        ,UserName
                        ,FullName
                        from(
                        select 
                        eh.EvaluationHierarchyId
                        ,p.PeopleId
                        ,anu.UserName
                        ,concat(p.FirstName,' ',p.LastName,'(',d.ShortName,'-',anu.UserName,')')FullName
                        from
                        People p join evaluationHierarchies eh on p.EvaluationHierarchyID=eh.EvaluationHierarchyId
                        join Departments d on d.DepartmentId=eh.DepartmentId
                        join AspNetUsers anu on p.PeopleId=anu.PeopleId
                        where 
                        1=1
                        and p.EffectiveEndDate is null
                        and eh.EffectiveEndDate is null
                        and d.EffectiveEndDate is null)tbl
                        order by FullName";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<object> ParticipantList()
        {
            var sQuery = "select " +
                "e.ParentEvaluationHierarchyId" +
                ",e.EvaluationHierarchyId" +
                ",d.ShortName" +
                ",p.PeopleId" +
                ",concat(p.FirstName, ' ', p.LastName, ' (', d.ShortName, ')')fullname" +
                ",e.SupervisorId " +
                "from " +
                "evaluationHierarchies e" +
                ", Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and d.DepartmentId = e.DepartmentId " +
                "and p.EvaluationHierarchyID = e.EvaluationHierarchyId " +
                "and p.EffectiveEndDate is null " +
                "and e.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }

        public IEnumerable<ParticipantView> ParticipantList2()
        {
            var sQuery = "select " +
                "e.ParentEvaluationHierarchyId" +
                ",e.EvaluationHierarchyId" +
                ",d.ShortName" +
                ",p.PeopleId" +
                ",concat(p.FirstName, ' ', p.LastName, ' (', d.ShortName, ')')fullname" +
                ",e.SupervisorId " +
                "from " +
                "evaluationHierarchies e" +
                ", Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and d.DepartmentId = e.DepartmentId " +
                "and p.EvaluationHierarchyID = e.EvaluationHierarchyId " +
                "and p.EffectiveEndDate is null " +
                "and e.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<ParticipantView> query = null;

            query = conn.Query<ParticipantView>(sQuery).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }
        public List<Task> PublicTaskList()
        {
            var query = appDbContext.Task;
            if (query != null)
            {
                return query.OrderBy(c => c.TaskId).Where(c => c.ResourceType == 2 && c.IsActive == true).ToList();
            }
            return (null);
        }
        public int CriteiaCount(int taskId)
        {
            var count = appDbContext.Criteria.Where(c => c.TaskId == taskId).Count();
            return count;
        }
        public List<Criteria> GetCriteriaList(int taskId)
        {
            var query = appDbContext.Criteria.Where(c => c.TaskId == taskId);
            if (query != null)
            {
                return query.OrderBy(c => c.CriteriaId).ToList();
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
        public IQueryable TaskList()
        {
            var query = appDbContext.Task;
            if (query != null)
            {
                return query.OrderBy(c => c.TaskId).Select(c => new { c.TaskId, c.Title });
            }
            return (null);
        }
        public object AllocatorLevel(int departmentId, int personId, int coacherId, int coacherDepartmentId)
        {
            var sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 4 Levell " +
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
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @departmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId " +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
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
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId]= @departmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell-1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) " +
                "SELECT[EvaluationHierarchyId] as id " +
                ",[ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' " +
                "else convert(nvarchar, ParentEvaluationHierarchyId) end as parent " +
                ",Levell " +
                "FROM EmpsCTE " +
                "where " +
                "1=1 " +
                "and EvaluationHierarchyId = @coacherDepartmentIdd " +
                "and PeopleId = @coacherIdd ";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> query = null;

            query = conn.Query<object>(sQuery, new { departmentIdd = departmentId, personIdd = personId, coacherIdd = coacherId, coacherDepartmentIdd = coacherDepartmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }
        public int HasParticipant(int evaluationId)
        {
            var count = appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluationId).Count();
            return count;
        }
        public int HasCompetencyParticipant(int evaluationBehaviouralCompetencyId)
        {
            var count = appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).Count();
            return count;
        }
        public bool? ParticipantConfirmation(int evaluationId)
        {
            var confirmation = appDbContext.EvaluationParticipant.Where(c => c.EvaluationId == evaluationId).SingleOrDefault().Confirmation;
            return confirmation;
        }
        public bool? CompetencyParticipantConfirmation(int evaluationBehaviouralCompetencyId)
        {
            var confirmation = appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault().Confirmation;
            return confirmation;
        }
        public PeriodDefinitoion FormulationInfo()
        {
            return appDbContext.PeriodDefinitoion.Include(c => c.EvaluationCoefficient).Where(c => c.PeriodDefinitoionId == this.GetPeriodDefinitionId()).SingleOrDefault();
        }
        public bool IsRootAllocator(int departmentId, int personId)
        {
            var sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 4 Levell " +
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
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @departmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId " +
                ",p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
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
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId]= @departmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.ShortName" +
                ", Levell-1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) " +
                "SELECT count([EvaluationHierarchyId]) as count " +
                "FROM EmpsCTE " +
                "where " +
                "1=1 ";
            IDbConnection conn = connProvider.Connection;
            conn.Open();
            IsRootView query = null;

            query = conn.Query<IsRootView>(sQuery, new { departmentIdd = departmentId, personIdd = personId }).SingleOrDefault();

            //conn.Close();
            conn.Dispose();


            if (query.count == 1 || query.count == 2)
            {
                return true;
            }

            return false;
        }
        public SectionPeriod BeginOfPeriod()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            SectionPeriod sectionPeriod = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 1).SingleOrDefault();
            return sectionPeriod;
        }
        public SectionPeriod IntermediateOfPeriod()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            SectionPeriod sectionPeriod = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 2).SingleOrDefault();
            return sectionPeriod;
        }
        public SectionPeriod EndOfPeriod()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            SectionPeriod sectionPeriod = appDbContext.SectionPeriod.Include(c => c.ExtendSectionPeriods).Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 3).SingleOrDefault();
            return sectionPeriod;
        }
        public SectionPeriod ProtestationOfPeriod()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            SectionPeriod sectionPeriod = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.StatusCode == 4).SingleOrDefault();
            return sectionPeriod;
        }
        public int GetDepartmentIdForAuthorization(int personId)
        {
            SectionPeriod sectionPeriod = BeginOfPeriod();
            List<ExtendSectionPeriod> extendSectionPeriod = appDbContext.ExtendSectionPeriod.
                Where(c => c.SectionPeriodId == sectionPeriod.SectionPeriodId).ToList();
            if (extendSectionPeriod.Count > 0)
            {
                foreach (var item in extendSectionPeriod)
                {
                    ExtendSectionPeriodWithPeople espwp = appDbContext.ExtendSectionPeriodWithPeople.Include
                        (c => c.ExtendSectionPeriod).Where(c =>
                           c.ExtendSectionPeriod.DateFrom <= DateTime.Now
                        && c.ExtendSectionPeriod.DateTo >= DateTime.Now
                        && c.ExtendSectionPeriodId == item.ExtendSectionPeriodId
                        && c.PeopleId == personId).FirstOrDefault();
                    if (espwp != null)
                    {
                        return espwp.EvaluationHierarchyId;
                    }
                }
            }
            return 0;
        }

        public int GetDepartmentIdForAuthorization2(int personId)
        {
            SectionPeriod sectionPeriod = EndOfPeriod();
            List<ExtendSectionPeriod> extendSectionPeriod = appDbContext.ExtendSectionPeriod.
                Where(c => c.SectionPeriodId == sectionPeriod.SectionPeriodId).ToList();
            if (extendSectionPeriod.Count > 0)
            {
                foreach (var item in extendSectionPeriod)
                {
                    ExtendSectionPeriodWithPeople espwp = appDbContext.ExtendSectionPeriodWithPeople.Include
                        (c => c.ExtendSectionPeriod).Where(c =>
                           c.ExtendSectionPeriod.DateFrom <= DateTime.Now
                        && c.ExtendSectionPeriod.DateTo >= DateTime.Now
                        && c.ExtendSectionPeriodId == item.ExtendSectionPeriodId
                        && c.PeopleId == personId).FirstOrDefault();
                    if (espwp != null)
                    {
                        return espwp.EvaluationHierarchyId;
                    }
                }
            }
            return 0;
        }

        public ScoreSchedule SelfScoreSchedule()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            ScoreSchedule scoreSchedule = appDbContext.ScoreSchedule.Where(c => c.PeriodDefinitionId == periodDefinitionId && c.ScoreScheduleTypeId == 1).SingleOrDefault();
            return scoreSchedule;
        }
        public ScoreSchedule ParticipantScoreSchedule()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            ScoreSchedule scoreSchedule = appDbContext.ScoreSchedule.Where(c => c.PeriodDefinitionId == periodDefinitionId && c.ScoreScheduleTypeId == 2).SingleOrDefault();
            return scoreSchedule;
        }
        public ScoreSchedule Coacher1ScoreSchedule()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            ScoreSchedule scoreSchedule = appDbContext.ScoreSchedule.Where(c => c.PeriodDefinitionId == periodDefinitionId && c.ScoreScheduleTypeId == 3).SingleOrDefault();
            return scoreSchedule;
        }
        public ScoreSchedule Coacher2ScoreSchedule()
        {
            int periodDefinitionId = appDbContext.SectionPeriod.Max(c => c.PeriodDefinitoionId);
            ScoreSchedule scoreSchedule = appDbContext.ScoreSchedule.Where(c => c.PeriodDefinitionId == periodDefinitionId && c.ScoreScheduleTypeId == 4).SingleOrDefault();
            return scoreSchedule;
        }
    }
}
