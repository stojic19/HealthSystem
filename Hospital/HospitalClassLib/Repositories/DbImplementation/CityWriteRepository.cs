using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class CityWriteRepository : WriteBaseRepository<City>, ICityWriteRepository
    {
        public CityWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
