using Hospital.SharedModel.Model.Enumerations;

namespace Hospital.Schedule.Model.Wrappers
{
    public class CategoryStatistic
    {
        public double AverageRating { get; set; }
        public SurveyCategory Category { get; set; }
    }
}
