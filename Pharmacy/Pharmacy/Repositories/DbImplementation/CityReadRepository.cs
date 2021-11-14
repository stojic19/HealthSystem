using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class CityReadRepository : ReadBaseRepository<int, City>, ICityReadRepository
    {
        public CityReadRepository(AppDbContext context) : base(context)
        {
        }
        public IEnumerable<City> GetByName(string Name)
        {
            IEnumerable<City> allCities = GetAll().Include(x => x.Country);
            List<City> cities = new List<City>();
            foreach (City city in allCities)
            {
                if (city.Name.Equals(Name))
                {
                    cities.Add(city);
                }
            }
            return cities;
        }
    }
}
