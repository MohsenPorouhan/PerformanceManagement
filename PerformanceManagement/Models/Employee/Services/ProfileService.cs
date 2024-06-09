using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PerformanceManagement.Models.Coacher.View;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace PerformanceManagement.Models.Employee.Services
{
    public class ProfileService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConnProvider connProvider;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProviderOBI connProviderOBI;

        public ProfileService(AppDbContext appDbContext, IConnProvider connProvider, UserManager<IdentityUser> userManager, IConnProviderOBI connProviderOBI)
        {
            this.appDbContext = appDbContext;
            this.connProvider = connProvider;
            this.userManager = userManager;
            this.connProviderOBI = connProviderOBI;
        }
        public int OBIChangePassword(IDbConnection conn, IDbTransaction transaction,string password,string userName)
        {
            //IDbConnection conn = connProviderOBI.Connection;
            string UpdateQuery = @"update USERS set U_PASSWORD=@Password where U_NAME=@UserName";
            //conn.Open();
            //IDbTransaction transactionDapper = conn.BeginTransaction();
            var result = conn.Execute(UpdateQuery, new
            {
                password,
                userName
            },transaction:transaction);

            //if (conn.State == ConnectionState.Open)
            //{
            //conn.Close();
            //conn.Dispose();
            //}

            return result;
        }
    }
}
