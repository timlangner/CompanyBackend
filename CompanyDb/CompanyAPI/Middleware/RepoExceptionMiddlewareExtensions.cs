using Microsoft.AspNetCore.Builder;

namespace CompanyAPI.Middleware
{
    public static class RepoExceptionMiddlewareExtensions
    {
        public static void UseRepoExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RepoExceptionMiddleware>();
        }
    }
}
