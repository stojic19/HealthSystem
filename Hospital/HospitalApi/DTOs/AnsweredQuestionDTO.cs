using Hospital.Model;
using Hospital.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class AnsweredQuestionDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int Rating { get; set; }
        public SurveyCategory Category { get; set; }
        public int AnsweredSurveyId { get; set; }
        public AnsweredSurveyDTO AnsweredSurvey { get; set; }
    }
}