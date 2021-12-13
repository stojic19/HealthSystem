using System.Collections.Generic;

namespace Hospital.Schedule.Model.Wrappers
{
    public class QuestionStatistic
    {
        public double AverageRating { get; set; }
        public int QuestionId { get; set; }
        public List<double> RatingCounts { get; set; }
    }
}
