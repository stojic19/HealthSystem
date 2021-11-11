using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Services
{
    public class PharmacyService
    {
        private readonly IUnitOfWork unitOfWork;
        public PharmacyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Pharmacy> GetPharmacies()
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            IEnumerable<Pharmacy> pharmacies = pharmacyRepo.GetAll().Include(x => x.City).ThenInclude(x => x.Country);
            return pharmacies;
        }
        public Pharmacy GetPharmacyById(int id)
        {
            IEnumerable<Pharmacy> pharmacies = GetPharmacies();
            Pharmacy pharmacy = pharmacies.FirstOrDefault(pharmacy => pharmacy.Id == id);
            return pharmacy;
        }
        public void SavePharmacy(Pharmacy pharmacy)
        {
            var cityRepo = unitOfWork.GetRepository<ICityReadRepository>();
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            City existingCity = cityRepo.GetByName(pharmacy.City.Name);
            Country country = countryRepo.GetByName(pharmacy.City.Country.Name);
            LinkEntities(pharmacy, country, existingCity);
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyWriteRepository>();
            pharmacyRepo.Add(pharmacy);
        }
        private static void LinkEntities(Pharmacy pharmacy, Country country, City existingCity)
        {
            if (existingCity != null)
            {
                LinkCity(pharmacy, country, existingCity);
            }
            else if (country != null)
            {
                LinkCountry(pharmacy.City, country);
            }
        }
        private static void LinkCountry(City city, Country country)
        {
            city.Country = country;
            city.CountryId = country.Id;
        }
        private static void LinkCity(Pharmacy pharmacy, Country country, City existingCity)
        {
            LinkCountry(existingCity, country);
            pharmacy.CityId = existingCity.Id;
            pharmacy.City = existingCity;
        }
        public Pharmacy FindPharmacyByName(string pharmacyName)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Pharmacy existingPharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyName));
            return existingPharmacy;
        }
        public Pharmacy FindPharmacyByApiKey(string apiKey)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Pharmacy pharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.ToString().Equals(apiKey));
            return pharmacy;
        }
    }
}
