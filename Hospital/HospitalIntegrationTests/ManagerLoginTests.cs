using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class ManagerLoginTests : BaseTest
    {
        public ManagerLoginTests(BaseFixture fixture) : base(fixture)
        {
           
        }
        [Fact]
        public async Task Manager_should_be_loged_in()
        {
            ArrangeDatabase();
            var managerRepo = UoW.GetRepository<IManagerReadRepository>();
            var manager = managerRepo.GetAll().FirstOrDefault(x => x.UserName == "testManagerUsername");
            var userDTO = new LoginDTO
            {
                Username = "testManagerUsername",
                Password = "111111aA" 
            };
            var content = GetContent(userDTO);
            var response = await Client.PostAsync(BaseUrl + "api/Login", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            //var responsePatients = JsonConvert.DeserializeObject(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            ClearDatabase();

        }

        private void ArrangeDatabase()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestCountry");
            if (country == null)
            {
                country = new Country()
                {
                    Name = "TestCountry",
                };
                UoW.GetRepository<ICountryWriteRepository>().Add(country);
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
            if (city == null)
            {
                city = new City()
                {
                    Name = "TestCity",
                    PostalCode = 00000,
                    Country = country

                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);
            }
            var manager = UoW.GetRepository<IManagerReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testManagerUsername");

            if (manager == null)
            {
                manager = new Manager()
                {
                    FirstName = "TestManager",
                    MiddleName = "TestManagerMiddleName",
                    LastName = "TestManagerLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TestManagerStreet",
                    UserName = "testManagerUsername",
                    Email = "testManager@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    City = city,
                    PasswordHash = "AQAAAAEAACcQAAAAECWA5y4mWKpWyKKafSP1nhpl3MfewvTk9GoRvk8yGqhqvscwOcBNUcIX4cjswLLVTA=="

                };
                UoW.GetRepository<IManagerWriteRepository>().Add(manager);
            }

        }

        private void ClearDatabase()
        {
            var manager = UoW.GetRepository<IManagerReadRepository>().GetAll()
               .FirstOrDefault(x => x.UserName == "testManagerUsername");
            if (manager == null) return;
            UoW.GetRepository<IManagerWriteRepository>().Delete(manager);
            var city = UoW.GetRepository<ICityReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCity");

            if (city != null)
            {
                UoW.GetRepository<ICityWriteRepository>().Delete(city);
            }

            var country = UoW.GetRepository<ICountryReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCountry");

            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }
        }
    }
}
