using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;
using Pharmacy.Repositories;
using PharmacyUnitTests.Base;
using Shouldly;
using Xunit;

namespace PharmacyUnitTests
{
    public class HospitalModelTests : BaseTest
    {
        public HospitalModelTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Should_get_Two_Hospitals()
        {
            ClearDbContext();

            Context.Hospitals.Add(new Hospital()
            {
                ApiKey = Guid.NewGuid(),
                Name = "Test name",
                StreetName = "some street",
                StreetNumber = "123b"
            });
            Context.Hospitals.Add(new Hospital()
            {
                ApiKey = Guid.NewGuid(),
                Name = "Test other name",
                StreetName = "some street",
                StreetNumber = "123b"
            });
            Context.Cities.Add(new City()
            {
                Country = new Country()
                {
                    Name = "Test country"
                },
                Name = "Test city"
            });
            Context.SaveChanges();

            var hospitals = UoW.GetRepository<IHospitalReadRepository>()
                .GetAll();

            hospitals.ShouldNotBeNull();
            hospitals.Count().ShouldBe(2);
        }
    }
}
