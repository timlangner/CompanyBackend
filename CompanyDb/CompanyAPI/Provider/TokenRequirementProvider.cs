using Chayns.Auth.ApiExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Provider
{
    public class TokenRequirementProvider : TokenRequirementProviderBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenRequirementProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Returns the locationId from current url path
        /// </summary>
        /// <returns></returns>
        public override Task<int?> GetRequiredLocationId()
        {
            // Get parts of current relative path
            var pathParts = _contextAccessor.HttpContext.Request.Path.Value.Trim('/').Split('/');

            // Schema of api is /api/{locationId}/[controller]
            var locationIdString = pathParts[1];

            if (int.TryParse(locationIdString, out var locationId))
                return Task.FromResult((int?)locationId);

            return Task.FromResult(default(int?));
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        public override Task<string> GetRequiredSiteId() => Task.FromResult(default(string));
    }
}
