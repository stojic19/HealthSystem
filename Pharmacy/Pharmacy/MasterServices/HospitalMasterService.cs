using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.MicroServices;

namespace Pharmacy.MasterServices
{
    public class HospitalMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        private CityMasterService _cityMasterService;
        private HospitalMicroService hospitalMicroService;
        public HospitalMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _cityMasterService = new CityMasterService(unitOfWork);
            hospitalMicroService = new HospitalMicroService();
        }
        public void SaveHospital(Hospital hospital)
        {
            City existingCity = _cityMasterService.GetCityByNameAndCountry(hospital.City.Name, hospital.City.Country.Name);
            var countryRepo = unitOfWork.GetRepository<ICountryReadRepository>();
            Country country = countryRepo.GetByName(hospital.City.Country.Name);
            hospitalMicroService.LinkHospitalWithExistingEntities(hospital, existingCity, country);
            var hospitalRepo = unitOfWork.GetRepository<IHospitalWriteRepository>();
            hospitalRepo.Add(hospital);
        }
        public Hospital FindHospitalByApiKey(string apiKey)
        {
            var hospitalRepo = unitOfWork.GetRepository<IHospitalReadRepository>();
            DbSet<Hospital> existingHospitals = hospitalRepo.GetAll();
            Hospital hospital = existingHospitals.FirstOrDefault(hospital => hospital.ApiKey.ToString().Equals(apiKey));
            return hospital;
        }
        public bool isUnique(Hospital hospital)
        {
            var hospitalRepo = unitOfWork.GetRepository<IHospitalReadRepository>();
            IEnumerable<Hospital> existingHospitals = hospitalRepo.GetAll().Include(x => x.City).ThenInclude(x => x.Country);
            return hospitalMicroService.isUnique(hospital, existingHospitals);
        }
    }
}
