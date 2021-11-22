using Hospital.Model;
using Hospital.Services;
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
    public class TransferingEquipmentTests : BaseTest
    {
        public TransferingEquipmentTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Count_should_be_twenty_four()
        {
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test name"
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 1, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(24);
        }

        [Fact]
        public void Count_should_be_twenty_three()
        {
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(6),
                EndDate = DateTime.Now.AddHours(8),
                RoomId = 1
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 2,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                Room = new Room()
                {
                    Id = 2,
                    Name = "Test room 2"
                }
            });

            Context.SaveChanges();

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(23);
        }

        [Fact]
        public void Count_should_be_one()
        {
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2),
                Room = new Room()
                {
                    Id = 3,
                    Name = "Test room 3"
                }
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(3)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 3, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(1);
        }

        [Fact]
        public void Should_be_empty()
        {
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(6.5),
                EndTime = DateTime.Now.AddHours(8)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 1, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.ShouldBeEmpty();
        }

        [Fact]
        public void Count_should_be_two()
        {
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(4.5)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
               .GetAvailableTerms(timePeriod, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(2);
        }

        [Fact]
        public void One_term_should_be_available()
        {
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3.5)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
               .GetAvailableTerms(timePeriod, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(1);
        }
    }
}
