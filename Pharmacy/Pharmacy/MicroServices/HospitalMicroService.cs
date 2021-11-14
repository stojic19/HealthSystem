using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.MicroServices
{
    public class HospitalMicroService
    {
        public HospitalMicroService() { }
        public void LinkHospitalWithExistingEntities(Hospital hospital, City city, Country country)
        {
            if (city != null)
            {
                LinkPharmacyWithExistingCity(hospital, city);
            }
            else
            {
                LinkPharmacyWithExistingCountry(hospital, country);
            }
        }
        private void LinkPharmacyWithExistingCity(Hospital hospital, City existingCity)
        {
            hospital.City = existingCity;
            hospital.CityId = existingCity.Id;
        }

        private void LinkPharmacyWithExistingCountry(Hospital hospital, Country country)
        {
            if (country != null)
            {
                hospital.City.CountryId = country.Id;
                hospital.City.Country = country;
            }
        }
        public bool isUnique(Hospital hospital, IEnumerable<Hospital> hospitals)
        {
            foreach (Hospital existingHospital in hospitals)
            {
                if (existingHospital.isEqual(hospital))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
