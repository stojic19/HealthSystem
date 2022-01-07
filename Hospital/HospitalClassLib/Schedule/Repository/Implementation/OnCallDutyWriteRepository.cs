using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Repository.Implementation
{
    public class OnCallDutyWriteRepository : WriteBaseRepository<OnCallDuty>, IOnCallDutyWriteRepository
    {
        public OnCallDutyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
