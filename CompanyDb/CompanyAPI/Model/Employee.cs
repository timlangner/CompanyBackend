using System;

namespace CompanyAPI.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public int DepartmentId { get; set; }
        public int AdressId { get; set; }
    }
}
