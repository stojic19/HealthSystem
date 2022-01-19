using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System.Linq;

namespace Hospital.Schedule.Repository.Implementation
{
    public class AnsweredSurveyReadRepository : ReadBaseRepository<int, AnsweredSurvey>, IAnsweredSurveyReadRepository
    {
        private readonly AppDbContext _context;

        public AnsweredSurveyReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public int GetSurveyId(int evenId)
        {
            return   GetAll().Where(x => x.ScheduledEventId == evenId)
                            .Select(x => x.Id)
                            .FirstOrDefault();
        }
    }
}
