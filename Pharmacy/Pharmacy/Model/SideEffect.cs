using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class SideEffect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SideEffect()
        {
                
        }
        public SideEffect(string name)
        {
            Name = name;
        }
    }
}
