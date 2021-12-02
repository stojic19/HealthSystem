using System.Collections.Generic;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Service
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
            foreach(City city in cities)
            {
                if(city.Country.Name.Equals(countryName))
                {
                    return city;
                }
            }
            return null;
        }
    }
}
