using Hospital.SharedModel.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;

namespace Hospital.SharedModel.Model
{
    public class User : IdentityUser<int>
    {
        private const int MaxCanceledEvents = 3;
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get;  set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public City City { get;  set; }
        public bool IsBlocked { get;  set; }
        public string PhotoEncoded { get; set; }

        public User()
        {
        }

        public User(string firstName, string middleName, string lastName, DateTime dateOfBirth, Gender gender,
            string street, string streetNumber, City city, string photoEncoded)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Street = street;
            StreetNumber = streetNumber;
            City = city;
            IsBlocked = false;
            PhotoEncoded = photoEncoded;
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public static bool IsMalicious(int numOfCanceledEventsInLastMonth)
        {
            return numOfCanceledEventsInLastMonth >= MaxCanceledEvents;
        }
    }
}