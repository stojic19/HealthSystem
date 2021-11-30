using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository
{
    public interface ICountryReadRepository : IReadBaseRepository<int, Country>
    {
    }
}
