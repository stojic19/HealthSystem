using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Schedule.Model
{
    public class Feedback
    {
        public int Id { get; }
        [Required]
        public int PatientId { get;  }
        public Patient Patient { get; }
        public FeedbackStatus FeedbackStatus { get; private set; }
        public DateTime CreatedDate { get; }
        [Required]
        public string Text { get;  }
        [Required]
        public bool IsPublishable { get; }
        [Required]
        public bool IsAnonymous { get; }

        public Feedback(int id, int patientId, Patient patient, string text, bool isPublishable, bool isAnonymous)
        {
            Id = id;
            PatientId = patientId;
            Patient = patient;
            FeedbackStatus = FeedbackStatus.NotApproved;
            CreatedDate = DateTime.Now;
            Text = text;
            IsPublishable = isPublishable;
            IsAnonymous = isAnonymous;
        }

        public Feedback()
        {
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Text) || string.IsNullOrWhiteSpace(Text)) throw new Exception();
            //TODO: add validations
        }

        public void Publish()
        {
            this.FeedbackStatus = FeedbackStatus.Approved;
        }

        public void Unpublish()
        {
            this.FeedbackStatus = FeedbackStatus.NotApproved;
        }

        public bool IsApproved()
        {
            return IsPublishable && FeedbackStatus == FeedbackStatus.Approved;
        }
    }
}
