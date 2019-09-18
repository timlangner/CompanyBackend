using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
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

        string selectCmd = "SELECT Id, Name, FoundedDate FROM viCompany";
        string spCreateCompany = "spCreateCompany";
        string spUpdateCompany = "spUpdateCompany";
        string spDeleteCompany = "spDeleteCompany";

        public CompanyRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Company>> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return (await sqlcon.QueryAsync<Company>(selectCmd)).AsList();
            }
        }

        public async Task<Company> Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return await sqlcon.QueryFirstOrDefaultAsync<Company>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }

        public async Task<bool> Create(CompanyDto companyDto)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyName", companyDto.Name);
            parameters.Add("@FoundedDate", companyDto.FoundedDate);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return  1 == await sqlcon.ExecuteAsync(spCreateCompany, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Update(int id, CompanyDto companyDto)
        {
            Company retval = new Company();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DbId", id);
            parameters.Add("@CompanyName", companyDto.Name);
            parameters.Add("@FoundedDate", companyDto.FoundedDate);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(spUpdateCompany, parameters, commandType: CommandType.StoredProcedure);
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
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DbId", id);

                    return 1 == await sqlcon.ExecuteAsync(spDeleteCompany, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQLERROR);
            }
        }

    }
}
