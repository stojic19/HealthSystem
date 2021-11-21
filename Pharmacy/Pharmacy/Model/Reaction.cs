using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class Reaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Reaction()
        {
                
        }
        public Reaction(string name)
        {
            Name = name;
        }
    }
}
