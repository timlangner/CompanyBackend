using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CompanyApp.Interface;
using CompanyApp.Model;
using Dapper;

namespace CompanyApp.Repository
{
    public class CompanyRepository : IBaseInterface<Company>
    {
        string dbSConStr = "";
        string selectCmd = "SELECT Id, Name, FoundedDate FROM viCompany";
        string deleteCmd = "UPDATE company SET DeleteTime = GetDate() WHERE Id = @Id";
        string spCreate = "spCreateCompany";
        string spUpdate = "spUpdateCompany";

        public CompanyRepository(string dbConnectionStr)
        {
            dbSConStr = dbConnectionStr;
        }

        public List<Company> Read()
        {
            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                return sqlcon.Query<Company>(selectCmd).AsList();
            }
        }

        public Company Read(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                return sqlcon.QueryFirst<Company>($"{selectCmd} WHERE Id = @Id", parameters);
            }
        }

        public Company Create(Company company)
        {
            Company retval = new Company();

            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", company.Name);
            parameters.Add("@FoundedDate", company.FoundedDate);

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                int id = sqlcon.ExecuteScalar<int>(spCreate, parameters, commandType: CommandType.StoredProcedure);

                parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                retval = sqlcon.QueryFirst<Company>($"{selectCmd} WHERE Id = @Id", parameters);
            }
            return retval;
        }

        public Company Update(Company company)
        {
            Company retval = new Company();

            var parameters = new DynamicParameters();
            parameters.Add("@DbId", company.Id);
            parameters.Add("@CompanyName", company.Name);
            parameters.Add("@FoundedDate", company.FoundedDate);

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                sqlcon.Execute(spUpdate, parameters, commandType: CommandType.StoredProcedure);

                parameters = new DynamicParameters();
                parameters.Add("@Id", company.Id);

                retval = sqlcon.QueryFirst<Company>($"{selectCmd} WHERE Id = @Id", parameters);
            }
            return retval;
        }

        public bool Delete(int id = 0)
        {
            bool retval = false;

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = sqlcon.Execute(deleteCmd, parameters);
                retval = (result == 1);
            }

            return retval;
        }

    }
}
