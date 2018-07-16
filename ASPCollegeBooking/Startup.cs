using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Email;
using ASPCollegeBooking.Models;
using ASPCollegeBooking.Services;
//using DotNetify;
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


            services.AddTransient<ITextFileOperations, TextFileOperations>();
            //https://korzh.com/blogs/net-tricks/aspnet-identity-store-user-data-in-claims
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, MyUserClaimsPrincipalFactory>();


            //  services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<BookingContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("RoomBookingConnection")));

            // Use SQL Database if in Azure, otherwise, use SQLite
            //----------------------
            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            //{
            services.AddDbContext<BookingContext>(options =>
                options.UseSqlite("Data Source= RoomBooking.db"));

            //https://elasticbeanstalk-us-east-2-834575397048.s3.us-east-2.amazonaws.com/RoomBooking.db?X-Amz-Expires=3562&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAILIUT3WXZB3GIMHQ/20180626/us-east-2/s3/aws4_request&X-Amz-Date=20180626T033526Z&X-Amz-SignedHeaders=host&X-Amz-Signature=e7b98f7cf100286eee70656f725b2d948ca0b91fa07200e17591a9f4c5aa52ac

            //  services.AddDbContext<BookingContext>(options => options.UseSqlite("Data Source= RoomBooking.db"));


            //https://s3.us-east-2.amazonaws.com/elasticbeanstalk-us-east-2-834575397048/RoomBooking.db  link to DB


            //arn:aws:s3:::elasticbeanstalk-us-east-2-834575397048

            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = admin.db"));

            //https://s3.us-east-2.amazonaws.com/elasticbeanstalk-us-east-2-834575397048/admin.db

            //}
            //else
            //{
            //services.AddDbContext<BookingContext>(options =>
            //    options.UseSqlite("Data Source = RoomBooking.db"));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //  services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = admin.db"));
            //}
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
            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.1&tabs=visual-studio%2Caspnetcore2x
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.1&tabs=visual-studio%2Caspnetcore2x
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();

            //http://dotnetify.net/react/Installation
            services.AddMemoryCache();
            // services.AddSignalR();
            //  services.AddDotNetify();



            //start http://romansimuta.com/post/authorization-with-roles-in-asp.net-core-mvc-web-application  authorization with roles
            //Underneath the covers, role-based authorization and claims-based authorization use a requirement, a requirement handler, and a pre-configured policy. These building blocks support the expression of authorization evaluations in code. The result is a richer, reusable, testable authorization structure.

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });
            //end - example [Authorize(Policy = "RequireAdminRole")]
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


            //http://dotnetify.net/react/Installation
            app.UseWebSockets();
            //   app.UseSignalR(routes => routes.MapDotNetifyHub());
            //   app.UseDotNetify();



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
            //Identity is enabled for the application by calling UseAuthentication in the Configure method. UseAuthentication adds authentication middleware to the request pipeline.
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
