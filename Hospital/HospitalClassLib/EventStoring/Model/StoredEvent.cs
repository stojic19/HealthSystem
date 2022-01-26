using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.EventStoring.Model.Enumerations;
using Hospital.SharedModel.Model;

namespace Hospital.EventStoring.Model
{
    [Table("StoredEvent", Schema = "EventStoring")]
    public class StoredEvent
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Step Step { get; set; }
        public string Username { get; set; }
    }
}
