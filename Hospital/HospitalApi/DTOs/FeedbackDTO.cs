using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public FeedbackStatus FeedbackStatus { get;set; }
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
        public bool IsPublishable { get;  set; }
        public bool IsAnonymous { get;  set; }
    }
}
