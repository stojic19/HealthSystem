using System;
using System.Collections.Generic;


namespace Hospital.Model
{
    public class AnsweredSurvey
    {
        public int Id { get; set; }
        public IEnumerable<AnsweredQuestion> AnsweredQuestions { get; set; }
        public DateTime AnsweredDate { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int ScheduledEventId { get; set; }
        public ScheduledEvent ScheduledEvent { get; set; }
        public Patient Patient { get; set; }


        
    }
}
