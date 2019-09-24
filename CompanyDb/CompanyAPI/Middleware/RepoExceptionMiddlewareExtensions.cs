using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Middleware
{
    public class RepoExceptionMiddlewareExtensions
    {
        public void UseRepoExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RepoExceptionMiddleware>();
        }
    }
}
