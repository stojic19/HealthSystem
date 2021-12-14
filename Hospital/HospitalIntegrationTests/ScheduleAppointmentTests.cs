using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
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
            var preferredDate = new DateTime(2021, 12, 18);
            var doctor = InsertDoctors();
            
            var response = await Client.GetAsync(BaseUrl + "api/ScheduledEvent/GetAvailableAppointments?specializationId=" + doctor.Specialization.Id +
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
            var appointmentToSchedule = new ScheduleAppointmentDTO()
            {
                DoctorId = InsertDoctors().Id,
                PatientId = InsertPatient().Id,
                StartDate = new DateTime(2021, 12, 24, 12, 0, 0)
            };

            var content = GetContent(appointmentToSchedule);
            var response = await Client.PostAsync(BaseUrl + "api/ScheduleAppointment/ScheduleAppointment", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var scheduledEvent = JsonConvert.DeserializeObject<ScheduledEvent>(responseContent);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            scheduledEvent.ShouldNotBeNull();
            scheduledEvent.DoctorId.ShouldBe(appointmentToSchedule.DoctorId);
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
                UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);

            }

            var room = UoW.GetRepository<IRoomReadRepository>().GetAll().FirstOrDefault();
            if(room == null){
                room = new Room()
                {
                    Name = "Ord1",
                    RoomType = RoomType.AppointmentRoom
                };
                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().Include(c => c.Country).FirstOrDefault();
            if (city == null)
            {
                var country = UoW.GetRepository<ICountryReadRepository>().GetAll().FirstOrDefault();
                if (country == null)
                {
                    country = new Country()
                    {
                        Name = "Test Country"
                    };
                    UoW.GetRepository<ICountryWriteRepository>().Add(country);
                }
                
                city = new City()
                {
                    CountryId = country.Id,
                    Name = "Test City"
                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);

            }

            var doctor = UoW
                .GetRepository<IDoctorReadRepository>()
                .GetAll().Include(d => d.Specialization).Include(d => d.ScheduledEvents).Include(d => d.Room).FirstOrDefault(x => x.Specialization.Name.ToLower() == "general practice" && x.UserName == "testDoctor1" || x.UserName == "testDoctor2");

            if (doctor == null)
            {
                var doctor1 = new Doctor()
                {
                    UserName = "testDoctor1",
                    SpecializationId = specialization.Id,
                    CityId = city.Id,
                    RoomId = room.Id
                };
                var doctor2 = new Doctor()
                {
                    UserName = "testDoctor2",
                    SpecializationId = specialization.Id,
                    CityId = city.Id,
                    RoomId = room.Id
                };

                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor1);
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor2);

                doctor = doctor1;

            }

            if (doctor.Room == null)
            {
                doctor.RoomId = room.Id;
            };

            if (doctor.ScheduledEvents != null) return doctor;
            var scheduledEvent1 = new ScheduledEvent()
            {
                StartDate = new DateTime(2021, 12, 19, 13, 00, 00),
                EndDate = new DateTime(2021, 12, 19, 14, 00, 00),
                DoctorId = doctor.Id,
                RoomId = doctor.RoomId,
                PatientId = InsertPatient().Id
            };
            var scheduledEvent2 = new ScheduledEvent()
            {
                StartDate = new DateTime(2021, 12, 20, 15, 00, 00),
                EndDate = new DateTime(2021, 12, 20, 16, 00, 00),
                DoctorId = doctor.Id,
                RoomId = doctor.RoomId,
                PatientId = InsertPatient().Id
            };

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent1);
            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
            doctor.ScheduledEvents.Append(scheduledEvent1);
            doctor.ScheduledEvents.Append(scheduledEvent2);

            return doctor;
        }

        private Patient InsertPatient()
        {
             var city = UoW.GetRepository<ICityReadRepository>().GetAll().Include(c => c.Country).FirstOrDefault();
            if (city == null)
            {
                var country = UoW.GetRepository<ICountryReadRepository>().GetAll().FirstOrDefault();
                if (country == null)
                {
                    country = new Country()
                    {
                        Name = "Test Country"
                    };
                    UoW.GetRepository<ICountryWriteRepository>().Add(country);
                }
                
                city = new City()
                {
                    CountryId = country.Id,
                    Name = "Test City"
                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);

            }

            var patient = UoW
                .GetRepository<IPatientReadRepository>()
                .GetAll().Include(d => d.MedicalRecord).ThenInclude(mr => mr.Doctor).FirstOrDefault(p => p.UserName == "testPatient");

            if (patient == null)
            {
                var medicalRecord = new MedicalRecord()
                {
                    DoctorId = InsertDoctors().Id
                };
                UoW.GetRepository<IMedicalRecordWriteRepository>().Add(medicalRecord);


                patient = new Patient()
                {
                    CityId = city.Id,
                    UserName = "testPatient",
                    MedicalRecordId = medicalRecord.Id
                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

            }
            return patient;
        }

    }
}