using Hospital.Schedule.Model;

namespace Hospital.MedicalRecords.Model
{
    public class Report
    {
        public int Id { get; set; }
        
        public int ScheduledEventId { get; set; }
        public ScheduledEvent ScheduledEvent { get; set; }
        public string WrittenReport { get; set; }
        
        public Report()
        {
            WrittenReport = "I have been the doctor in charge of this patient since November 2010. I have"
                +"seen the patient regularly since then, on average once or twice a year. For purposes " +
                "of this medical report, I re - examined the patient today.";
        }

    }
}
