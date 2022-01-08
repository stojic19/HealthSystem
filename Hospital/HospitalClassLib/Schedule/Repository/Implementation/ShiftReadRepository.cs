using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ShiftReadRepository : ReadBaseRepository<int, Shift>, IShiftReadRepository
    {
        private readonly AppDbContext _context;
        public ShiftReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Shift GetShiftForDoctor(int doctorId)
        {
           return _context.Doctors.Where(d => d.Id == doctorId).Include(d => d.Shift).First().Shift;
        }
    }
}
