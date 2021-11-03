using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ManagerWriteRepository : WriteBaseRepository<Manager>, IManagerWriteRepository
    {
        public ManagerWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
