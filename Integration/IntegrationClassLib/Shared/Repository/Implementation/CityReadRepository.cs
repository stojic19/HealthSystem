using System.Collections.Generic;
using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Shared.Repository.Implementation
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
            foreach(City city in allCities)
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
