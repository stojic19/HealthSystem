using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Database.EfStructures;
using Hospital.EventStoring.Model;
using Hospital.EventStoring.Model.Enumerations;
using Hospital.EventStoring.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.EventStoring.Repository.Implementation
{
    public class StoredEventReadRepository : ReadBaseRepository<Guid, StoredEvent>, IStoredEventReadRepository
    {
        public StoredEventReadRepository(AppDbContext context) : base(context)
        {
        }

        private IEnumerable<StoredEvent> GetEventsStartedScheduling()
        {
            return GetAll().Where(se =>
                DateTime.Compare(se.Time, DateTime.Now.AddDays(-30)) >= 0 && se.Step.Equals(Step.StartScheduling));
        }

        public IEnumerable<int> GetStatisticsPerPartOfDay()
        {
            var result = new List<int>();
            result.Add(GetEventsStartedScheduling().Count(se =>
                    se.Time.TimeOfDay >= new TimeSpan(0, 0, 0) && se.Time.TimeOfDay < new TimeSpan(8, 0, 0)));
            result.Add(GetEventsStartedScheduling().Count(se =>
                    se.Time.TimeOfDay >= new TimeSpan(8, 0, 0) && se.Time.TimeOfDay < new TimeSpan(12, 0, 0)));
            result.Add(GetEventsStartedScheduling().Count(se =>
                se.Time.TimeOfDay >= new TimeSpan(12, 0, 0) && se.Time.TimeOfDay < new TimeSpan(16, 0, 0)));
            result.Add(GetEventsStartedScheduling().Count(se =>
                    se.Time.TimeOfDay >= new TimeSpan(16, 0, 0) && se.Time.TimeOfDay < new TimeSpan(20, 0, 0)));
            result.Add(GetEventsStartedScheduling().Count(se =>
                     se.Time.TimeOfDay >= new TimeSpan(20, 0, 0) && se.Time.TimeOfDay < new TimeSpan(23, 59, 59)));
            return result;
        }

        public IEnumerable<int> GetStatisticsPerDayOfWeek(DayOfWeek day)
        {
            var result = new List<int>();
            var startScheduling = 0;
            var schedule = 0;
            foreach (var eventi in GetEventsStartedSchedulingInLastWeek())
            {
                if (!eventi.Time.DayOfWeek.Equals(day)) continue;
                if (eventi.Step == Step.StartScheduling) startScheduling++;
                if (eventi.Step == Step.Schedule) schedule++;
            }

            result.Add(schedule);
            result.Add(startScheduling - schedule);
            return result;
        }

        private IEnumerable<StoredEvent> GetEventsStartedSchedulingInLastWeek()
        {
            return GetAll().Where(se =>
                DateTime.Compare(se.Time, DateTime.Now.AddDays(-7)) >= 0 &&
                (se.Step.Equals(Step.StartScheduling) || se.Step.Equals(Step.Schedule)));
        }

        private IEnumerable<StoredEvent> GetEventsStartedSchedulingInLastYear()
        {
            return GetAll().Where(se =>
                DateTime.Compare(se.Time, DateTime.Now.AddYears(-1)) >= 0
                && se.Step.Equals(Step.StartScheduling));
        }

        private int GetEventsStartedSchedulingInLastYearPerMonth(int month)
        {
            return GetEventsStartedSchedulingInLastYear().Count(se => se.Time.Month == month);
        }

        public IEnumerable<int> GetStatisticsPerMonths()
        {
            var result = new List<int>();
            for (var i = 1; i <= 12; i++)
            {
                result.Add(GetEventsStartedSchedulingInLastYearPerMonth(i));
            }

            return result;
        }

        public IEnumerable<StoredEvent> GetAllByStep(Step step)
        {
            return GetAll().Where(se => se.Step.Equals(step));
        }

    }
}
