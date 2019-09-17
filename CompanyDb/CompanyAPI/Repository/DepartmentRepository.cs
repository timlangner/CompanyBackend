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
    public class DepartmentRepository : IBaseInterface<Department, DepartmentDto>
    {
        private readonly IDbContext _dbContext;

        string selectCmd = "SELECT Id, Name, Description, CompanyId FROM viDepartment";
        string spCreateDepartment = "spCreateDepartment";
        string spUpdateDepartment = "spUpdateDepartment";
        string spDeleteDepartment = "spDeleteDepartment";

        public DepartmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Department> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.Query<Department>(selectCmd).AsList();
            }
        }

        public Department Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.QueryFirstOrDefault<Department>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }

        public bool Create(DepartmentDto departmentDto)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", departmentDto.Name);
            parameters.Add("@Description", departmentDto.Description);
            parameters.Add("@CompanyId", departmentDto.CompanyId);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == sqlcon.Execute(spCreateDepartment, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"ERROR MESSAGE: ${ex}");
                return false;
            }
        }

        public bool Update(int id, DepartmentDto departmentDto)
        {
            Department retval = new Department();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DbId", id);
            parameters.Add("@Name", departmentDto.Name);
            parameters.Add("@Description", departmentDto.Description);
            parameters.Add("@CompanyId", departmentDto.CompanyId);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == sqlcon.Execute(spUpdateDepartment, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"ERROR MESSAGE: ${ex}");
                return false;
            }
        }

        public bool Delete(int id = 0)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DbId", id);

            try
            {
                using (var sqlcon = _dbContext.GetConnection())
                {
                    return 1 == sqlcon.Execute(spDeleteDepartment, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"ERROR MESSAGE: {ex}");
                return false;
            }
        }
    }
}
