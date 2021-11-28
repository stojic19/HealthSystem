using Hospital.SharedModel.Model;
using System;

namespace Hospital.Schedule.Model
{
    public class Referral
    {
        public int Id { get; set; }
        public DateTime IssuedDate { get; set; }
        public bool IsUrgent { get; set; }
        public string Description { get; set; }
        
        public Doctor Doctor { get; set; }
    }
}
