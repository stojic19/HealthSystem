using Hospital.Database.EfStructures;
using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Model;
using System.Collections.Generic;
using System.Linq;
using Hospital.SharedModel.Repository;

namespace Hospital.Repositories.DbImplementation
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
