using Hospital.SharedModel.Model.Enumerations;

namespace Hospital.Schedule.Model
{
    public class AnsweredQuestion
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int Rating { get; set; }
        public SurveyCategory Category { get; set; }
        public int AnsweredSurveyId { get; set; }
        public AnsweredSurvey AnsweredSurvey { get; set; }
    }
}
