using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IReportWriteRepository : IWriteBaseRepository<Report>
    {
        public void CreateNewReport(Report newReport);
    }
}
