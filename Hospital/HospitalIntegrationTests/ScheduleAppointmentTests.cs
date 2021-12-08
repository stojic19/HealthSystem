using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
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
            var doctor = InsertDoctors();

            var response = await Client.GetAsync(BaseUrl + "api/Doctor/GetDoctorsWithSpecialization?specializationId=" + doctor.SpecializationId);
            var responseContent = await response.Content.ReadAsStringAsync();
            var doctors = JsonConvert.DeserializeObject<IEnumerable<Doctor>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
            doctors.Count.ShouldNotBe(0);
            doctors.First().SpecializationId.ShouldBe(doctor.Specialization.Id);
        }

        [Fact]
        public async Task Should_return_available_appointments()
        {
            // arrange
            var dateRange = new TimePeriod()
            {
                StartTime = new DateTime(2021, 12, 9, 08, 00, 00),
                EndTime = new DateTime(2021, 12, 10, 18, 00, 00)
            };
            var doctor = InsertDoctors();

            // act
            var response = await Client.GetAsync(BaseUrl + "api/ScheduledEvent/GetAvailableAppointments?specializationId=" + doctor.Specialization.Id +
                                                 "&startDate=" + dateRange.StartTime.ToString() + "&endDate=" + dateRange.EndTime.ToString());
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments = JsonConvert.DeserializeObject<IEnumerable<DateTime>>(responseContent).ToList();

            // assert 
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.Count.ShouldBe(21);
        }

        [Fact]
        public async Task Schedule_appointment_should_return_200_OK()
        {
            var appointmentToSchedule = new ScheduleAppointmentDTO()
            {
                DoctorId = InsertDoctors().Id,
                PatientId = 6,
                StartDate = new DateTime(2021, 12, 12, 12, 0, 0)
            };

            var content = GetContent(appointmentToSchedule);
            var response = await Client.PostAsync(BaseUrl + "api/ScheduleAppointment/ScheduleAppointment", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var scheduledEvent = JsonConvert.DeserializeObject<ScheduledEvent>(responseContent);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            scheduledEvent.ShouldNotBeNull();

            // TODO: change Doctor.Id to DoctorId after refactoring the model !
            scheduledEvent.Doctor.Id.ShouldBe(appointmentToSchedule.DoctorId);
        }


        private Doctor InsertDoctors()
        {
            var specialization = UoW.GetRepository<ISpecializationReadRepository>()
                .GetAll()
                .FirstOrDefault();

            if (specialization == null)
            {
                specialization = new Specialization()
                {
                    Name = "General Practice"
                };
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().Include(c => c.Country).FirstOrDefault();

            var doctor = UoW
                .GetRepository<IDoctorReadRepository>()
                .GetAll().Include(d => d.Specialization).FirstOrDefault(x => x.Specialization.Name.ToLower() == "general practice");

            if (doctor == null)
            {
                var doctor1 = new Doctor()
                {
                    SpecializationId = specialization.Id,
                    CityId = city.Id
                };
                var doctor2 = new Doctor()
                {
                    SpecializationId = specialization.Id,
                    CityId = city.Id
                };

                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor1);
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor2);

                doctor = doctor1;
            }

            var appointments = UoW.GetRepository<IScheduledEventReadRepository>()
                .GetAll();

            if (appointments == null)
            {
                var scheduledEvent1 = new ScheduledEvent()
                {
                    StartDate = new DateTime(2021, 12, 9, 13, 00, 00),
                    EndDate = new DateTime(2021, 12, 9, 14, 00, 00),

                };
                var scheduledEvent2 = new ScheduledEvent()
                {
                    StartDate = new DateTime(2021, 12, 10, 15, 00, 00),
                    EndDate = new DateTime(2021, 12, 10, 16, 00, 00),

                };

                doctor.ScheduledEvents.Append(scheduledEvent2);
            }

            return doctor;
        }
    }
}