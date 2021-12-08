using System;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using HospitalUnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace HospitalUnitTests
{
    public class ScheduleAppointmentTests : BaseTest
    {
        public ScheduleAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public void Should_return_available_appointments()
        {
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2021, 12, 9, 13, 00, 00),
                    EndDate = new DateTime(2021, 12, 9, 14, 00, 00),
                    Doctor = new Doctor()
                    {
                        Id = 1,
                        SpecializationId = 3
                    }
                });

            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2021, 12, 10, 15, 00, 00),
                    EndDate = new DateTime(2021, 12, 10, 16, 00, 00),
                    Doctor = new Doctor()
                    {
                        Id = 2,
                        SpecializationId = 3
                    }
                });

            Context.Specializations.Add(new Specialization()
            {
                Id = 3,
                Name = "General practice"
            });

            Context.SaveChanges();

            var preferredTimePeriod = new TimePeriod()
            {
                StartTime = new DateTime(2021, 12, 9, 08, 00, 00),
                EndTime = new DateTime(2021, 12, 10, 18, 00, 00)
            };

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var repo = UoW.GetRepository<IScheduledEventReadRepository>();
            var scheduledEvents = repo.GetAvailableAppointments(doctor.Id, preferredTimePeriod);

            scheduledEvents.ShouldNotBeNull();
            scheduledEvents.Count().ShouldBe(20);
        }
    }
}
