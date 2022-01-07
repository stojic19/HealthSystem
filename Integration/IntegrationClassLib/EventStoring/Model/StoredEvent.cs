using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Integration.EventStoring.Model
{
    [Table("StoredEvent", Schema = "EventStoring")]
    public class StoredEvent
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string StateData { get; set; }
        public int UserId { get; set; }
    }
}
