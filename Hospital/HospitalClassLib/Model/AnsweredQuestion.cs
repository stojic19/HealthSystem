using Hospital.Model.Enumerations;


namespace Hospital.Model
{
    public class AnsweredQuestion
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int Rating { get; set; }
        public SurveyCategory Category { get; set; }
    }
}
