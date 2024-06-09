using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.Employee.View;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.Services
{
    public class BehaviouralCompetencyAssignmentService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public BehaviouralCompetencyAssignmentService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> GetAssignedCompetencyList(DataTableParameter dataTableParameter, int personId, int primaryDepartmentId, string employee = null, string periodDefinitionIdDT2 = null)
        {
            int? personIdDT = null;
            int? departmentIdDT = null;
            //int? periodDefinitionIdDT2Param = null;
            int? periodDefinitionIdDT2Param = int.Parse(periodDefinitionIdDT2);
            if (employee != null && employee != "")
            {
                personIdDT = int.Parse(employee.Split("-")[0]);
                departmentIdDT = int.Parse(employee.Split("-")[1]);
            }
            //else
            //{
            //    periodDefinitionIdDT2Param = null;
            //}
            String[] aColumns = { "PeriodCode", "PeriodTitle", "allocatorFullName", "allocatorDepartmentName", "allocatorRoleName", "text", "Title", "PeriodCode", "PeriodTitle" };
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            string limit;
            string order;
            string where = " and (";
            //int exactOrder = dataTableParameter.orderColumn + 1;
            //if (exactOrder == 9)
            //{
            //    exactOrder += 5;
            //}
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

            string queryTotalResult = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and eh.SupervisorId = p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ", p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ", Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ", id" +
                ", text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationBehaviouralCompetencyId" +
                ", BehaviouralCompetencyId" +
                ", Title" +
                ", RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ", AllocatorEvaluationBehaviouralHierarchyId" +
                ", allocatorDepartmentName" +
                ", allocatorRoleName" +
                ", AllocatorRoleId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", PeriodDefinitoionId" +
                ", BehaviouralCompetencyWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY ebc.EvaluationBehaviouralCompetencyId desc) As indexx" +
                ", EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", ebc.EvaluationBehaviouralCompetencyId" +
                ", ebc.BehaviouralCompetencyId" +
                ", bc.Title" +
                ",ebc.RecieverAllocationPersonId" +
                ",ebc.RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", ebc.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=ebc.AllocatorEvaluationBehaviouralHierarchyId and p.PeopleId=ebc.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",ebc.AllocatorEvaluationBehaviouralHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=ebc.AllocatorEvaluationBehaviouralHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",ebc.AllocatorRoleId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",ebc.BehaviouralCompetencyWeight " +
                ",ebc.EvaluationAcceptanceStatusId " +
                ",(select case when ebpp.ParticipantId > 0 then 1 end from EvaluationBehaviouralParticipant ebpp where ebpp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId and ebpp.RequestDate =(select MAX(ebp2.RequestDate) from EvaluationBehaviouralParticipant ebp2 where ebp2.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId))hasParticipant " +
                ",(select ebpp.Confirmation from EvaluationBehaviouralParticipant ebpp where ebpp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId and ebpp.RequestDate =(select MAX(ebp2.RequestDate) from EvaluationBehaviouralParticipant ebp2 where ebp2.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId=[EvalHierarchyId] " +
                "and ebc.RecieverAllocationPersonId= PeopleId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId= ebc.BehaviouralCompetencyId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= ebc.PeriodDefinitoionId " +
                "where Levell!=1 " +
                "and ebc.PeriodDefinitoionId= ISNULL(@periodDefinitionIdDTt22, ebc.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2, RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationBehaviouralHierarchyId = ISNULL(@departmentIdDT2, RecieverAllocationEvaluationBehaviouralHierarchyId) " +
                ")dd where 1=1  ";

            string queryFilteredTotal = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and eh.SupervisorId = p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ", p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ", Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ", id" +
                ", text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationBehaviouralCompetencyId" +
                ", BehaviouralCompetencyId" +
                ", Title" +
                ", RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ", AllocatorEvaluationBehaviouralHierarchyId" +
                ", allocatorDepartmentName" +
                ", allocatorRoleName" +
                ", AllocatorRoleId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", PeriodDefinitoionId" +
                ", BehaviouralCompetencyWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY ebc.EvaluationBehaviouralCompetencyId desc) As indexx" +
                ", EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", ebc.EvaluationBehaviouralCompetencyId" +
                ", ebc.BehaviouralCompetencyId" +
                ", bc.Title" +
                ",ebc.RecieverAllocationPersonId" +
                ",ebc.RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", ebc.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=ebc.AllocatorEvaluationBehaviouralHierarchyId and p.PeopleId=ebc.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",ebc.AllocatorEvaluationBehaviouralHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=ebc.AllocatorEvaluationBehaviouralHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",ebc.AllocatorRoleId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",ebc.BehaviouralCompetencyWeight " +
                ",ebc.EvaluationAcceptanceStatusId " +
                ",(select case when ebpp.ParticipantId > 0 then 1 end from EvaluationBehaviouralParticipant ebpp where ebpp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId and ebpp.RequestDate =(select MAX(ebp2.RequestDate) from EvaluationBehaviouralParticipant ebp2 where ebp2.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId))hasParticipant " +
                ",(select ebpp.Confirmation from EvaluationBehaviouralParticipant ebpp where ebpp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId and ebpp.RequestDate =(select MAX(ebp2.RequestDate) from EvaluationBehaviouralParticipant ebp2 where ebp2.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId=[EvalHierarchyId] " +
                "and ebc.RecieverAllocationPersonId= PeopleId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId= ebc.BehaviouralCompetencyId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= ebc.PeriodDefinitoionId " +
                "where Levell!=1 " +
                "and ebc.PeriodDefinitoionId= ISNULL(@periodDefinitionIdDTt22, ebc.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2, RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationBehaviouralHierarchyId = ISNULL(@departmentIdDT2, RecieverAllocationEvaluationBehaviouralHierarchyId) " +
                ")dd where 1=1  " +
                where;

            string sQuery = "WITH EmpsCTE AS( " +
                "select " +
                "eh.EvaluationHierarchyId" +
                ", p.PeopleId" +
                ", eh.SupervisorId" +
                ", eh.ParentEvaluationHierarchyId" +
                ",eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName" +
                ", 1 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ",Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and eh.SupervisorId = p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "union " +
                "select " +
                "((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId EvaluationHierarchyId" +
                ", p.PeopleId" +
                ",eh.SupervisorId" +
                ",eh.EvaluationHierarchyId ParentEvaluationHierarchyId" +
                ", eh.EvaluationHierarchyId[EvalHierarchyId]" +
                ", CONCAT(p.FirstName, ' ', p.LastName)ShortName" +
                ",4 Levell " +
                "from " +
                "evaluationHierarchies eh" +
                ", Departments d" +
                ",People p " +
                "where 1 = 1 " +
                "and eh.DepartmentId = d.DepartmentId " +
                "and eh.EvaluationHierarchyId = p.EvaluationHierarchyID " +
                "and eh.EffectiveEndDate is null " +
                "and d.EffectiveEndDate is null " +
                "and p.EffectiveEndDate is null " +
                "and p.EvaluationHierarchyID is not null " +
                "and eh.SupervisorId != p.PeopleId " +
                "and p.PeopleId = @personIdd " +
                "and eh.[EvaluationHierarchyId] = @primaryDepartmentIdd " +
                "UNION ALL " +
                "SELECT C.EvaluationHierarchyId" +
                ", C.PeopleId" +
                ", C.SupervisorId" +
                ", C.ParentEvaluationHierarchyId" +
                ", C.[EvalHierarchyId]" +
                ", C.ShortName" +
                ", Levell+1 levell " +
                "FROM EmpsCTE AS P " +
                "JOIN dbo.ChartConfirmationn AS C " +
                "ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) " +
                "select " +
                "indexx" +
                ", id" +
                ", text" +
                ", parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", EvaluationBehaviouralCompetencyId" +
                ", BehaviouralCompetencyId" +
                ", Title" +
                ", RecieverAllocationPersonId" +
                ", RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", AllocatorPersonId" +
                ", allocatorFullName" +
                ", AllocatorEvaluationBehaviouralHierarchyId" +
                ", allocatorDepartmentName" +
                ", allocatorRoleName" +
                ", AllocatorRoleId" +
                ", PeriodCode" +
                ", PeriodTitle" +
                ", PeriodDefinitoionId" +
                ", BehaviouralCompetencyWeight " +
                ",EvaluationAcceptanceStatusId " +
                ",hasParticipant " +
                ",participantConfirmation " +
                "from( " +
                "SELECT " +
                "ROW_NUMBER() OVER(ORDER BY ebc.EvaluationBehaviouralCompetencyId desc) As indexx" +
                ", EmpsCTE.[EvaluationHierarchyId] as id" +
                ", [ShortName] as text" +
                ",case when convert(nvarchar, ParentEvaluationHierarchyId) is null then '#' else convert(nvarchar, ParentEvaluationHierarchyId) end as parent" +
                ", [EvalHierarchyId]" +
                ", Levell" +
                ", ebc.EvaluationBehaviouralCompetencyId" +
                ", ebc.BehaviouralCompetencyId" +
                ", bc.Title" +
                ",ebc.RecieverAllocationPersonId" +
                ",ebc.RecieverAllocationEvaluationBehaviouralHierarchyId" +
                ", ebc.AllocatorPersonId" +
                ", (select CONCAT(p.FirstName,' ', p.LastName) from People p where p.EvaluationHierarchyID=ebc.AllocatorEvaluationBehaviouralHierarchyId and p.PeopleId=ebc.AllocatorPersonId and p.EffectiveEndDate is null)allocatorFullName" +
                ",ebc.AllocatorEvaluationBehaviouralHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId and eh.EvaluationHierarchyId=ebc.AllocatorEvaluationBehaviouralHierarchyId and d.EffectiveEndDate is null and eh.EffectiveEndDate is null)allocatorDepartmentName" +
                ",(select anr.Name from AspNetRoles anr where anr.Id=AllocatorRoleId)allocatorRoleName" +
                ",ebc.AllocatorRoleId" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ",pd.PeriodDefinitoionId" +
                ",ebc.BehaviouralCompetencyWeight " +
                ",ebc.EvaluationAcceptanceStatusId " +
                ",(select case when ebpp.ParticipantId > 0 then 1 end from EvaluationBehaviouralParticipant ebpp where ebpp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId and ebpp.RequestDate =(select MAX(ebp2.RequestDate) from EvaluationBehaviouralParticipant ebp2 where ebp2.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId))hasParticipant " +
                ",(select ebpp.Confirmation from EvaluationBehaviouralParticipant ebpp where ebpp.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId and ebpp.RequestDate =(select MAX(ebp2.RequestDate) from EvaluationBehaviouralParticipant ebp2 where ebp2.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId))participantConfirmation " +
                "FROM EmpsCTE " +
                "join EvaluationBehaviouralCompetency ebc on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId=[EvalHierarchyId] " +
                "and ebc.RecieverAllocationPersonId= PeopleId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId= ebc.BehaviouralCompetencyId " +
                "join PeriodDefinitoion pd on pd.PeriodDefinitoionId= ebc.PeriodDefinitoionId " +
                "where Levell!=1 " +
                "and ebc.PeriodDefinitoionId= ISNULL(@periodDefinitionIdDTt22, ebc.PeriodDefinitoionId) " +
                "and RecieverAllocationPersonId = ISNULL(@personIdDT2, RecieverAllocationPersonId) " +
                "and RecieverAllocationEvaluationBehaviouralHierarchyId = ISNULL(@departmentIdDT2, RecieverAllocationEvaluationBehaviouralHierarchyId) " +
                ")dd where 1=1  " +
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
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT
                }).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT,
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    personIdd = personId,
                    primaryDepartmentIdd = primaryDepartmentId,
                    periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                    personIdDT2 = personIdDT,
                    departmentIdDT2 = departmentIdDT
                }).ToList();
            }
            object totalResult = conn.Query(queryTotalResult, new
            {
                personIdd = personId,
                primaryDepartmentIdd = primaryDepartmentId,
                periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                personIdDT2 = personIdDT,
                departmentIdDT2 = departmentIdDT
            }).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                personIdd = personId,
                primaryDepartmentIdd = primaryDepartmentId,
                periodDefinitionIdDTt22 = periodDefinitionIdDT2Param,
                personIdDT2 = personIdDT,
                departmentIdDT2 = departmentIdDT
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return dictionary;
        }

        public List<TruthView> GetTruthView2(int behaviouralCompetencyId, int evaluationBehaviouralCompetencyId)
        {
            var sQuery = "select " +
                "t.Title TruthTitle" +
                ",'HRAdmin' Creator " +
                "from Truth t " +
                "where t.BehaviouralCompetencyId = @behaviouralCompetencyIdd " +
                "union " +
                "select " +
                "ct.Title" +
                ",'Coacher'Creator " +
                "from CoacherTruth ct join EvaluationBehaviouralCompetency ebc on ct.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                "and ct.EvaluationBehaviouralCompetencyId = @evluationBehaviouralCompetencyIdd " +
                "and ebc.BehaviouralCompetencyId = @behaviouralCompetencyIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<TruthView> truthView = null;

            truthView = conn.Query<TruthView>(sQuery, new { behaviouralCompetencyIdd = behaviouralCompetencyId, evluationBehaviouralCompetencyIdd = evaluationBehaviouralCompetencyId }).ToList();

            //conn.Close();
            conn.Dispose();
            return truthView;
        }

        public List<SensibleEventTableView> GetSensibleEvent(int evaluationBehaviouralCompetencyId)
        {
            var sQuery = "select " +
                "se.SensibleEventId" +
                ", se.Title" +
                ", se.Description" +
                ",bc.Title TaskTitle" +
                ", se.EventType" +
                ", se.PersonId" +
                ", (select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.PersonId and p.EffectiveEndDate is null and p.PositionType = 1)CreatedPersonFullName" +
                ",se.PersonDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.PersonDepartmentId)CreatedDepartmentName" +
                ",se.EmployeeId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p where p.PeopleId = se.EmployeeId and p.EffectiveEndDate is null and p.PositionType = 1)EmployeeFullName" +
                ",se.EmployeeDepartmentId" +
                ",(select d.ShortName from evaluationHierarchies eh join Departments d on eh.DepartmentId = d.DepartmentId " +
                "where eh.EffectiveEndDate is null and d.EffectiveEndDate is null and eh.EvaluationHierarchyId = se.EmployeeDepartmentId)EmployeeDepartmentName" +
                ",se.PersonRole" +
                ",anr.Name RoleName" +
                ", se.Visibility" +
                ",se.FileName" +
                ",se.FilePath" +
                ",se.SensibleEventDate" +
                ",se.PeriodDefinitionId " +
                "from SensibleEvent se join RelatedCompetencyWithSensibleEvent rc on se.SensibleEventId = rc.SensibleEventId " +
                "join BehaviouralCompetency bc on rc.BehaviouralCompetencyId = bc.BehaviouralCompetencyId " +
                "join AspNetRoles anr on se.PersonRole = anr.Id " +
                "where " +
                "1 = 1 " +
                "and rc.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<SensibleEventTableView> SensibleEventTableView = null;

            SensibleEventTableView = conn.Query<SensibleEventTableView>(sQuery, new { evaluationBehaviouralCompetencyIdd = evaluationBehaviouralCompetencyId }).ToList();

            //conn.Close();
            conn.Dispose();
            return SensibleEventTableView;
        }

        public EvaluationBehaviourCompetencyScore GetCompetencyEvaluationScore(int evaluationBehaviouralCompetencyId, int level)
        {
            EvaluationBehaviourCompetencyScore evaluationBehaviourCompetencyScore = appDbContext.EvaluationBehaviourCompetencyScore.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId && c.CoacherLevel == level).SingleOrDefault();
            return evaluationBehaviourCompetencyScore;
        }

        public string DeleteCompetencyAssignment(int evaluationBehaviouralCompetencyId, int? hasParticipant)
        {
            try
            {
                EvaluationBehaviouralCompetency evaluationBehaviouralCompetency = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).SingleOrDefault();
                bool hasCoacherTruth = appDbContext.CoacherTruth.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).Any();
                if (hasParticipant == 1)
                {
                    appDbContext.Remove(appDbContext.EvaluationBehaviouralParticipant.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetency.EvaluationBehaviouralCompetencyId).SingleOrDefault());
                }
                if (hasCoacherTruth)
                {
                    appDbContext.RemoveRange(appDbContext.CoacherTruth.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationBehaviouralCompetencyId).ToList());
                }
                appDbContext.Remove(evaluationBehaviouralCompetency);

                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                DbUpdateException du2 = due;
                string message = "در صورتی که برای شایستگی رفتاری اختصاصی مورد نظر، محاسبه یا وقایع حساس ثبت کرده اید، قابل حذف نمی باشد" + Environment.NewLine + Environment.NewLine + due.InnerException.Message;

                return message;
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }

        public Dictionary<object, object> AssignBehaviouralCompetency(int periodDefinitionId, string departmentSupervisor, string[] subSetEmployeeId, int[] behaviourCompetency, string participant, int personId, string roleId, string tagEditorTruth)
        {
            string[] truth = null;
            if (tagEditorTruth != null)
            {
                truth = tagEditorTruth.Split(",");
            }
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            ArrayList arrList = new ArrayList();

            var employeeId = 0;
            var hierarchyId = 0;
            foreach (var item in subSetEmployeeId)
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
                                where (p.EffectiveEndDate == null && eh.EffectiveEndDate == null && d.EffectiveEndDate == null && ebc.EvaluationAcceptanceStatusId!=3 && ebc.EvaluationAcceptanceStatusId != 4 && ebc.RecieverAllocationPersonId == employeeId && ebc.RecieverAllocationEvaluationBehaviouralHierarchyId == hierarchyId && ebc.BehaviouralCompetencyId == itemCompetency && ebc.PeriodDefinitoionId == periodDefinitionId)
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
                        List<CoacherTruth> coacherTruths = new List<CoacherTruth>();
                        if (truth != null)
                        {
                            foreach (var itemTruth in truth)
                            {
                                CoacherTruth coacherTruth = new CoacherTruth();
                                coacherTruth.Title = itemTruth;
                                coacherTruth.CreatedBy = personId;
                                coacherTruth.CreatedDate = DateTime.Now;
                                coacherTruths.Add(coacherTruth);
                            }
                        }

                        EvaluationBehaviouralCompetency evaluationBehaviouralCompetency = new EvaluationBehaviouralCompetency();
                        evaluationBehaviouralCompetency.AllocatorPersonId = personId;
                        evaluationBehaviouralCompetency.AllocatorRoleId = roleId;
                        evaluationBehaviouralCompetency.AllocatorEvaluationBehaviouralHierarchyId = int.Parse(departmentSupervisor.Split('-')[0]);
                        evaluationBehaviouralCompetency.PeriodDefinitoionId = periodDefinitionId;
                        evaluationBehaviouralCompetency.RecieverAllocationPersonId = int.Parse(item.Split('-')[1]);
                        evaluationBehaviouralCompetency.RecieverAllocationEvaluationBehaviouralHierarchyId = int.Parse(item.Split('-')[0]);
                        evaluationBehaviouralCompetency.BehaviouralCompetencyId = itemCompetency;
                        evaluationBehaviouralCompetency.EvaluationAcceptanceStatusId = 1;
                        if (truth != null)
                        {
                            evaluationBehaviouralCompetency.CoacherTruths = coacherTruths;
                        }
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
        public Dictionary<object, object> TransferCompetencyAssignment(EmployeeView employeeView, int allocatorId, string roleId, int priorPriodDefinitionId, int periodDefinitionId)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                Dictionary<object, object> dictionary = new Dictionary<object, object>();

                try
                {
                    int result = 0;
                    List<string> evalDuplicateList = new List<string>();

                    if (employeeView.Receiver.Count() > 0)
                    {
                        foreach (var item in employeeView.Receiver)
                        {

                            int receiverId = int.Parse(item.Split("-")[0]);
                            int receiverDepartmentId = int.Parse(item.Split("-")[1]);

                            List<EvaluationBehaviouralCompetency> priorCompetencyEvaluationList = appDbContext.EvaluationBehaviouralCompetency.Where(c =>
                                c.RecieverAllocationPersonId == receiverId &&
                                c.RecieverAllocationEvaluationBehaviouralHierarchyId == receiverDepartmentId &&
                                c.AllocatorPersonId == allocatorId &&
                                c.PeriodDefinitoionId == priorPriodDefinitionId &&
                                (c.RefutationCause != "3" && c.RefutationCause != "4" || c.RefutationCause == null)).ToList();
                            foreach (var priorCompetencyEvaluation in priorCompetencyEvaluationList)
                            {
                                bool isDuplicateTransition = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.PeriodDefinitoionId == periodDefinitionId && c.PriorEvaluationBehaviouralCompetencyId == priorCompetencyEvaluation.EvaluationBehaviouralCompetencyId).Any();
                                var query = from ebc in appDbContext.EvaluationBehaviouralCompetency
                                            join bh in appDbContext.BehaviouralCompetency on ebc.BehaviouralCompetencyId equals bh.BehaviouralCompetencyId
                                            join p in appDbContext.People on new { a = ebc.RecieverAllocationPersonId, b = ebc.RecieverAllocationEvaluationBehaviouralHierarchyId } equals new { a = p.PeopleId, b = p.EvaluationHierarchyID ?? Convert.ToInt32(0) }
                                            join eh in appDbContext.evaluationHierarchies on ebc.RecieverAllocationEvaluationBehaviouralHierarchyId equals eh.EvaluationHierarchyId
                                            join d in appDbContext.Departments on eh.DepartmentId equals d.DepartmentId
                                            where (p.EffectiveEndDate == null && eh.EffectiveEndDate == null && d.EffectiveEndDate == null && ebc.EvaluationAcceptanceStatusId != 3 && ebc.EvaluationAcceptanceStatusId != 4 && ebc.RecieverAllocationPersonId == receiverId && ebc.RecieverAllocationEvaluationBehaviouralHierarchyId == receiverDepartmentId && ebc.BehaviouralCompetencyId == priorCompetencyEvaluation.BehaviouralCompetencyId && ebc.PeriodDefinitoionId == periodDefinitionId)
                                            select new { bh.Title, d.ShortName, p.FirstName, p.LastName };
                                var tableResult = query.SingleOrDefault();

                                if (!isDuplicateTransition && tableResult == null)
                                {
                                    BehaviouralCompetency priorBehaviouralCompetency = appDbContext.BehaviouralCompetency.Where(c => c.BehaviouralCompetencyId == priorCompetencyEvaluation.BehaviouralCompetencyId).SingleOrDefault();
                                   
                                    EvaluationBehaviouralCompetency evaluationBehavioural = new EvaluationBehaviouralCompetency();

                                    evaluationBehavioural.BehaviouralCompetencyId = priorCompetencyEvaluation.BehaviouralCompetencyId;
                                    evaluationBehavioural.RecieverAllocationPersonId = receiverId;
                                    evaluationBehavioural.RecieverAllocationEvaluationBehaviouralHierarchyId = receiverDepartmentId;
                                    evaluationBehavioural.PeriodDefinitoionId = periodDefinitionId;
                                    evaluationBehavioural.AllocatorPersonId = allocatorId;
                                    evaluationBehavioural.AllocatorEvaluationBehaviouralHierarchyId = employeeView.AllocatorDepartmentId;
                                    evaluationBehavioural.AllocatorRoleId = roleId;
                                    evaluationBehavioural.EvaluationAcceptanceStatusId = 1;
                                    evaluationBehavioural.CreatedBy = allocatorId;
                                    evaluationBehavioural.CreatedDate = DateTime.Now;
                                    evaluationBehavioural.IsPriorPeriodTransition = true;
                                    evaluationBehavioural.PriorPeriodDefinitionId = priorPriodDefinitionId;
                                    evaluationBehavioural.PriorEvaluationBehaviouralCompetencyId = priorCompetencyEvaluation.EvaluationBehaviouralCompetencyId;
                                    evaluationBehavioural.BehaviouralCompetencyWeight = priorCompetencyEvaluation.BehaviouralCompetencyWeight;

                                    //if (item.ParticipantId != null)
                                    //{
                                    //    List<EvaluationParticipant> evalParticipants = new List<EvaluationParticipant>();
                                    //    EvaluationParticipant evaluationParticipant = new EvaluationParticipant();
                                    //    evaluationParticipant.ParticipantId = item2.ParticipantId ?? 0;
                                    //    evaluationParticipant.ParticipantEvaluationHierarchyId = item2.ParticipantDepartmentId ?? 0;
                                    //    evaluationParticipant.RequestDate = DateTime.Now;
                                    //    evaluationParticipant.CreatedBy = allocatorId;
                                    //    evaluationParticipant.CreatedDate = DateTime.Now;
                                    //    evalParticipants.Add(evaluationParticipant);
                                    //    evaluation.EvaluationParticipants = evalParticipants;
                                    //}

                                    appDbContext.Add(evaluationBehavioural);
                                    result = appDbContext.SaveChanges();
                                }

                                else
                                {
                                    evalDuplicateList.Add(appDbContext.BehaviouralCompetency.Where(c => c.BehaviouralCompetencyId == priorCompetencyEvaluation.BehaviouralCompetencyId).SingleOrDefault().Title);
                                }

                            }

                        }
                        dictionary.Add("duplicate", evalDuplicateList);
                    }
                    transaction.Commit();
                    dictionary.Add("result", result);
                    return dictionary;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    dictionary.Add("exception", e.Message);
                    return dictionary;
                }

            }

        }
        public Dictionary<string, object> BehaviouralWeightAssignment(List<EvaluationCompetencyView> competenciesWeightList, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            ShareService shareService = new ShareService(appDbContext, null);

            int? competencyWeightSummation = 0;
            int? weightWayRealization = null;

            int numberOFEval = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.RecieverAllocationPersonId == competenciesWeightList.FirstOrDefault().RecieverAllocationPersonId &&
              c.RecieverAllocationEvaluationBehaviouralHierarchyId == competenciesWeightList.FirstOrDefault().RecieverAllocationEvaluationBehaviouralHierarchyId
              && c.PeriodDefinitoionId == shareService.GetMaxPeriodDefinitionId() && (c.AllocatorEvaluationBehaviouralHierarchyId == competenciesWeightList.FirstOrDefault().AllocatorDepartmentId
              || c.AllocatorRoleId == roleId2)
              ).Count();

            if (numberOFEval == competenciesWeightList.Count)
            {
                foreach (var competencyItem in competenciesWeightList)
                {
                    var weightWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == competencyItem.PeriodDefinitionId).SingleOrDefault();
                    weightWayRealization = weightWay.WeightWayBehaviour;

                    var competencyEvaluation = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == competencyItem.EvaluationBehaviouralCompetencyId).SingleOrDefault();
                    if (weightWay.WeightWayBehaviour == 1)//percent
                    {
                        if (competencyItem.CompetencyWeight == 0)
                        {
                            dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                            return dictionary;
                        }
                    }
                    else if ((competencyItem.CompetencyWeight < NumberScaleMinMax(weightWay, 0, 1) || competencyItem.CompetencyWeight > NumberScaleMinMax(weightWay, 1, 1)) && weightWay.WeightWayTask == 2)//likert
                    {
                        return NumberScaleMinMax(dictionary, weightWay, 1);
                    }
                    if (competencyEvaluation.AllocatorRoleId == roleId && competencyEvaluation.BehaviouralCompetencyWeight != competencyItem.CompetencyWeight)
                    {
                        competencyEvaluation.BehaviouralCompetencyWeight = competencyItem.CompetencyWeight;
                        competencyEvaluation.LastUpdatedBy = personId;
                        competencyEvaluation.LastUpdatedDate = DateTime.Now;
                        appDbContext.EvaluationBehaviouralCompetency.Update(competencyEvaluation);
                    }
                    competencyWeightSummation += competencyItem.CompetencyWeight;
                }
                if (competencyWeightSummation != 100 && weightWayRealization == 1)
                {
                    dictionary.Add("result", "مجموع اهداف باید برابر با 100 باشد.");
                    return dictionary;
                }
            }
            else
            {
                dictionary.Add("malicious", "true");
                return dictionary;
            }
            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
        public Dictionary<string, object> BehaviouralScoreAssignment(List<EvaluationCompetencyView> listOfScores, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var roleId = appDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var roleId2 = appDbContext.Roles.Where(c => c.Name == "HRAdmin").SingleOrDefault().Id;

            ShareService shareService = new ShareService(appDbContext, null);

            //int? taskScoreSummation = 0;
            int? scoreWayRealization = null;

            int numberOFEval = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.RecieverAllocationPersonId == listOfScores.FirstOrDefault().RecieverAllocationPersonId &&
              c.RecieverAllocationEvaluationBehaviouralHierarchyId == listOfScores.FirstOrDefault().RecieverAllocationEvaluationBehaviouralHierarchyId
              && c.PeriodDefinitoionId == shareService.GetMaxPeriodDefinitionId() && (c.AllocatorEvaluationBehaviouralHierarchyId == listOfScores.FirstOrDefault().AllocatorDepartmentId
              || c.AllocatorRoleId == roleId2) && (c.EvaluationAcceptanceStatusId != 3 && c.EvaluationAcceptanceStatusId != 4)
              ).Count();
            if (numberOFEval >= listOfScores.Count)
            {
                foreach (var evaluationItem in listOfScores)
                {
                    var scoreWay = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == evaluationItem.PeriodDefinitionId).SingleOrDefault();

                    scoreWayRealization = scoreWay.WeightWayScore;
                    var result = appDbContext.EvaluationBehaviourCompetencyScore.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationItem.EvaluationBehaviouralCompetencyId && c.CoacherLevel == evaluationItem.CoacherLevel).SingleOrDefault();

                    int[] evalScore = new int[1];
                    evalScore[0] = evaluationItem.CompetencyScore ?? 0;
                    //There is always one item in this loop 
                    foreach (var evaluationScoreItem in evalScore)
                    {
                        if (scoreWay.WeightWayScore == 1)//percent
                        {
                            if (evaluationScoreItem == 0)
                            {
                                dictionary.Add("result", "بازه مجاز جهت تعیین وزن بین 1 تا 100 می باشد");
                                return dictionary;
                            }
                        }
                        else if ((evaluationScoreItem < NumberScaleMinMax(scoreWay, 0, 3) || evaluationScoreItem > NumberScaleMinMax(scoreWay, 1, 3)) && scoreWay.WeightWayScore == 2)//likert
                        {
                            return NumberScaleMinMax(dictionary, scoreWay, 3);
                        }
                        if (result != null && result.Score != evaluationItem.CompetencyScore)
                        {
                            result.Score = evaluationItem.CompetencyScore ?? 0;
                            result.LastUpdatedBy = personId;
                            result.LastUpdatedDate = DateTime.Now;
                            appDbContext.Update(result);
                        }
                        else if (result == null)
                        {
                            EvaluationBehaviourCompetencyScore evaluationScore = new EvaluationBehaviourCompetencyScore();
                            evaluationScore.Score = evaluationItem.CompetencyScore ?? 0;
                            evaluationScore.CoacherLevel = evaluationItem.CoacherLevel;
                            evaluationScore.EvaluationBehaviouralCompetencyId = evaluationItem.EvaluationBehaviouralCompetencyId;
                            evaluationScore.RoleId = roleId;
                            evaluationScore.CoacherId = personId;
                            evaluationScore.CreatedBy = personId;
                            evaluationScore.CreatedDate = DateTime.Now;
                            //taskScoreSummation += evaluationScoreItem.Score;
                            appDbContext.EvaluationBehaviourCompetencyScore.Add(evaluationScore);
                        }

                    }
                    //if (criteriaScoreSummation != 100 && scoreWayRealization == 1 && hasCriteria)
                    //{
                    //    dictionary.Add("result", "مجموع شاخص های هر هدف باید برابر با 100 باشد.");
                    //    return dictionary;
                    //}
                    //criteriaScoreSummation = 0;
                }
                //if (taskScoreSummation != 100 && scoreWayRealization == 1)
                //{
                //    dictionary.Add("result", "مجموع اهداف باید برابر با 100 باشد.");
                //    return dictionary;
                //}
            }
            else
            {
                dictionary.Add("malicious", "true");
                return dictionary;
            }
            var saveChangeResult = appDbContext.SaveChanges();
            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
        private Dictionary<string, object> NumberScaleMinMax(Dictionary<string, object> dictionary, PeriodDefinitoion weightWay, int whichLikert)
        {
            int? likertWay = 0;
            switch (whichLikert)
            {
                case 1:
                    likertWay = weightWay.LikertWeightWayTaskId;
                    break;
                case 2:
                    likertWay = weightWay.LikertWeightWayBehaiviourId;
                    break;
                case 3:
                    likertWay = weightWay.LikertScoreWayId;
                    break;
                    //default:
            }
            var numberScaleMinMax = from ls in appDbContext.LikertScale
                                    join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                                    //select new{ id2=i,dep=d.}
                                    where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == likertWay)
                                    select new { ls.LikertScaleId, ld.NumberScale };
            dictionary.Add("result", $"بازه مجاز جهت تعیین وزن بین {numberScaleMinMax.Min(c => c.NumberScale)} تا {numberScaleMinMax.Max(c => c.NumberScale)} می باشد");
            return dictionary;
        }
        private int NumberScaleMinMax(PeriodDefinitoion weightWay, int minMax, int whichLikert)
        {
            int? likertWay = 0;
            switch (whichLikert)
            {
                case 1:
                    likertWay = weightWay.LikertWeightWayTaskId;
                    break;
                case 2:
                    likertWay = weightWay.LikertWeightWayBehaiviourId;
                    break;
                case 3:
                    likertWay = weightWay.LikertScoreWayId;
                    break;
                    //default:
            }
            var numberScaleMinMax = from ls in appDbContext.LikertScale
                                    join ld in appDbContext.LikertDescribor on ls.LikertScaleId equals ld.LikertScaleId
                                    //select new{ id2=i,dep=d.}
                                    where (ls.EffectiveEndDate == null && ld.EffectiveEndDate == null && ls.LikertScaleId == likertWay)
                                    select new { ls.LikertScaleId, ld.NumberScale };
            if (minMax == 1)
            {
                return numberScaleMinMax.Max(c => c.NumberScale);
            }

            return numberScaleMinMax.Min(c => c.NumberScale);
        }
        public int RenewCompetencyContract(PerformCompetencyConfirmationView evaluation, int personId, string roleId, string roleId2)
        {
            List<Notification> notificationList = new List<Notification>();

            foreach (var evaluationItem in evaluation.EvaluationCompetencyId)
            {
                var fetchEval = appDbContext.EvaluationBehaviouralCompetency.Where(c => c.EvaluationBehaviouralCompetencyId == evaluationItem).SingleOrDefault();
                if (fetchEval != null)
                {
                    Notification notification = new Notification();
                    if (evaluation.EvaluationCompetencyAcceptanceStatusId == 2)
                    {
                        fetchEval.EvaluationAcceptanceStatusId = 2;
                        notification.Title = "دستوری";
                    }
                    else if (evaluation.EvaluationCompetencyAcceptanceStatusId == 3)
                    {
                        fetchEval.EvaluationAcceptanceStatusId = 3;
                        notification.Title = "صرف نظر";
                    }
                    fetchEval.LastUpdatedBy = personId;
                    fetchEval.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(fetchEval);
                    notification.AllocatorHierarchyId = fetchEval.AllocatorEvaluationBehaviouralHierarchyId;
                    notification.AllocatorPersonId = personId;
                    notification.AllocatorRoleId = roleId;
                    notification.ReceiverPersonId = fetchEval.RecieverAllocationPersonId;
                    notification.ReceiverHierarchtId = fetchEval.RecieverAllocationEvaluationBehaviouralHierarchyId;
                    notification.ReceiverRoleId = roleId2;
                    notification.NotificationTitleId = 2;
                    notification.Visibility = false;
                    notification.PeriodDefinitionId = fetchEval.PeriodDefinitoionId;
                    notification.EvaluationId = fetchEval.EvaluationBehaviouralCompetencyId;
                    notification.CreatedDate = DateTime.Now;
                    notificationList.Add(notification);
                }
            }
            appDbContext.AddRange(notificationList);
            int result = appDbContext.SaveChanges();
            return result;
        }
        public List<ScoreView> GetCompetencyScore(int evaluationBehaviouralCompetencyId)
        {
            var sQuery = "select " +
                "tbl.EvaluationBehaviouralCompetencyId" +
                ",bc.Title" +
                ",sum(score1)score1" +
                ",sum(score2)score2" +
                ",sum(score3)score3" +
                ",sum(score4)score4" +
                ",sum(participantScore)participantScore" +
                ",sum(selfScore)selfScore " +
                "from( " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", ebcs.Score as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = 1 " +
                "union all " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", null as score1" +
                ", ebcs.Score as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = 2 " +
                "union all " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", null as score1" +
                ", null as score2" +
                ", ebcs.Score as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = 3 " +
                "union all " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", ebcs.Score as score4" +
                ", null as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = 4 " +
                "union all " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", ebcs.Score as participantScore" +
                ", null as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = -1 " +
                "union all " +
                "select " +
                "ebcs.CoacherId" +
                ", ebcs.CoacherLevel" +
                ", ebcs.EvaluationBehaviouralCompetencyId" +
                ", null as score1" +
                ", null as score2" +
                ", null as score3" +
                ", null as score4" +
                ", null as participantScore" +
                ", ebcs.Score as selfScore" +
                ", null as planningAdminScore " +
                "from " +
                "EvaluationBehaviourCompetencyScore ebcs " +
                "where " +
                "1 = 1 " +
                "and ebcs.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebcs.CoacherLevel = 0 " +
                ") as tbl join EvaluationBehaviouralCompetency ebc on ebc.EvaluationBehaviouralCompetencyId = tbl.EvaluationBehaviouralCompetencyId " +
                "join BehaviouralCompetency bc on bc.BehaviouralCompetencyId = ebc.BehaviouralCompetencyId " +
                "group by tbl.EvaluationBehaviouralCompetencyId,bc.Title";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<ScoreView> scoreView = null;

            scoreView = conn.Query<ScoreView>(sQuery, new { evaluationBehaviouralCompetencyIdd = evaluationBehaviouralCompetencyId }).ToList();

            //conn.Close();
            conn.Dispose();
            return scoreView;
        }
        public List<CoacherTruthView> GetTruthView(int behaviouralCompetencyId, int evaluationBehaviouralCompetencyId)
        {
            var sQuery = "select " +
                "t.BehaviouralCompetencyId" +
                ",t.TruthId" +
                ",t.Title" +
                ",'admin'Type" +
                ",null " +
                "from Truth t " +
                "where t.BehaviouralCompetencyId = @BehaviouralCompetencyIdd " +
                "union all " +
                "select " +
                "ebc.BehaviouralCompetencyId " +
                ",ct.CoacherTruthId" +
                ",ct.Title" +
                ",'coacher'Type" +
                ",ebc.AllocatorPersonId " +
                "from CoacherTruth ct join EvaluationBehaviouralCompetency ebc on ct.EvaluationBehaviouralCompetencyId = ebc.EvaluationBehaviouralCompetencyId " +
                "where " +
                "1 = 1 " +
                "and ct.EvaluationBehaviouralCompetencyId = @evaluationBehaviouralCompetencyIdd " +
                "and ebc.BehaviouralCompetencyId = @BehaviouralCompetencyIdd";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<CoacherTruthView> coacherTruthView = null;

            coacherTruthView = conn.Query<CoacherTruthView>(sQuery, new
            {
                BehaviouralCompetencyIdd = behaviouralCompetencyId,
                EvaluationBehaviouralCompetencyIdd = evaluationBehaviouralCompetencyId,
            }).ToList();

            //conn.Close();
            conn.Dispose();
            return coacherTruthView;
        }
    }
}
