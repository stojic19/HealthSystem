using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Pharmacies.Service;
using Integration.Shared.Model;
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
            Context.Pharmacies.RemoveRange(Context.Pharmacies);
            Context.SaveChanges();
            MakePharmacies();
            Context.SaveChanges();
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
        private void MakePharmacies()
        {
            Pharmacy pharmacy1 = new Pharmacy
            {
                Id = 1,
                Name = "Apoteka1",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy pharmacy2 = new Pharmacy
            {
                Id = 2,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad1",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy pharmacy3 = new Pharmacy
            {
                Id = 3,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija1" }
                },
                StreetName = "Nova",
                StreetNumber = "29"
            };
            Pharmacy pharmacy4 = new Pharmacy
            {
                Id = 4,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova1",
                StreetNumber = "29"
            };
            Pharmacy pharmacy5 = new Pharmacy
            {
                Id = 5,
                Name = "Apoteka",
                City = new City
                {
                    Name = "Novi Sad",
                    Country = new Country { Name = "Srbija" }
                },
                StreetName = "Nova",
                StreetNumber = "29b"
            };
            Context.Pharmacies.Add(pharmacy1);
            Context.Pharmacies.Add(pharmacy2);
            Context.Pharmacies.Add(pharmacy3);
            Context.Pharmacies.Add(pharmacy4);
            Context.Pharmacies.Add(pharmacy5);
        }
    }
}
