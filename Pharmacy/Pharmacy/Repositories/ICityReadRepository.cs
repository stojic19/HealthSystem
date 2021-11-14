using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories
{
    public interface ICityReadRepository : IReadBaseRepository<int, City>
    {
        public IEnumerable<City> GetByName(string Name);
    }
}
