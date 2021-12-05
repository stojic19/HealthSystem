using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
