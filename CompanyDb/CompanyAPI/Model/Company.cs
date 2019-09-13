using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? FoundedDate { get; set; }
    }
}
