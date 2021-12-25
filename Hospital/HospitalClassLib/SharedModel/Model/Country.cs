using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.SharedModel.Model
{
    public class Country : ValueObject
    {
        public string Name { get;}

        public Country(string name) {
            Name = name;
            Validate();
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name)) {
                throw new Exception(); //napraviti exception 
            }

            if (Name.Any(char.IsDigit))
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
