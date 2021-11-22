using Pharmacy.Model;
using Pharmacy.Repositories;
using PharmacyUnitTests.Base;
using Shouldly;
using Xunit;

namespace PharmacyUnitTests
{
    public class ModelTests : BaseTest
    {
        public ModelTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void City_should_not_be_null()
        {
            Context.Cities.Add(new City()
            {
                Id = 2,
                Name = "Test city",
                Country = new Country()
                {
                    Id = 1,
                    Name = "Test country"
                },
                PostalCode = 100
            });
            Context.SaveChanges();

            var city = UoW.GetRepository<ICityReadRepository>().GetCityByName("Test city");
            city.ShouldNotBeNull();
        }
    }
}
