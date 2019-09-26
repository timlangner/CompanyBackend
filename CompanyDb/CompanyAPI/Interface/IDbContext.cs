using System.Data;

namespace CompanyAPI.Interface
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
    }
}
