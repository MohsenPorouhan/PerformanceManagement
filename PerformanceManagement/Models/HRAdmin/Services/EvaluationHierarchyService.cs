using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class EvaluationHierarchyService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public EvaluationHierarchyService(AppDbContext appDbContext, IConnProvider connProvider, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IEnumerable<object> GetCurrentPEmployee2()
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "distinct " +
                "p.PeopleId" +
                ",p.FirstName" +
                ",p.LastName" +
                ",anu.UserName " +
                "from AspNetUsers anu join People p on anu.PeopleId = p.PeopleId " +
                "where " +
                "1 = 1 " +
                "and p.EffectiveEndDate is null";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public IEnumerable<object> GetCurrentPEmployee()
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "distinct " +
                "p.PeopleId" +
                ",p.FirstName" +
                ",p.LastName" +
                ",anu.UserName " +
                "from AspNetUsers anu join People p on anu.PeopleId = p.PeopleId " +
                "where " +
                "1 = 1 ";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public int EditDepartment(int departmentId, string departmentName)
        {
            Department department = appDbContext.Departments.Where(c => c.DepartmentId == departmentId && c.EffectiveEndDate == null).SingleOrDefault();
            department.ShortName = departmentName;
            department.LongName = departmentName;

            appDbContext.Update(department);
            int result = appDbContext.SaveChanges();

            return result;
        }

        public HierarchyWithSeparatorDepartmentView AscendHierarchyWithSeparatorDepartment(int userId, int departmentId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"
WITH EmpsCTE AS( 
                select 
                eh.EvaluationHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 1 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId
                , cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and eh.SupervisorId = p.PeopleId 
                and p.PeopleId = @userIdd 
                and eh.[EvaluationHierarchyId] = @departmentIdd 
                union 
                select 
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,1 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId
                , cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and p.EvaluationHierarchyID is not null 
                and eh.SupervisorId != p.PeopleId 
                and p.PeopleId = @userIdd 
                and eh.[EvaluationHierarchyId]= @departmentIdd 
                UNION ALL 
                SELECT C.EvaluationHierarchyId
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.ShortName
                , Levell+1 levell
                , C.EvalHierarchyId
                , concat(p.pathh,  ',' , cast(c.EvalHierarchyId as nvarchar))pathh 
                FROM EmpsCTE AS P 
                JOIN dbo.ChartConfirmationn AS C 
                ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) 
                SELECT 
                Levell
                ,@userIdd as PeopleId
                ,@departmentIdd EvalHierarchyId
                ,pathh
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                when Levell = 5 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                else null 
                end Department
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=3) 
                when Levell = 5 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=2) 
                when Levell = 4 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                else null 
                end Chairmanship
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=4)  
                when Levell = 5 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=3)  
                when Levell = 4 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=2)  
                when Levell = 3 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1)  
                else null  
                end Management
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=5) 
                when Levell = 5 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=4) 
                when Levell = 4 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=3)  
                when Levell = 3 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=2)  
                when Levell = 2 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                else null  
                end VicePresident 
                ,dbo.Intermediary(@userIdd, @departmentIdd) Intermediary 
                FROM EmpsCTE where 1=1 and ParentEvaluationHierarchyId is null 
                order by Levell desc
