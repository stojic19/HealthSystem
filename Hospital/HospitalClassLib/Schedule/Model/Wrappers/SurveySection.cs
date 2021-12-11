using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace Hospital.Schedule.Model.Wrappers
{
    public class SurveySection
    {
        public List<Question> Questions { get; set; }
        public SurveyCategory Category { get; set; }
    }
}
