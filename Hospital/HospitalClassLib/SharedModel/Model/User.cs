using Hospital.SharedModel.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.SharedModel.Model
{
    public class User : IdentityUser<int>
    {
    [Required]
    public string FirstName { get;  }
    [Required]
    public string MiddleName { get;  }
    [Required]
    public string LastName { get;  }
    [Required]
    public DateTime DateOfBirth { get; }
    [Required]
    public Gender Gender { get;  }
    [Required]
    public string Street { get;  }
    [Required]
    public string StreetNumber { get;  }
    public City City { get; }

    public User(string firstName, string middleName, string lastName, DateTime dateOfBirth, Gender gender, string street, string streetNumber, City city)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Street = street;
        StreetNumber = streetNumber;
        City = city; 
    }
    }
}
