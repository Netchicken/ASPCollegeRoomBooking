using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Migrations;
using ASPCollegeBooking.Models;
using ASPCollegeBooking.Models.AccountViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

//using VisitorManagement.Data;
//using VisitorManagement.DTO;
//using VisitorManagement.Models;

namespace ASPCollegeBooking.Business
{
    public class TextFileOperations : ITextFileOperations
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BookingContext _context;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public TextFileOperations(
            BookingContext context,
           IHostingEnvironment hostingEnvironment,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext applicationDbContext,
            RoleManager<IdentityRole> rolemanager,

                UserManager<ApplicationUser> userManager)
        {
            _environment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _applicationDbContext = applicationDbContext;
            _roleManager = rolemanager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool StaffSignInManager(string Email, string Password)
        {
            //--------------Get the data from the Text file --------------


            var AllStaffNames = LoadStaffNamesFromFile();

            string[] PW = Email.Split(' ');

            //ok this sucks
            int name = AllStaffNames.Count(a => a.Name == Email);


            // int PW = AllStaffNames.Count(a => a.Department == Password);

            if (name > 0 && Password == PW[0])
            {
                return true;
            }
            return false;
        }

        public void UploadStaffFile()
        {

            //--------------Get the data from the Text file --------------
            var AllStaffNames = LoadStaffNamesFromFile();

            //add in the ones from the db
            AllStaffNames.AddRange(RemoveDuplicateStaff());

            //sort the new data and pull out only uniques
            IOrderedEnumerable<StaffNames> orderedStaffNames = AllStaffNames.Distinct().OrderBy(
                i => i.Department).ThenBy(i => i.Name);


            SaveToStaffNamesTable(orderedStaffNames);

            //whoa! hacked into the authorization to auto add members in

            SaveToIdentityTables(orderedStaffNames);
        }

        private void SaveToIdentityTables(IOrderedEnumerable<StaffNames> orderedStaffNames)
        {
            foreach (var staff in orderedStaffNames)
            {
                string fullname = staff.Name;

                //doesn't do all of Cobus De Lang

                String[] name = fullname.Split(' ');
                RegisterViewModel model = new RegisterViewModel();
                model.Password = name[0] + name[1];


                string NameWithDot = staff.Name.Replace(' ', '.');

                //create a new user
                var user = new ApplicationUser
                {
                    UserName = NameWithDot + "@visioncollege.ac.nz", //name[0] + name[1],
                    FirstName = name[0],
                    LastName = name[1],
                    Email = NameWithDot + "@visioncollege.ac.nz"
                };

                var result = _userManager.CreateAsync(user, model.Password);
            }


            //  var result2 = 
            //  var result3 = _signInManager.SignInAsync(user, isPersistent: true);



            // AddRolesToStaff(user, model);
        }

        private void AddToRoles()
        {
            foreach (var user in _userManager.Users)
            {
                var result = _userManager.AddToRoleAsync(user, "Member");
            }
        }

        private void SaveToStaffNamesTable(IOrderedEnumerable<StaffNames> orderedStaffNames)
        {
            _context.AddRange(orderedStaffNames);
            _context.SaveChanges();
        }

        private List<StaffNames> LoadStaffNamesFromFile()
        {
            string[] lines = { };

            //Gets or sets the absolute path to the directory that contains the web-servable application content files.
            string webRootPath = _environment.WebRootPath;

            FileInfo filePath = new FileInfo(Path.Combine(webRootPath, "Staff.txt"));
            lines = System.IO.File.ReadAllLines(filePath.ToString());

            //holds all the staff names
            Dictionary<string, StaffNames> StaffnamesDict = new Dictionary<string, StaffNames>();

            List<StaffNames> AllStaffNames = new List<StaffNames>();

            foreach (var name in lines)
            {
                StaffNames staffnames = new StaffNames();
                string[] data = name.Split(',');
                staffnames.Name = data[0];
                staffnames.Department = data[1];

                //if the name isn't there, add it in
                if (!StaffnamesDict.ContainsKey(staffnames.Name))
                {
                    StaffnamesDict.Add(staffnames.Name, staffnames);
                    AllStaffNames.Add(staffnames);
                }
            }

            return AllStaffNames;
        }

        private void AddRolesToStaff(ApplicationUser user, RegisterViewModel model)
        {
            //pass in the user and the pw
            //  var result = _userManager.CreateAsync(user, model.Password);

            var result = _userManager.CreateAsync(user, model.Password);

            //if (result.IsCompletedSuccessfully)

            //{

            // Add a user to the default role, or any role you prefer here

            _userManager.AddToRoleAsync(user, "Member");

            _signInManager.SignInAsync(user, isPersistent: true);

            //  return RedirectToLocal(returnUrl);
            //}
        }


        private void AddToAdmin(List<StaffNames> AllStaffNames)
        {
            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-2.1&tabs=aspnetcore2x
            //  https://www.jerriepelser.com/blog/authenticate-oauth-aspnet-core-2/
            //Users users = new

            //_applicationDbContext.Users.AddRange();
        }


        public IEnumerable<StaffNames> RemoveDuplicateStaff()
        {
            //holds all the staff names
            Dictionary<string, StaffNames> StaffnamesDict = new Dictionary<string, StaffNames>();
            List<StaffNames> AllStaffNames = new List<StaffNames>();

            foreach (var name in _context.StaffNames.Distinct().ToList())
            //.Select(x => new { x.Name, x.Department })
            {
                //have to add each unique name to the dictionary 
                StaffNames staffnames = new StaffNames();
                staffnames.Name = name.Name;
                staffnames.Department = name.Department;
                staffnames.Id = name.Id;
                //added here
                if (!StaffnamesDict.ContainsKey(staffnames.Name))
                {
                    StaffnamesDict.Add(staffnames.Name, staffnames);
                    AllStaffNames.Add(staffnames);
                }
            }
            //todo pull out all the existing names from the application user db 
            //Empty the Database to remove old values

            foreach (var staff in _context.StaffNames.ToList())
            {
                _context.StaffNames.Remove(staff);
            }
            _context.SaveChanges();

            //empty the identity table

            foreach (var staff in _applicationDbContext.Users.ToList())
            {
                _applicationDbContext.Users.Remove(staff);
            }
            _applicationDbContext.SaveChanges();

            //save the filtered data
            //_context.AddRange(AllStaffNames);
            //_context.SaveChanges();


            return AllStaffNames;
        }


        public IEnumerable<string> LoadConditionsForAcceptanceText()
        {
            string[] lines = { };

            //Gets or sets the absolute path to the directory that contains the web-servable application content files.
            string webRootPath = _environment.WebRootPath;
            //Gets or sets the absolute path to the directory that contains the application content files.
            //    string contentRootPath = _environment.ContentRootPath;

            FileInfo filePath = new FileInfo(Path.Combine(webRootPath, "ConditionsForAdmittance.txt"));

            lines = File.ReadAllLines(filePath.ToString());

            return lines.ToList();
        }
        //---- Downloading and backup of files
        //https://stackoverflow.com/questions/44432294/download-file-using-asp-net-core

        //public void DownloadVisitorsExcel(string filePath, string fileName)
        //{

        //    IEnumerable<Visitor> AllVisitors = (IEnumerable<Visitor>)_context.Visitor.Find();


        //    var sb = new StringBuilder();
        //    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", "First Name", "Last Name", "Business", "Date In", "Date Out", "Staff Name", "Department", Environment.NewLine);
        //    foreach (var item in AllVisitors)
        //    {
        //        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", item.FirstName, item.LastName, item.Business, item.DateIn.ToShortDateString() + item.DateIn.ToShortTimeString(), item.DateOut.ToShortDateString() + item.DateOut.ToShortTimeString(), item.StaffName.Name, item.StaffName.Department, Environment.NewLine);
        //    }


        ////Get Current Response  
        //var response = _httpContextAccessor.HttpContext.Response;
        ////response.BufferOutput = true;
        //response.Clear();
        ////response.ClearHeaders();
        ////response.ContentEncoding = Encoding.Unicode;
        //response.Body.    AddHeader("content-disposition", "attachment;filename=Visitors.CSV ");
        //response.ContentType = "text/plain";
        //response.Body.Write(sb.ToString());
        //response.Body.EndWrite();



        //IFileProvider provider = new PhysicalFileProvider(filePath);
        //IFileInfo fileInfo = provider.GetFileInfo(fileName);
        //var readStream = fileInfo.CreateReadStream();
        //var mimeType = "text/plain";


        //  return FileMode(readStream, mimeType, fileName);


    }

}
//}
