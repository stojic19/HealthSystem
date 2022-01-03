using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model
{
   public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        public Shift() { }

        public Shift(int id, string name, int from, int to) {
            Id = id;
            Name = name;
            From = from;
            To = to;
        }
    }
}
