using System;

namespace Integration.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public bool isEqual(City city)
        {
            return Name.Equals(city.Name) && Country.Name.Equals(city.Country.Name);
        }
    }
}
