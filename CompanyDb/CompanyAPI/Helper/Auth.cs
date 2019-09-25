using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Helper
{
    public class Auth
    {
        public static Model.Payload GetUser(HttpContext httpContext)
        {
            string authheader = httpContext.Request.Headers["Authorization"];
            Model.Payload retval = new Model.Payload();

            if (authheader != null)
            {
                var authheaderStr = httpContext.Request.Headers["Authorization"].ToString();
                var payload64str = authheaderStr.Substring("Bearer ".Length).Trim().Split(".")[1];

                byte[] temp = null;
                try
                {
                    temp = Convert.FromBase64String(payload64str.Replace('-', '+').Replace('_', '/'));
                }
                catch (Exception) { }
                try
                {
                    temp = Convert.FromBase64String(payload64str.Replace('-', '+').Replace('_', '/') + "=");
                }
                catch (Exception) { }
                try
                {
                    temp = Convert.FromBase64String(payload64str.Replace('-', '+').Replace('_', '/') + "==");
                }
                catch (Exception) { }
                var payloadstr = System.Text.Encoding.ASCII.GetString(temp);

                retval = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Payload>(payloadstr);
            }


            return retval;
        }
    }
}
