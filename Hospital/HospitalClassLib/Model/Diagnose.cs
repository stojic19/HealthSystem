using System;

namespace Hospital.Model
{
    public class Diagnose
    {
        public int Id { get; set; }
        public string Illness { get; set; }
        public string Symptoms { get; set; }
        public string Anamnesis { get; set; }
        public DateTime Date { get; set; }
    }
}
