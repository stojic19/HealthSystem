using Hospital.SharedModel.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.SharedModel.Model
{
    public class User : IdentityUser<int>
    {
        private const int MaxCanceledEvents = 3;
        [Required] public string FirstName { get; private set; }
        [Required] public string MiddleName { get; private set; }
        [Required] public string LastName { get; private set; }
        [Required] public DateTime DateOfBirth { get; private set; }
        [Required] public Gender Gender { get; private set; }
        [Required] public string Street { get; private set; }
        [Required] public string StreetNumber { get; private set; }
        public int CityId { get; private set; }
        public City City { get; }
        public bool IsBlocked { get; private set; }
        public string PhotoEncoded { get; private set; }

        public User()
        {
        }

        public User(string firstName, string middleName, string lastName, DateTime dateOfBirth, Gender gender,
            string street, string streetNumber, int cityId, string photoEncoded)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Street = street;
            StreetNumber = streetNumber;
            CityId = cityId;
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