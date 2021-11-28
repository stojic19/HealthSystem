using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class CityWriteRepository : WriteBaseRepository<City>, ICityWriteRepository
    {
        public CityWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
