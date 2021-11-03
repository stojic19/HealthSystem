using Hospital.Model;
using Hospital.Repositories.Base;
using System.Collections.Generic;

namespace Hospital.Repositories
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        IEnumerable<City> GetAllByCountryId(int countryId);
    }
}
