using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

//http://romansimuta.com/post/authorization-with-roles-in-asp.net-core-mvc-web-application


namespace ASPCollegeBooking.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context,
            IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Member" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist) roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            }

            await DoWeHaveAdminYet(serviceProvider);
        }

        private static async Task DoWeHaveAdminYet(IServiceProvider serviceProvider)
        {
            //set up a new config to find out if we have an admin
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //is there a user matching the details in appsettings?
            var _user = await userManager.FindByEmailAsync(config.GetSection("AppSettings")["UserEmail"]);
            //No?
            if (_user == null)
            {
                //lets make an admin get the details from the json file and assign it to admin
                var poweruser = new ApplicationUser
                {
                    //get the settings from appsettings.json
                    UserName = config.GetSection("AppSettings")["UserEmail"],
                    Email = config.GetSection("AppSettings")["UserEmail"]
                };
                var UserPassword = config.GetSection("AppSettings")["UserPassword"];
                var createPowerUser = await userManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded) await userManager.AddToRoleAsync(poweruser, "Admin");
            }
        }
    }
}
