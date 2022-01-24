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
using Microsoft.EntityFrameworkCore;

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
            RegisterAndLogin("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll().Include(p => p.MedicalRecord).FirstOrDefault(p => p.UserName == "testPatientUsername");

            var response = await PatientClient.GetAsync(BaseUrl + "api/MedicalRecord/GetPatientWithRecord/" + patient.UserName);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var foundPatient = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().FirstOrDefault(x => x.UserName.ToUpper().Equals(patient.UserName.ToUpper()));
            var foundMedicalRecord =
                UoW.GetRepository<IMedicalRecordReadRepository>().GetById(foundPatient.MedicalRecordId);
            foundPatient.ShouldNotBeNull();
            foundPatient.UserName.ShouldBe("testPatientUsername");
            foundPatient.MedicalRecord.ShouldNotBeNull();
            foundMedicalRecord.Id.ShouldBe(foundPatient.MedicalRecordId);

        }

    }
}
