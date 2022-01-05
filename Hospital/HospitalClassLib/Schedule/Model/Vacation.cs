using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model
{
    public class Vacation : ValueObject
    {
        public VacationType Type { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Vacation() { }

        public Vacation(VacationType type, DateTime start, DateTime end)
        {
            this.Type = type;
            this.StartDate = start;
            this.EndDate = end;
            Validate();
        }

        public void Validate()
        {
            if (!Enum.IsDefined<VacationType>(Type) || (this.Type == VacationType.Vacation && this.GetDuration() > 7)
                || DateTime.Compare(this.EndDate, this.StartDate) < 0)
                throw new ArgumentException("Not Valid");
        }

        public double GetDuration() {
            return (this.EndDate - this.StartDate).TotalDays;
        }

        public Vacation ExtendPeriod(DateTime endTime)
        {
            return new Vacation(this.Type, this.StartDate, endTime);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return StartDate;
            yield return EndDate;
        }
    }
}
