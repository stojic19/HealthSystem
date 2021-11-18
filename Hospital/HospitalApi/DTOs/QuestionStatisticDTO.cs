using Hospital.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class QuestionStatisticDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public SurveyCategory Category { get; set; }
        public int SurveyId { get; set; }
        public double AverageRating { get; set; }
        public IEnumerable<double> NumberOfEachRating { get; set; }
    }
}
