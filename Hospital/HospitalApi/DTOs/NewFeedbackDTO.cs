namespace HospitalApi.DTOs
{
    public class NewFeedbackDTO
    {
        public string PatientUsername { get; set; }
        public int PatientId { get; set; }
        public string Text { get; set;  }
        public bool IsPublishable { get; set; }
        public bool IsAnonymous { get; set;  }
    }
}
