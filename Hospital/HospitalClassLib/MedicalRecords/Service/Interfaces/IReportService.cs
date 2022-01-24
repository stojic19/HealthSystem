using Hospital.MedicalRecords.Model;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Service.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Report> GetAllReports(string userName);
    }
}
