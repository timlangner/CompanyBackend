using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CompanyApp.Controller
{
    class EmployeeController
    {
        string dbConStr = "";
        string companyReadCmd = "SELECT Id, FirstName, LastName, Birthdate, DepartmentId, AdressId FROM Employee";

        public EmployeeController(string DbConStr)
        {
            dbConStr = DbConStr;
        }

        public List<Model.Employee> Read()
        {
            List<Model.Employee> retval = new List<Model.Employee>();
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                using (SqlCommand cmd = new SqlCommand(companyReadCmd, sqlcon))
                {
                    sqlcon.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Model.Employee employee = new Model.Employee();
                            employee.Id = (int)reader["Id"];
                            employee.FirstName = reader["FirstName"].ToString();
                            employee.LastName = reader["LastName"].ToString();
                            employee.Birthdate = Convert.ToDateTime(reader["Birthdate"]);
                            employee.DepartmentId = (int)reader["DepartmentId"];
                            employee.AdressId = (int)reader["AdressId"];

                            retval.Add(employee);
                        }
                    }
                }
            }
            return retval;
        }

        public bool Delete(string id = "0")
        {
            bool retval = false;
            var storedProcedure = "spDeleteEmployee";

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedure, sqlcon))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sqlcon.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = cmd.ExecuteNonQuery();
                    retval = (result == 1);

                }

                return retval;
            }
        }

        public bool Update(int id, string firstName, string lastName, DateTime birthdate, int departmentId)
        {
            bool retval = false;
            var storedProcedure = "spCreateOrUpdateEmployee";

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedure, sqlcon))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Birthdate", birthdate);
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                    sqlcon.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = cmd.ExecuteNonQuery();
                    retval = (result == 1);

                }

                return retval;
            }
        }
    }
}
