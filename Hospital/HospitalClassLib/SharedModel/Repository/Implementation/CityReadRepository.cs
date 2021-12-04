using Hospital.Database.EfStructures;
using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.SharedModel.Repository.Implementation
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
