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

        [Theory]
        [MemberData(nameof(Data))]
        public void Checks_finding_available_terms(TimePeriod timePeriod, int foundAvailableTerms)
        {

            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = new DateTime(2022, 12, 1, 10, 0, 0),
                EndDate = new DateTime(2022, 12, 1, 10, 30, 0),
                RoomId = 2,
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 2,
                StartDate = new DateTime(2022, 12, 3, 6, 0, 0),
                EndDate = new DateTime(2022, 12, 3, 8, 0, 0),
                RoomId = 2,
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 3,
                StartDate = new DateTime(2022, 12, 5, 3, 0, 0),
                EndDate = new DateTime(2022, 12, 5, 3, 30, 0),
                RoomId = 2,
            });

            Context.SaveChanges();

            var availableTerms = new AvailableTermsService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(foundAvailableTerms);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            var timePeriod = new TimePeriod(new DateTime(2022, 12, 10, 0, 0, 0), new DateTime(2022, 12, 11, 0, 1, 0));
            retVal.Add(new object[] { timePeriod, 24 });

            var timePeriod2 = new TimePeriod(new DateTime(2022, 12, 1, 0, 0, 0), new DateTime(2022, 12, 2, 0, 1, 0));
            retVal.Add(new object[] { timePeriod2, 22 });

            var timePeriod3 = new TimePeriod(new DateTime(2022, 12, 1, 9, 0, 0), new DateTime(2022, 12, 1, 11, 1, 0));
            retVal.Add(new object[] { timePeriod3, 1 });

            var timePeriod4 = new TimePeriod(new DateTime(2022, 12, 3, 6, 30, 0), new DateTime(2022, 12, 3, 8, 1, 0));
            retVal.Add(new object[] { timePeriod4, 0 });

            var timePeriod5 = new TimePeriod(new DateTime(2022, 12, 5, 2, 0, 0), new DateTime(2022, 12, 5, 5, 31, 0));
            retVal.Add(new object[] { timePeriod5, 1 });

            var timePeriod6 = new TimePeriod(new DateTime(2022, 12, 5, 2, 0, 0), new DateTime(2022, 12, 5, 3, 31, 0));
            retVal.Add(new object[] { timePeriod6, 1 });

            return retVal;
        }
    }
}
