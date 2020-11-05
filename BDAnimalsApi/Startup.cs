using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDAnimalsApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BDAnimalsApi.Models;
using BDAnimalsApi.Repository.IRepository;
using BDAnimalsApi.Repository;
using AutoMapper;
using BDAnimalsApi.Mappers;
using System.Reflection;

namespace BDAnimalsApi
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IScientificClassRepository, ScientificClassRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();

            services.AddAutoMapper(typeof(BDAnimalsMappings));
            services.AddSwaggerGen(options=> {
                options.SwaggerDoc("BDAnimalApiSpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "BDAnimal API",
                        Version = "1",
                        Contact= new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Name="Ridwanul Islam",
                            Email="ridwan.pust@gmail.com"
                        }
                        
                    });
            });
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options=> {
                options.SwaggerEndpoint("/swagger/BDAnimalApiSpec/swagger.json","BDAnimal API");
                options.RoutePrefix="";
            
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
