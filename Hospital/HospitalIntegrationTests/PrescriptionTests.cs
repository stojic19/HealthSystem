using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository;
using HospitalApi.Controllers;
using HospitalApi.DTOs;
using HospitalApi.HttpRequestSenders;
using HospitalIntegrationTests.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
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
            RegisterAndLogin("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetByUsername("testPatientUsername");

            var medication = new Medication
            {
                Name = "PrescriptionTestMedication"
            };
            
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
            var response = new RestResponse();
            response.StatusCode = HttpStatusCode.OK;
            stubSender.Setup(m => m.Post(IntegrationBaseUrl + "api/Prescription/PostPrescription",
                It.IsAny<PrescriptionToIntegrationDTO>())).Returns(response);

            var controller = new PrescriptionController(UoW, null, stubSender.Object);
            var result = controller.CreateNewPrescription(content).GetType();

            var presc = UoW.GetRepository<IPrescriptionReadRepository>().GetAll()
                .FirstOrDefault(x => x.StartDate == content.StartDate);
            UoW.GetRepository<IPrescriptionWriteRepository>().Delete(presc);
            UoW.GetRepository<IMedicationWriteRepository>().Delete(medication);
            presc.ShouldNotBeNull();
            presc.MedicationId.ShouldBe(medication.Id);
            result.ShouldBe(typeof(OkObjectResult));
        }

        [Fact]
        public async Task Should_return_prescription_for_a_scheduled_event()
        {
            RegisterAndLogin("Patient");
            
            var scheduledEventId = PrepareDatabase();

            var response = await PatientClient.GetAsync(BaseUrl +
                                                         "api/Prescription/GetPrescriptionForScheduledEvent?scheduledEventId=" +
                                                         scheduledEventId + "&patientUsername=" + "testPatientUsername");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var prescriptions = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .Include(p => p.MedicalRecord)
                .ThenInclude(mr => mr.Prescriptions);

            prescriptions.Count().ShouldBe(1);

            //delete data from database
        }

        private int  PrepareDatabase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>()
                .GetAll()
                .Include(p => p.MedicalRecord)
                .ThenInclude(mr => mr.Prescriptions)
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

            var doctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll().FirstOrDefault(d => d.UserName == "testDoctorUsername");

            var scheduledEvent = new ScheduledEvent(0, false, true, new DateTime(2022, 01, 24, 13, 00, 00), new DateTime(2022, 01, 24, 13, 30, 00), new DateTime(), patient.Id, doctor.Id, doctor);

            var prescriptions =  patient.MedicalRecord.Prescriptions.ToList();
            prescriptions.Add(new Prescription()
            {
                Patient = patient,
                IssuedDate = DateTime.Now,
                StartDate = new DateTime(2022, 01, 27),
                EndDate = new DateTime(2022, 01, 31),
                Medication = new Medication()
                {
                    Name = "Paracetamol"
                }, 
                ScheduledEvent = scheduledEvent
            });
            return scheduledEvent.Id;
        }
            
    }
}
