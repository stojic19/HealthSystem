using System;
using Hospital.Model;
using Hospital.Repositories;
using HospitalUnitTests.Base;
using Xunit;
using System.Linq;
using Shouldly;

namespace HospitalUnitTests
{
    public class UnitTest1 : BaseTest
    {
        [Fact]
        public void Test1()
        {
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "Serbia"
            });

            Context.Countries.Add(new Country()
            {
                Id = 2,
                Name = "Bulgaria"
            });

            Context.SaveChanges();

            var countries = UoW.GetRepository<ICountryReadRepository>().GetAll();
            countries.ShouldNotBeNull();
            countries.Count().ShouldBe(2);

        }

        public UnitTest1(BaseFixture fixture) : base(fixture)
        {
            
        }
    }
}
