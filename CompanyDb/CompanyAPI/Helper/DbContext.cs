using CompanyAPI.Interface;
using CompanyAPI.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Helper
{
    public class DbContext : IDbContext
    {
        private readonly DbSettings _settings;
        public DbContext(IOptions<DbSettings> options)
        {
            _settings = options.Value;  
        }

        public IDbConnection GetConnection()
        {
            var con = new SqlConnection(_settings.Connection);
            con.Open();
            return con;
        }
    }
}
