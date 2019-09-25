using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

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

        public static bool GetUACGroupFromSite(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"].ToString() ;

            const string url = "https://chaynssvc.tobit.com/v0.5/164986/user/";
            try
            {
                var webRequest = WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 20000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", token);
                    using (Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            JObject DataObj = JObject.Parse(jsonResponse);
                            var uacGroups = DataObj["data"]["uacGroups"];
                            var firstUACGroup = (int) uacGroups[0]["id"];
                            Console.WriteLine(firstUACGroup);

                            return firstUACGroup == 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }
    }
}
