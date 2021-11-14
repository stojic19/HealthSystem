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
    public class HospitalService
    {
        private readonly IUnitOfWork unitOfWork;
        private CityService cityService;
        private HospitalMicroService hospitalMicroService;
        public HospitalService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            cityService = new CityService(unitOfWork);
            hospitalMicroService = new HospitalMicroService();
        }
        public void SaveHospital(Hospital hospital)
        {
            City existingCity = cityService.GetCityByNameAndCountry(hospital.City.Name, hospital.City.Country.Name);
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
