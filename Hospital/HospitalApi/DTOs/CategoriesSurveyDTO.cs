using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class CategoriesSurveyDTO
    {
        public int SurveyId { get; set; }
        public SurveySectionDTO doctorSection { get; set; }
        public SurveySectionDTO medicalStaffSection { get; set; }
        public SurveySectionDTO hospitalSection { get; set; }

    }
}