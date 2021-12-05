using Hospital.Model.Enumerations;
using System.Collections.Generic;

namespace Hospital.Model.DataObjects
{
    public class SurveySection
    {
        public List<Question> questions { get; set; }

        public SurveyCategory category;
    }
}
