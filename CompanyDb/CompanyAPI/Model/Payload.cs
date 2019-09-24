using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Model
{
    public class Payload
    {
        public string jti { get; set; }
        public string sub { get; set; }
        public int type { get; set; }
        public DateTime exp { get; set; }
        public DateTime iat { get; set; }
        public int LocationID { get; set; }
        public string SiteID { get; set; }
        public bool IsAdmin { get; set; }
        public int TobitUserID { get; set; }
        public string PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
