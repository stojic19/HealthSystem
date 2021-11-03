using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class CountryReadRepository : ReadBaseRepository<int, Country>, ICountryReadRepository
    {
        public CountryReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
