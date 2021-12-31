using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Shared.Model;
using IntegrationUnitTests.Base;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class TimeRangeTests : BaseTest
    {
        public TimeRangeTests(BaseFixture fixture) : base(fixture)
        {
            ClearDbContext();
        }
        [Theory]
        [MemberData(nameof(GetTimeRangeData))]
        public void Overlap_tests(TimeRange left, TimeRange right, bool shouldBe)
        {
            left.OverlapsWith(right).ShouldBe(shouldBe);
        }

        public static IEnumerable<object[]> GetTimeRangeData()
        {
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
            {
                new TimeRange(new DateTime(2021,1,1), new DateTime(2021,1,2)),
                new TimeRange(new DateTime(2021,1,2), new DateTime(2021,1,3)),
                false
            });
            retVal.Add(new object[]
            {
                new TimeRange(new DateTime(2021,1,1), new DateTime(2021,1,3)),
                new TimeRange(new DateTime(2021,1,2), new DateTime(2021,1,3)),
                true
            });
            retVal.Add(new object[]
            {
                new TimeRange(new DateTime(2021,1,1), new DateTime(2021,1,3)),
                new TimeRange(new DateTime(2021,1,2), new DateTime(2021,1,4)),
                true
            });
            return retVal;
        }

        [Fact]
        public void Time_range_includes_test()
        {
            TimeRange timeRange = new TimeRange(new DateTime(2021, 1, 1), new DateTime(2021, 1, 3));
            DateTime included = new DateTime(2021, 1, 2);
            DateTime notIncluded = new DateTime(2021, 1, 4);
            Assert.True(timeRange.Includes(included));
            Assert.False(timeRange.Includes(notIncluded));
        }
        [Fact]
        public void Time_range_in_past_test()
        {
            TimeRange notInPast = new TimeRange(new DateTime(2021, 1, 1), DateTime.Now.AddDays(1));
            TimeRange inPast = new TimeRange(new DateTime(2021, 1, 1), DateTime.Now.AddDays(-1));
            Assert.False(notInPast.IsInPast());
            Assert.True(inPast.IsInPast());
        }
    }
}
