using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Model
{
    public class Survey
    {
        public int Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public bool isActive { get; private set; }
        public IEnumerable<Question> Questions { get; private set; }
        public IEnumerable<AnsweredSurvey> AnsweredSurveys { get; private set; }

        public Survey()
        {

        }
        public Survey(bool isActive)
        {
            this.CreatedDate = DateTime.Now;
            this.Questions = new List<Question>();
            this.AnsweredSurveys = new List<AnsweredSurvey>();
            this.isActive = isActive;
            
            Validate();
        }

        private void Validate()
        {
        }

        public void CreateAnsweredSurvey(AnsweredSurvey answeredSurvey, Patient patient)
        {
            //UoW.GetRepository<IAnsweredSurveyWriteRepository>()
            answeredSurvey.SetPatient(patient);
            AnsweredSurveys.ToList().Add(answeredSurvey);
        }

        internal bool IsActiveSurvey()
        {
            return this.isActive;
        }

    }
}
