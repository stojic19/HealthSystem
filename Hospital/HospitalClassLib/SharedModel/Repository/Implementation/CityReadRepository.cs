using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class CityReadRepository : ReadBaseRepository<int, City>, ICityReadRepository
    {
        private readonly AppDbContext _context;

        public CityReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
