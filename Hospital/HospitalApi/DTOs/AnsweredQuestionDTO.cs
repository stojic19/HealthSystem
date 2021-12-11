using Hospital.SharedModel.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class AnsweredQuestionDTO
    {
        public int QuestionId { get; set; }
        public int Rating { get; set; }
        public SurveyCategory Type { get; set; }

    }
}