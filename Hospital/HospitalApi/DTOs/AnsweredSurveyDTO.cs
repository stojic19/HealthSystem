using System;
using System.Collections.Generic;

namespace HospitalApi.DTOs
{
    public class AnsweredSurveyDTO
    {

        public IEnumerable<AnsweredQuestionDTO> questions { get; set; }
        public DateTime AnsweredDate { get; set; }

        public int SurveyId { get; set; }
        public int ScheduledEventId { get; set; }
    }
}