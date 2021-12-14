﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class ScheduleAppointmentDTO
    {
        public DateTime StartDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}