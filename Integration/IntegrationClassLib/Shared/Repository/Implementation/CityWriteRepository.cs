using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class CityWriteRepository : WriteBaseRepository<City>, ICityWriteRepository
    {
        public CityWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
