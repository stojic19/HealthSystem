using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.SharedModel.Model
{
    public class Country : ValueObject
    {
        public string Name { get; private set; }

        public Country(string name) {
            Name = name;
            Validate();
        }

        public Country()
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
        }
    }
}
