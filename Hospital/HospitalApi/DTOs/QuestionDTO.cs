using Hospital.SharedModel.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public SurveyCategory Category { get; set; }
        public int SurveyId { get; set; }
    }
}