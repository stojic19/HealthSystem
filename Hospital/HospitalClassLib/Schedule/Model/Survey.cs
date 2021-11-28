using System;
using System.Collections.Generic;

namespace Hospital.Schedule.Model
{
    public class Survey
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<AnsweredSurvey> AnsweredSurveys { get; set; }
    }
}
