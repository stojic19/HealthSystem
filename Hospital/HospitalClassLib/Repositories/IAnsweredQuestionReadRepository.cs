using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    interface IAnsweredQuestionReadRepository : IReadBaseRepository<int, AnsweredQuestion>
    {
    }
}
