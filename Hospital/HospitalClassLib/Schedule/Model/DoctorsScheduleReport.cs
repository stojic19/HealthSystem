using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model
{
    public class DoctorsScheduleReport
    {
        public int NumOfAppointments { get; set; }
        public int NumOfOnCallShifts { get; set; }
        public int NumOfPatients { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DoctorsScheduleReport() { }

        public DoctorsScheduleReport(int numOfAppointments, int numOfOnCallShifts, int numOfPatients, DateTime startTime, DateTime endTime)
        {
            NumOfAppointments = numOfAppointments;
            NumOfOnCallShifts = numOfOnCallShifts;
            NumOfPatients = numOfPatients;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
