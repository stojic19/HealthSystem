using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class ManagerWriteRepository : WriteBaseRepository<Manager>, IManagerWriteRepository
    {
        public ManagerWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
