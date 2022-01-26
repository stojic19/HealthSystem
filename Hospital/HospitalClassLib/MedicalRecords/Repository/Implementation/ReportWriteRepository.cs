using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{

    public class ReportWriteRepository : WriteBaseRepository<Report>, IReportWriteRepository
    {
        private readonly AppDbContext _context;
        public ReportWriteRepository (AppDbContext context) : base(context)
        {
            this._context = context;
        }
         public void CreateNewReport(Report newReport)
        {
            Add(newReport);
        }
    }
}
