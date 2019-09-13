using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CompanyApp.Controller
{
    class AdressController
    {
        string dbConStr = "";
        string companyReadCmd = "SELECT Id, FirstName, LastName, Birthdate, DepartmentId, AdressId FROM Employee";

        public AdressController(string DbConStr)
        {
            dbConStr = DbConStr;
        }
        public List<Model.Adress> Read()
        {
            List<Model.Adress> retval = new List<Model.Adress>();
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                using (SqlCommand cmd = new SqlCommand(companyReadCmd, sqlcon))
                {
                    sqlcon.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Model.Adress adress = new Model.Adress();
                            adress.Id = (int)reader["Id"];
                            adress.Street = reader["Street"].ToString();
                            adress.City = reader["City"].ToString();
                            adress.Country = reader["Country"].ToString();

                            retval.Add(adress);
                        }
                    }
                }
            }
            return retval;
        }
    }
}
