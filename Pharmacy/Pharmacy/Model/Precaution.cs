using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class Precaution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Precaution()
        {

        }
        public Precaution(string name)
        {
            Name = name;
        }
    }
}
