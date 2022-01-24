using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.SharedModel.Model
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public ICollection<OnCallDuty> OnCallDuties { get; set; }
        public ICollection<Vacation> Vacations { get; set; }

        public DoctorSchedule() { }

        public DoctorSchedule(int id) {
            Id = id;
        }

        public DoctorSchedule(ICollection<OnCallDuty> onCallDuties, ICollection<Vacation> vacations) {
            OnCallDuties = onCallDuties;
            Vacations = vacations;
        }

        public void AddOnCallDuty(OnCallDuty onCallDuty) {
            OnCallDuties.Add(onCallDuty);
        }

        public void RemoveOnCallDuty(OnCallDuty onCallDuty)
        {
            OnCallDuties.Remove(onCallDuty);
        }

        public void AddVacation(Vacation vacation) {
            Vacations.Add(vacation);
        }

        public void RemoveVacation(Vacation vacation)
        {
            Vacations.Remove(vacation);
        }

        public bool IsAvailable(TimePeriod timePeriod)
        {
            if (IsOnVacation(timePeriod))
            {
                return false;
            }
            return true;
        }

        private bool IsOnVacation(TimePeriod timePeriod)
        {

            foreach (Vacation vacation in Vacations)
            {
                TimePeriod vacationPeriod = new TimePeriod(vacation.StartDate, vacation.EndDate);
                if (timePeriod.OverlapsWith(vacationPeriod))
                {
                    return true;
                }
            }
            return false;
        }

        public int NumberOfOnCallDuties(TimePeriod timePeriod)
        {

            List<OnCallDuty> foundDuties = new List<OnCallDuty>();

            foreach (var duty in OnCallDuties)
            {
                var day = (1 + (duty.Week - 1) * 7);
                DateTime startOfDuty = new DateTime(2022, duty.Month, day);
                DateTime endOfDuty = new DateTime(2022, duty.Month, day + 5);
                if (timePeriod.OverlapsWith(new TimePeriod(startOfDuty, endOfDuty)))
                {
                    foundDuties.Add(duty);
                }
            }

            return foundDuties.Count;
        }

    }
}
