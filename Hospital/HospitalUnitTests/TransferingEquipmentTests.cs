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
            var room = InsertRoom(1);
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

            InsertEvent(1, 1, DateTime.Now.AddHours(6), DateTime.Now.AddHours(8));
            InsertEvent(2, 2, DateTime.Now.AddHours(3), DateTime.Now.AddHours(3.5));

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(23);
        }

        [Fact]
        public void Count_should_be_one()
        {
            InsertEvent(3, 3, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3));
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
            InsertEvent(4, 1, DateTime.Now.AddHours(6), DateTime.Now.AddHours(8));

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 1, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.ShouldBeEmpty();
        }

        [Fact]
        public void Two_terms_should_be_available()
        {
            InsertEvent(5, 2, DateTime.Now.AddHours(3), DateTime.Now.AddHours(3.5));
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(5.5)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
               .GetAvailableTerms(timePeriod, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(2);
        }

        [Fact]
        public void One_term_should_be_available()
        {
            InsertEvent(6, 2, DateTime.Now.AddHours(3), DateTime.Now.AddHours(3.5));
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

        private Room InsertRoom(int id)
        {
            var room = Context.Rooms.Find(id);

            if (room == null)
            {
                room = new Room()
                {
                    Id = id,
                    Name = "Test room"
                };
                Context.Rooms.Add(room);
                Context.SaveChanges();
            }

            return room;
        }

        private ScheduledEvent InsertEvent(int id, int roomId, DateTime start, DateTime end)
        {
            var appointment = Context.ScheduledEvents
                              .FirstOrDefault(x => x.RoomId == roomId &&
                                                   x.StartDate == start && x.EndDate == end);

            if (appointment == null)
            {
                appointment = new ScheduledEvent()
                {
                    Id = id,
                    StartDate = start,
                    EndDate = end,
                    Room = InsertRoom(roomId)
                };
                Context.ScheduledEvents.Add(appointment);
                Context.SaveChanges();
            }

            return appointment;
        }
    }
}
