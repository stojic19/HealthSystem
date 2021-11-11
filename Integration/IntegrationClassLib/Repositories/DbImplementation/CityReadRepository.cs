using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Integration.Repositories.DbImplementation
{
    public class CityReadRepository : ReadBaseRepository<int, City>, ICityReadRepository
    {
        public CityReadRepository(AppDbContext context) : base(context)
        {
        }
        public City GetByName(string Name)
        {
            DbSet<City> cities = GetAll();
            City existingCity = cities.FirstOrDefault(city => city.Name.Equals(Name));
            return existingCity;
        }
    }
}
