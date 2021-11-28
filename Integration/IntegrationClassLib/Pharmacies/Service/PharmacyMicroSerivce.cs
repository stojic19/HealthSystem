using System.Collections.Generic;
using Integration.Shared.Model;

namespace Integration.Pharmacies.Service
{
    public class PharmacyMicroSerivce
    {
        public PharmacyMicroSerivce() { }
        public void LinkPharmacyWithExistingEntities(Model.Pharmacy pharmacy, City city, Country country)
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
        private void LinkPharmacyWithExistingCity(Model.Pharmacy pharmacy, City existingCity)
        {
            pharmacy.City = existingCity;
            pharmacy.CityId = existingCity.Id;
        }

        private void LinkPharmacyWithExistingCountry(Model.Pharmacy pharmacy, Country country)
        {
            if (country != null)
            {
                pharmacy.City.CountryId = country.Id;
                pharmacy.City.Country = country;
            }
        }
        public bool isUnique(Model.Pharmacy pharmacy, IEnumerable<Model.Pharmacy> pharmacies)
        {
            foreach (Model.Pharmacy existingPharmacy in pharmacies)
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
