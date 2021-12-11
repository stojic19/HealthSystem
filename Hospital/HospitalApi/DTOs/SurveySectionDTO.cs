using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace HospitalApi.DTOs
{
    public class SurveySectionDTO
    {

        public string Name { get; set; }
        public List<QuestionDTO> questions { get; set; }

        public SurveyCategory category;

    }
}