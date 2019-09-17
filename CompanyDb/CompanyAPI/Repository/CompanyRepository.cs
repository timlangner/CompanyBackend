﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using Dapper;
using CompanyAPI.Model.Dto;

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

        public List<Company> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.Query<Company>(selectCmd).AsList();
            }
        }

        public Company Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.QueryFirstOrDefault<Company>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }

        public bool Create(CompanyDto companyDto)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyName", companyDto.Name);
            parameters.Add("@FoundedDate", companyDto.FoundedDate);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return 1 == sqlcon.Execute(spCreateCompany, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public bool Update(int id, CompanyDto companyDto)
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
                    return 1 == sqlcon.Execute(spUpdateCompany, parameters, commandType: CommandType.StoredProcedure);
                }
            } catch(SqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }        
        }

        public bool Delete(int id = 0)
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DbId", id);

                return 1 == sqlcon.Execute(spDeleteCompany, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
