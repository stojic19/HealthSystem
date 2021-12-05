using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model.DataObjects
{
    public class CategoriesSurvey
    {
        public int SurveyId { get; set; }
        public SurveySection doctorSection { get; set; }
        public SurveySection medicalStaffSection { get; set; }
        public SurveySection hospitalSection { get; set; }
    }
}
