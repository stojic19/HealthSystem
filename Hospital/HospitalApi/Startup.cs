using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hospital.Database.Infrastructure;
using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Repository.Implementation;
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
using System.Text.Json.Serialization;
using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using HospitalApi.HttpRequestSenders;
using HospitalApi.HttpRequestSenders.Implementation;
using Microsoft.AspNetCore.Identity;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.Schedule.Service;
using Hospital.Schedule.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HospitalApi", Version = "v1" });
            });

            services.AddDbContextPool<AppDbContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("HOSPITAL_DB_PATH");
                options.UseNpgsql(connectionString);
                using (var context = new AppDbContext((DbContextOptions<AppDbContext>)options.Options))
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }
            });

            services.AddIdentity<User, IdentityRole<int>>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IPatientSurveyService, PatientSurveyService>();
            services.AddScoped<IScheduledEventService, ScheduledEventService>();
            services.AddScoped<ISurveyService, SurveyService>();
<<<<<<< HEAD
           
=======

            var builder = new ContainerBuilder();
>>>>>>> 1dc5757 (feat: started major refactoring for docker compose # Please enter the commit message for your changes. Lines starting # with '#' will be ignored, and an empty message aborts the commit. # # interactive rebase in progress; onto 7cda7f8 # Last commands done (12 commands done): # pick 0cec2d4 feat: fixed minor bugs, checkpoint # pick c5ec46e feat: started major refactoring for docker compose # No commands remaining. # You are currently rebasing branch 'feature/mili-docker-compose' on '7cda7f8'. # # Changes to be committed: # modified: Hospital/HospitalApi/Startup.cs # modified: Integration/Integration.sln # modified: Integration/IntegrationApi/Startup.cs # modified: Integration/IntegrationIntegrationTests/IntegrationIntegrationTests.csproj # modified: Pharmacy/Pharmacy.sln # renamed: Pharmacy/PharamcyApi/Adapters/PDF/IPDFAdapter.cs -> Pharmacy/PharmacyApi/Adapters/PDF/IPDFAdapter.cs # renamed: Pharmacy/PharamcyApi/Adapters/PDF/Implementation/DynamicPDFAdapter.cs -> Pharmacy/PharmacyApi/Adapters/PDF/Implementation/DynamicPDFAdapter.cs # renamed: Pharmacy/PharamcyApi/Attributes/GuidValidationAttribute.cs -> Pharmacy/PharmacyApi/Attributes/GuidValidationAttribute.cs # renamed: Pharmacy/PharamcyApi/ConfigurationMappers/PharmacyDetails.cs -> Pharmacy/PharmacyApi/ConfigurationMappers/PharmacyDetails.cs # renamed: Pharmacy/PharamcyApi/Controllers/Base/BasePharmacyController.cs -> Pharmacy/PharmacyApi/Controllers/Base/BasePharmacyController.cs # renamed: Pharmacy/PharamcyApi/Controllers/BenefitController.cs -> Pharmacy/PharmacyApi/Controllers/BenefitController.cs # renamed: Pharmacy/PharamcyApi/Controllers/ComplaintController.cs -> Pharmacy/PharmacyApi/Controllers/ComplaintController.cs # renamed: Pharmacy/PharamcyApi/Controllers/MedicineController.cs -> Pharmacy/PharmacyApi/Controllers/MedicineController.cs # renamed: Pharmacy/PharamcyApi/Controllers/MedicineProcurementController.cs -> Pharmacy/PharmacyApi/Controllers/MedicineProcurementController.cs # renamed: Pharmacy/PharamcyApi/Controllers/MedicineSpecificationController.cs -> Pharmacy/PharmacyApi/Controllers/MedicineSpecificationController.cs # renamed: Pharmacy/PharamcyApi/Controllers/PrescriptionController.cs -> Pharmacy/PharmacyApi/Controllers/PrescriptionController.cs # renamed: Pharmacy/PharamcyApi/Controllers/RegistrationController.cs -> Pharmacy/PharmacyApi/Controllers/RegistrationController.cs # renamed: Pharmacy/PharamcyApi/Controllers/ReportController.cs -> Pharmacy/PharmacyApi/Controllers/ReportController.cs # renamed: Pharmacy/PharamcyApi/Controllers/TenderingController.cs -> Pharmacy/PharmacyApi/Controllers/TenderingController.cs # renamed: Pharmacy/PharamcyApi/DTO/ApplyTenderOfferDTO.cs -> Pharmacy/PharmacyApi/DTO/ApplyTenderOfferDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/Base/BaseCommunicationDTO.cs -> Pharmacy/PharmacyApi/DTO/Base/BaseCommunicationDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/BenefitIdDTO.cs -> Pharmacy/PharmacyApi/DTO/BenefitIdDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/BenefitMakeDTO.cs -> Pharmacy/PharmacyApi/DTO/BenefitMakeDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/BenefitSendDTO.cs -> Pharmacy/PharmacyApi/DTO/BenefitSendDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/CheckMedicineAvailabilityRequestDTO.cs -> Pharmacy/PharmacyApi/DTO/CheckMedicineAvailabilityRequestDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/CheckMedicineAvailabilityResponseDTO.cs -> Pharmacy/PharmacyApi/DTO/CheckMedicineAvailabilityResponseDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/ComplaintDTO.cs -> Pharmacy/PharmacyApi/DTO/ComplaintDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/ComplaintResponseDTO.cs -> Pharmacy/PharmacyApi/DTO/ComplaintResponseDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/ComplaintsAndResponsesDTO.cs -> Pharmacy/PharmacyApi/DTO/ComplaintsAndResponsesDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/ConsumptionReportDTO.cs -> Pharmacy/PharmacyApi/DTO/ConsumptionReportDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/CreateComplaintResponseDTO.cs -> Pharmacy/PharmacyApi/DTO/CreateComplaintResponseDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/CreateMedicineDTO.cs -> Pharmacy/PharmacyApi/DTO/CreateMedicineDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/HospitalRegisteredDTO.cs -> Pharmacy/PharmacyApi/DTO/HospitalRegisteredDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/MedicineDTO.cs -> Pharmacy/PharmacyApi/DTO/MedicineDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/MedicineProcurementRequestDTO.cs -> Pharmacy/PharmacyApi/DTO/MedicineProcurementRequestDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/MedicineSpecificationFileDTO.cs -> Pharmacy/PharmacyApi/DTO/MedicineSpecificationFileDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/PrescriptionHttpDto.cs -> Pharmacy/PharmacyApi/DTO/PrescriptionHttpDto.cs # renamed: Pharmacy/PharamcyApi/DTO/PrescriptionSftpDto.cs -> Pharmacy/PharmacyApi/DTO/PrescriptionSftpDto.cs # renamed: Pharmacy/PharamcyApi/DTO/RegisterHospitalDTO.cs -> Pharmacy/PharmacyApi/DTO/RegisterHospitalDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/SftpCredentialsDTO.cs -> Pharmacy/PharmacyApi/DTO/SftpCredentialsDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/SpecificationDTO.cs -> Pharmacy/PharmacyApi/DTO/SpecificationDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/TenderOfferDTO.cs -> Pharmacy/PharmacyApi/DTO/TenderOfferDTO.cs # renamed: Pharmacy/PharamcyApi/DTO/UpdateMedicineDTO.cs -> Pharmacy/PharmacyApi/DTO/UpdateMedicineDTO.cs # renamed: Pharmacy/PharamcyApi/GrpcServices/MedicineInventoryServiceImpl.cs -> Pharmacy/PharmacyApi/GrpcServices/MedicineInventoryServiceImpl.cs # renamed: Pharmacy/PharamcyApi/HttpRequestSenders/IHttpRequestSender.cs -> Pharmacy/PharmacyApi/HttpRequestSenders/IHttpRequestSender.cs # renamed: Pharmacy/PharamcyApi/HttpRequestSenders/Implementation/HttpRequestSender.cs -> Pharmacy/PharmacyApi/HttpRequestSenders/Implementation/HttpRequestSender.cs # renamed: Pharmacy/PharamcyApi/MedicineReports/Report-637744918941556526.pdf -> Pharmacy/PharmacyApi/MedicineReports/Report-637744918941556526.pdf # new file: Pharmacy/PharmacyApi/MedicineSpecifications/Aspirin637741373083849312.pdf # new file: Pharmacy/PharmacyApi/MedicineSpecifications/AspirinSpecification637734574962430489.txt # renamed: Pharmacy/PharamcyApi/PharmacyApi.csproj -> Pharmacy/PharmacyApi/PharmacyApi.csproj # renamed: Pharmacy/PharamcyApi/Program.cs -> Pharmacy/PharmacyApi/Program.cs # renamed: Pharmacy/PharamcyApi/Properties/launchSettings.json -> Pharmacy/PharmacyApi/Properties/launchSettings.json # renamed: Pharmacy/PharamcyApi/Protos/medicineInventoryModel.proto -> Pharmacy/PharmacyApi/Protos/medicineInventoryModel.proto # renamed: Pharmacy/PharamcyApi/Protos/medicineInventoryService.proto -> Pharmacy/PharmacyApi/Protos/medicineInventoryService.proto # renamed: Pharmacy/PharamcyApi/Responses.cs -> Pharmacy/PharmacyApi/Responses.cs # renamed: Pharmacy/PharamcyApi/Startup.cs -> Pharmacy/PharmacyApi/Startup.cs # renamed: Pharmacy/PharamcyApi/appsettings.Development.json -> Pharmacy/PharmacyApi/appsettings.Development.json # renamed: Pharmacy/PharamcyApi/appsettings.json -> Pharmacy/PharmacyApi/appsettings.json # modified: Pharmacy/PharmacyIntegrationTests/PharmacyIntegrationTests.csproj # modified: Stacks/Build/Gateway/Files/Config/api_gateway.conf # modified: Stacks/Build/Hospital/Dockerfile # modified: Stacks/Build/Integration/Dockerfile # modified: Stacks/Build/Pharmacy/Dockerfile # modified: Stacks/docker-compose.yml # modified: WebClients/ManagerWebClient/package.json # modified: WebClients/ManagerWebClient/proxy.conf.json # modified: WebClients/ManagerWebClient/server.js # modified: WebClients/ManagerWebClient/src/app/app-routing.module.ts # modified: WebClients/ManagerWebClient/src/app/benefits/benefit-list/benefit-list.component.html # modified: WebClients/ManagerWebClient/src/app/benefits/benefit-list/benefit-list.component.ts # modified: WebClients/ManagerWebClient/src/app/complaints/complaints-list/complaints-list.component.html # modified: WebClients/ManagerWebClient/src/app/complaints/complaints-list/complaints-list.component.ts # modified: WebClients/ManagerWebClient/src/app/components/navbar/navbar.component.html # modified: WebClients/ManagerWebClient/src/app/components/navbar/navbar.component.ts # modified: WebClients/ManagerWebClient/src/app/hospital-equipment/hospital-equipment.component.ts # modified: WebClients/ManagerWebClient/src/app/hospital-overview/hospital-overview.component.ts # modified: WebClients/ManagerWebClient/src/app/medicine-specification-requests/medicine-specification-list/medicine-specification-list.component.html # modified: WebClients/ManagerWebClient/src/app/medicine-specification-requests/medicine-specification-list/medicine-specification-list.component.ts # modified: WebClients/ManagerWebClient/src/app/pharmacies/pharmacies-list.component.html # modified: WebClients/ManagerWebClient/src/app/pharmacies/pharmacies-list.component.ts # modified: WebClients/ManagerWebClient/src/app/room-info/display-room-info/display-room-info.component.ts # modified: WebClients/ManagerWebClient/src/app/services/benefits.service.ts # modified: WebClients/ManagerWebClient/src/app/services/complaints.service.ts # modified: WebClients/ManagerWebClient/src/app/services/medication-report.service.ts # modified: WebClients/ManagerWebClient/src/app/services/medicine-specification-requests.service.ts # modified: WebClients/ManagerWebClient/src/app/services/pharmacy.service.ts # modified: WebClients/ManagerWebClient/src/app/services/room-inventories.service.ts # modified: WebClients/ManagerWebClient/src/app/services/room-position.service.ts # modified: WebClients/ManagerWebClient/src/app/services/room-schedule.service.ts # modified: WebClients/ManagerWebClient/src/app/services/rooms.service.ts # modified: WebClients/ManagerWebClient/src/environments/environment.prod.ts # modified: WebClients/ManagerWebClient/src/environments/environment.ts # modified: WebClients/PatientWebClient/proxy.conf.json # modified: WebClients/PatientWebClient/src/environments/environment.prod.ts #)

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
