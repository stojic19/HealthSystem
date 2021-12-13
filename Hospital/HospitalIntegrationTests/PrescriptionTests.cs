using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.Controllers;
using HospitalApi.DTOs;
using HospitalApi.HttpRequestSenders;
using HospitalIntegrationTests.Base;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class PrescriptionTests : BaseTest
    {
        public PrescriptionTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void New_prescription_should_return_200()
        {
            Country country = new Country
            {
                Name = "PrescriptionTestCountry"
            };
            UoW.GetRepository<ICountryWriteRepository>().Add(country);
            City city = new City
            {
                Name = "PrescriptionTestCity",
                CountryId = country.Id
            };
            UoW.GetRepository<ICityWriteRepository>().Add(city);
            Specialization specialization = new Specialization
            {
                Name = "PrescriptionTestSpecialization"
            };
            UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);
            Room room = new Room
            {
                RoomType = RoomType.AppointmentRoom
            };
            UoW.GetRepository<IRoomWriteRepository>().Add(room);
            Doctor doctor = new Doctor
            {
                FirstName = "PrescriptionTestDoctor",
                CityId = city.Id,
                SpecializationId = specialization.Id,
                RoomId = room.Id
            };
            UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            MedicalRecord medRec = new MedicalRecord
            {
                DoctorId = doctor.Id
            };
            UoW.GetRepository<IMedicalRecordWriteRepository>().Add(medRec);
            Patient patient = new Patient
            {
                FirstName = "PrescriptionTestPatientFirstName",
                LastName = "PrescriptionTestLastName",
                CityId = city.Id,
                MedicalRecordId = medRec.Id
            };
            Medication medication = new Medication
            {
                Name = "PrescriptionTestMedication"
            };
            UoW.GetRepository<IPatientWriteRepository>().Add(patient);
            UoW.GetRepository<IMedicationWriteRepository>().Add(medication); 
            var content = new NewPrescriptionDTO
            {
                PatientId = patient.Id,
                MedicineId = medication.Id,
                EndDate = new DateTime(2022, 11, 20),
                StartDate = new DateTime(2020, 1, 3),
            };
            var integrationContent = new PrescriptionToIntegrationDTO
            {
                PatientFirstName = patient.FirstName,
                PatientLastName = patient.LastName,
                StartDate = content.StartDate,
                EndDate = content.EndDate,
                IssuedDate = DateTime.Now,
                MedicineName = medication.Name,
            };
            var stubSender = new Mock<IHttpRequestSender>();
            RestResponse response = new RestResponse();
            response.StatusCode = HttpStatusCode.OK;
            stubSender.Setup(m => m.Post(IntegrationBaseUrl + "api/Prescription/PostPrescription",
                It.IsAny<PrescriptionToIntegrationDTO>())).Returns(response);
            //ACT
            PrescriptionController controller = new PrescriptionController(UoW, stubSender.Object);
            var result = controller.CreateNewPrescription(content).GetType();
            //ASSERT
            var presc = UoW.GetRepository<IPrescriptionReadRepository>().GetAll()
                .FirstOrDefault(x => x.StartDate == content.StartDate);
            UoW.GetRepository<IPrescriptionWriteRepository>().Delete(presc);
            UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            UoW.GetRepository<ISpecializationWriteRepository>().Delete(specialization);
            UoW.GetRepository<IMedicationWriteRepository>().Delete(medication);
            presc.ShouldNotBeNull();
            presc.MedicationId.ShouldBe(medication.Id);
            result.ShouldBe(typeof(OkObjectResult));
        }
    }
}
