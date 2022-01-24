using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class ScheduleAppointmentTests : BaseTest
    {
        public ScheduleAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Should_return_doctors_with_specialization()
        {
            RegisterAndLogin("Patient");
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");
            var response = await PatientClient.GetAsync(BaseUrl + "api/Doctor/GetDoctorsWithSpecialization?specializationName=" + doctor.Specialization.Name);
            var responseContent = await response.Content.ReadAsStringAsync();
            var doctors = JsonConvert.DeserializeObject<IEnumerable<Doctor>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
            doctors.Count.ShouldNotBe(0);
        }

        [Fact]
        public async Task Should_return_available_appointments()
        {
            RegisterAndLogin("Patient");
            var preferredDate = new DateTime(2021, 12, 18);
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");

            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduleAppointment/GetAvailableAppointments?doctorId=" + doctor.Id +
                                                 "&preferredDate=" + preferredDate.ToString());
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments = JsonConvert.DeserializeObject<IEnumerable<DateTime>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.Count.ShouldBe(16);
        }

        [Fact]
        public async Task Schedule_appointment_should_return_200_OK()
        {
            RegisterAndLogin("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetByUsername("testPatientUsername");
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().First(d => d.UserName == "testDoctorUsername");
            var appointmentToSchedule = new ScheduleAppointmentDTO()
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                DoctorsRoomId = doctor.RoomId,
                PatientUsername = patient.UserName,
                StartDate = new DateTime(2021, 12, 24, 12, 0, 0)
            };

            var content = GetContent(appointmentToSchedule);
            var response = await PatientClient.PostAsync(BaseUrl + "api/ScheduleAppointment/ScheduleAppointment", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var scheduledEvent = JsonConvert.DeserializeObject<ScheduledEvent>(responseContent);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            scheduledEvent.ShouldNotBeNull();
        }


    }
}