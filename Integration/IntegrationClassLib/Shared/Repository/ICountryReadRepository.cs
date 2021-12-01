using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository
{
    public interface ICountryReadRepository : IReadBaseRepository<int, Country>
    {
        public Country GetByName(string Name);
    }
}
