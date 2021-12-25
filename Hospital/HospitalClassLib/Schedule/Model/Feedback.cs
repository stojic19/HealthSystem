using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;

namespace Hospital.Schedule.Model
{
    public class Feedback
    {
        public int Id { get; }
        public Patient Patient { get; }

        public FeedbackStatus FeedbackStatus { get;  }  

        public DateTime CreatedDate { get; }
        public string Text { get;  }
        public bool IsPublishable { get; set; }
    }
}
