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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class RecommendedAppointmentTests : BaseTest
    {
        public RecommendedAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Should_return_200_OK()
        {
            var doctor = InsertDoctors();
            var startDate = "2/2/2022";
            var endDate = "3/2/2022";
            var doctorId = doctor.Id;
            var isDoctorPriority = false;

            var response =
                await Client.GetAsync(BaseUrl + "api/RecommendedAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + startDate + "&dateEnd=" + endDate + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            ClearTestData();
        }

        [Fact]
        public async Task Should_return_available_appointments()
        {
            var doctor = InsertDoctors();
            var startDate = "2/2/2022";
            var endDate = "3/2/2022";
            var doctorId = doctor.Id;
            var isDoctorPriority = false;

            var response =
                await Client.GetAsync(BaseUrl + "api/RecommendedAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + startDate + "&dateEnd=" + endDate + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.ShouldNotBeNull();
            ClearTestData();
        }

        [Fact]
        public async Task Should_return_available_appointments_for_doctor_priority()
        {

            var doctor = InsertDoctors();
            var dateStart = "12/14/2021";
            var dateEnd = "12/15/2022";
            var doctorId = doctor.Id;
            var isDoctorPriority = true;

            var response =
                await Client.GetAsync(BaseUrl + "api/RecommendedAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + dateStart + "&dateEnd=" + dateEnd + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.ShouldNotBeNull();
            availableAppointments[0].Doctor.Id.ShouldBe(doctorId);
            ClearTestData();
        }

        [Fact]
        public async Task Should_return_available_appointments_for_date_priority()
        {
            var doctor = InsertDoctors();
            var dateStart = "12/14/2021";
            var dateEnd = "12/15/2021";
            var doctorId = doctor.Id;
            var isDoctorPriority = false;

            var response =
                await Client.GetAsync(BaseUrl + "api/RecommendedAppointment/GetRecommendedAppointments?doctorId=" + doctorId + "&dateStart=" + dateStart + "&dateEnd=" + dateEnd + "&isDoctorPriority=" + isDoctorPriority);
            var responseContent = await response.Content.ReadAsStringAsync();
            var availableAppointments =
                JsonConvert.DeserializeObject<IEnumerable<AvailableAppointmentDTO>>(responseContent).ToList();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            availableAppointments.Count.ShouldNotBe(0);
            availableAppointments.ShouldNotBeNull();
            availableAppointments[0].Doctor.Specialization.Id.Equals(doctor.SpecializationId);
            ClearTestData();
        }

        private void ClearTestData()
        {

            var patient = UoW.GetRepository<IPatientReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "Test-Patient");

            if (patient == null) return;
            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll().Include(mr => mr.Doctor)
                    .FirstOrDefault(x => x.Id == patient.MedicalRecordId);

                UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
            }
            var scheduledEvents = UoW.GetRepository<IScheduledEventReadRepository>()
                .GetAll().ToList()
                .Where(s => s.Doctor.UserName == "TestDoctor1");

            /*if (scheduledEvents == null) return;
            {

                UoW.GetRepository<IScheduledEventWriteRepository>().DeleteRange(scheduledEvents);
            }*/

            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "Test-Room");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }

            var firstDoctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "TestDoctor1");

            if (firstDoctor != null)
            {
                UoW.GetRepository<IDoctorWriteRepository>().Delete(firstDoctor);
            }

            var secondDoctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.UserName == "TestDoctor2");

            if (secondDoctor != null)
            {
                UoW.GetRepository<IDoctorWriteRepository>().Delete(secondDoctor);
            }


            var city = UoW.GetRepository<ICityReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "Test-City");

            if (city != null)
            {
                UoW.GetRepository<ICityWriteRepository>().Delete(city);
            }

            var country = UoW.GetRepository<ICountryReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "Test-Country");

            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }

           
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
            if (room == null)
            {
                room = new Room()
                {
                    Name = "Test-Room",
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
                        Name = "Test-Country"
                    };
                    UoW.GetRepository<ICountryWriteRepository>().Add(country);
                }

                city = new City()
                {
                    CountryId = country.Id,
                    Name = "Test-City"
                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);

            }

            var doctor = UoW
                .GetRepository<IDoctorReadRepository>()
                .GetAll().Include(d => d.Specialization).Include(d => d.ScheduledEvents).Include(d => d.Room).FirstOrDefault(x => x.Specialization.Name.ToLower() == "general practice" && x.UserName == "TestDoctor1" || x.UserName == "TestDoctor2");

            if (doctor == null)
            {
                var doctor1 = new Doctor()
                {
                    UserName = "TestDoctor1",
                    SpecializationId = specialization.Id,
                    CityId = city.Id,
                    RoomId = room.Id
                };
                var doctor2 = new Doctor()
                {
                    UserName = "TestDoctor2",
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
                        Name = "Test-Country"
                    };
                    UoW.GetRepository<ICountryWriteRepository>().Add(country);
                }

                city = new City()
                {
                    CountryId = country.Id,
                    Name = "Test-City"
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
                    UserName = "Test-Patient",
                    MedicalRecordId = medicalRecord.Id
                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

            }
            return patient;
        }


    }
}
