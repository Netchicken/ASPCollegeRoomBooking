using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;
using ASPCollegeBooking.Services;
using Microsoft.AspNetCore.Localization;

namespace ASPCollegeBooking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                //set the culture for New Zealand for the whole project this might not be working, check it out by commenting it out.
                var cultureInfo = new CultureInfo("en-NZ");
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-NZ");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-NZ") };
            });


            //  services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));




            //services.AddDbContext<BookingContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("RoomBookingConnection")));

            // Use SQL Database if in Azure, otherwise, use SQLite
            //----------------------
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<BookingContext>(options =>
                    options.UseSqlite("Data Source= RoomBooking.db"));



                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = admin.db"));
            }
            else
            {

                services.AddDbContext<BookingContext>(options =>
                    options.UseSqlite("Data Source = RoomBooking.db"));

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = admin.db"));
            }
            //end------------------------------

            ////If this code detects that it is running in production (which indicates the Azure environment), then it uses the connection string you configured to connect to the SQL Database.

            // The Database.Migrate() call helps you when it is run in Azure, because it automatically creates the databases that your .NET Core app needs, based on its migration configuration. 

            // Automatically perform database migration
            //  services.BuildServiceProvider().GetService<BookingContext>().Database.Migrate();


            //  services.AddDbContext<BookingContext>(options => options.UseSqlite("Data Source = RoomBooking.db"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Change the password requirements of the login system
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //https://joonasw.net/view/aspnet-core-localization-deep-dive
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-NZ")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {

                DefaultRequestCulture = new RequestCulture("en-NZ"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(); //added this to run js
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Events}/{action=Scheduler}/{id?}");

            });
        }
    }
}
