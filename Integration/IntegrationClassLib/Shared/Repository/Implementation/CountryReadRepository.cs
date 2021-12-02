﻿using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Shared.Repository.Implementation
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
