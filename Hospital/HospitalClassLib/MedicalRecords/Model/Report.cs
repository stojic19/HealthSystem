using Hospital.Schedule.Model;
using System;

namespace Hospital.MedicalRecords.Model
{
    public class Report
    {
        public int Id { get; private set; }       
        public int ScheduledEventId { get; private set; }
        public ScheduledEvent ScheduledEvent { get; private set; }
        public string WrittenReport { get; private set; }     
        public DateTime CreatedDate { get; private set; }
        public Report()
        {
            WrittenReport = "I have been the doctor in charge of this patient since November 2010. I have" +
                            " seen the patient regularly since then, on average once or twice a year. For purposes " +
                            "of this medical report, I re - examined the patient today.";
            CreatedDate = DateTime.Now;
        }

    }
}
