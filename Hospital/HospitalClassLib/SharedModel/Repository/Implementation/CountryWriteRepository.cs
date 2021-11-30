using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class CountryWriteRepository : WriteBaseRepository<Country>, ICountryWriteRepository
    {
        public CountryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
