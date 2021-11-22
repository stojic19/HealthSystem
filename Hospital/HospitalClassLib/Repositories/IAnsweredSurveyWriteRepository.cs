using Hospital.Model;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public interface IAnsweredSurveyWriteRepository : IWriteBaseRepository<AnsweredSurvey>
    {
        void create(IEnumerable<AnsweredQuestion> answers, Guid userId, int scheduledEventId);
    }
}
