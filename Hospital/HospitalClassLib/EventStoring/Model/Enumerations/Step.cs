using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.EventStoring.Model.Enumerations
{
    public enum Step
    {
        StartScheduling,
        DateNext,
        SpecializationNext,
        SpecializationBack,
        DoctorNext,
        DoctorBack,
        AppointmentBack,
        Schedule
    }
}
