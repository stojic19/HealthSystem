using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;

namespace Pharmacy.MasterServices
{
    public class CityMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public CityMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public City GetCityByNameAndCountry(string cityName, string countryName)
        {
            var cityRepo = unitOfWork.GetRepository<ICityReadRepository>();
            IEnumerable<City> cities = cityRepo.GetByName(cityName);
            foreach (City city in cities)
            {
                if (city.Country.Name.Equals(countryName))
                {
                    return city;
                }
            }
            return null;
        }
    }
}
