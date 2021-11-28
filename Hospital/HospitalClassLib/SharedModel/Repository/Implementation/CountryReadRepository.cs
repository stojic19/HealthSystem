using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class CountryReadRepository : ReadBaseRepository<int, Country>, ICountryReadRepository
    {
        public CountryReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
