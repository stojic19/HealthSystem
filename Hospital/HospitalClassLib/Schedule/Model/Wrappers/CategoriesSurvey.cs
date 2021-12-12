using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model.Wrappers
{
    public class CategoriesSurvey
    {
        public int SurveyId { get; set; }
        public SurveySection DoctorSection { get; set; }
        public SurveySection MedicalStaffSection { get; set; }
        public SurveySection HospitalSection { get; set; }
    }
}
