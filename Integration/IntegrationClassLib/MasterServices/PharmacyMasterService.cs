using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using Integration.MasterServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.MicroServices;

namespace Integration.MasterServices
{
    public class PharmacyMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        private CityMasterService _cityMasterService;
        private PharmacyMicroSerivce pharmacyMicroService;
        public PharmacyMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _cityMasterService = new CityMasterService(unitOfWork);
            pharmacyMicroService = new PharmacyMicroSerivce();
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
            City existingCity = _cityMasterService.GetCityByNameAndCountry(pharmacy.City.Name, pharmacy.City.Country.Name);
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            Country country = countryRepo.GetByName(pharmacy.City.Country.Name);
            pharmacyMicroService.LinkPharmacyWithExistingEntities(pharmacy, existingCity, country);
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyWriteRepository>();
            pharmacyRepo.Add(pharmacy);
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
        public bool isUnique(Pharmacy pharmacy)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            IEnumerable<Pharmacy> existingPharmacies = pharmacyRepo.GetAll().Include(x => x.City).ThenInclude(x => x.Country);
            return pharmacyMicroService.isUnique(pharmacy, existingPharmacies);
        }
    }
}
