using System.Collections.Generic;
using System.Linq;
using Integration.Pharmacies.Repository;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using Integration.Shared.Repository.Base;
using Integration.Shared.Service;
using Microsoft.EntityFrameworkCore;

namespace Integration.Pharmacies.Service
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
        public IEnumerable<Model.Pharmacy> GetPharmacies()
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            IEnumerable<Model.Pharmacy> pharmacies = pharmacyRepo.GetAll().Include(x => x.City).ThenInclude(x => x.Country);
            return pharmacies;
        }
        public Model.Pharmacy GetPharmacyById(int id)
        {
            IEnumerable<Model.Pharmacy> pharmacies = GetPharmacies();
            Model.Pharmacy pharmacy = pharmacies.FirstOrDefault(pharmacy => pharmacy.Id == id);
            return pharmacy;
        }
        public void SavePharmacy(Model.Pharmacy pharmacy)
        {
            City existingCity = _cityMasterService.GetCityByNameAndCountry(pharmacy.City.Name, pharmacy.City.Country.Name);
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            Country country = countryRepo.GetByName(pharmacy.City.Country.Name);
            pharmacyMicroService.LinkPharmacyWithExistingEntities(pharmacy, existingCity, country);
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyWriteRepository>();
            pharmacyRepo.Add(pharmacy);
        }

        public void UpdatePharmacy(Model.Pharmacy pharmacy)
        {
            City existingCity = _cityMasterService.GetCityByNameAndCountry(pharmacy.City.Name, pharmacy.City.Country.Name);
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            Country country = countryRepo.GetByName(pharmacy.City.Country.Name);
            pharmacyMicroService.LinkPharmacyWithExistingEntities(pharmacy, existingCity, country);
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyWriteRepository>();
            pharmacyRepo.Update(pharmacy);
        }

        public Model.Pharmacy FindPharmacyByName(string pharmacyName)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Model.Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Model.Pharmacy existingPharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.Name.Equals(pharmacyName));
            return existingPharmacy;
        }
        public Model.Pharmacy FindPharmacyByApiKey(string apiKey)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            DbSet<Model.Pharmacy> existingPharmacies = pharmacyRepo.GetAll();
            Model.Pharmacy pharmacy = existingPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.ToString().Equals(apiKey));
            return pharmacy;
        }
        public bool isUnique(Model.Pharmacy pharmacy)
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            IEnumerable<Model.Pharmacy> existingPharmacies = pharmacyRepo.GetAll().Include(x => x.City).ThenInclude(x => x.Country);
            return pharmacyMicroService.isUnique(pharmacy, existingPharmacies);
        }
    }
}
