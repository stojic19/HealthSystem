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
            if (string.IsNullOrWhiteSpace(Name) || Name.Any(char.IsDigit))
            {
                throw new Exception(); //napraviti exception 
            }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
