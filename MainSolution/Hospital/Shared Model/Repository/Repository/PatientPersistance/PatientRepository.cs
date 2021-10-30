using Model;
using System.Collections.Generic;

namespace Repository.PatientPersistance
{
    public class PatientRepository : IPatientRepository
    {
        public bool AddIngredientAllergenIfUnique(Patient patient, string newAllergen)
        {
            throw new System.NotImplementedException();
        }

        public bool AddMedicineAllergenIfUnique(Patient patient, string newAllergen)
        {
            throw new System.NotImplementedException();
        }

        public void Create(Patient newValue)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Patient GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<Patient> GetValues()
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveIngredientAllergen(Patient patient, string allergen)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveMedicineAllergen(Patient patient, string allergen)
        {
            throw new System.NotImplementedException();
        }

        public void Save(List<Patient> values)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Patient newValue)
        {
            throw new System.NotImplementedException();
        }
    }
}