using Hospital.Model.Enumerations;
using System;

namespace Hospital.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Street { get; set; }
        public string StreetNumber { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
