using System.Collections.Generic;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        public IEnumerable<City> GetByName(string Name);
    }
}
