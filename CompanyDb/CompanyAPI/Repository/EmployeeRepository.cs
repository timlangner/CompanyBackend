using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Repository
{
    public class EmployeeRepository : IBaseInterface<Employee, EmployeeDto>
    {
        private readonly IDbContext _dbContext;

        string selectCmd = "SELECT Id, FirstName, LastName, Birthdate, DepartmentId, AdressId FROM viEmployee";
        string spCreateEmployee = "spCreateEmployee";
        string spUpdateEmployee = "spCreateOrUpdateEmployee";
        string spDeleteEmployee = "spDeleteEmployee";

        public EmployeeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return (await sqlcon.QueryAsync<Employee>(selectCmd)).AsList();
            }
        }

        public async Task<Employee> Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return await sqlcon.QueryFirstOrDefaultAsync<Employee>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }

        public async Task<bool> Create(EmployeeDto employeeDto)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FirstName", employeeDto.FirstName);
            parameters.Add("@LastName", employeeDto.LastName);
            parameters.Add("@Birthdate", employeeDto.Birthdate);
            parameters.Add("@DepartmentId", employeeDto.DepartmentId);
            parameters.Add("@AdressId", employeeDto.AdressId);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return 1 == await sqlcon.ExecuteAsync(spCreateEmployee, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Update(int id, EmployeeDto employeeDto)
        {
            Employee retval = new Employee();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", id);
            parameters.Add("@FirstName", employeeDto.FirstName);
            parameters.Add("@LastName", employeeDto.LastName);
            parameters.Add("@Birthdate", employeeDto.Birthdate);
            parameters.Add("@DepartmentId", employeeDto.DepartmentId);
            parameters.Add("@AdressId", employeeDto.AdressId);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(spUpdateEmployee, parameters, commandType: CommandType.StoredProcedure);
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
            if (id < 0)
            {
                throw new Helper.RepoException(Helper.RepoResultType.WRONGPARAMETER);
            }
            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);

                    return 1 == await sqlcon.ExecuteAsync(spDeleteEmployee, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQLERROR);
            }
        }
    }
}
