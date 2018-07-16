using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

//https://stackoverflow.com/questions/49859831/asp-net-core-userclaimsprincipalfactory-lost-claims

//The problem was that I have override the wrong method. I should override GenerateClaimsAsync instead of CreateAsync. Now it works. 


namespace ASPCollegeBooking.Business
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        //https://korzh.com/blogs/net-tricks/aspnet-identity-store-user-data-in-claims

        public MyUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));
            identity.AddClaim(new Claim("LastName", user.LastName ?? ""));

            return identity;
        }


    }
}
