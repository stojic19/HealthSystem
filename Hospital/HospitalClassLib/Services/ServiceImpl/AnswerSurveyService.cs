using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.ServiceImpl
{
    public class AnswerSurveyService : IPatientSurveyService
    {
        private IUnitOfWork UoW;

        public AnswerSurveyService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        //TODO:Prepravi na neki kul response
        public void createAnsweredSurvey(IEnumerable<AnsweredQuestion> answers,Guid userId,int scheduledEventId)
        {
            var repo = UoW.GetRepository<IAnsweredSurveyWriteRepository>();
            repo.create(answers, userId, scheduledEventId);

        }


        public int getSurvey(int Id)
        {
            var patientRepository = UoW.GetRepository<IPatientReadRepository>();
            Patient patient = patientRepository.GetById(Id);
            var hospitalTreatment = UoW.GetRepository<IHospitalTreatmentReadRepository>();

            IEnumerable<HospitalTreatment> finishedTreatments = hospitalTreatment.GetAll().Where(x => x.MedicalRecordId == patient.MedicalRecord.Id && x.IsDone == true);

            return finishedTreatments.Count();
            
        }
    }
}
