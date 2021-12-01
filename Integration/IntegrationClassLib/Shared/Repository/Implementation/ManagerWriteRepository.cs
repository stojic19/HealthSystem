using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class ManagerWriteRepository : WriteBaseRepository<Manager>, IManagerWriteRepository
    {
        public ManagerWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
