using Hospital.SharedModel.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.SharedModel.Model
{
    public class User : IdentityUser<int>
    {
        private const int MaxCanceledEvents = 3;
        [Required] public string FirstName { get; }
        [Required] public string MiddleName { get; }
        [Required] public string LastName { get; }
        [Required] public DateTime DateOfBirth { get; }
        [Required] public Gender Gender { get; }
        [Required] public string Street { get; }
        [Required] public string StreetNumber { get; }
        public City City { get; }
        public bool IsBlocked { get; set; }
        public string PhotoEncoded { get; set; }

        public User()
        {
        }

        public User(string firstName, string middleName, string lastName, DateTime dateOfBirth, Gender gender,
            string street, string streetNumber, City city, bool isBlocked, string photoEncoded)
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

        public bool isMalicious(int numOfCanceledEventsInLastMonth)
        {
            return numOfCanceledEventsInLastMonth >= MaxCanceledEvents;
        }
    }
}