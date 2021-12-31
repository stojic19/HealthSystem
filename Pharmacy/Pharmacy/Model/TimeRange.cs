using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class TimeRange
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        private TimeRange() { }
        public TimeRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Validate();
        }
        private void Validate()
        {
            if (StartDate > EndDate) throw new ArgumentException("Start Date should be less than End Date");
        }

        public bool OverlapsWith(TimeRange timeRange)
        {
            if (timeRange.Includes(StartDate)) return true;
            if (Includes(timeRange.StartDate)) return true;
            return false;
        }

        public bool Includes(DateTime dateTime)
        {
            return StartDate < dateTime && EndDate > dateTime;
        }

        public bool IsInPast()
        {
            return EndDate < DateTime.Now;
        }
    }
}
