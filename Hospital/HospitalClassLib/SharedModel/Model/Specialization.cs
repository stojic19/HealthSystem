using System;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Hospital.SharedModel.Model
{
    public class Specialization
    {
        public int Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Specialization(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) || Name.Any(char.IsDigit))
            {
                throw new Exception();
            }
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }
        public void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}
