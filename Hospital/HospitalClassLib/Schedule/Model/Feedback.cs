using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Schedule.Model
{
    public class Feedback
    {
        public int Id { get; private set; }
        [Required]
        public int PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public FeedbackStatus FeedbackStatus { get; private set; }
        public DateTime CreatedDate { get; private set; }
        [Required]
        public string Text { get; private set; }
        [Required]
        public bool IsPublishable { get; private set; }
        [Required]
        public bool IsAnonymous { get; private set; }

        public Feedback(int patientId, string text, bool isPublishable, bool isAnonymous)
        {
            PatientId = patientId;
            FeedbackStatus = FeedbackStatus.NotApproved;
            CreatedDate = DateTime.Now;
            Text = text;
            IsPublishable = isPublishable;
            IsAnonymous = isAnonymous;
            ValidateOnCreate();
        }

        public Feedback()
        {
        }

        private void ValidateOnCreate()
        {
            if (string.IsNullOrEmpty(Text) || string.IsNullOrWhiteSpace(Text)) throw new Exception();
            if (FeedbackStatus != FeedbackStatus.NotApproved) throw new Exception();
        }
        private void ValidateOnPublish()
        {
            if (FeedbackStatus != FeedbackStatus.Approved) throw new Exception();
        }
        private void ValidateOnUnpublish()
        {
            if (FeedbackStatus != FeedbackStatus.NotApproved) throw new Exception();
        }

        public void Publish()
        {
            this.FeedbackStatus = FeedbackStatus.Approved;
            ValidateOnPublish();
        }

        public void Unpublish()
        {
            this.FeedbackStatus = FeedbackStatus.NotApproved;
            ValidateOnUnpublish();
        }

        public bool IsApproved()
        {
            return IsPublishable && FeedbackStatus == FeedbackStatus.Approved;
        }
    }
}
