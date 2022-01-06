using Autofac;
using Autofac.Extensions.DependencyInjection;
using Grpc.Core;
using Integration.Database.Infrastructure;
using Integration.Partnership.Service;
using Integration.Shared.Repository.Base;
using Integration.Shared.Repository.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Integration.Database.EfStructures;
using IntegrationAPI.HttpRequestSenders;
using IntegrationAPI.HttpRequestSenders.Implementation;
using Integration.Tendering.Service;
using Microsoft.EntityFrameworkCore;

namespace IntegrationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private Server server;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegrationApi", Version = "v1" });
            });
            services.AddHostedService<BenefitRabbitMqService>();
            services.AddHostedService<NewTenderOfferRabbitMQService>();

            services.AddDbContextPool<AppDbContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("INTEGRATION_DB_PATH");
                options.UseNpgsql(connectionString);
                using (var context = new AppDbContext((DbContextOptions<AppDbContext>)options.Options))
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }
            });

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DbModule());
            builder.RegisterModule(new RepositoryModule()
            {

                RepositoryAssemblies = new List<Assembly>()
                {
                    typeof (CityReadRepository).Assembly
                },
                Namespace = "Repository"


            });
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<HttpRequestSender>().As<IHttpRequestSender>();
            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntegrationApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            server = new Server
            {
                Services = { },
                Ports = { new ServerPort("127.0.0.1", 3000, ServerCredentials.Insecure) }
            };
            server.Start();

            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            if (server != null)
            {
                server.ShutdownAsync().Wait();
            }
        }
    }
}
