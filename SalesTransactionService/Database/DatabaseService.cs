using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransactionService
{
    public class DatabaseService : IDatabaseService
    {
        IConfiguration config;

        public DatabaseService(IConfiguration config)
        {
            this.config = config;

        }

        public async Task<IDbConnection> GetConnection()
        {
            var conn = new SqlConnection(config.GetConnectionString("DefaultConnection")); // append application name here. and handle user id
            conn.Open();
            return conn;
        }
    }
}
