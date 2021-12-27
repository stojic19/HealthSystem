using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Hospital.SharedModel.Model
{
    public class Specialization : ValueObject
    {
        public string Name { get; }
        public string Description { get; }

        public Specialization(string name, string description)
        {
            Name = name;
            Description = description;
            Validate();
        }

        public Specialization()
        {
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) || Name.Any(char.IsDigit))
            {
                throw new Exception();
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
