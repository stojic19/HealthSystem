using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model.Wrappers;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalUnitTests
{
    public class AvailableTermsTests : BaseTest
    {
        public AvailableTermsTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Count_should_be_twenty_four()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            var availableTerms = new AvailableTermsService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(24);
        }

        [Fact]
        public void Count_should_be_twenty_three()
        {
            ClearDbContext();
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 2,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                RoomId = 2,
            });
            Context.SaveChanges();

            var availableTerms = new AvailableTermsService(UoW)
                .GetAvailableTerms(timePeriod, 2, 1, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(23);
        }

        [Fact]
        public void Count_should_be_one()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(3),
                RoomId = 1
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(3)
            };

            var availableTerms = new AvailableTermsService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(1);
        }

        [Fact]
        public void Should_be_empty()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(6),
                EndDate = DateTime.Now.AddHours(8),
                RoomId = 1
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(6.5),
                EndTime = DateTime.Now.AddHours(8)
            };

            var availableTerms = new AvailableTermsService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.ShouldBeEmpty();
        }

        [Fact]
        public void Two_terms_should_be_available()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                RoomId = 1
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(5.5)
            };

            var availableTerms = new AvailableTermsService(UoW)
               .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(2);
        }

        [Fact]
        public void One_term_should_be_available()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                RoomId = 1
            });
            Context.SaveChanges();
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3.5)
            };

            var availableTerms = new AvailableTermsService(UoW)
               .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(1);
        }
    }
}