";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            HierarchyWithSeparatorDepartmentView query = null;

            query = conn.Query<HierarchyWithSeparatorDepartmentView>(sQuery, new
            {
                userIdd = userId,
                departmentIdd = departmentId
            }).SingleOrDefault();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public int AscendHierarchyPreventParentShiftToChild(int ParentUserId, int ParentDepartmentId, int DepartmentId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"
declare  @ParentUserId as int
declare @ParentDepartmentId as int
declare @DepartmentId as int
set @ParentUserId=@ParentUserIdd;
set @ParentDepartmentId=@ParentDepartmentIdd;
set @DepartmentId=@DepartmentIdd;
WITH EmpsCTE AS(
                select
                eh.EvaluationHierarchyId
				,eh.EvaluationHierarchyId EvalHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 0 Levell
				,cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh
                from
                evaluationHierarchies eh,
                Departments d,
                People p
                where 1 = 1
                and eh.DepartmentId = d.DepartmentId
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
                and eh.EffectiveEndDate is null
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null
                and eh.SupervisorId = p.PeopleId
                and p.PeopleId = @ParentUserId
                and eh.[EvaluationHierarchyId] = @ParentDepartmentId
                union
                select
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
				,eh.EvaluationHierarchyId EvalHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,0 Levell
				,cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh
                from
                evaluationHierarchies eh,
                Departments d,
                People p
                where 1 = 1
                and eh.DepartmentId = d.DepartmentId
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
                and eh.EffectiveEndDate is null
                and d.EffectiveEndDate is null
                and p.EffectiveEndDate is null
                and p.EvaluationHierarchyID is not null
                and eh.SupervisorId != p.PeopleId
                and p.PeopleId = @ParentUserId
                and eh.[EvaluationHierarchyId]=@ParentDepartmentId
                UNION ALL
                SELECT C.EvaluationHierarchyId
				,c.EvalHierarchyId
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.ShortName
                , Levell+1 levell
				,concat(p.pathh ,  '->' ,cast(c.EvalHierarchyId as nvarchar))pathh
                FROM EmpsCTE AS P
                JOIN dbo.ChartConfirmationn AS C
                ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) 
                SELECT [EvaluationHierarchyId] as id
				,EvalHierarchyId
                ,[ShortName] as text
                ,case when convert(nvarchar(max),ParentEvaluationHierarchyId) is null then '#'
                --when convert(nvarchar, EvaluationHierarchyId) = 9 then '#'
                else convert(nvarchar(max), ParentEvaluationHierarchyId) end as parent 
				,Levell
				,pathh
				,PeopleId
                FROM EmpsCTE
				where 
				EvalHierarchyId=@DepartmentId
				order by 1
";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            int query = 0;

            query = conn.Query<int>(sQuery, new
            {
                ParentUserIdd = ParentUserId,
                ParentDepartmentIdd = ParentDepartmentId,
                DepartmentIdd = DepartmentId
            }).Count();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public HierarchyWithSeparatorDepartmentView AscendHierarchyWithSeparatorDepartmentWithNoLock(int userId, int departmentId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"
set transaction isolation level read uncommitted;
WITH EmpsCTE AS( 
                select 
                eh.EvaluationHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 1 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId
                , cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and eh.SupervisorId = p.PeopleId 
                and p.PeopleId = @userIdd 
                and eh.[EvaluationHierarchyId] = @departmentIdd 
                union 
                select 
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,1 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId
                , cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and p.EvaluationHierarchyID is not null 
                and eh.SupervisorId != p.PeopleId 
                and p.PeopleId = @userIdd 
                and eh.[EvaluationHierarchyId]= @departmentIdd 
                UNION ALL 
                SELECT C.EvaluationHierarchyId
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.ShortName
                , Levell+1 levell
                , C.EvalHierarchyId
                , concat(p.pathh,  ',' , cast(c.EvalHierarchyId as nvarchar))pathh 
                FROM EmpsCTE AS P 
                JOIN dbo.ChartConfirmationn AS C 
                ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) 
                SELECT 
                Levell
                ,@userIdd as PeopleId
                ,@departmentIdd EvalHierarchyId
                ,pathh
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                when Levell = 5 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                else null 
                end Department
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=3) 
                when Levell = 5 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=2) 
                when Levell = 4 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                else null 
                end Chairmanship
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=4)  
                when Levell = 5 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=3)  
                when Levell = 4 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=2)  
                when Levell = 3 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1)  
                else null  
                end Management
                ,case 
                when Levell = 6 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=5) 
                when Levell = 5 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=4) 
                when Levell = 4 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=3)  
                when Levell = 3 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=2)  
                when Levell = 2 and dbo.Intermediary(@userIdd, @departmentIdd)=0 then(select v from( 
                select row_number() over(order by roww)OrderRow,v from( 
                select '1' roww, value v from string_split(pathh,',') 
                )tb1)tb2 where OrderRow=1) 
                else null  
                end VicePresident 
                ,dbo.Intermediary(@userIdd, @departmentIdd) Intermediary 
                FROM EmpsCTE where 1=1 and ParentEvaluationHierarchyId is null 
                order by Levell desc
