using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo2.Models;
using Microsoft.EntityFrameworkCore;
using WebApiDemo2.Repository;

namespace WebApiDemo2
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

            services.AddControllers();
            var connectionstring = Configuration.GetConnectionString("SqlCon");
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connectionstring));
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiDemo2", Version = "v1" });
            });
            services.AddCors(
                options => options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader().AllowAnyMethod())
                );
            services.AddCors(
                  options => options.AddPolicy(name: "AllowOrigin", builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader().AllowAnyMethod())
                  );
        }
   
    


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo2 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder => {

                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
