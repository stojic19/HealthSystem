using System.Linq;
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
            ClearDbContext();

            Context.Cities.Add(new City()
            {
                Name = "Test city",
                Country = new Country()
                {
                    Name = "Test country"
                },
                PostalCode = 100
            });
            Context.SaveChanges();

            var city = UoW.GetRepository<ICityReadRepository>().GetCityByName("Test city");
            var cities = UoW.GetRepository<ICityReadRepository>().GetAll();
            cities.Count().ShouldBe(1);
            city.ShouldNotBeNull();
        }
    }
}
