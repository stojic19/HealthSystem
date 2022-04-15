using IntegrationUnitTests.Base;
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
            Location location = new(45.258253, 19.8012564);

            City city = location.GetCity();

            city.ShouldNotBeNull();
            city.Name.ShouldBe("Novi Sad City");
            city.Country.Name.ShouldBe("Serbia");
        }

        [Fact]
        public void Get_return_of_country_in_location()
        {
            ClearDbContext();
            Location location = new(45.258253, 19.8012564);

            Country country = location.GetCountry();

            country.ShouldNotBeNull();
            country.Name.ShouldBe("Serbia");
        }

        [Fact]
        public void Get_return_of_full_address_in_location()
        {
            ClearDbContext();
            Location location = new(45.258253, 19.8012564);

            string address = location.GetFullAddress();

            address.ShouldNotBeNull();
            address.ShouldBe("Roda, Булевар војводе Степе 32, 21137 Novi Sad City, Serbia");
        }

        [Fact]
        public void Get_return_of_street_name_in_location()
        {
            ClearDbContext();
            Location location = new(45.258253, 19.8012564);

            string address = location.GetStreetName();

            address.ShouldNotBeNull();
            address.ShouldBe("Булевар војводе Степе");
        }

        [Fact]
        public void Get_return_of_house_number_in_location()
        {
            ClearDbContext();
            Location location = new(45.258253, 19.8012564);

            string address = location.GetHouseNumber();

            address.ShouldNotBeNull();
            address.ShouldBe("32");
        }
    }
}
