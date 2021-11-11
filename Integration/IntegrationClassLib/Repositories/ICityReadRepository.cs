using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        public City GetByName(string Name);
    }
}
