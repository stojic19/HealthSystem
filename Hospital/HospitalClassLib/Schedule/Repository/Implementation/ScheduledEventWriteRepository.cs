﻿using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventWriteRepository : WriteBaseRepository<ScheduledEvent>, IScheduledEventWriteRepository
    {
        public ScheduledEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
