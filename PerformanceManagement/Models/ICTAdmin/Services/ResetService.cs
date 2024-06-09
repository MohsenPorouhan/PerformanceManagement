using Dapper;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.ICTAdmin.Services
{
    public class ResetService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public ResetService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }

        public IEnumerable<object> UserList()
        {
            IDbConnection conn = connProvider.Connection;
            string sQuery = @"select  distinct
                            p.PeopleId
                            ,anu.Id
                            ,anu.UserName
                            ,CONCAT(anu.UserName, '    ', p.FirstName, ' ', p.LastName)FullName 
                            from 
                            AspNetUsers anu join People p on anu.PeopleId = p.PeopleId 
                            where 
                            1 = 1 
                            and p.EffectiveEndDate is null ";
            // if (conn.State == ConnectionState.Closed)
            // {
            conn.Open();
            //}
            List<object> query = null;

            query = conn.Query<object>(sQuery, new
            {
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
