using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Repositories.DbImplementation
{
    public class CityReadRepository : ReadBaseRepository<int, City>, ICityReadRepository
    {
        private readonly AppDbContext _context;

        public CityReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<City> GetAllByCountryId(int countryId)
        {
            return _context.Set<City>().Where(x => x.CountryId == countryId);
        }
    }
}
