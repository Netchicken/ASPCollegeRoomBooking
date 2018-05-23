using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//http://romansimuta.com/post/authorization-with-roles-in-asp.net-core-mvc-web-application setting up roles and authorization Policies can be applied using the Policy property on the AuthorizeAttribute attribute:
namespace ASPCollegeBooking.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            ViewData["PageID"] = "Admin";
            ViewData["Message"] = "Admin page restricted to the super admin user";
            return View();
        }
        public IActionResult Settings()
        {
            ViewData["Message"] = "Settings page restricted to the super admin user";
            return View();
        }
    }
}