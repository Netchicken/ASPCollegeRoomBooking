using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Business;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//http://romansimuta.com/post/authorization-with-roles-in-asp.net-core-mvc-web-application setting up roles and authorization Policies can be applied using the Policy property on the AuthorizeAttribute attribute:
namespace ASPCollegeBooking.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        private readonly BookingContext _context;
        public UserManager<ApplicationUser> UserManager { get; }
        public AdminController(BookingContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }



        //public ActionResult Index()
        //{
        //    ViewData["PageID"] = "Admin";
        //    ViewData["Message"] = "Admin page restricted to the super admin user";

        //    //  List<string> userids = UserManager.UserRoles.Where(a => a.RoleId == "").Select(b => b.UserId).Distinct().ToList();
        //    //The first step: get all user id collection as userids based on role from db.UserRoles

        //    // var listUsers = UserManager.GetUserAsync("Member").Result;
        //    // The second step: find all users collection from _db.Users which 's Id is contained at userids ;


        //    return View();
        //}
        //public IActionResult Settings()
        //{
        //    ViewData["Message"] = "Settings page restricted to the super admin user";
        //    return View();
        //}
    }
}