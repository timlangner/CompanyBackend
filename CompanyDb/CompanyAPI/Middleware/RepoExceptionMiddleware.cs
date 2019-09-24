using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CompanyAPI.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CompanyAPI.Middleware
{
    public class RepoExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RepoExceptionMiddleware> _logger;

        public RepoExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<RepoExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RepoException ex)
            {
                switch (ex.ExType)
                {
                    case RepoResultType.SQLERROR:
                        context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        break;
                    case RepoResultType.NOTFOUND:
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case RepoResultType.WRONGPARAMETER:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Request failed ver heavily", new { context });
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
