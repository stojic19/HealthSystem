using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;

namespace Hospital.Schedule.Model
{
    public class AnsweredSurvey
    {
        public int Id { get; private set; }
        public IEnumerable<AnsweredQuestion> AnsweredQuestions { get; private set; }
        public DateTime AnsweredDate { get; private set; }
        public int SurveyId { get; private set; }
        public Survey Survey { get; private set; }
        public int PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public int ScheduledEventId { get; private set; }
        public ScheduledEvent ScheduledEvent { get; private set; }

        public AnsweredSurvey()
        {
                
        }
        public AnsweredSurvey(IEnumerable<AnsweredQuestion> answeredQuestions, DateTime answeredDate, int surveyId, Survey survey, int patientId, Patient patient, int scheduledEventId, ScheduledEvent scheduledEvent)
        {
            AnsweredQuestions = answeredQuestions;
            AnsweredDate = answeredDate;
            SurveyId = surveyId;
            Survey = survey;
            PatientId = patientId;
            Patient = patient;
            ScheduledEventId = scheduledEventId;
            ScheduledEvent = scheduledEvent;
            Validate();
        }

        public void SetPatient(Patient patient)
        {
            Patient = patient;
        }

        private void Validate()
        {
            if (SurveyId <= 0 || ScheduledEventId <= 0 || PatientId <= 0 ) throw new Exception();
        }
    }
}
