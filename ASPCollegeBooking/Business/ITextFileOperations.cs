using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;
//using VisitorManagement.Models;

namespace ASPCollegeBooking.Business
{
    public interface ITextFileOperations
    {
        void UploadStaffFile();
        IEnumerable<string> LoadConditionsForAcceptanceText();
        IEnumerable<StaffNames> RemoveDuplicateStaff();
    }
}
