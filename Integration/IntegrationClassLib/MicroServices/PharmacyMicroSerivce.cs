using Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MicroServices
{
    public class PharmacyMicroSerivce
    {
        public PharmacyMicroSerivce() { }
        public void LinkPharmacyWithExistingEntities(Pharmacy pharmacy, City city, Country country)
        {
            if (city != null)
            {
                LinkPharmacyWithExistingCity(pharmacy, city);
            }
            else
            {
                LinkPharmacyWithExistingCountry(pharmacy, country);
            }
        }
        private void LinkPharmacyWithExistingCity(Pharmacy pharmacy, City existingCity)
        {
            pharmacy.City = existingCity;
            pharmacy.CityId = existingCity.Id;
        }

        private void LinkPharmacyWithExistingCountry(Pharmacy pharmacy, Country country)
        {
            if (country != null)
            {
                pharmacy.City.CountryId = country.Id;
                pharmacy.City.Country = country;
            }
        }
        public bool isUnique(Pharmacy pharmacy, IEnumerable<Pharmacy> pharmacies)
        {
            foreach (Pharmacy existingPharmacy in pharmacies)
            {
                if (existingPharmacy.isEqual(pharmacy))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
