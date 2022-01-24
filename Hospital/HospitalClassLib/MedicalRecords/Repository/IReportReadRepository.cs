using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Repository
{
    public interface IReportReadRepository : IReadBaseRepository<int, Report>
    {

        public Report GetReport(int userId, int eventId);
        public IEnumerable<Report> GetAllReports(string userName);
    }
}
