using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class NewFeedbackDTO
    {
        public int PatientId { get; set; }
        public string Text { get; set; }
        public bool IsPublishable { get; set; }
    }
}
