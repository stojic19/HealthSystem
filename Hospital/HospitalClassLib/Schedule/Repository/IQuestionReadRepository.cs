using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IQuestionReadRepository : IReadBaseRepository<int, Question>
    {
    }
}
