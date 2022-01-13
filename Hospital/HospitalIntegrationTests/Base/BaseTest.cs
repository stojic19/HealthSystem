using Hospital.SharedModel.Repository.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Hospital.SharedModel.Model;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using Xunit;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository;
using System;
using Hospital.SharedModel.Model.Enumerations;
using System.Linq;

namespace HospitalIntegrationTests.Base
{
    [Collection("IntegrationTests")]
    public abstract class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        public IUnitOfWork UoW => _fixture.UoW;
        public HttpClient ManagerClient => _fixture.ManagerClient;
        public HttpClient PatientClient => _fixture.PatientClient;
        public CookieContainer CookieContainer => _fixture.CookieContainer;
        public string BaseUrl => "https://localhost:44303/";
        public string IntegrationBaseUrl => "https://localhost:44302/";
        public string ManagerToken { get; set; }
        public string PatientToken { get; set; }

        public void AddCookie(string name, string value, string domain)
        {
            CookieContainer.Add(new Cookie(name, value) { Domain = domain });
        }

        public StringContent GetContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content, settings: new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            }), Encoding.UTF8, "application/json");
        }
        public void RegisterAndLogin(string role)
        {
            object user = null;
            if (role == "Patient")
            {
                user = UoW.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.UserName.Equals("testLogInPatient")).FirstOrDefault();
                if (user == null)
                {
                    user = new NewPatientDTO()
                    {
                        Email = "testLogInPatient@nekimejl.com",
                        Password = "111111aA",
                        FirstName = "testLogInPatient",
                        LastName = "testLogInPatient",
                        UserName = "testLogInPatient"

                    };

                    PatientClient.PostAsync(BaseUrl + "api/Registration/Register", GetContent(user))
                    .GetAwaiter()
                    .GetResult();
                }
                var loginDTO = new LoginDTO()
                {
                    Username = "testLogInPatient",
                    Password = "111111aA",
                };

                var response = PatientClient.PostAsync(BaseUrl + "api/Login/LogIn", GetContent(loginDTO))
                    .GetAwaiter()
                    .GetResult();

                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var json = JsonConvert.DeserializeObject<TempUserDTO>(result);
                PatientToken = json.Token;

                PatientClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", PatientToken);
            }
            else if (role == "Manager")
            {
                user = UoW.GetRepository<IManagerReadRepository>().GetAll().Where(x => x.UserName.Equals("testLogInManager")).FirstOrDefault();
                if (user == null)
                {
                    user = new NewManagerDTO()
                    {
                        FirstName = "TestManager",
                        LastName = "TestManagerLastName",
                        MiddleName = "TestMnagerMiddleName",
                        DateOfBirth = new DateTime(),
                        Gender = Gender.Female,
                        Street = "TestManagerStreet",
                        UserName = "testLogInManager",
                        CityId = 1,
                        Email = "testManager@gmail.com",
                        Password = "111111aA"

                    };
                    var r = ManagerClient.PostAsync(BaseUrl + "api/Registration/RegisterManager", GetContent(user))
                    .GetAwaiter()
                    .GetResult();
                }
                var loginDTO = new LoginDTO()
                {
                    Username = "testLogInManager",
                    Password = "111111aA"
                };

                var response = ManagerClient.PostAsync(BaseUrl + "api/Login", GetContent(loginDTO))
                    .GetAwaiter()
                    .GetResult();

                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var json = JsonConvert.DeserializeObject<TempUserDTO>(result);
                ManagerToken = json.Token;

                ManagerClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", ManagerToken);
            }
        }
    }

    public class TempUserDTO
    {
        public SignInResult Result { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
