using Dapper;
using PerformanceManagement.Models.ICTAdmin;
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
    public class EmployeeManagementService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public EmployeeManagementService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public Dictionary<object, object> GetEmployeeList(DataTableParameter dataTableParameter)
        {
            String[] aColumns = { "FirstName", "LastName", "UserName", "IdNumber", "Email" };
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
                                        ,PeopleId
                                        ,FirstName
                                        ,LastName
                                        ,UserId
                                        ,UserName
                                        , IdNumber
                                        , Email
                                        from( 
                                        select 
                                        ROW_NUMBER() OVER(ORDER BY PeopleId desc) As indexx
                                        , PeopleId
                                        , FirstName
                                        , LastName
                                        , UserId
                                        , UserName 
                                        , IdNumber
                                        , Email
                                        from 
                                        (select 
                                        distinct 
                                        p.PeopleId
                                        , p.FirstName
                                        , p.LastName
                                        , anu.Id UserId
                                        , anu.UserName
                                        , anu.IdNumber
                                        , anu.Email
                                        from 
                                        People p join AspNetUsers anu on p.PeopleId = anu.PeopleId 
                                        where 
                                        1 = 1 
                                        and p.EffectiveEndDate is null)tbl 
                                        )tbl2 where 1 = 1 ";

            string queryFilteredTotal = @"select 
                                        indexx
                                        ,PeopleId
                                        ,FirstName
                                        ,LastName
                                        ,UserId
                                        ,UserName
                                        , IdNumber
                                        , Email
                                        from( 
                                        select 
                                        ROW_NUMBER() OVER(ORDER BY PeopleId desc) As indexx
                                        , PeopleId
                                        , FirstName
                                        , LastName
                                        , UserId
                                        , UserName 
                                        , IdNumber
                                        , Email
                                        from 
                                        (select 
                                        distinct 
                                        p.PeopleId
                                        , p.FirstName
                                        , p.LastName
                                        , anu.Id UserId
                                        , anu.UserName
                                        , anu.IdNumber
                                        , anu.Email
                                        from 
                                        People p join AspNetUsers anu on p.PeopleId = anu.PeopleId 
                                        where 
                                        1 = 1 
                                        and p.EffectiveEndDate is null)tbl 
                                        )tbl2 where 1 = 1 " +
                where;
            string sQuery = @"select 
                            indexx
                            ,PeopleId
                            ,FirstName
                            ,LastName
                            ,UserId
                            ,UserName
                            , IdNumber
                            , Email
                            from( 
                            select 
                            ROW_NUMBER() OVER(ORDER BY PeopleId desc) As indexx
                            , PeopleId
                            , FirstName
                            , LastName
                            , UserId
                            , UserName 
                            , IdNumber
                            , Email
                            from 
                            (select 
                            distinct 
                            p.PeopleId
                            , p.FirstName
                            , p.LastName
                            , anu.Id UserId
                            , anu.UserName
                            , anu.IdNumber
                            , anu.Email
                            from 
                            People p join AspNetUsers anu on p.PeopleId = anu.PeopleId 
                            where 
                            1 = 1 
                            and p.EffectiveEndDate is null)tbl 
                            )tbl2 where 1 = 1" +
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

        public int EditEmployeeInfo(int peopleId, string firstName, string lastName, int idNumber, string email)
        {
            List<People> people = appDbContext.People.Where(c => c.PeopleId == peopleId).ToList();
            ApplicationUser applicationUser = appDbContext.applicationUsers.Where(c => c.People.PeopleId == peopleId).SingleOrDefault();
            applicationUser.IdNumber = idNumber;
            applicationUser.Email = email;
            applicationUser.NormalizedEmail = email.ToUpper();

            appDbContext.Update(applicationUser);
            foreach (var item in people)
            {
                item.FirstName = firstName;
                item.LastName = lastName;
                appDbContext.Update(item);
            }
            int result = appDbContext.SaveChanges();
            return result;
        }

        public int EditPublicTask(int taskId, string title, string description, int[] correspondentTask, int personId)
        {
            Task task = appDbContext.Task.Where(c => c.TaskId == taskId).SingleOrDefault();
            task.Title = title;
            task.Description = description;
            task.LastUpdatedBy = personId;
            task.LastUpdatedDate = DateTime.Now;
            appDbContext.Update(task);

            appDbContext.RemoveRange(appDbContext.PublicTask.Where(c => c.TaskId == taskId).ToList());

            List<PublicTask> publicTask = new List<PublicTask>();

            foreach (var item in correspondentTask)
            {
                publicTask.Add(new PublicTask() { TaskId = taskId, CorrespondentTaskId = item, CreatedBy = personId, CreatedDate = DateTime.Now, LastUpdatedBy = personId, LastUpdatedDate = DateTime.Now });
            }
            appDbContext.AddRange(publicTask);

            var result = appDbContext.SaveChanges();

            return result;
        }

    }
}
