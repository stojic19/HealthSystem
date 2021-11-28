using Hospital.SharedModel.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;

namespace Hospital.SharedModel.Model
{
    public class User : IdentityUser<int>

    {
    public string FirstName { get; set; }
    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    public string Street { get; set; }
    public string StreetNumber { get; set; }

    public int CityId { get; set; }
    public City City { get; set; }
    }
}
