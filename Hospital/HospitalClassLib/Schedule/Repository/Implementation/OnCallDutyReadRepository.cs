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
    public class OnCallDutyReadRepository : ReadBaseRepository<int, OnCallDuty>, IOnCallDutyReadRepository
    {
        public OnCallDutyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
