using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model
{
    public class DoctorsScheduleReport : ValueObject
    {
        public int NumOfAppointments { get; private set; }
        public int NumOfOnCallShifts { get; private set; }
        public int NumOfPatients { get; private set; }
        public TimePeriod DateRange { get; }

        public DoctorsScheduleReport() { }

        public DoctorsScheduleReport(int numOfAppointments, int numOfOnCallShifts, int numOfPatients, TimePeriod dateRange)
        {
            NumOfAppointments = numOfAppointments;
            NumOfOnCallShifts = numOfOnCallShifts;
            NumOfPatients = numOfPatients;
            DateRange = dateRange;
            Validate();
        }

        private void Validate()
        {
            if (double.IsNegative(NumOfAppointments) || double.IsNegative(NumOfOnCallShifts) || double.IsNegative(NumOfPatients))
                throw new Exception("Not Valid");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NumOfAppointments;
            yield return NumOfOnCallShifts;
            yield return NumOfPatients;
        }
    }
}
