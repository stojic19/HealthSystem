using Hospital.Database.EfStructures;
using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Model;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class CityWriteRepository : WriteBaseRepository<City>, ICityWriteRepository
    {
        public CityWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
