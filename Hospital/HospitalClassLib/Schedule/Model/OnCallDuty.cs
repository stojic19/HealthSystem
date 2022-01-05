using Hospital.SharedModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model
{
    public class OnCallDuty
    {
        public int Id { get; private set; }
        public int Month { get; private set; }
        public int Week { get; private set; }
        public ICollection<Doctor> DoctorsOnDuty { get; private set; }

        public OnCallDuty(int month, int week, ICollection<Doctor> doctorsOnDuty)
        {
            Month = month;
            Week = week;
            DoctorsOnDuty = doctorsOnDuty;
            this.Validate();
        }
        public OnCallDuty() { }

        private void Validate()
        {
            if (this.DoctorsOnDuty.Count == 0 || Month > 12 || Month < 1 || Week < 1 || Week > 4)
                throw new ArgumentException("Not Valid");
        }

        public void AddDoctor(Doctor newDoctor)
        {
            this.DoctorsOnDuty.Add(newDoctor);
        }

        public void RemoveDoctor(Doctor doctor)
        {
            this.DoctorsOnDuty.Remove(doctor);
        }
    }
}
