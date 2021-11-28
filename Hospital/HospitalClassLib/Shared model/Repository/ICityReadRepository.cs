using System.Collections.Generic;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        IEnumerable<City> GetAllByCountryId(int countryId);
    }
}
