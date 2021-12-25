namespace Hospital.SharedModel.Model
{
    public class City 
    {
        public int Id { get; }
        public string Name { get;}
        public int PostalCode { get;  }
        public int CountryId { get; }
        public Country Country { get; }
        
        public City(int id, string name, int postalCode, int countryId, Country country)
        {
            Id = id;
            Name = name;
            PostalCode = postalCode;
            CountryId = countryId;
            Country = country;
        }
    }
}
