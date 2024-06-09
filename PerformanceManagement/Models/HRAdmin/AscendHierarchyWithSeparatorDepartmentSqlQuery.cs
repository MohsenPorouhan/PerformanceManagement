using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class AscendHierarchyWithSeparatorDepartmentSqlQuery
    {
        public int Levell { get; set; }
        public int PeopleId { get; set; }
        public int EvalHierarchyId { get; set; }
        public string pathh { get; set; }
        public string Department { get; set; }
        public string Chairmanship { get; set; }
        public string Management { get; set; }
        public string VicePresident { get; set; }
        public string Intermediary { get; set; }
        public string Select()
        {
            return @"
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
        }

    }
}
