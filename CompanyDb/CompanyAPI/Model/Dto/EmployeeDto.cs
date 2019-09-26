using System;

namespace CompanyAPI.Model.Dto
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public int DepartmentId { get; set; }
        public int AdressId { get; set; }
    }
}
