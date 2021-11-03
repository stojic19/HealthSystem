using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ManagerReadRepository : ReadBaseRepository<int, Manager>, IManagerReadRepository
    {
        public ManagerReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
