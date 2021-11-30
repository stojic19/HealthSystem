using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class CountryWriteRepository : WriteBaseRepository<Country>, ICountryWriteRepository
    {
        public CountryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
