using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Service
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _uow;
       
        public ReportService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IEnumerable<Report> GetAllReports(string userName)
        {
          return _uow.GetRepository<IReportReadRepository>().GetAllReports(userName);
        }
      
    }
}
