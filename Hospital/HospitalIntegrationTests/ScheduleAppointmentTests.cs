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

            var response = await Client.GetAsync(BaseUrl + "api/Doctor/GetDoctorsWithSpecialization?specializationName=" + doctor.Specialization.Name);
            var responseContent = await response.Content.ReadAsStringAsync();
            var doctors = JsonConvert.DeserializeObject<IEnumerable<Doctor>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
            doctors.Count.ShouldNotBe(0);
        }

        [Fact]
        public async Task Should_return_available_appointments()
        {
            var preferredDate = new DateTime(2021, 12, 18);
            var doctor = InsertDoctors();

            var response = await Client.GetAsync(BaseUrl + "api/ScheduleAppointment/GetAvailableAppointments?doctorId=" + doctor.Id +
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
            var doctor = InsertDoctors();
            var patient = InsertPatient(doctor.Id);
            var appointmentToSchedule = new ScheduleAppointmentDTO()
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                DoctorsRoomId = doctor.RoomId,
                PatientUsername = patient.UserName,
                StartDate = new DateTime(2021, 12, 24, 12, 0, 0)
            };

            var content = GetContent(appointmentToSchedule);
            var response = await Client.PostAsync(BaseUrl + "api/ScheduleAppointment/ScheduleAppointment", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var scheduledEvent = JsonConvert.DeserializeObject<ScheduledEvent>(responseContent);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            scheduledEvent.ShouldNotBeNull();
        }


        private Doctor InsertDoctors()
        {
            var doctor = UoW
                .GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization).Include(d => d.Room)
                .FirstOrDefault(d => d.Specialization.Name.ToLower().Equals("general practice"));

            if (doctor == null)
            {
                var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.RoomType == RoomType.AppointmentRoom);

                if (room == null)
                {
                    room = new Room()
                    {
                        Name = "test room ",
                        RoomType = RoomType.AppointmentRoom
                    };
                    UoW.GetRepository<IRoomWriteRepository>().Add(room);
                }

                var shift = UoW.GetRepository<IShiftReadRepository>().GetAll().FirstOrDefault() ?? new Shift()
                {
                    Name = "First",
                    From = 7,
                    To = 15
                };

                var doctor1 = new Doctor()
                {
                    UserName = "Test doctor1",
                    RoomId = room.Id,
                    Specialization = new Specialization("General Practice", ""),
                    Shift = shift
                };

                var doctor2 = new Doctor()
                {
                    UserName = "Test doctor1",
                    RoomId = room.Id,
                    Specialization = new Specialization("General Practice", ""),
                    Shift = shift
                };

                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor1);
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor2);
                doctor = doctor1;
            }

            if (doctor.ScheduledEvents != null) return doctor;
            
            var scheduledEvent1 = new ScheduledEvent(0, false, false, new DateTime(2021, 12, 19, 13, 00, 00),
                new DateTime(2021, 12, 19, 14, 00, 00),
                new DateTime(), InsertPatient(doctor.Id).Id, doctor.Id, doctor);

            var scheduledEvent2 = new ScheduledEvent(0, false, false, new DateTime(2021, 12, 20, 15, 00, 00),
                new DateTime(2021, 12, 20, 16, 00, 00), new DateTime(), InsertPatient(doctor.Id).Id, doctor.Id, doctor);

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent1);
            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
            doctor.ScheduledEvents.Append(scheduledEvent1);
            doctor.ScheduledEvents.Append(scheduledEvent2);

            return doctor;
        }

        private Patient InsertPatient(int doctorId)
        {
            var patient = UoW
                .GetRepository<IPatientReadRepository>()
                .GetAll().Include(d => d.MedicalRecord).ThenInclude(mr => mr.Doctor).FirstOrDefault(p => p.UserName == "testPatient");

            if (patient != null) return patient;
            var medicalRecord = new MedicalRecord(1, null, 0, 0, doctorId, null);
            UoW.GetRepository<IMedicalRecordWriteRepository>().Add(medicalRecord);

            patient = new Patient(medicalRecord)
            {
                UserName = "testPatient",
                Email = "testmail@gmail.com"
            };
            UoW.GetRepository<IPatientWriteRepository>().Add(patient);
            return patient;
        }

    }
}