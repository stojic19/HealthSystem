﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<City> Cities { get; set; } 
    }
}