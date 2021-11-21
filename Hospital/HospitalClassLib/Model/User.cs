using Hospital.Model.Enumerations;
using System;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Model
{
    public class User : IdentityUser<Guid>

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
