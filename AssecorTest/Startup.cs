using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assecor.Data;
using Assecor.Data.Core.IRepositories;
using Assecor.Data.Persistence.Repositories;
using Assecor.Services.CsvHelper;
using Assecor.Services.Implementations;
using Assecor.Services.Interfaces;
using Assecor.WebApi.Middleware;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AssecorTest
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                                .AddXmlSerializerFormatters();
            services.AddAutoMapper();

            services.AddDbContext<ApplicationDBContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Repositories Injection
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ICsvService, CsvService>();

            //Service Injection
            services.AddScoped<IPersonsService, PersonsService>();
            services.AddScoped<IColorService, ColorService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomExceptiontusMiddleware();
            }
            else
            {
                app.UseCustomExceptiontusMiddleware();
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();

        }

    }
}
