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
            RegisterAndLogin("Patient");
            var doctor = InsertDoctors();

            var response = await PatientClient.GetAsync(BaseUrl + "api/Doctor/GetDoctorsWithSpecialization?specializationId=" + doctor.SpecializationId);
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
            var doctor = InsertDoctors();
            
            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/GetAvailableAppointments?specializationId=" + doctor.Specialization.Id +
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


        private Doctor InsertDoctors()
        {
            var shift = UoW.GetRepository<IShiftReadRepository>()
                .GetAll()
                .FirstOrDefault() ?? new Shift()
            {
                Name = "TestShift",
                From = 23,
                To = 7
            };

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
                    UserName = "testDoctor1",
                    SpecializationId = specialization.Id,
                    CityId = city.Id,
                    RoomId = room.Id,
                    ShiftId = shift.Id
                };

                var doctor2 = new Doctor()
                {
                    UserName = "testDoctor2",
                    SpecializationId = specialization.Id,
                    CityId = city.Id,
                    RoomId = room.Id,
                    ShiftId = shift.Id
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