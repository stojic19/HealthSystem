﻿using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Services
{
    public class CityService
    {
        private readonly IUnitOfWork unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
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
