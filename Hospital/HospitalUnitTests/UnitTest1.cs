using Hospital.Model;
using Hospital.Repositories;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace HospitalUnitTests
{
    public class UnitTest1 : BaseTest
    {
        public UnitTest1(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Test1()
        {
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "Srbija"
            });

            Context.Countries.Add(new Country()
            {
                Id = 2,
                Name = "BiH"
            });

            Context.SaveChanges();
            var countries = UoW.GetRepository<ICountryReadRepository>().GetAll();
            countries.ShouldNotBeNull();
            countries.Count().ShouldBe(2);
        }
    }
}
