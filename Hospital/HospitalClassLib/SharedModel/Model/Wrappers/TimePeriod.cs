using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.SharedModel.Model.Wrappers
{
    public class TimePeriod : ValueObject
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public TimePeriod(DateTime start, DateTime end)
        {
            this.StartTime = start;
            this.EndTime = end;
            try
            {
                Validate();
            }
            catch (Exception ex) { }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartTime;
            yield return EndTime;
        }

        public void Validate() {
            if(DateTime.Compare(this.EndTime, this.StartTime) <= 0)
                throw new Exception("Not Valid");
        }

        public bool OverlapsWith(TimePeriod timePeriod)
        {

            if (DateTime.Compare(timePeriod.StartTime, this.StartTime) == 0)
                return true;

            if (DateTime.Compare(timePeriod.StartTime, this.StartTime) < 0)
            {
                if (DateTime.Compare(timePeriod.EndTime, this.StartTime) > 0)
                    return true;
            }

            if (DateTime.Compare(timePeriod.StartTime, this.StartTime) > 0
                && (DateTime.Compare(this.EndTime, timePeriod.EndTime) > 0))
                return true;

            return false;
        }

        public bool OverlapsWith(IEnumerable<TimePeriod> timePeriods)
        {

            foreach (TimePeriod timePeriod in timePeriods)
            {
                if (this.OverlapsWith(timePeriod))
                    return true;
            }
            return false;
        }

        public bool IsInPast()
        {
            if (DateTime.Compare(this.EndTime, DateTime.Now) < 0)
                return true;
            else
                return false;
        }

        public bool Includes(DateTime dateTime)
        {
            if (DateTime.Compare(dateTime, this.StartTime) >= 0 || DateTime.Compare(dateTime, this.EndTime) <= 0)
                return true;
            return false;
        }

        public IEnumerable<TimePeriod> Split(double duration)
        {

            var possibleTerms = new List<TimePeriod>();
            TimeSpan wantedInterval = this.EndTime - this.StartTime;
            double intervalInHours = wantedInterval.TotalHours;

            var term = new TimePeriod(this.StartTime, this.StartTime.AddHours(duration));
            possibleTerms.Add(term);

            for (int i = 0; i < intervalInHours / duration - 2; i++)
            {
                term = new TimePeriod(possibleTerms.ElementAt(i).EndTime, term.StartTime.AddHours(duration));
                possibleTerms.Add(term);
            }

            return possibleTerms;
        }

    }
}
