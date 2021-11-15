using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories
{
    public interface ICountryReadRepository : IReadBaseRepository<int, Country>
    {
        public Country GetByName(string Name);
    }
}
