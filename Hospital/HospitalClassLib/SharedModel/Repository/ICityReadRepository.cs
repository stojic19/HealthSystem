using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Model;
using System.Collections.Generic;

namespace Hospital.SharedModel.Repository
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        IEnumerable<City> GetAllByCountryId(int countryId);
    }
}
