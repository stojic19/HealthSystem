using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.SharedModel.Model
{
    public class City : ValueObject
    {
        public string Name { get;}
        public int PostalCode { get;  }
        public Country Country { get; }
        
        public City(string name, int postalCode, Country country)
        {
            Name = name;
            PostalCode = postalCode;
            Country = country;
            Validate();
        }

        private void Validate()
        {
            if (Name.Any(char.IsDigit)) throw new Exception();
            if (string.IsNullOrEmpty(Name)) throw new Exception();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return PostalCode;
            yield return Country;
        }
    }
}
