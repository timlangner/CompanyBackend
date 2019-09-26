using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using Dapper;
using CompanyAPI.Model.Dto;
using System.Threading.Tasks;

namespace CompanyAPI.Repository
{
    public class CompanyRepository : IBaseInterface<Company, CompanyDto>
    {

        private readonly IDbContext _dbContext;

        private const string SelectCmd = "SELECT Id, Name, FoundedDate FROM viCompany";
        private const string SpCreateCompany = "spCreateCompany";
        private const string SpUpdateCompany = "spUpdateCompany";
        private const string SpDeleteCompany = "spDeleteCompany";
        private object _loggerFactory;

        public CompanyRepository(IDbContext dbContext, object loggerFactory)
        {
            _dbContext = dbContext;
            _loggerFactory = loggerFactory;
        }

        public async Task<List<Company>> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return (await sqlcon.QueryAsync<Company>(SelectCmd)).AsList();
            }
        }

        public async Task<Company> Read(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return await sqlcon.QueryFirstOrDefaultAsync<Company>($"{SelectCmd} WHERE Id = @id", parameters);
            }
        }

        public async Task<bool> Create(CompanyDto companyDto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", companyDto.Name);
            parameters.Add("@FoundedDate", companyDto.FoundedDate);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return  1 == await sqlcon.ExecuteAsync(SpCreateCompany, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Update(int id, CompanyDto companyDto)
        {
            var retval = new Company();

            var parameters = new DynamicParameters();
            parameters.Add("@DbId", id);
            parameters.Add("@CompanyName", companyDto.Name);
            parameters.Add("@FoundedDate", companyDto.FoundedDate);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(SpUpdateCompany, parameters, commandType: CommandType.StoredProcedure);
                }
            } catch(SqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }        
        }

        public async Task<bool> Delete(int id = 0)
        {
            if (id < 0)
            {
                throw new Helper.RepoException(Helper.RepoResultType.WRONGPARAMETER);
            }
            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DbId", id);

                    return 1 == await sqlcon.ExecuteAsync(SpDeleteCompany, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQLERROR);
            }
        }

    }
}