";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            HierarchyWithSeparatorDepartmentView query = null;

            query = conn.Query<HierarchyWithSeparatorDepartmentView>(sQuery, new
            {
                userIdd = userId,
                departmentIdd = departmentId
            }).SingleOrDefault();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public int MaxLevelAscend(int userId, int departmentId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"
declare  @userId as int
declare @departmentId as int
set @userId=@userIdd;
set @departmentId=@departmentIdd;
WITH EmpsCTE AS(
                select
                eh.EvaluationHierarchyId
				,eh.EvaluationHierarchyId EvalHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 1 Levell
				,cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh
                from
                evaluationHierarchies eh,
                Departments d,
                People p
                where 1 = 1
                and eh.DepartmentId = d.DepartmentId
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
                and eh.EffectiveEndDate is null
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null
                and eh.SupervisorId = p.PeopleId
                and p.PeopleId = @userId
                and eh.[EvaluationHierarchyId] = @departmentId
                union
                select
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
				,eh.EvaluationHierarchyId EvalHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,1 Levell
				,cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh
                from
                evaluationHierarchies eh,
                Departments d,
                People p
                where 1 = 1
                and eh.DepartmentId = d.DepartmentId
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
                and eh.EffectiveEndDate is null
                and d.EffectiveEndDate is null
                and p.EffectiveEndDate is null
                and p.EvaluationHierarchyID is not null
                and eh.SupervisorId != p.PeopleId
                and p.PeopleId = @userId
                and eh.[EvaluationHierarchyId]=@departmentId
                UNION ALL
                SELECT C.EvaluationHierarchyId
				,c.EvalHierarchyId
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.ShortName
                , Levell+1 levell
				,concat(p.pathh ,  '->' ,cast(c.EvalHierarchyId as nvarchar))pathh
                FROM EmpsCTE AS P
                JOIN dbo.ChartConfirmationn AS C
                ON P.ParentEvaluationHierarchyId = C.[EvaluationHierarchyId]) 
                SELECT 
				max(Levell) MaxLevelAscend
                FROM EmpsCTE order by 1
";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            int result = 0;

            result = conn.Query<int>(sQuery, new
            {
                userIdd = userId,
                departmentIdd = departmentId
            }).SingleOrDefault();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (result);
        }

        public int MaxLevelDescend(int userId, int departmentId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"
declare  @userId as int
declare @departmentId as int
set @userId=@userIdd;
set @departmentId=@departmentIdd;
WITH EmpsCTE AS(
select
eh.EvaluationHierarchyId
, p.PeopleId
, eh.SupervisorId
, eh.ParentEvaluationHierarchyId
, CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
, 1 Levell
,eh.EvaluationHierarchyId EvalHierarchyId
,cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh
,p.PositionType
from
evaluationHierarchies eh,
Departments d,
People p
where 1 = 1
and eh.DepartmentId = d.DepartmentId
and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
and eh.EffectiveEndDate is null
and d.EffectiveEndDate is null 
and p.EffectiveEndDate is null
and eh.SupervisorId = p.PeopleId
and p.PeopleId = @userId
and eh.[EvaluationHierarchyId] = @departmentId
--and ParentEvaluationHierarchyId is null
union
select
((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
,p.PeopleId
,eh.SupervisorId
,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
, CONCAT(p.FirstName, ' ', p.LastName)ShortName
,1 Levell
,eh.EvaluationHierarchyId EvalHierarchyId
,cast(eh.EvaluationHierarchyId as nvarchar(max)) as pathh
,p.PositionType
from
evaluationHierarchies eh,
Departments d,
People p
where 1 = 1
and eh.DepartmentId = d.DepartmentId
and eh.EvaluationHierarchyId = p.EvaluationHierarchyID
and eh.EffectiveEndDate is null
and d.EffectiveEndDate is null
and p.EffectiveEndDate is null
and p.EvaluationHierarchyID is not null
and eh.SupervisorId != p.PeopleId
and p.PeopleId = @userId
and eh.[EvaluationHierarchyId]=@departmentId
--and ParentEvaluationHierarchyId is null
UNION ALL
SELECT C.EvaluationHierarchyId
, C.PeopleId
, C.SupervisorId
, C.ParentEvaluationHierarchyId
, C.ShortName
, Levell+1 levell
,C.EvalHierarchyId
,concat(p.pathh ,  '->' ,cast(c.EvalHierarchyId as nvarchar))pathh
,p.PositionType
FROM EmpsCTE AS P
JOIN dbo.ChartConfirmationn AS C
ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]
) 
SELECT
MAX(Levell) MaxLevelDescend
FROM EmpsCTE where 1=1 order by 1
";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            int result = 0;

            result = conn.Query<int>(sQuery, new
            {
                userIdd = userId,
                departmentIdd = departmentId
            }).SingleOrDefault();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (result);
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"select 
                            d.DepartmentId
                            ,concat(d.ShortName,'(',CONCAT(p.FirstName,' ',p.LastName),' - ',anu.UserName,')')ShortName
                            from
                            Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId
                            join People p on eh.SupervisorId=p.PeopleId and eh.EvaluationHierarchyId=p.EvaluationHierarchyID
                            join AspNetUsers anu on p.PeopleId=anu.PeopleId
                            where 
                            1=1
                            and d.EffectiveEndDate is null
                            and p.EffectiveEndDate is null
                            and eh.EffectiveEndDate is null";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<Department> query = null;

            query = conn.Query<Department>(sQuery).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public IEnumerable<Department> GetPoolDepartment()
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"select 
                            d.DepartmentId
                            ,concat(d.ShortName,'(',CONCAT(p.FirstName,' ',p.LastName),' - ',anu.UserName,')')ShortName
                            from
                            Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId
                            join People p on eh.SupervisorId=p.PeopleId and eh.EvaluationHierarchyId=p.EvaluationHierarchyID
                            join AspNetUsers anu on p.PeopleId=anu.PeopleId
                            where 
                            1=1
                            and d.EffectiveEndDate is null
                            and p.EffectiveEndDate is null
                            and eh.EffectiveEndDate is null
                            and eh.DepartmentType=2";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<Department> query = null;

            query = conn.Query<Department>(sQuery).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }

        public IEnumerable<Chart> GetTree()
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"select
                            EvaluationHierarchyId Id
                            ,ShortName Text
                            ,case 
                            when ParentEvaluationHierarchyId is null then '#' else ParentEvaluationHierarchyId
                            end Parent
                            ,PositionType
                            from
                            (select 
                            eh.EvaluationHierarchyId
                            ,p.PeopleId
                            ,eh.SupervisorId 
                            ,convert(nvarchar,eh.ParentEvaluationHierarchyId)ParentEvaluationHierarchyId
                            ,eh.EvaluationHierarchyId EvalHierarchyId
                            ,CONCAT(d.ShortName,'(',p.FirstName,' ',p.LastName,' - ',anu.UserName,')')ShortName
                            ,p.PositionType
                            from 
                            evaluationHierarchies eh,
                            Departments d,
                            People p,
                            AspNetUsers anu
                            where 1=1
                            and eh.DepartmentId=d.DepartmentId
                            and eh.EvaluationHierarchyId=p.EvaluationHierarchyID
                            and eh.EffectiveEndDate is null 
                            and d.EffectiveEndDate is null
                            and p.EffectiveEndDate is null
                            and eh.SupervisorId=p.PeopleId
                            and p.PeopleId=anu.PeopleId
                            union
                            select 
                            ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc))+10000000)+p.PeopleId-1 EvaluationHierarchyId
                            ,p.PeopleId
                            ,eh.SupervisorId
                            ,convert(nvarchar,eh.EvaluationHierarchyId) ParentEvaluationHierarchyId
                            ,eh.EvaluationHierarchyId EvalHierarchyId
                            ,CONCAT(p.FirstName,' ',p.LastName,' - ',anu.UserName)ShortName
                            ,p.PositionType
                            from 
                            evaluationHierarchies eh,
                            Departments d,
                            People p,
                            AspNetUsers anu
                            where 1=1
                            and eh.DepartmentId=d.DepartmentId
                            and eh.EvaluationHierarchyId=p.EvaluationHierarchyID
                            and eh.EffectiveEndDate is null 
                            and d.EffectiveEndDate is null
                            and p.EffectiveEndDate is null
                            and p.EvaluationHierarchyID is not null
                            and eh.SupervisorId != p.PeopleId
                            and p.PeopleId=anu.PeopleId)tbl
                            order by ShortName desc";

            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<Chart> query = null;

            query = conn.Query<Chart>(sQuery).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public object SwitchDepartment(int selectedDepartment2, int parentName2, int positionType2, int positionId3, int departmentType2, int createdOrUpdatedby)
        {
            using (IDbContextTransaction transaction = appDbContext.Database.BeginTransaction())
            {
                //IDbConnection conn = connProviderOBI.Connection;
                //IDbTransaction transactionOBI = null;
                try
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();


                    if (selectedDepartment2 == parentName2)
                    {
                        dictionary.Add("notAllowedSameOriginDestination", "واحد مبداء و مقصد نمی تواند یکسان باشد.");
                        return dictionary;
                    }
                    EvaluationHierarchy evaluationHierarchy = appDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == selectedDepartment2 && c.EffectiveEndDate == null).SingleOrDefault();
                    EvaluationHierarchy parentEvalutionHierarchy2 = appDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == parentName2 && c.EffectiveEndDate == null).SingleOrDefault();
                    int maxLevelAscend = this.MaxLevelDescend(evaluationHierarchy.SupervisorId, selectedDepartment2);
                    int maxLevelDescend = this.MaxLevelAscend(parentEvalutionHierarchy2.SupervisorId, parentName2);
                    if (this.AscendHierarchyPreventParentShiftToChild(parentEvalutionHierarchy2.SupervisorId, parentName2, selectedDepartment2) > 0)
                    {
                        dictionary.Add("notAllowedShiftParentToChild", "انتقال واحد پدر به فرزند مجاز نمی باشد.");
                        return dictionary;
                    }
                    if ((maxLevelAscend + maxLevelDescend) > 6)
                    {
                        dictionary.Add("notAllowedMoreThan6Level", "امکان تعریف بیش از 6 سطح سازمانی وجود ندارد.");
                        return dictionary;
                    }

                    if (evaluationHierarchy.ParentEvaluationHierarchyId == parentName2)
                    {
                        dictionary.Add("notAllowedSameParent", "واحد پدر مورد نظر تکراری می باشد.");
                        return dictionary;
                    }

                    evaluationHierarchy.EffectiveEndDate = DateTime.Now;
                    evaluationHierarchy.LastUpdatedById = createdOrUpdatedby;
                    evaluationHierarchy.LastUpdatedDate = DateTime.Now;
                    appDbContext.Update(evaluationHierarchy);

                    People supervisor = appDbContext.People.Where(c => c.PeopleId == evaluationHierarchy.SupervisorId && c.EvaluationHierarchyID == evaluationHierarchy.EvaluationHierarchyId && c.EffectiveEndDate == null).SingleOrDefault();
                    if (supervisor.PositionType != positionType2 || supervisor.JobId != positionId3)
                    {
                        supervisor.EffectiveEndDate = DateTime.Now;
                        appDbContext.Update(supervisor);
                        People supervisorInsertion = new People
                        {
                            PeopleId = supervisor.PeopleId,
                            EffectiveStartDate = DateTime.Now,
                            FirstName = supervisor.FirstName,
                            LastName = supervisor.LastName,
                            PositionType = positionType2,
                            EvaluationHierarchyID = supervisor.EvaluationHierarchyID,
                            changeStatus = 101, //101 is switchDepartment
                            JobId = positionId3
                        };
                        appDbContext.Add(supervisorInsertion);
                    }
                    int result = appDbContext.SaveChanges();
                    int primaryPositionTypeCount = appDbContext.People.Where(c => c.PeopleId == evaluationHierarchy.SupervisorId && c.EvaluationHierarchyID != null && c.PositionType == 1 && c.EffectiveEndDate == null).Count();
                    if (primaryPositionTypeCount > 1)
                    {
                        dictionary.Add("moreThanOnePrimaryPositionType", "مربی انتصاب یافته به واحد مورد نظر نمی تواند بیش از یک واحد اصلی داشته باشد");
                        return dictionary;
                    }

                    EvaluationHierarchy parentEvalutionHierarchy = appDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == parentName2 && c.EffectiveEndDate == null).SingleOrDefault();
                    EvaluationHierarchy evaluationHierarchyInsertion = new EvaluationHierarchy
                    {
                        EvaluationHierarchyId = evaluationHierarchy.EvaluationHierarchyId,
                        EffectiveStartDate = DateTime.Now,
                        ParentEvaluationHierarchyId = parentName2,
                        ParentEffectiveStartDate = parentEvalutionHierarchy.EffectiveStartDate,
                        SupervisorId = evaluationHierarchy.SupervisorId,
                        CreatedById = createdOrUpdatedby,
                        CreationDate = DateTime.Now,
                        DepartmentType = departmentType2,
                        DepartmentId = evaluationHierarchy.DepartmentId,
                        DepartmentEffectiveStartDate = evaluationHierarchy.DepartmentEffectiveStartDate
                    };
                    appDbContext.Add(evaluationHierarchyInsertion);


                    int result2 = 0;
                    foreach (var item in this.GetAllSubsetEmployeesWithNoLock(selectedDepartment2, evaluationHierarchy.SupervisorId))
                    {
                        ChartInfo chartInfo = appDbContext.ChartInfo.Where(c => c.PeopleId == item.PeopleId && c.EvaluationHierarchyId == item.EvalHierarchyId && c.EffectiveEndDate == null).SingleOrDefault();
                        chartInfo.EffectiveEndDate = DateTime.Now;
                        chartInfo.LastUpdatedById = createdOrUpdatedby;
                        chartInfo.LastUpdatedDate = DateTime.Now;
                        appDbContext.Update(chartInfo);
                        result = appDbContext.SaveChanges();
                        HierarchyWithSeparatorDepartmentView separatorDepartmentView = this.AscendHierarchyWithSeparatorDepartmentWithNoLock(item.PeopleId, item.EvalHierarchyId);

                        ChartInfo chartInfoInsertion = new ChartInfo
                        {
                            PeopleId = chartInfo.PeopleId,
                            EvaluationHierarchyId = chartInfo.EvaluationHierarchyId,
                            Department = separatorDepartmentView.Department,
                            Chairmanship = separatorDepartmentView.Chairmanship,
                            Management = separatorDepartmentView.Management,
                            VicePresident = separatorDepartmentView.VicePresident,
                            Level = Convert.ToInt32(separatorDepartmentView.Levell),
                            Intermediary = Convert.ToBoolean(separatorDepartmentView.Intermediary),
                            EffectiveStartDate = DateTime.Now,
                            CreatedById = createdOrUpdatedby,
                            CreatedDate = DateTime.Now
                        };
                        appDbContext.Add(chartInfoInsertion);
                    }
                    int result3 = appDbContext.SaveChanges();
                    dictionary.Add("successfully", result + result2 + result3);
                    transaction.Commit();
                    return dictionary;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    //transactionOBI.Rollback();
                }
            }
            return 0;
        }
        public IEnumerable<SubsetHierarchyAndEmployeeView> GetAllSubsetEmployees(int departmentId, int peopleId)
        {
            var sQuery = @"WITH EmpsCTE AS( select 
                eh.EvaluationHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 0 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and eh.SupervisorId = p.PeopleId 
                and p.PeopleId = @peopleIdd  
                and eh.[EvaluationHierarchyId] = @departmentIdd 
                union 
                select 
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,0 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and p.EvaluationHierarchyID is not null 
                and eh.SupervisorId != p.PeopleId 
                and p.PeopleId = @peopleIdd  
                and eh.[EvaluationHierarchyId]= @departmentIdd 
                UNION ALL 
                SELECT C.EvaluationHierarchyId
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.ShortName
                , Levell+1 levell
                , C.EvalHierarchyId 
                FROM EmpsCTE AS P 
                JOIN dbo.ChartConfirmationn AS C 
                ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) 
                SELECT 
                pp.[EvaluationHierarchyId]
                , pp.[ShortName] as text
                ,case when convert(nvarchar, pp.EvaluationHierarchyId) = @departmentIdd then '' 
                else convert(nvarchar, pp.ParentEvaluationHierarchyId) end as parent
                , pp.Levell
                , pp.PeopleId
                , pp.EvalHierarchyId 
                FROM EmpsCTE as pp 
                where 1=1 
                order by 1 ";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<SubsetHierarchyAndEmployeeView> query = null;

            query = conn.Query<SubsetHierarchyAndEmployeeView>(sQuery, new { departmentIdd = departmentId, peopleIdd = peopleId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }
        public IEnumerable<SubsetHierarchyAndEmployeeView> GetAllSubsetEmployeesWithNoLock(int departmentId, int peopleId)
        {
            var sQuery = @"
set transaction isolation level read uncommitted;
WITH EmpsCTE AS( select 
                eh.EvaluationHierarchyId
                , p.PeopleId
                , eh.SupervisorId
                , eh.ParentEvaluationHierarchyId
                , CONCAT(d.ShortName, '(', p.FirstName, ' ', p.LastName, ')')ShortName
                , 0 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and eh.SupervisorId = p.PeopleId 
                and p.PeopleId = @peopleIdd  
                and eh.[EvaluationHierarchyId] = @departmentIdd 
                union 
                select 
                ((ROW_NUMBER() OVER(ORDER BY p.PeopleId asc)) + 10000000) + p.PeopleId - 1 EvaluationHierarchyId
                ,p.PeopleId
                ,eh.SupervisorId
                ,eh.EvaluationHierarchyId ParentEvaluationHierarchyId
                , CONCAT(p.FirstName, ' ', p.LastName)ShortName
                ,0 Levell
                ,eh.EvaluationHierarchyId EvalHierarchyId 
                from 
                evaluationHierarchies eh,
                Departments d,
                People p 
                where 1 = 1 
                and eh.DepartmentId = d.DepartmentId 
                and eh.EvaluationHierarchyId = p.EvaluationHierarchyID 
                and eh.EffectiveEndDate is null 
                and d.EffectiveEndDate is null 
                and p.EffectiveEndDate is null 
                and p.EvaluationHierarchyID is not null 
                and eh.SupervisorId != p.PeopleId 
                and p.PeopleId = @peopleIdd  
                and eh.[EvaluationHierarchyId]= @departmentIdd 
                UNION ALL 
                SELECT C.EvaluationHierarchyId
                , C.PeopleId
                , C.SupervisorId
                , C.ParentEvaluationHierarchyId
                , C.ShortName
                , Levell+1 levell
                , C.EvalHierarchyId 
                FROM EmpsCTE AS P 
                JOIN dbo.ChartConfirmationn AS C 
                ON C.ParentEvaluationHierarchyId = P.[EvaluationHierarchyId]) 
                SELECT 
                pp.[EvaluationHierarchyId]
                , pp.[ShortName] as text
                ,case when convert(nvarchar, pp.EvaluationHierarchyId) = @departmentIdd then '' 
                else convert(nvarchar, pp.ParentEvaluationHierarchyId) end as parent
                , pp.Levell
                , pp.PeopleId
                , pp.EvalHierarchyId 
                FROM EmpsCTE as pp 
                where 1=1 
                order by 1 ";

            IDbConnection conn = connProvider.Connection;
            conn.Open();
            List<SubsetHierarchyAndEmployeeView> query = null;

            query = conn.Query<SubsetHierarchyAndEmployeeView>(sQuery, new { departmentIdd = departmentId, peopleIdd = peopleId }).ToList();

            //conn.Close();
            conn.Dispose();
            return query;
        }
        public int OBIAssignRole(IDbConnection conn, IDbTransaction transaction, string GMemberId, string GNameId)
        {
            //IDbConnection conn = connProviderOBI.Connection;
            string insertQuery = @"INSERT INTO GROUPMEMBERS(G_MEMBER_Id,G_NAME_Id) VALUES(@GMemberId,@GNameId)";
            //conn.Open();
            //IDbTransaction transactionDapper = conn.BeginTransaction();
            var result = conn.Execute(insertQuery, new
            {
                GMemberId,
                GNameId
            }, transaction: transaction);

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            //conn.Dispose();
            //}

            return result;
        }
        public int OBIDeleteRole(IDbConnection conn, IDbTransaction transaction, string GMemberId, string GNameId)
        {
            //IDbConnection conn = connProviderOBI.Connection;
            string insertQuery = @"DELETE FROM GROUPMEMBERS where G_MEMBER_Id=@GMemberId and G_NAME_Id=@GNameId";
            //conn.Open();
            //IDbTransaction transactionDapper = conn.BeginTransaction();
            var result = conn.Execute(insertQuery, new
            {
                GMemberId,
                GNameId
            }, transaction: transaction);

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            //conn.Dispose();
            //}

            return result;
        }
        public OBIUserView OBIGetUser(IDbConnection conn, IDbTransaction transaction, string userName)
        {
            //IDbConnection conn = connProviderOBI.Connection;
            string Query = @"select * from USERS where U_NAME=@UserName";
            //conn.Open();
            //IDbTransaction transactionDapper = conn.BeginTransaction();
            var result = conn.Query<OBIUserView>(Query, new
            {
                userName
            }, transaction: transaction).SingleOrDefault();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            //conn.Dispose();
            //}

            return result;
        }
        public OBIGroupView OBIGetGroup(IDbConnection conn, IDbTransaction transaction, string GroupName)
        {
            //IDbConnection conn = connProviderOBI.Connection;
            string Query = @"select * from GROUPS where G_NAME=@GroupName";
            //conn.Open();
            //IDbTransaction transactionDapper = conn.BeginTransaction();
            var result = conn.Query<OBIGroupView>(Query, new
            {
                GroupName
            }, transaction: transaction).SingleOrDefault();

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            //conn.Dispose();
            //}

            return result;
        }
    }
}
