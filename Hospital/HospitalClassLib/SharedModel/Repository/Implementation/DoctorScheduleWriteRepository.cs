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
    class DoctorScheduleWriteRepository : WriteBaseRepository<DoctorSchedule>, IDoctorScheduleWriteRepository
    {
        public DoctorScheduleWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
