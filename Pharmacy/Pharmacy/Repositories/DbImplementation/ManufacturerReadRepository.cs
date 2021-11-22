using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repositories.DbImplementation
{
    public class ManufacturerReadRepository : ReadBaseRepository<int, Manufacturer>, IManufacturerReadRepository    
    {
        public ManufacturerReadRepository(AppDbContext context) : base(context)
        {
            
        }

        public Manufacturer GetManufacturerByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name == name);
        }
    }
}
