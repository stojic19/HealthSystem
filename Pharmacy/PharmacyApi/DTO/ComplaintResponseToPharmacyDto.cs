﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class ComplaintResponseToPharmacyDto
    {
        public DateTime ComplaintCreatedDate { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ApiKey { get; set; }
    }
}