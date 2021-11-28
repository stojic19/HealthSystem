using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class CountryWriteRepository : WriteBaseRepository<Country>, ICountryWriteRepository
    {
        public CountryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
