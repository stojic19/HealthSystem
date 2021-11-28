using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;

namespace Hospital.Schedule.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }

        public FeedbackStatus FeedbackStatus { get; set; }  

        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
        public bool IsPublishable { get; set; }
    }
}
