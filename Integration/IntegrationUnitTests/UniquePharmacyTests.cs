using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.MicroServices;
using Integration.Model;
using Integration.Repositories;
using IntegrationUnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class UniquePharmacyTests : BaseTest
    {
        public UniquePharmacyTests(BaseFixture fixture) : base(fixture)
        {

        }
        [Theory]
        [MemberData(nameof(Data))]
        public void Is_unique_pharmacy(Pharmacy pharmacy, bool shouldBe)
        {
            IEnumerable<Pharmacy> pharmacies = UoW.GetRepository<IPharmacyReadRepository>().GetAll()
                .Include(x => x.City).ThenInclude(x => x.Country);
            PharmacyMicroSerivce pharmacyMicroService = new PharmacyMicroSerivce();
            bool retVal = pharmacyMicroService.isUnique(pharmacy, pharmacies);
            retVal.ShouldBe(shouldBe);
        }
        public static IEnumerable<object[]> Data()
        {
            List<object[]> retVal = new List<object[]>();
            Pharmacy unique = new Pharmacy
            {
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy notUnique = new Pharmacy
            {
                Name = "Apoteka1",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            retVal.Add(new object[] {unique, true});
            retVal.Add(new object[] {notUnique, false});
            return retVal;
        }
    }
}
