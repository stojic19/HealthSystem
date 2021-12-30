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
        public int Id { get; set; }
        public DateTime OnCallDutyDate { get; }
        public List<Doctor> DoctorsOnDuty { get; }

        public OnCallDuty() { }

        public OnCallDuty(DateTime onCallDutyDate, List<Doctor> doctorsOnDuty)
        {
            OnCallDutyDate = onCallDutyDate;
            DoctorsOnDuty = doctorsOnDuty;
            this.Validate();
        }

        private void Validate()
        {
            if(this.DoctorsOnDuty.Count == 0 || DateTime.Compare(this.OnCallDutyDate, DateTime.Now) < 0 )
                throw new Exception("Not Valid");
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
