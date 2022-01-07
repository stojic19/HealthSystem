﻿using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalIntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class ManagingOnCallShiftsTests : BaseTest
    {
        public ManagingOnCallShiftsTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Adding_doctor_to_shift_should_return_200()
        {

            var doctor = InsertDoctor("doctor");
            var shift = InsertOnCallDuty(1, 3);

            var response = await Client.GetAsync(BaseUrl + "api/OnCallShifts/AddDoctor?shiftId=" + shift.Id + "&doctorId=" + doctor.Id);


            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var updatedShift = UoW.GetRepository<IOnCallDutyReadRepository>()
                .GetAll().Include(x => x.DoctorsOnDuty).FirstOrDefault(x => x.Month == 1 && x.Week == 3);

            updatedShift.DoctorsOnDuty.Count.ShouldBe(2);

            ClearAllTestData();

        }


        [Fact]
        public async Task Remove_doctor_from_shift_should_return_200()
        {
            var shift = InsertOnCallDuty(1, 1);
            var doctor = InsertDoctor("testdoctor");

            var response = await Client.GetAsync(BaseUrl + "api/OnCallShifts/RemoveDoctor?shiftId=" + shift.Id + "&doctorId=" + doctor.Id);


            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var updatedShift = UoW.GetRepository<IOnCallDutyReadRepository>()
                .GetAll().Include(x => x.DoctorsOnDuty).FirstOrDefault(x => x.Month == 1 && x.Week == 1);

            updatedShift.DoctorsOnDuty.Count.ShouldBe(0);

            ClearAllTestData();

        }

        private Doctor InsertDoctor(string username)
        {
            var city = UoW.GetRepository<ICityReadRepository>()
                   .GetAll()
                   .FirstOrDefault();

            if (city == null)
            {
                var country = UoW.GetRepository<ICountryReadRepository>()
                    .GetAll()
                    .FirstOrDefault();

                if (country == null)
                {
                    country = new Country()
                    {
                        Name = "Test country"
                    };
                    UoW.GetRepository<ICountryWriteRepository>().Add(country);
                }

                city = new City()
                {
                    CountryId = country.Id,
                    Name = "Test city",
                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);
            }

            var doctor = UoW
                .GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization).Include(d => d.Room)
                .FirstOrDefault(d => d.UserName.Equals(username));

            if (doctor == null)
            {
                var specialization = UoW.GetRepository<ISpecializationReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.Name.ToLower().Equals("general practice"));
                if (specialization == null)
                {
                    specialization = new Specialization()
                    {
                        Name = "General Practice"
                    };
                    UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);
                }

                var shift = UoW.GetRepository<IShiftReadRepository>().GetAll().FirstOrDefault(x => x.Name == "Second");
                if (shift == null) {
                    shift = new Shift()
                    {
                        Name = "Second",
                        From = 16,
                        To = 24
                    };
                    UoW.GetRepository<IShiftWriteRepository>().Add(shift);
                }

                var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll()
                    .FirstOrDefault(x => x.RoomType == RoomType.AppointmentRoom);
                if (room == null)
                {
                    room = new Room()
                    {
                        Name = "test room",
                        RoomType = RoomType.AppointmentRoom
                    };
                    UoW.GetRepository<IRoomWriteRepository>().Add(room);
                }


                doctor = new Doctor()
                {
                    UserName = username,
                    CityId = city.Id,
                    RoomId = room.Id,
                    ShiftId = shift.Id,
                    SpecializationId = specialization.Id
                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);

            }

            return doctor;
        }

        private OnCallDuty InsertOnCallDuty(int month, int week)
        {
            var shift = UoW.GetRepository<IOnCallDutyReadRepository>()
                .GetAll().Include(x => x.DoctorsOnDuty)
                .FirstOrDefault(x => x.Month == month && x.Week == week);

            var doctorsOnDuty = new List<Doctor>();
            var doctor = InsertDoctor("testdoctor");
            doctorsOnDuty.Add(doctor);

            if (shift == null)
            {
                shift = new OnCallDuty(month, week, doctorsOnDuty);

                UoW.GetRepository<IOnCallDutyWriteRepository>().Add(shift);
            }

            return shift;
        }

        private void ClearAllTestData()
        {

            var firstDoctor = UoW.GetRepository<IDoctorReadRepository>()
              .GetAll()
              .FirstOrDefault(x => x.UserName == "doctor");

            if (firstDoctor != null)
                UoW.GetRepository<IDoctorWriteRepository>().Delete(firstDoctor);

            var secondDoctor = UoW.GetRepository<IDoctorReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.UserName == "testdoctor");

            if (secondDoctor != null)
                UoW.GetRepository<IDoctorWriteRepository>().Delete(secondDoctor);

            var city = UoW.GetRepository<ICityReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "Test city");

            if (city != null)
            {
                UoW.GetRepository<ICityWriteRepository>().Delete(city);
            }

            var country = UoW.GetRepository<ICountryReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "Test country");

            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }

            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "test room");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }

            var firstDuty = UoW.GetRepository<IOnCallDutyReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Month == 1 && x.Week == 3);

            if (firstDuty != null)
                UoW.GetRepository<IOnCallDutyWriteRepository>().Delete(firstDuty);

            var secondDuty = UoW.GetRepository<IOnCallDutyReadRepository>()
              .GetAll()
              .FirstOrDefault(x => x.Month == 1 && x.Week == 1);

            if (secondDuty != null)
                UoW.GetRepository<IOnCallDutyWriteRepository>().Delete(secondDuty);

            var shift = UoW.GetRepository<IShiftReadRepository>().GetAll().FirstOrDefault(x => x.Name == "Second");

            if (shift != null)
            {

                UoW.GetRepository<IShiftWriteRepository>().Delete(shift);
            }

        }
    }
}