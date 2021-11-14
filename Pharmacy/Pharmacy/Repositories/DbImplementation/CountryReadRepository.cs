using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories.DbImplementation
{
    public class CountryReadRepository : ReadBaseRepository<int, Country>, ICountryReadRepository
    {
        public CountryReadRepository(AppDbContext context) : base(context)
        {
        }
        public Country GetByName(string Name)
        {
            DbSet<Country> countries = GetAll();
            Country existingCountry = countries.FirstOrDefault(city => city.Name.Equals(Name));
            return existingCountry;
        }
    }
}
