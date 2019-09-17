using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Interface
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
    }
}
