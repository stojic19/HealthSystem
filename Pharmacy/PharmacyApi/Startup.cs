using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pharmacy.EfStructures;
using Pharmacy.Infrastructure;
using Pharmacy.Repositories.Base;
using Pharmacy.Repositories.DbImplementation;
using Pharmacy.Services;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.GrpcServices;
using PharmacyApi.HttpRequestSenders;
using PharmacyApi.HttpRequestSenders.Implementation;
using PharmacyApi.Protos;

namespace PharmacyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private Server server;

        private IUnitOfWork unitOfWork;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PharmacyApi", Version = "v1" });
            });
            services.AddHostedService<NewTenderRabbitMQService>();
            services.AddHostedService<CloseTenderRabbitMQService>();
            services.AddHostedService<WinningTenderOfferRabbitMQService>();

            PharmacyDetails details = new PharmacyDetails();

            var communicationMode = Configuration.GetValue<string>("PharmacyMode");
            if (communicationMode == "DEFAULT")
            {
                Configuration.GetSection("DefaultPharmacy").Bind(details);
            } 
            else if (communicationMode == "SMTP")
            {
                Configuration.GetSection("SMTPPharmacy").Bind(details);
            }

            services.AddSingleton<PharmacyDetails>(details);

            services.AddDbContextPool<AppDbContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("PHARMACY_DB_PATH");
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
            builder.RegisterModule(new RepositoryModule()
            {

                RepositoryAssemblies = new List<Assembly>()
                {
                    typeof (MedicineReadRepository).Assembly
                },
                Namespace = "Pharmacy.Repositories"


            }); 
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<HttpRequestSender>().As<IHttpRequestSender>();
            builder.Populate(services);
            var container = builder.Build();

            unitOfWork = container.Resolve<IUnitOfWork>();
            services.AddTransient<MedicineInventoryServiceImpl>(_ =>
            {
                return new MedicineInventoryServiceImpl(unitOfWork);
            });

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmacyApi v1"));
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
                Services = { MedicineInventoryService.BindService(new MedicineInventoryServiceImpl(unitOfWork))},
                Ports = { new ServerPort("127.0.0.1", 7000, ServerCredentials.Insecure) }
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
