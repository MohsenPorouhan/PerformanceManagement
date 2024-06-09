using Dapper;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.ICTAdmin.Services
{
    public class AccountService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;

        public AccountService(AppDbContext appDbContext, IConnProvider connProvider)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
        }
        public int OBIRegister(IDbConnection conn, IDbTransaction transaction, string password, string userName,string Id)
        {
            //IDbConnection conn = connProviderOBI.Connection;
            string UpdateQuery = @"INSERT INTO USERS(Id,U_NAME,MAIL_ADDRESS,U_PASSWORD)VALUES(@Id,@userName,@userName,@password)";
            //conn.Open();
            //IDbTransaction transactionDapper = conn.BeginTransaction();
            var result = conn.Execute(UpdateQuery, new
            {
                Id,
                userName,
                password
            }, transaction: transaction);

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            //conn.Dispose();
            //}

            return result;
        }
    }
}
