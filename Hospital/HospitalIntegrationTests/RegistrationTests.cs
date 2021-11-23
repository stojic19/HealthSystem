using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Hospital.Model;
using Hospital.Repositories;
using HospitalApi.Controllers;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace HospitalIntegrationTests
{
    public class RegistrationTests : BaseTest
    {
        private ITestOutputHelper _itoh;
        public RegistrationTests(BaseFixture fixture, ITestOutputHelper itoh) : base(fixture)
        {
            _itoh = itoh;
        }

        [Fact]
        public async Task Register_should_return_200()
        {
            var medRec = new NewMedicalRecordDTO()
            {
                Id = 14
            };

            var newPatient = new UserRegistration()
            {
                UserName = "testName",
                Password = "Test Passw0rd",
                CityId = 1
            };

            newPatient.MedicalRecord = medRec;

            var content = GetContent(newPatient);
            
            var response = await Client.PostAsync(BaseUrl + "api/Registration", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            //var responseContent = await response.Content.ReadAsStringAsync();

            var foundRegisteredUser = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName.Equals(newPatient.UserName));

            foundRegisteredUser.ShouldNotBeNull();
            foundRegisteredUser.UserName.ShouldBe("testName");
        }
    }
}
