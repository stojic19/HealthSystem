﻿using Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class PharmacyDTO
    {
        public string Name { get; set; }

        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public City City { get; set; }
        public string BaseUrl { get; set; }
    }
}