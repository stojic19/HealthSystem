using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class AnsweredSurveyDTO
    {
        public int Id { get; set; }
        public IEnumerable<AnsweredQuestionDTO> AnsweredQuestions { get; set; }
        public DateTime AnsweredDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
