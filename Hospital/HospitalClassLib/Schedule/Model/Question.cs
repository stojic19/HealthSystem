using Hospital.SharedModel.Model.Enumerations;

namespace Hospital.Schedule.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public SurveyCategory Category { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
