using System;
using System.Collections.Generic;
using Hospital.Medical_records.Model;

namespace Hospital.Schedule.Model
{
    public class AnsweredSurvey
    {
       public int Id { get; set; }
       public IEnumerable<AnsweredQuestion> AnsweredQuestions  { get; set; }
       public DateTime AnsweredDate { get; set; }
       public int SurveyId { get; set; }
       public Survey Survey { get; set; }
       public Patient Patient { get; set; }
    }
}
