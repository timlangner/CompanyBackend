using System;

namespace CompanyAPI.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? FoundedDate { get; set; }
    }
}
