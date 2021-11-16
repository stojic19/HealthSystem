using Integration.MicroServices;
using Integration.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationClassLibTests
{
    public class IsUniquePharmacyTests
    {
        [Theory]
        [MemberData(nameof(PharmacyUniqueData))]
        public void Is_unique_pharmacy(Pharmacy pharmacy, IEnumerable<Pharmacy> pharmacies, bool shouldBe)
        {
            PharmacyMicroSerivce pharmacyMicroService = new PharmacyMicroSerivce();
            bool retVal = pharmacyMicroService.isUnique(pharmacy, pharmacies);
            Assert.Equal(shouldBe, retVal);
        }
        public static IEnumerable<object[]> PharmacyUniqueData()
        {
            List<object[]> retVal = new List<object[]>();
            Pharmacy checkedPharmacy = new Pharmacy
            {
                Name = "Apoteka",
                StreetName = "Nova",
                StreetNumber = "29",
                City = new City { Name = "Novi Sad", Country = new Country { Name = "Srbija" } }
            };
            Pharmacy pharmacy1 = new Pharmacy
            {
                Name = "Apoteka",
                StreetName = "Nova",
                StreetNumber = "29",
                City = new City { Name = "Beograd", Country = new Country { Name = "Srbija" } }
            };
            Pharmacy pharmacy2 = new Pharmacy
            {
                Name = "Apoteka",
                StreetName = "Nova",
                StreetNumber = "29",
                City = new City { Name = "Novi Sad", Country = new Country { Name = "Crna Gora" } }
            };
            Pharmacy pharmacy3 = new Pharmacy
            {
                Name = "Apoteka1",
                StreetName = "Nova",
                StreetNumber = "29",
                City = new City { Name = "Novi Sad", Country = new Country { Name = "Srbija" } }
            };
            Pharmacy pharmacy4 = new Pharmacy
            {
                Name = "Apoteka",
                StreetName = "Nova1",
                StreetNumber = "29",
                City = new City { Name = "Novi Sad", Country = new Country { Name = "Srbija" } }
            };
            Pharmacy pharmacy5 = new Pharmacy
            {
                Name = "Apoteka",
                StreetName = "Nova",
                StreetNumber = "28",
                City = new City { Name = "Novi Sad", Country = new Country { Name = "Srbija" } }
            };
            List<Pharmacy> unique = new List<Pharmacy>();
            unique.Add(pharmacy1);
            unique.Add(pharmacy2);
            unique.Add(pharmacy3);
            unique.Add(pharmacy4);
            unique.Add(pharmacy5);
            retVal.Add(new object[] { checkedPharmacy, unique, true });
            List<Pharmacy> notUnique = new List<Pharmacy>();
            notUnique.Add(pharmacy3);
            notUnique.Add(pharmacy5);
            notUnique.Add(checkedPharmacy);
            retVal.Add(new object[] { checkedPharmacy, notUnique, false });
            return retVal;
        }
    }
}
