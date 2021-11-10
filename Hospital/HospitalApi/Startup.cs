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
using Autofac;
using Hospital.Infrastructure;
using System.Reflection;
using Hospital.Repositories.DbImplementation;
using Hospital.Repositories.Base;
using Autofac.Extensions.DependencyInjection;

namespace HospitalApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("MyCorsImplementationPolicy", builder => builder.WithOrigins("*")));

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HospitalApi", Version = "v1" });
            });
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DbModule());
            builder.RegisterModule(new RepositoryModule()
            {

                RepositoryAssemblies = new List<Assembly>()
                {
                    typeof (CityReadRepository).Assembly
                },
                Namespace = "Hospital.Repositories"


            }); 
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyCorsImplementationPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
