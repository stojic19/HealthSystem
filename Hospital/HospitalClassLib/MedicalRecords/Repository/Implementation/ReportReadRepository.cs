using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class ReportReadRepository : ReadBaseRepository<int, Report>, IReportReadRepository
    {
        public ReportReadRepository(AppDbContext context) : base(context)
        {
        }
    
        public IEnumerable<Report> GetAllReports(string userName) 
        { 
            return (List<Report>)GetAll().Where(x => x.ScheduledEvent.Patient.UserName == userName);
        }

        public Report GetReport(int eventId)
        {
            return GetAll().Include(x => x.ScheduledEvent.Doctor).FirstOrDefault(x => x.ScheduledEventId == eventId); 
        }

    }
}
