using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class CityReadRepository : ReadBaseRepository<int, City>, ICityReadRepository
    {
        public CityReadRepository(AppDbContext context) : base(context)
        {
        }

        public bool CheckIfExists(string name)
        {
            var foundCity = GetAll()
                .FirstOrDefault(x => x.Name == name);
            return foundCity != null;
        }

        public City GetCityByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name == name);
        }
    }
}
