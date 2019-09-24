using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Helper;
using CompanyAPI.Interface;
using CompanyAPI.Middleware;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CompanyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IBaseInterface<Company, CompanyDto>, CompanyRepository>();
            services.AddScoped<IBaseInterface<Department, DepartmentDto>, DepartmentRepository>();
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));
            
            services.AddSingleton<IDbContext, DbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRepoExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
