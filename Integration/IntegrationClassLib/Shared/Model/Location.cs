using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Integration.Shared.Model
{
    public class LocationInfo
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Formatted { get; set; }
        public string Street { get; set; }
        public string Housenumber { get; set; }

    }

    public class LocationResults
    {
        public List<LocationInfo> Results { get; set; }
    }

    public class Location : ValueObject
    {
        private static string geoapify_api_key = "5bdc7059b84b4066963d7e6c07e9f3af";
        private double Latitude { get; }

        private double Longitude { get; }

        private Location()
        {
        }

        public Location(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
            Validate();
        }

        private void Validate()
        {
            if (Latitude < -90 || Latitude > 90 || Longitude <= -180 || Longitude > 180)
            {
                throw new Exception();
            }
        }

        public City GetCity()
        {
            RestClient client = new RestClient();
            string targetUrl = "https://api.geoapify.com/v1/geocode/reverse?lat=" + Latitude.ToString().Replace(",", ".") + "&lon=" + Longitude.ToString().Replace(",", ".") +
                               "&format=json&apiKey=" + geoapify_api_key;
            RestRequest request = new RestRequest(targetUrl);
            IRestResponse response = client.Get(request);
            LocationInfo result = JsonConvert.DeserializeObject<LocationResults>(response.Content).Results.First();
            return new City
            {
                Name = result.City,
                PostalCode = Int32.Parse(result.Postcode),
                Country = new Country {Name = result.Country }
            };
        }

        public Country GetCountry()
        {
            RestClient client = new RestClient();
            string targetUrl = "https://api.geoapify.com/v1/geocode/reverse?lat=" + Latitude.ToString().Replace(",", ".") + "&lon=" + Longitude.ToString().Replace(",", ".") +
                               "&format=json&apiKey=" + geoapify_api_key;
            RestRequest request = new RestRequest(targetUrl);
            IRestResponse response = client.Get(request);
            LocationInfo result = JsonConvert.DeserializeObject<LocationResults>(response.Content).Results.First();
            return new Country
            {
                Name = result.Country
            };
        }

        public string GetFullAddress()
        {
            RestClient client = new RestClient();
            string targetUrl = "https://api.geoapify.com/v1/geocode/reverse?lat=" + Latitude.ToString().Replace(",", ".") + "&lon=" + Longitude.ToString().Replace(",", ".") +
                               "&format=json&apiKey=" + geoapify_api_key;
            RestRequest request = new RestRequest(targetUrl);
            IRestResponse response = client.Get(request);
            LocationInfo result = JsonConvert.DeserializeObject<LocationResults>(response.Content).Results.First();
            return result.Formatted;
        }

        public string GetStreetName()
        {
            RestClient client = new RestClient();
            string targetUrl = "https://api.geoapify.com/v1/geocode/reverse?lat=" + Latitude.ToString().Replace(",", ".") + "&lon=" + Longitude.ToString().Replace(",", ".") +
                               "&format=json&apiKey=" + geoapify_api_key;
            RestRequest request = new RestRequest(targetUrl);
            IRestResponse response = client.Get(request);
            LocationInfo result = JsonConvert.DeserializeObject<LocationResults>(response.Content).Results.First();
            return result.Street;
        }

        public string GetHouseNumber()
        {
            RestClient client = new RestClient();
            string targetUrl = "https://api.geoapify.com/v1/geocode/reverse?lat=" + Latitude.ToString().Replace(",", ".") + "&lon=" + Longitude.ToString().Replace(",", ".") +
                               "&format=json&apiKey=" + geoapify_api_key;
            RestRequest request = new RestRequest(targetUrl);
            IRestResponse response = client.Get(request);
            LocationInfo result = JsonConvert.DeserializeObject<LocationResults>(response.Content).Results.First();
            return result.Housenumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
