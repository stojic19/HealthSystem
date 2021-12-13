using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class AllergyDTO
    {
        public int MedicalIngredientId { get; set; }
        public MedicationIngredientDTO MedicationIngredient { get; set; }
    }
}
