using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
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
            
            var scheduledEventId = PrepareDatabase(1);

            var response = await PatientClient.GetAsync(BaseUrl +
                                                         "api/Prescription/GetPrescriptionForScheduledEvent?scheduledEventId=" +
                                                         scheduledEventId + "&patientUsername=" + "testPatientUsername");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .Include(p => p.MedicalRecord)
                .ThenInclude(mr => mr.Prescriptions).FirstOrDefault(p => p.UserName == "testPatientUsername");

            patient.MedicalRecord.Prescriptions.Count().ShouldBe(1);
            patient.MedicalRecord.Prescriptions.First().EndDate.ShouldBe(new DateTime(2022, 01, 31));
            DatabaseCleanUp();
            DeleteDataFromDataBase();
        }

        [Fact]
        public async Task Should_return_no_content()
        {
            RegisterAndLogin("Patient");

            var scheduledEventId = PrepareDatabase(2);

            var response = await PatientClient.GetAsync(BaseUrl +
                                                        "api/Prescription/GetPrescriptionForScheduledEvent?scheduledEventId=" +
                                                        scheduledEventId + "&patientUsername=" + "testPatientUsername");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .Include(p => p.MedicalRecord)
                .ThenInclude(mr => mr.Prescriptions).FirstOrDefault(p => p.UserName == "testPatientUsername");

            patient.MedicalRecord.Prescriptions.Count().ShouldBe(0);
            DatabaseCleanUp();
            DeleteDataFromDataBase();
        }

        private int PrepareDatabase(int testNumber)
        {
            var patient = UoW.GetRepository<IPatientReadRepository>()
                .GetAll()
                .Include(p => p.MedicalRecord)
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

            var doctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll().Include(d => d.Room).FirstOrDefault(d => d.UserName == "testDoctorUsername");

            var scheduledEvent = new ScheduledEvent(0, false, true, new DateTime(2022, 01, 24, 13, 00, 00), new DateTime(2022, 01, 24, 13, 30, 00), 
                new DateTime(),  patient.Id, doctor.Id, doctor);

            var secondScheduledEvent = new ScheduledEvent(0, false, true, new DateTime(2022, 01, 25, 13, 00, 00), new DateTime(2022, 01, 25, 13, 30, 00),
                new DateTime(), patient.Id, doctor.Id, doctor);

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
            UoW.GetRepository<IScheduledEventWriteRepository>().Add(secondScheduledEvent);

            if (testNumber == 1)
            {
                var prescription = new Prescription()
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
                };

                patient.AddPrescription(prescription);
                UoW.GetRepository<IPatientWriteRepository>().Update(patient);
            }
            return testNumber == 1 ? scheduledEvent.Id : secondScheduledEvent.Id;
        }

        private void DatabaseCleanUp()
        {
            
            var se1 = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                .FirstOrDefault((se => DateTime.Compare(se.EndDate, new DateTime(2022, 01, 24, 13, 30, 00)) == 0));
            if (se1 != null ) UoW.GetRepository<IScheduledEventWriteRepository>().Delete(se1);

            var se2 = UoW.GetRepository<IScheduledEventReadRepository>().GetAll().FirstOrDefault((se => DateTime.Compare(se.EndDate, new DateTime(2022, 01, 25, 13, 30, 00)) == 0));
            if (se2 != null) UoW.GetRepository<IScheduledEventWriteRepository>().Delete(se2);

            var pr = UoW.GetRepository<IPrescriptionReadRepository>().GetAll().Include(p => p.Patient)
                .FirstOrDefault(pr => pr.Patient.UserName == "testPatientUsername");
            if (pr != null) UoW.GetRepository<IPrescriptionWriteRepository>().Delete(pr);
        }


    }
}
