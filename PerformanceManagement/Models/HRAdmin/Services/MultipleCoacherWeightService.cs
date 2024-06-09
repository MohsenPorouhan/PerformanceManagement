using Dapper;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class MultipleCoacherWeightService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public MultipleCoacherWeightService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public Dictionary<object, object> GetMultipleCoacherWeightList(DataTableParameter dataTableParameter, int? periodDefinitionId, int? employeeId)
        {
            String[] aColumns = { "PeriodCode", "PeriodTitle", "FullName" };
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
                ",RecieverAllocationPersonId" +
                ",FullName" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",TaskCoacherNumber" +
                ",CompetencyCoacherNumber" +
                ",ROUND(FinalTaskScore,3)FinalTaskScore" +
                ",ROUND(FinalCompetencyScore,3)FinalCompetencyScore " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY tbl.PeriodDefinitoionId asc))  indexx" +
                ", tbl.RecieverAllocationPersonId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", tbl.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", sum(tbl.TaskCoacherNumber)TaskCoacherNumber" +
                ", sum(tbl.CompetencyCoacherNumber)CompetencyCoacherNumber" +
                ",(select isnull(mtcoefc.FinalTaskScore,'-1') FinalTaskScore " +
                "from MultipleTaskCoacherOfEmployeeFinalCalc mtcoefc " +
                "where mtcoefc.EmployeeId = tbl.RecieverAllocationPersonId " +
                "and mtcoefc.PeriodDefinitionId = tbl.PeriodDefinitoionId) as FinalTaskScore" +
                ",(select isnull(mccoefc.FinalCompetencyScore,'-1') FinalCompetencyScore " +
                "from MultipleCompetencyCoacherOfEmployeeFinalCalc mccoefc " +
                "where mccoefc.EmployeeId = tbl.RecieverAllocationPersonId " +
                "and mccoefc.PeriodDefinitionId = tbl.PeriodDefinitoionId) as FinalCompetencyScore " +
                "from " +
                "(select " +
                "fsc.RecieverAllocationPersonId" +
                ", fsc.PeriodDefinitoionId" +
                ", COUNT(*) TaskCoacherNumber" +
                ", null CompetencyCoacherNumber " +
                "from " +
                "FinalScoreCalculation fsc " +
                "where " +
                "1 = 1 " +
                "and fsc.PeriodDefinitoionId = ISNULL(null, fsc.PeriodDefinitoionId) " +
                "group by fsc.RecieverAllocationPersonId, fsc.PeriodDefinitoionId " +
                "having COUNT(*) > 1 " +
                "union " +
                "select " +
                "fscc.RecieverAllocationPersonId" +
                ", fscc.PeriodDefinitoionId" +
                ", null" +
                ", COUNT(*) CompetencyCoacherNumber " +
                "from " +
                "FinalScoreCompetencyCalculation fscc " +
                "where " +
                "1 = 1 " +
                "and fscc.PeriodDefinitoionId = ISNULL(null, fscc.PeriodDefinitoionId) " +
                "group by fscc.RecieverAllocationPersonId, fscc.PeriodDefinitoionId " +
                "having COUNT(*) > 1 " +
                ")tbl join People p on tbl.RecieverAllocationPersonId = p.PeopleId " +
                "join PeriodDefinitoion pd on tbl.PeriodDefinitoionId = pd.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and p.PositionType = 1 " +
                "and p.EffectiveEndDate is null " +
                "and tbl.RecieverAllocationPersonId = ISNULL(null, tbl.RecieverAllocationPersonId) " +
                "group by " +
                "tbl.RecieverAllocationPersonId" +
                ",tbl.PeriodDefinitoionId" +
                ",p.FirstName" +
                ",p.LastName" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ") as tbl2 where 1 = 1 ";
            string queryFilteredTotal = "select " +
                "indexx" +
                ",RecieverAllocationPersonId" +
                ",FullName" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",TaskCoacherNumber" +
                ",CompetencyCoacherNumber" +
                ",ROUND(FinalTaskScore,3)FinalTaskScore" +
                ",ROUND(FinalCompetencyScore,3)FinalCompetencyScore " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY tbl.PeriodDefinitoionId asc))  indexx" +
                ", tbl.RecieverAllocationPersonId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", tbl.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", sum(tbl.TaskCoacherNumber)TaskCoacherNumber" +
                ", sum(tbl.CompetencyCoacherNumber)CompetencyCoacherNumber" +
                ",(select isnull(mtcoefc.FinalTaskScore,'-1') FinalTaskScore " +
                "from MultipleTaskCoacherOfEmployeeFinalCalc mtcoefc " +
                "where mtcoefc.EmployeeId = tbl.RecieverAllocationPersonId " +
                "and mtcoefc.PeriodDefinitionId = tbl.PeriodDefinitoionId) as FinalTaskScore" +
                ",(select isnull(mccoefc.FinalCompetencyScore,'-1') FinalCompetencyScore " +
                "from MultipleCompetencyCoacherOfEmployeeFinalCalc mccoefc " +
                "where mccoefc.EmployeeId = tbl.RecieverAllocationPersonId " +
                "and mccoefc.PeriodDefinitionId = tbl.PeriodDefinitoionId) as FinalCompetencyScore " +
                "from " +
                "(select " +
                "fsc.RecieverAllocationPersonId" +
                ", fsc.PeriodDefinitoionId" +
                ", COUNT(*) TaskCoacherNumber" +
                ", null CompetencyCoacherNumber " +
                "from " +
                "FinalScoreCalculation fsc " +
                "where " +
                "1 = 1 " +
                "and fsc.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, fsc.PeriodDefinitoionId) " +
                "group by fsc.RecieverAllocationPersonId, fsc.PeriodDefinitoionId " +
                "having COUNT(*) > 1 " +
                "union " +
                "select " +
                "fscc.RecieverAllocationPersonId" +
                ", fscc.PeriodDefinitoionId" +
                ", null" +
                ", COUNT(*) CompetencyCoacherNumber " +
                "from " +
                "FinalScoreCompetencyCalculation fscc " +
                "where " +
                "1 = 1 " +
                "and fscc.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, fscc.PeriodDefinitoionId) " +
                "group by fscc.RecieverAllocationPersonId, fscc.PeriodDefinitoionId " +
                "having COUNT(*) > 1 " +
                ")tbl join People p on tbl.RecieverAllocationPersonId = p.PeopleId " +
                "join PeriodDefinitoion pd on tbl.PeriodDefinitoionId = pd.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and p.PositionType = 1 " +
                "and p.EffectiveEndDate is null " +
                "and tbl.RecieverAllocationPersonId = ISNULL(@employeeIdd, tbl.RecieverAllocationPersonId) " +
                "group by " +
                "tbl.RecieverAllocationPersonId" +
                ",tbl.PeriodDefinitoionId" +
                ",p.FirstName" +
                ",p.LastName" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ") as tbl2 where 1 = 1 " +
                where;
            string sQuery = "select " +
                "indexx" +
                ",RecieverAllocationPersonId" +
                ",FullName" +
                ",PeriodDefinitoionId" +
                ",PeriodCode" +
                ",PeriodTitle" +
                ",TaskCoacherNumber" +
                ",CompetencyCoacherNumber" +
                ",ROUND(FinalTaskScore,3)FinalTaskScore" +
                ",ROUND(FinalCompetencyScore,3)FinalCompetencyScore " +
                "from( " +
                "select " +
                "(ROW_NUMBER() OVER(ORDER BY tbl.PeriodDefinitoionId asc))  indexx" +
                ", tbl.RecieverAllocationPersonId" +
                ", CONCAT(p.FirstName, ' ', p.LastName)FullName" +
                ", tbl.PeriodDefinitoionId" +
                ", pd.PeriodCode" +
                ", pd.PeriodTitle" +
                ", sum(tbl.TaskCoacherNumber)TaskCoacherNumber" +
                ", sum(tbl.CompetencyCoacherNumber)CompetencyCoacherNumber" +
                ",(select isnull(mtcoefc.FinalTaskScore,'-1') FinalTaskScore " +
                "from MultipleTaskCoacherOfEmployeeFinalCalc mtcoefc " +
                "where mtcoefc.EmployeeId = tbl.RecieverAllocationPersonId " +
                "and mtcoefc.PeriodDefinitionId = tbl.PeriodDefinitoionId) as FinalTaskScore" +
                ",(select isnull(mccoefc.FinalCompetencyScore,'-1') FinalCompetencyScore " +
                "from MultipleCompetencyCoacherOfEmployeeFinalCalc mccoefc " +
                "where mccoefc.EmployeeId = tbl.RecieverAllocationPersonId " +
                "and mccoefc.PeriodDefinitionId = tbl.PeriodDefinitoionId) as FinalCompetencyScore " +
                "from " +
                "(select " +
                "fsc.RecieverAllocationPersonId" +
                ", fsc.PeriodDefinitoionId" +
                ", COUNT(*) TaskCoacherNumber" +
                ", null CompetencyCoacherNumber " +
                "from " +
                "FinalScoreCalculation fsc " +
                "where " +
                "1 = 1 " +
                "and fsc.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, fsc.PeriodDefinitoionId) " +
                "group by fsc.RecieverAllocationPersonId, fsc.PeriodDefinitoionId " +
                "having COUNT(*) > 1 " +
                "union " +
                "select " +
                "fscc.RecieverAllocationPersonId" +
                ", fscc.PeriodDefinitoionId" +
                ", null" +
                ", COUNT(*) CompetencyCoacherNumber " +
                "from " +
                "FinalScoreCompetencyCalculation fscc " +
                "where " +
                "1 = 1 " +
                "and fscc.PeriodDefinitoionId = ISNULL(@periodDefinitionIdd, fscc.PeriodDefinitoionId) " +
                "group by fscc.RecieverAllocationPersonId, fscc.PeriodDefinitoionId " +
                "having COUNT(*) > 1 " +
                ")tbl join People p on tbl.RecieverAllocationPersonId = p.PeopleId " +
                "join PeriodDefinitoion pd on tbl.PeriodDefinitoionId = pd.PeriodDefinitoionId " +
                "where " +
                "1 = 1 " +
                "and p.PositionType = 1 " +
                "and p.EffectiveEndDate is null " +
                "and tbl.RecieverAllocationPersonId = ISNULL(@employeeIdd, tbl.RecieverAllocationPersonId) " +
                "group by " +
                "tbl.RecieverAllocationPersonId" +
                ",tbl.PeriodDefinitoionId" +
                ",p.FirstName" +
                ",p.LastName" +
                ",pd.PeriodCode" +
                ",pd.PeriodTitle" +
                ") as tbl2 where 1 = 1 " +
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
                    employeeIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }
                ).ToList();
            }
            else if (dataTableParameter.length == -1)
            {
                query = conn.Query<object>(sQuery, new
                {
                    sVal = "%" + dataTableParameter.search + "%",
                    employeeIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }).ToList();
            }
            else if (!dataTableParameter.search.Equals(""))
            {
                query = conn.Query<object>(sQuery, new
                {
                    start = dataTableParameter.start + 1,
                    endd = dataTableParameter.length + dataTableParameter.start,
                    sVal = "%" + dataTableParameter.search + "%",
                    employeeIdd = employeeId,
                    periodDefinitionIdd = periodDefinitionId
                }
                ).ToList();
            }
            object totalResult = conn.Query(queryTotalResult).Count();

            object filterTotal = conn.Query(queryFilteredTotal, new
            {
                sVal = "%" + dataTableParameter.search + "%",
                employeeIdd = employeeId,
                periodDefinitionIdd = periodDefinitionId
            }).Count();
            //conn.Close();
            conn.Dispose();
            dictionary.Add("recordsTotal", totalResult);
            dictionary.Add("recordsFiltered", filterTotal);
            dictionary.Add("draw", dataTableParameter.draw);
            dictionary.Add("aaData", query);

            return (dictionary);
        }
        public IEnumerable<object> GetTaskCoacherList(int periodDefinitionId, int employeeId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "fsc.FinalScoreCalculationId" +
                ",fsc.AllocatorLevel" +
                ",fsc.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId " +
                "where eh.EvaluationHierarchyId = fsc.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null)CoacherDepartmentName" +
                ",fsc.AllocatorPersonId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p " +
                "where p.PeopleId = fsc.AllocatorPersonId and p.PositionType = 1 and p.EffectiveEndDate is null)CoacherFullName" +
                ",fsc.CoacherType" +
                ",(select anr.Name from AspNetRoles anr where anr.Id = fsc.CoacherType)RoleName" +
                ",fsc.PeriodDefinitoionId" +
                ",fsc.RecieverAllocationEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId=eh.DepartmentId " +
                "where eh.EvaluationHierarchyId = fsc.RecieverAllocationEvaluationHierarchyId and d.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null)EmployeeDepartmentName" +
                ",fsc.RecieverAllocationPersonId" +
                ",ROUND(fsc.FinalScore,3)FinalScore " +
                "from " +
                "FinalScoreCalculation fsc " +
                "where " +
                "1 = 1 " +
                "and fsc.PeriodDefinitoionId = @periodDefinitionIdd " +
                "and fsc.RecieverAllocationPersonId = @employeeIdd ";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                periodDefinitionIdd = periodDefinitionId,
                employeeIdd = employeeId
            }).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public Dictionary<string, object> WeightUpTaskOfMultipleCoacher([FromBody]List<MultipleTaskCoacherView> listOfMultipleTaskCoacher, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            double summation = 0;
            double? finalTaskScore = 0;
            bool isTrue = false;

            foreach (var item in listOfMultipleTaskCoacher)
            {
                FinalScoreCalculation finalScoreCalculation = appDbContext.FinalScoreCalculation.Where(c => c.FinalScoreCalculationId == item.FinalScoreCalculationId).SingleOrDefault();
                if (finalScoreCalculation != null)
                {
                    isTrue = true;
                    finalScoreCalculation.MultipleCoacherWeight = item.TaskWeight;
                    appDbContext.Update(finalScoreCalculation);
                    summation += item.TaskWeight;
                    double taskWeight = item.TaskWeight / 100;
                    finalTaskScore += taskWeight * finalScoreCalculation.FinalScore;
                }
            }
            var query = appDbContext.MultipleTaskCoacherOfEmployeeFinalCalc.Where(c => c.EmployeeId == listOfMultipleTaskCoacher.FirstOrDefault().EmployeeId
              && c.PeriodDefinitionId == listOfMultipleTaskCoacher.FirstOrDefault().PeriodDefinitoionId).SingleOrDefault();
            if (query != null)
            {
                isTrue = false;
                dictionary.Add("duplication", "duplication");
            }
            else if (summation == 100)
            {
                MultipleTaskCoacherOfEmployeeFinalCalc multipleTaskCoacherOfEmployeeFinalCalc = new MultipleTaskCoacherOfEmployeeFinalCalc();
                multipleTaskCoacherOfEmployeeFinalCalc.EmployeeId = listOfMultipleTaskCoacher.FirstOrDefault().EmployeeId;
                multipleTaskCoacherOfEmployeeFinalCalc.PeriodDefinitionId = listOfMultipleTaskCoacher.FirstOrDefault().PeriodDefinitoionId;
                multipleTaskCoacherOfEmployeeFinalCalc.FinalTaskScore = finalTaskScore;
                appDbContext.Add(multipleTaskCoacherOfEmployeeFinalCalc);
            }
            else
            {
                isTrue = false;
                dictionary.Add("overSummation", "over");
            }
            int saveChangeResult = 0;
            if (isTrue)
            {
                saveChangeResult = appDbContext.SaveChanges();
            }

            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
        public IEnumerable<object> GetCompetencyCoacherList(int periodDefinitionId, int employeeId)
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = "select " +
                "fscc.FinalScoreCompetencyCalculationId" +
                ",fscc.AllocatorLevel" +
                ",fscc.AllocatorEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId " +
                "where eh.EvaluationHierarchyId = fscc.AllocatorEvaluationHierarchyId and d.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null)CoacherDepartmentName" +
                ",fscc.AllocatorPersonId" +
                ",(select CONCAT(p.FirstName, ' ', p.LastName) from People p " +
                "where p.PeopleId = fscc.AllocatorPersonId and p.PositionType = 1 and p.EffectiveEndDate is null)CoacherFullName" +
                ",fscc.CoacherType" +
                ",(select anr.Name from AspNetRoles anr where anr.Id = fscc.CoacherType)RoleName" +
                ",fscc.PeriodDefinitoionId" +
                ",fscc.RecieverAllocationEvaluationHierarchyId" +
                ",(select d.ShortName from Departments d join evaluationHierarchies eh on d.DepartmentId = eh.DepartmentId " +
                "where eh.EvaluationHierarchyId = fscc.RecieverAllocationEvaluationHierarchyId and d.EffectiveEndDate is null " +
                "and eh.EffectiveEndDate is null)EmployeeDepartmentName" +
                ",fscc.RecieverAllocationPersonId" +
                ",ROUND(fscc.FinalCompetneciesScore, 3)FinalScore " +
                "from " +
                "FinalScoreCompetencyCalculation fscc " +
                "where " +
                "1 = 1 " +
                "and fscc.PeriodDefinitoionId = @periodDefinitionIdd " +
                "and fscc.RecieverAllocationPersonId = @employeeIdd ";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
                periodDefinitionIdd = periodDefinitionId,
                employeeIdd = employeeId
            }).ToList();
            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            conn.Dispose();
            //}

            return (query);
        }
        public Dictionary<string, object> WeightUpCompetencyOfMultipleCoacher([FromBody]List<MultipleCompetencyCoacherView> listOfMultipleCompetencyCoacher, int personId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            double summation = 0;
            double? finalCompetencyScore = 0;
            bool isTrue = false;

            foreach (var item in listOfMultipleCompetencyCoacher)
            {
                FinalScoreCompetencyCalculation finalScoreCompetencyCalculation = appDbContext.FinalScoreCompetencyCalculation.Where(c => c.FinalScoreCompetencyCalculationId == item.FinalScoreCompetencyCalculationId).SingleOrDefault();
                if (finalScoreCompetencyCalculation != null)
                {
                    isTrue = true;
                    finalScoreCompetencyCalculation.MultipleCoacherWeight = item.CompetencyWeight;
                    appDbContext.Update(finalScoreCompetencyCalculation);
                    summation += item.CompetencyWeight;
                    double taskWeight = item.CompetencyWeight / 100;
                    finalCompetencyScore += taskWeight * finalScoreCompetencyCalculation.FinalCompetneciesScore;
                }
            }
            var query = appDbContext.MultipleTaskCoacherOfEmployeeFinalCalc.Where(c => c.EmployeeId == listOfMultipleCompetencyCoacher.FirstOrDefault().EmployeeId
              && c.PeriodDefinitionId == listOfMultipleCompetencyCoacher.FirstOrDefault().PeriodDefinitoionId).SingleOrDefault();
            if (query != null)
            {
                isTrue = false;
                dictionary.Add("duplication", "duplication");
            }
            else if (summation == 100)
            {
                MultipleCompetencyCoacherOfEmployeeFinalCalc multipleCompetencyCoacherOfEmployeeFinalCalc = new MultipleCompetencyCoacherOfEmployeeFinalCalc();
                multipleCompetencyCoacherOfEmployeeFinalCalc.EmployeeId = listOfMultipleCompetencyCoacher.FirstOrDefault().EmployeeId;
                multipleCompetencyCoacherOfEmployeeFinalCalc.PeriodDefinitionId = listOfMultipleCompetencyCoacher.FirstOrDefault().PeriodDefinitoionId;
                multipleCompetencyCoacherOfEmployeeFinalCalc.FinalCompetencyScore = finalCompetencyScore;
                appDbContext.Add(multipleCompetencyCoacherOfEmployeeFinalCalc);
            }
            else
            {
                isTrue = false;
                dictionary.Add("overSummation", "over");
            }
            int saveChangeResult = 0;
            if (isTrue)
            {
                saveChangeResult = appDbContext.SaveChanges();
            }

            dictionary.Add("result", saveChangeResult);
            return dictionary;
        }
    }
}
