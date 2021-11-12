using Integration.Model;
using Integration.Repositories.Base;
using System.Collections.Generic;

namespace Integration.Repositories
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        public IEnumerable<City> GetByName(string Name);
    }
}
