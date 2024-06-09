using Microsoft.Extensions.Configuration;
using PerformanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Util
{
    public class OBIConnProvider : IConnProviderOBI
    {
        private readonly IConfiguration config;
        public OBIConnProvider(IConfiguration config)
        {
            this.config = config;
        }

        private SqlConnection sqlConn { get; set; }
        public IDbConnection Connection
        {
            get
            {
                ///if (sqlConn == null)
                sqlConn = new SqlConnection(config.GetConnectionString("OBIDBConnection"));
                return sqlConn;
            }
        }
    }
    public interface IConnProviderOBI
    {
        IDbConnection Connection { get; }
    }
}
