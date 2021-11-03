using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface ICountryReadRepository : IReadBaseRepository<int, Country>
    {
    }
}
