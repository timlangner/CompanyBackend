using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CompanyAPI.Repository
{
    public class DepartmentRepository : IBaseInterface<Department, DepartmentDto>
    {
        private readonly IDbContext _dbContext;

        private const string SelectCmd = "SELECT Id, Name, Description, CompanyId FROM viDepartment";
        private const string SpCreateDepartment = "spCreateDepartment";
        private const string SpUpdateDepartment = "spUpdateDepartment";
        private const string SpDeleteDepartment = "spDeleteDepartment";

        public DepartmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Department>> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.Query<Department>(SelectCmd).AsList();
            }
        }

        public async Task<Department> Read(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return await sqlcon.QueryFirstOrDefaultAsync<Department>($"{SelectCmd} WHERE Id = @id", parameters);
            }
        }

        public async Task<bool> Create(DepartmentDto departmentDto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", departmentDto.Name);
            parameters.Add("@Description", departmentDto.Description);
            parameters.Add("@CompanyId", departmentDto.CompanyId);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(SpCreateDepartment, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Update(int id, DepartmentDto departmentDto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DbId", id);
            parameters.Add("@Name", departmentDto.Name);
            parameters.Add("@Description", departmentDto.Description);
            parameters.Add("@CompanyId", departmentDto.CompanyId);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(SpUpdateDepartment, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Delete(int id = 0)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DbId", id);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(SpDeleteDepartment, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
