using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using HospitalIntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class MedicalRecordTests : BaseTest
    {
        public MedicalRecordTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Get_patient_with_medical_record_should_return_200()
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
            var medRec = new MedicalRecord()
            {
                DoctorId = doctor.Id
            };

            var patient = new Patient()
            {
                UserName = "testUserName",
                CityId = 1,
                MedicalRecord = medRec
            };
            UoW.GetRepository<IPatientWriteRepository>().Add(patient);
            
            var response = await Client.GetAsync(BaseUrl + "api/MedicalRecord/GetPatientWithRecord");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var foundPatient = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().FirstOrDefault(x => x.UserName.ToUpper().Equals(patient.UserName.ToUpper()));
            var foundMedicalRecord =
                UoW.GetRepository<IMedicalRecordReadRepository>().GetById(foundPatient.MedicalRecordId);
            foundPatient.ShouldNotBeNull();
            foundPatient.UserName.ShouldBe("testUserName");
            foundPatient.MedicalRecord.ShouldNotBeNull();
            foundMedicalRecord.Id.ShouldBe(foundPatient.MedicalRecordId);

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

