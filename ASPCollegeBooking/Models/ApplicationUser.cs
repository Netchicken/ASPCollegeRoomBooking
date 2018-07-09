using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASPCollegeBooking.Models
{
    //  Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
        }

        public ApplicationUser(string userName, string firstName, string lastName)
            : base(userName)
        {
            base.Email = userName;
            this.FirstName = firstName;
            this.LastName = lastName;


        }


     public string FirstName { get; set; }


  public string LastName { get; set; }


        public string FullName => $"{this.FirstName} {this.LastName}";

    }

}
