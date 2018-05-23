using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASPCollegeBooking.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //    : base()
        //{
        //}

        //public ApplicationUser(string userName, string firstName, string lastName, DateTime birthDay)
        //    : base(userName)
        //{
        //    base.Email = userName;
        //    this.FirstName = firstName;
        //    this.LastName = lastName;
        //    this.BirthDay = birthDay;

        //}


        //[StringLength(50)] public string FirstName { get; set; } = "Fnanon";


        //[StringLength(50)] public string LastName { get; set; } = "Lnanon";


        //[DataType(DataType.Date)]
        //public DateTime BirthDay { get; set; } = DateTime.Today.Date;

        //public string FullName => $"{this.FirstName} {this.LastName}";

    }

}
