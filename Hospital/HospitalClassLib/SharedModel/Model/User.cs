using Hospital.SharedModel.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;

namespace Hospital.SharedModel.Model
{
    public class User : IdentityUser<int>
    {
        private const int MaxCanceledEvents = 3;
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public City City { get; private set; }
        public bool IsBlocked { get; private set; }
        public string PhotoEncoded { get; private set; }

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
            //TODO: add validate method
        }

        public void Block()
        {
            IsBlocked = true;
            //Validate();
        }

        public bool IsMalicious(int numOfCanceledEventsInLastMonth)
        {
            return numOfCanceledEventsInLastMonth >= MaxCanceledEvents;
        }
    }
}