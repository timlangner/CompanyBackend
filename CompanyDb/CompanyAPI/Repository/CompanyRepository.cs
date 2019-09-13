using System;
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
        string dbConStr = "";
        string selectCmd = "select Id, Name, FoundedDate from viCompany";
        string spCreateCompany = "spCreateCompany";
        string spUpdateCompany = "spUpdateCompany";
        string spDeleteCompany = "spDeleteCompany";

        public CompanyRepository(string dbConnectionStr)
        {
            dbConStr = dbConnectionStr;
        }

        public List<Company> Read()
        {
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return sqlcon.Query<Company>(selectCmd).AsList();
            }
        }

        public Company Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return sqlcon.QueryFirstOrDefault<Company>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }

        public bool Create(CompanyDto companyDto)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyName", companyDto.Name);
            parameters.Add("@FoundedDate", companyDto.FoundedDate);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
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
                using (SqlConnection sqlcon = new SqlConnection(dbConStr))
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
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DbId", id);

                return 1 == sqlcon.Execute(spDeleteCompany, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
