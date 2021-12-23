using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IAnsweredSurveyReadRepository : IReadBaseRepository<int, AnsweredSurvey>
    {
        public int GetSurveyId(int id);
    }
}
