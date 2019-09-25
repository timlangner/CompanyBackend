using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CompanyAPI.Helper
{
    public class Auth
    {
        public static Model.ChaynsUser GetUser(HttpContext httpContext)
        {
            string authheader = httpContext.Request.Headers["Authorization"];
            Model.ChaynsUser retval = new Model.ChaynsUser();

            if (authheader != null)
            {
                var authheaderStr = httpContext.Request.Headers["Authorization"].ToString();
                var payload64Str = authheader.Split(" ")[1].Trim().Split(".")[1].Trim();

                while (payload64Str.Length % 4 != 0)
                {
                    payload64Str += "=";
                }

                var payloadstr = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(payload64Str));


                retval = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.ChaynsUser>(payloadstr);
            }
            return retval;
        }

        public static bool GetUACGroupFromSite(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"];

            if (token != null)
            {
                token.ToString();
            }

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

                            if (uacGroups != null)
                            {
                                var firstUACGroup = (int)uacGroups[0]["id"];
                                return firstUACGroup == 1;
                            }

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
