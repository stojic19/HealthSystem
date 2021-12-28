using IntegrationUnitTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Shared.Model;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class LocationTests : BaseTest
    {
        public LocationTests(BaseFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public void Get_return_of_city_in_location()
        {
            ClearDbContext();
            Location location = new Location(45.258253, 19.8012564);

            City city = location.GetCity();

            city.ShouldNotBeNull();
            city.Name.ShouldBe("Novi Sad City");
            city.Country.Name.ShouldBe("Serbia");
        }
    }
}
