using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class ManagerReadRepository : ReadBaseRepository<int, Manager>, IManagerReadRepository
    {
        public ManagerReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
