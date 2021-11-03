using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class CountryWriteRepository : WriteBaseRepository<Country>, ICountryWriteRepository
    {
        public CountryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
