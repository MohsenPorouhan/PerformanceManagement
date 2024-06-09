using Dapper;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class BehaviouralCompetencyAssignService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public BehaviouralCompetencyAssignService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> AssignBehaviouralCompetency(int periodDefinitionId, string[] employee, int[] behaviourCompetency, int competencyWeight, string participant, int personId, string roleId)
        {
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            ArrayList arrList = new ArrayList();

            var employeeId = 0;
            var hierarchyId = 0;
            foreach (var item in employee)
            {
                employeeId = int.Parse(item.Split('-')[1]);
                hierarchyId = int.Parse(item.Split('-')[0]);
                foreach (var itemCompetency in behaviourCompetency)
                {
                    var query = from ebc in appDbContext.EvaluationBehaviouralCompetency
                                join bh in appDbContext.BehaviouralCompetency on ebc.BehaviouralCompetencyId equals bh.BehaviouralCompetencyId
                                join p in appDbContext.People on new { a = ebc.RecieverAllocationPersonId, b = ebc.RecieverAllocationEvaluationBehaviouralHierarchyId } equals new { a = p.PeopleId, b = p.EvaluationHierarchyID ?? Convert.ToInt32(0) }
                                join eh in appDbContext.evaluationHierarchies on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId equals eh.EvaluationHierarchyId
                                join d in appDbContext.Departments on eh.DepartmentId equals d.DepartmentId
                                where (p.EffectiveEndDate == null && eh.EffectiveEndDate == null && d.EffectiveEndDate == null && ebc.RecieverAllocationPersonId == employeeId && ebc.RecieverAllocationEvaluationBehaviouralHierarchyId == hierarchyId && ebc.BehaviouralCompetencyId == itemCompetency && ebc.PeriodDefinitoionId == periodDefinitionId)
                                select new { bh.Title, d.ShortName, p.FirstName, p.LastName };
                    var tableResult = query.SingleOrDefault();
                    //var ebc = appDbContext.EvaluationBehaviouralCompetency.Include(c=>c.RecieverAllocation).Include(c=>c.BehaviouralCompetency).Where(c => c.RecieverAllocationPersonId == employeeId && c.RecieverAllocationEvaluationBehaviouralHierarchyId == hierarchyId && c.BehaviouralCompetencyId == itemCompetency && c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
                    if (tableResult != null)
                    {
                        arrList.Add(string.Format("شایستگی رفتاری {0} به کارمند  {1} مربوط به واحد سازمانی {2} در دوره زمانی مورد نظر تکراری می باشد", tableResult.Title, tableResult.FirstName + ' ' + tableResult.LastName, tableResult.ShortName));
                    }
                    else
                    {
                        List<EvaluationBehaviouralParticipant> evaluationBehaviouralParticipant = null;
                        if (participant != null)
                        {
                            evaluationBehaviouralParticipant = new List<EvaluationBehaviouralParticipant>();

                            evaluationBehaviouralParticipant.Add(new EvaluationBehaviouralParticipant
                            {
                                ParticipantId = int.Parse(participant.Split('-')[1]),
                                ParticipantEvaluationBehaviouralHierarchyId = int.Parse(participant.Split('-')[0]),
                                RequestDate = DateTime.Now,
                                CreatedBy = personId,
                                CreatedDate = DateTime.Now
                            });
                        }

                        EvaluationBehaviouralCompetency evaluationBehaviouralCompetency = new EvaluationBehaviouralCompetency();
                        evaluationBehaviouralCompetency.AllocatorPersonId = personId;
                        evaluationBehaviouralCompetency.AllocatorRoleId = roleId;
                        evaluationBehaviouralCompetency.PeriodDefinitoionId = periodDefinitionId;
                        evaluationBehaviouralCompetency.BehaviouralCompetencyWeight = competencyWeight;
                        evaluationBehaviouralCompetency.RecieverAllocationPersonId = int.Parse(item.Split('-')[1]);
                        evaluationBehaviouralCompetency.RecieverAllocationEvaluationBehaviouralHierarchyId = int.Parse(item.Split('-')[0]);
                        evaluationBehaviouralCompetency.BehaviouralCompetencyId = itemCompetency;
                        evaluationBehaviouralCompetency.EvaluationAcceptanceStatusId = 1;
                        if (participant != null)
                        {
                            evaluationBehaviouralCompetency.EvaluationBehaviouralParticipants = evaluationBehaviouralParticipant;
                        }
                        evaluationBehaviouralCompetency.CreatedBy = personId;
                        evaluationBehaviouralCompetency.CreatedDate = DateTime.Now;

                        appDbContext.EvaluationBehaviouralCompetency.Add(evaluationBehaviouralCompetency);

                    }
                }
            }
            var result = appDbContext.SaveChanges();
            dictionary.Add("duplicate", arrList);
            dictionary.Add("result", result);
            return dictionary;
        }

        public string EditBehaviouralCompetencyAssignment(int? evaluationBehaviouralParticipantId, int behaviouralCompetencyWeight, int? departmentId, int? participentIdd, int evaluationBehaviouralCompetencyId, int personId)
        {
            EvaluationBehaviouralCompetency evaluationBehaviouralCompetency = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault();
            if (evaluationBehaviouralCompetency.BehaviouralCompetencyWeight != behaviouralCompetencyWeight)
            {
                evaluationBehaviouralCompetency.BehaviouralCompetencyWeight = behaviouralCompetencyWeight;
                evaluationBehaviouralCompetency.LastUpdatedDate = DateTime.Now;
                evaluationBehaviouralCompetency.LastUpdatedBy = personId;
                appDbContext.Update(evaluationBehaviouralCompetency);
            }

            EvaluationBehaviouralParticipant evaluationBehaviouralParticipant = null;
            if (evaluationBehaviouralParticipantId != null)
            {
                evaluationBehaviouralParticipant = appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralParticipantId == evaluationBehaviouralParticipantId).SingleOrDefault();
            }

            if (evaluationBehaviouralParticipantId != null && departmentId == null && participentIdd == null)
            {
                try
                {
                    appDbContext.Remove(evaluationBehaviouralParticipant);
                }
                catch (DbUpdateException due)
                {
                    string message = due.InnerException.Message;
                    return "notRemovedException";
                }
                catch (Exception e)
                {
                    string message = e.InnerException.Message;
                    return "notRemovedException";
                }
            }
            else if (evaluationBehaviouralParticipantId != null && departmentId != null && participentIdd != null
                && (evaluationBehaviouralParticipant.ParticipantId != participentIdd || evaluationBehaviouralParticipant.ParticipantEvaluationBehaviouralHierarchyId != departmentId))
            {
                evaluationBehaviouralParticipant.ParticipantId = participentIdd ?? throw new NoNullAllowedException();
                evaluationBehaviouralParticipant.ParticipantEvaluationBehaviouralHierarchyId = departmentId ?? throw new NoNullAllowedException();
                evaluationBehaviouralParticipant.LastUpdatedBy = personId;
                evaluationBehaviouralParticipant.LastUpdatedDate = DateTime.Now;
                appDbContext.Update(evaluationBehaviouralParticipant);
            }
            else if (evaluationBehaviouralParticipantId == null && departmentId != null && participentIdd != null)
            {
                EvaluationBehaviouralParticipant evaluationBehaviouralParticipant2 = new EvaluationBehaviouralParticipant();
                evaluationBehaviouralParticipant2.EvaluationBehaviouralCompetencyId = evaluationBehaviouralCompetencyId;
                evaluationBehaviouralParticipant2.ParticipantId = participentIdd ?? throw new NoNullAllowedException();
                evaluationBehaviouralParticipant2.ParticipantEvaluationBehaviouralHierarchyId = departmentId ?? throw new NoNullAllowedException();
                evaluationBehaviouralParticipant2.RequestDate = DateTime.Now;
                evaluationBehaviouralParticipant2.CreatedBy = personId;
                evaluationBehaviouralParticipant2.CreatedDate = DateTime.Now;
                appDbContext.Add(evaluationBehaviouralParticipant2);
            }
            int result = appDbContext.SaveChanges();
            return result.ToString();
        }

        public string BehaviouralCompetencyAssignmentDeletion(int evaluationBehaviouralCompetencyId)
        {
            try
            {
                if (appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).Any())
                {
                    appDbContext.Remove(appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault());
                }
                appDbContext.Remove(appDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault());
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل نمره دهی یا محاسبات پایان دوره یا ثبت وقایع حساس به شایستگی رفتاری مورد نظر امکان حذف وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }

        public Dictionary<object, object> GetAssignmentBehaviouralList(DataTableParameter dataTableParameter, string periodDefinitionIdDT2, string departmentIdDT2, string peopleIdDT2)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "UserName", "FullName", "ShortName", "Title", "BehaviouralCompetencyWeight", "participantFullName", "participantDepartmentName" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            //int exactOrder = dataTableParameter.orderColumn + 1;
            if (dataTableParameter.orderable == true)
            {
                order = "order by " + aColumns[dataTableParameter.orderColumn] + " " + dataTableParameter.orderDIR;
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
            string queryTotalResult = @"select 
                            indexx
                            ,PeriodDefinitoionId
                            ,PeriodCode
                            ,PeriodTitle
                            ,RecieverAllocationPersonId
                            ,UserName
                            ,FullName
                            ,RecieverAllocationEvaluationBehaviouralHierarchyId
                            ,ShortName
                            ,EvaluationBehaviouralCompetencyId
                            ,BehaviouralCompetencyId
                            ,Title
                            ,BehaviouralCompetencyWeight
                            ,EvaluationBehaviouralParticipantId
                            ,participantFullName
                            ,ParticipantEvaluationBehaviouralHierarchyId
                            ,ParticipantId
                            ,participantDepartmentName
                            from(select
                            ROW_NUMBER() OVER(ORDER BY ebc.EvaluationBehaviouralCompetencyId desc) As indexx
                            , pd.PeriodDefinitoionId
                            , pd.PeriodCode
                            , pd.PeriodTitle
                            , ebc.RecieverAllocationPersonId
                            , anu.UserName
                            , CONCAT(p.FirstName, ' ', p.LastName)FullName
                            , ebc.RecieverAllocationEvaluationBehaviouralHierarchyId
                            , d.ShortName
                            , ebc.EvaluationBehaviouralCompetencyId
                            , bc.BehaviouralCompetencyId
                            , bc.Title
                            , ebc.BehaviouralCompetencyWeight
                            , ebp.EvaluationBehaviouralParticipantId
                            , (select CONCAT(pp.FirstName, ' ', pp.LastName) from People pp where pp.PeopleId = ebp.ParticipantId and
                            pp.EvaluationHierarchyID = ebp.ParticipantEvaluationBehaviouralHierarchyId and pp.EffectiveEndDate is null)participantFullName
                            ,ebp.ParticipantEvaluationBehaviouralHierarchyId
                            ,ebp.ParticipantId
                            ,(select dd.ShortName
                            from People pp, evaluationHierarchies eh2,Departments dd
                            where pp.PeopleId = ebp.ParticipantId
                            and pp.EvaluationHierarchyID = ebp.ParticipantEvaluationBehaviouralHierarchyId
                            and eh2.EvaluationHierarchyId = ebp.ParticipantEvaluationBehaviouralHierarchyId
                            and dd.DepartmentId = eh2.DepartmentId
                            and dd.EffectiveEndDate is null
                            and eh2.EffectiveEndDate is null
                            and pp.EffectiveEndDate is null) participantDepartmentName
                            from
                            EvaluationBehaviouralCompetency ebc join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId
                            join People p on p.EvaluationHierarchyID = ebc.RecieverAllocationEvaluationBehaviouralHierarchyId and ebc.RecieverAllocationPersonId = p.PeopleId
                            join AspNetUsers anu on p.PeopleId = anu.PeopleId
                            join evaluationHierarchies eh on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId
                            join Departments d on eh.DepartmentId = d.DepartmentId
                            join PeriodDefinitoion pd on pd.PeriodDefinitoionId = ebc.PeriodDefinitoionId
                            left join EvaluationBehaviouralParticipant ebp on ebp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId
                            where
                            1 = 1
                            and p.EffectiveEndDate is null
                            and eh.EffectiveEndDate is null
                            and d.EffectiveEndDate is null
                            and ebc.AllocatorRoleId = (select anr.Id from AspNetRoles anr where anr.Name = 'HRAdmin')
                            ) as tb where 1 = 1  ";
            string queryFilteredTotal = @"select 
                                        indexx
                                        ,PeriodDefinitoionId
                                        ,PeriodCode
                                        ,PeriodTitle
                                        ,RecieverAllocationPersonId
                                        ,UserName
                                        ,FullName
                                        ,RecieverAllocationEvaluationBehaviouralHierarchyId
                                        ,ShortName
                                        ,EvaluationBehaviouralCompetencyId
                                        ,BehaviouralCompetencyId
                                        ,Title
                                        ,BehaviouralCompetencyWeight
                                        ,EvaluationBehaviouralParticipantId
                                        ,participantFullName
                                        ,ParticipantEvaluationBehaviouralHierarchyId
                                        ,ParticipantId
                                        ,participantDepartmentName 
                                        from ( select 
                                        ROW_NUMBER() OVER(ORDER BY ebc.EvaluationBehaviouralCompetencyId desc) As indexx
                                        , pd.PeriodDefinitoionId
                                        , pd.PeriodCode
                                        , pd.PeriodTitle
                                        , ebc.RecieverAllocationPersonId
                                        ,anu.UserName
                                        , CONCAT(p.FirstName, ' ', p.LastName)FullName
                                        , ebc.RecieverAllocationEvaluationBehaviouralHierarchyId
                                        , d.ShortName
                                        , ebc.EvaluationBehaviouralCompetencyId
                                        , bc.BehaviouralCompetencyId
                                        , bc.Title
                                        , ebc.BehaviouralCompetencyWeight
                                        , ebp.EvaluationBehaviouralParticipantId
                                        , (select CONCAT(pp.FirstName, ' ', pp.LastName) from People pp where pp.PeopleId = ebp.ParticipantId and 
                                        pp.EvaluationHierarchyID = ebp.ParticipantEvaluationBehaviouralHierarchyId and pp.EffectiveEndDate is null)participantFullName
                                        ,ebp.ParticipantEvaluationBehaviouralHierarchyId
                                        ,ebp.ParticipantId
                                        ,(select dd.ShortName 
                                        from People pp, evaluationHierarchies eh2,Departments dd 
                                        where pp.PeopleId = ebp.ParticipantId 
                                        and pp.EvaluationHierarchyID = ebp.ParticipantEvaluationBehaviouralHierarchyId 
                                        and eh2.EvaluationHierarchyId = ebp.ParticipantEvaluationBehaviouralHierarchyId 
                                        and dd.DepartmentId = eh2.DepartmentId 
                                        and dd.EffectiveEndDate is null 
                                        and eh2.EffectiveEndDate is null 
                                        and pp.EffectiveEndDate is null) participantDepartmentName 
                                        from 
                                        EvaluationBehaviouralCompetency ebc join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId 
                                        join People p on p.EvaluationHierarchyID = ebc.RecieverAllocationEvaluationBehaviouralHierarchyId and ebc.RecieverAllocationPersonId = p.PeopleId 
                                        join AspNetUsers anu on p.PeopleId=anu.PeopleId
                                        join evaluationHierarchies eh on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId 
                                        join Departments d on eh.DepartmentId = d.DepartmentId 
                                        join PeriodDefinitoion pd on pd.PeriodDefinitoionId = ebc.PeriodDefinitoionId 
                                        left join EvaluationBehaviouralParticipant ebp on ebp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId 
                                        where 
                                        1 = 1 
                                        and p.EffectiveEndDate is null 
                                        and eh.EffectiveEndDate is null 
                                        and d.EffectiveEndDate is null
                                        and ebc.AllocatorRoleId=(select anr.Id from AspNetRoles anr where anr.Name='HRAdmin')
                                        and pd.PeriodDefinitoionId=isnull(@periodDefinitoionIdd,pd.PeriodDefinitoionId)
                                        and ebc.RecieverAllocationEvaluationBehaviouralHierarchyId=isnull(@departmentIdd,ebc.RecieverAllocationEvaluationBehaviouralHierarchyId)
                                        and ebc.RecieverAllocationPersonId=isnull(@peopleIdd,ebc.RecieverAllocationPersonId)
                                        ) as tb where 1 = 1" +
                where;
            string sQuery = @"select 
                            indexx
                            ,PeriodDefinitoionId
                            ,PeriodCode
                            ,PeriodTitle
                            ,RecieverAllocationPersonId
                            ,UserName
                            ,FullName
                            ,RecieverAllocationEvaluationBehaviouralHierarchyId
                            ,ShortName
                            ,EvaluationBehaviouralCompetencyId
                            ,BehaviouralCompetencyId
                            ,Title
                            ,BehaviouralCompetencyWeight
                            ,EvaluationBehaviouralParticipantId
                            ,participantFullName
                            ,ParticipantEvaluationBehaviouralHierarchyId
                            ,ParticipantId
                            ,participantDepartmentName 
                            from ( select 
                            ROW_NUMBER() OVER(ORDER BY ebc.EvaluationBehaviouralCompetencyId desc) As indexx
                            , pd.PeriodDefinitoionId
                            , pd.PeriodCode
                            , pd.PeriodTitle
                            , ebc.RecieverAllocationPersonId
                            ,anu.UserName
                            , CONCAT(p.FirstName, ' ', p.LastName)FullName
                            , ebc.RecieverAllocationEvaluationBehaviouralHierarchyId
                            , d.ShortName
                            , ebc.EvaluationBehaviouralCompetencyId
                            , bc.BehaviouralCompetencyId
                            , bc.Title
                            , ebc.BehaviouralCompetencyWeight
                            , ebp.EvaluationBehaviouralParticipantId
                            , (select CONCAT(pp.FirstName, ' ', pp.LastName) from People pp where pp.PeopleId = ebp.ParticipantId and 
                            pp.EvaluationHierarchyID = ebp.ParticipantEvaluationBehaviouralHierarchyId and pp.EffectiveEndDate is null)participantFullName
                            ,ebp.ParticipantEvaluationBehaviouralHierarchyId
                            ,ebp.ParticipantId
                            ,(select dd.ShortName 
                            from People pp, evaluationHierarchies eh2,Departments dd 
                            where pp.PeopleId = ebp.ParticipantId 
                            and pp.EvaluationHierarchyID = ebp.ParticipantEvaluationBehaviouralHierarchyId 
                            and eh2.EvaluationHierarchyId = ebp.ParticipantEvaluationBehaviouralHierarchyId 
                            and dd.DepartmentId = eh2.DepartmentId 
                            and dd.EffectiveEndDate is null 
                            and eh2.EffectiveEndDate is null 
                            and pp.EffectiveEndDate is null) participantDepartmentName 
                            from 
                            EvaluationBehaviouralCompetency ebc join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId 
                            join People p on p.EvaluationHierarchyID = ebc.RecieverAllocationEvaluationBehaviouralHierarchyId and ebc.RecieverAllocationPersonId = p.PeopleId 
                            join AspNetUsers anu on p.PeopleId=anu.PeopleId
                            join evaluationHierarchies eh on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId 
                            join Departments d on eh.DepartmentId = d.DepartmentId 
                            join PeriodDefinitoion pd on pd.PeriodDefinitoionId = ebc.PeriodDefinitoionId 
                            left join EvaluationBehaviouralParticipant ebp on ebp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId 
                            where 
                            1 = 1 
                            and p.EffectiveEndDate is null 
                            and eh.EffectiveEndDate is null 
                            and d.EffectiveEndDate is null
                            and ebc.AllocatorRoleId=(select anr.Id from AspNetRoles anr where anr.Name='HRAdmin')
                            and pd.PeriodDefinitoionId=isnull(@periodDefinitoionIdd,pd.PeriodDefinitoionId)
                            and ebc.RecieverAllocationEvaluationBehaviouralHierarchyId=isnull(@departmentIdd,ebc.RecieverAllocationEvaluationBehaviouralHierarchyId)
                            and ebc.RecieverAllocationPersonId=isnull(@peopleIdd,ebc.RecieverAllocationPersonId)
                            ) as tb where 1 = 1 " +
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
                    periodDefinitoionIdd = periodDefinitionIdDT2,
                    departmentIdd = departmentIdDT2,
                    peopleIdd = peopleIdDT2
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    periodDefinitoionIdd = periodDefinitionIdDT2,
                    departmentIdd = departmentIdDT2,
                    peopleIdd = peopleIdDT2
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    periodDefinitoionIdd = periodDefinitionIdDT2,
                    departmentIdd = departmentIdDT2,
                    peopleIdd = peopleIdDT2
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                periodDefinitoionIdd = periodDefinitionIdDT2,
                departmentIdd = departmentIdDT2,
                peopleIdd = peopleIdDT2
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return (dictionary);
        }
        public List<object> GetPublicCompetencyDepartment(int periodDefinitionId)
        {
            var sQuery = @"select 
                        distinct
                        eh.EvaluationHierarchyId
                        ,d.DepartmentId
                        ,concat(d.ShortName,' (',p.FirstName,' ',p.LastName,'-',anu.UserName,')')ShortName
                        from
                        EvaluationBehaviouralCompetency ebc join BehaviouralCompetency bc on ebc.BehaviouralCompetencyId = bc.BehaviouralCompetencyId
                        join evaluationHierarchies eh on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId = eh.EvaluationHierarchyId
                        join Departments d on eh.DepartmentId = d.DepartmentId
                        join People p on eh.SupervisorId=p.PeopleId and eh.EvaluationHierarchyId=p.EvaluationHierarchyID
                        join AspNetUsers anu on p.PeopleId=anu.PeopleId
                        where
                        1 = 1
                        and ebc.AllocatorRoleId=(select anr.Id from AspNetRoles anr where anr.Name='HRAdmin')
                        and ebc.PeriodDefinitoionId = @periodDefinitionIdd
                        and eh.EffectiveEndDate is null
                        and d.EffectiveEndDate is null
                        and p.EffectiveEndDate is null";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> departmentList = null;

            departmentList = conn.Query<object>(sQuery, new { periodDefinitionIdd = periodDefinitionId }).ToList();

            //conn.Close();
            conn.Dispose();
            return departmentList;
        }
        public List<object> GetPublicCompetencyReceiver(int departmentId)
        {
            var sQuery = @"select distinct
                        p.PeopleId
                        ,anu.UserName
                        ,CONCAT(p.FirstName,' ',LastName,' (',anu.UserName,')')FullName
                        from 
                        People p join AspNetUsers anu on p.PeopleId=anu.PeopleId
                        join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId=p.EvaluationHierarchyID and ebc.RecieverAllocationPersonId=p.PeopleId
                        join BehaviouralCompetency bc on ebc.BehaviouralCompetencyId=bc.BehaviouralCompetencyId
                        where 
                        1=1
                        and ebc.AllocatorRoleId=(select anr.Id from AspNetRoles anr where anr.Name='HRAdmin')
                        and p.EffectiveEndDate is null
                        and p.EvaluationHierarchyID=@departmentIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<object> receiverList = null;

            receiverList = conn.Query<object>(sQuery, new { departmentIdd = departmentId }).ToList();

            //conn.Close();
            conn.Dispose();
            return receiverList;
        }
    }
}
