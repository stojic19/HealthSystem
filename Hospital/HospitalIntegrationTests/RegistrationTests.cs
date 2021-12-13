using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
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
            ClearUserWithUserName("testUserName");

            var doctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll()
                .FirstOrDefault();

            if (doctor == null)
            {
                doctor = new Doctor()
                {
                    UserName = "testDoctor"
                };
            }

            var medRec = new NewMedicalRecordDTO()
            {
                DoctorId = doctor.Id
            };

            var newPatient = new NewPatientDTO()
            {
                UserName = "testUserName",
                Password = "Test Passw0rd",
                Email = "testemail@gmail.com",
                CityId = 1,
                MedicalRecord = medRec
            };

            var content = GetContent(newPatient);

            var response = await Client.PostAsync(BaseUrl + "api/Registration/Register", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            //var responseContent = await response.Content.ReadAsStringAsync();

            var foundRegisteredUser = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().FirstOrDefault(x => x.UserName.ToUpper().Equals(newPatient.UserName.ToUpper()));

            // brisi medrec i korisnika


            foundRegisteredUser.ShouldNotBeNull();
            foundRegisteredUser.UserName.ShouldBe("testUserName");

        }

        private void ClearUserWithUserName(string testusername)
        {
            var user = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "testUserName");


            if (user != null)
            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.Patient.UserName == user.UserName);

                UoW.GetRepository<IPatientWriteRepository>().Delete(user);
            }

        }


        private Patient InsertUser(string testUserName)
        {
            var user = UoW.GetRepository<IPatientReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.UserName == testUserName);

            if (user == null)
            {
                var city = UoW.GetRepository<ICityReadRepository>()
                    .GetAll()
                    .FirstOrDefault();

                if (city == null)
                {
                    var country = UoW.GetRepository<ICountryReadRepository>()
                        .GetAll()
                        .FirstOrDefault();

                    if (country == null)
                    {
                        country = new Country()
                        {
                            Name = "Test country"
                        };
                        UoW.GetRepository<ICountryWriteRepository>().Add(country);
                    }

                    city = new City()
                    {
                        CountryId = country.Id,
                        Name = "Test city",
                        PostalCode = 10
                    };
                    UoW.GetRepository<ICityWriteRepository>().Add(city);
                }

                ;

                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.Id == user.MedicalRecordId);

                if (medicalRecord == null)
                {
                    var doctor = UoW.GetRepository<IDoctorReadRepository>()
                        .GetAll()
                        .FirstOrDefault();

                    if (doctor == null)
                    {
                        var specialization = UoW.GetRepository<ISpecializationReadRepository>()
                            .GetAll()
                            .FirstOrDefault(x => x.Name.ToUpper() == "GP");

                        if (specialization == null)
                        {
                            specialization = new Specialization()
                            {
                                Name = "GP"
                            };
                        }

                        doctor = new Doctor()
                        {
                            UserName = "testDoctor",
                            SpecializationId = specialization.Id
                        };
                    }

                    medicalRecord = new MedicalRecord()
                    {
                        DoctorId = doctor.Id
                    };
                }

                user = new Patient()
                {
                    UserName = "testUserName",
                    PasswordHash = "Test Passw0rd",
                    CityId = city.Id,
                    MedicalRecordId = medicalRecord.Id
                };

            }

            return new Patient();

        }
    }
}