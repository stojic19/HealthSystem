using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class CityReadRepository : ReadBaseRepository<int, City>, ICityReadRepository
    {
        public CityReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
