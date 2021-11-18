using Hospital.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class AnsweredSurvey
    {
       public int Id { get; set; }
       public IEnumerable<AnsweredQuestion> AnsweredQuestions  { get; set; }
       public DateTime AnsweredDate { get; set; }
       public int SurveyId { get; set; }
       public Survey Survey { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
