using Hospital.MedicalRecords.Model;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Service.Interfaces
{
    public interface IReportService
    {
        public IEnumerable<Report> GetAllReports(string userName);
        public Report GetReport(int eventId);
        public IEnumerable<Report> GetAllReports();
    }
}
