using System;
using System.Linq;
using Hospital.SharedModel.Repository.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using Xunit;

namespace SeleniumTests.Base
{
    public abstract class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        public IUnitOfWork UoW => _fixture.UoW;
        public HttpClient Client => _fixture.Client;
        public CookieContainer CookieContainer => _fixture.CookieContainer;
        public string BaseUrl => "https://localhost:44303/";

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

        public void RegisterUser(string role)
        {
            object user = null;
            if (role == "Patient")
            {
                user = UoW.GetRepository<IPatientReadRepository>()
                    .GetAll().FirstOrDefault(x => x.UserName.Equals("testPatientUsername"));

                if (user == null)
                {
                    var testCity = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
                    var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().FirstOrDefault(x => x.FirstName == "TestDoctor");

                    var newPatient = new NewPatientDTO()
                    {
                        FirstName = "TestPatient",
                        MiddleName = "TestPatientMiddleName",
                        LastName = "TestPatientLastName",

                        DateOfBirth = DateTime.Now,
                        Gender = Gender.Female,
                        Street = "TesPatientStreet",
                        StreetNumber = "44",
                        CityId = testCity.Id,
                        Email = "testPatient@gmail.com",
                        UserName = "testPatientUsername",
                        Password = "TestProba123",

                        PhoneNumber = "testPatientPhoneNumber",
                        MedicalRecord = new NewMedicalRecordDTO()
                        {
                            DoctorId = testDoctor.Id,
                            Height = 0,
                            Weight = 0,
                            BloodType = BloodType.Undefined,
                            JobStatus = JobStatus.Undefined

                        },


                    };
                    Client.PostAsync(BaseUrl + "api/Registration/Register", GetContent(newPatient))
                     .GetAwaiter()
                     .GetResult();
                    var createdPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.UserName.Equals("testPatientUsername"));
                    createdPatient.EmailConfirmed = true;
                    UoW.GetRepository<IPatientWriteRepository>().Update(createdPatient);
                }
            }

            else if (role == "Manager")
            {
                user = UoW.GetRepository<IManagerReadRepository>().GetAll().FirstOrDefault(x => x.UserName.Equals("testLogInManager"));
                if (user != null) return;
                {
                    

                    var city = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
                    if (city == null)
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

                        city = new City()
                        {
                            Name = "TestCity",
                            PostalCode = 00000,
                            Country = country

                        };
                        UoW.GetRepository<ICityWriteRepository>().Add(city);
                    }

                    user = new NewManagerDTO()
                    {
                        FirstName = "TestManager",
                        LastName = "TestManagerLastName",
                        MiddleName = "TestManagerMiddleName",
                        DateOfBirth = new DateTime(),
                        Gender = Gender.Female,
                        Street = "TestManagerStreet",
                        UserName = "testLogInManager",
                        CityId = city.Id,
                        Email = "testManager@gmail.com",
                        Password = "111111aA"

                    };
                    var r = Client.PostAsync(BaseUrl + "api/Registration/RegisterManager", GetContent(user))
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }
    }
}
