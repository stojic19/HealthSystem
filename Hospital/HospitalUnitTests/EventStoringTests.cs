﻿using Hospital.EventStoring.Model;
using Hospital.EventStoring.Model.Enumerations;
using Hospital.EventStoring.Service;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace HospitalUnitTests
{
    public class EventStoringTests : BaseTest
    {
        //TODO: POPRAVI TEST
        private readonly EventStoringService _eventStoringService;
        public EventStoringTests(BaseFixture fixture) : base(fixture)
        {
            _eventStoringService = new(UoW);
        }

        [Fact]
        public void Get_statistics_per_part_of_day()
        {
            #region Arrange

            ClearDbContext();
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 22, 2, 32, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 10, 32, 0),
                Step=Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 8, 32, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 11, 5, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 12, 12, 32, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 15, 32, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 18, 17, 32, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 21, 32, 0),
                Step = Step.StartScheduling
            });
            Context.SaveChanges();
            #endregion

            var statistics = _eventStoringService.GetStatisticsPerPartOfDay().ToList();

            statistics[0].ShouldBe(1);
            statistics[1].ShouldBe(3);
            statistics[2].ShouldBe(2);
            statistics[3].ShouldBe(1);
            statistics[4].ShouldBe(1);
        }


      /*  [Fact]
        public void Get_statistics_per_day_of_week()
        {
            ClearDbContext();
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 10, 32, 0),
                Step = Step.StartScheduling
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 20, 10, 34, 0),
                Step = Step.Schedule
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2022, 1, 23, 18, 34, 0),
                Step = Step.StartScheduling
            });
            Context.SaveChanges();

            var eventStoringService = new EventStoringService(UoW);
            var statistics = eventStoringService.GetStatisticsPerDayOfWeek();

            statistics.Thursday[0].ShouldBe(1);
            statistics.Thursday[1].ShouldBe(0);
            statistics.Sunday[1].ShouldBe(1);
        }*/
        [Fact]
        public void Get_statistics_per_month()
        {
            ClearDbContext();
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 20, 10, 32, 0),
                Step = Step.StartScheduling
            });
            
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 23, 18, 34, 0),
                Step = Step.StartScheduling
            });
            Context.SaveChanges();

            var eventStoringService = new EventStoringService(UoW);
            var statistics = eventStoringService.GetStatisticsPerMonths().ToList();

            statistics[11].ShouldBe(2);
        }
        [Fact]
        public void Get_number_of_steps()
        {
            ClearDbContext();
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 20, 10, 32, 0),
                Step = Step.StartScheduling
            });

            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 20, 10, 32, 30),
                Step = Step.DateNext
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 20, 10, 32, 31),
                Step = Step.SpecializationNext
            });

            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 20, 10, 32, 32),
                Step = Step.DoctorNext
            });
            Context.StoredEvents.Add(new StoredEvent
            {
                Username = "testPatient123",
                Time = new DateTime(2021, 12, 20, 10, 32, 40),
                Step = Step.Schedule
            });
            Context.SaveChanges();

            var eventStoringService = new EventStoringService(UoW);
            var statistics = eventStoringService.GetNumberOfSteps().ToList();

            statistics[0].NumberOfSteps.ShouldBe(5);
            statistics[0].NumberOfScheduled.ShouldBe(1);
        }
    }
}
