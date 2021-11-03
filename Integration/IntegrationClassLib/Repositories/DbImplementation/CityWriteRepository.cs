using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class CityWriteRepository : WriteBaseRepository<City>, ICityWriteRepository
    {
        public CityWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
