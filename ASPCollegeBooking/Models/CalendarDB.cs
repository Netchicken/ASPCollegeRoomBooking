using System;
using System.ComponentModel.DataAnnotations;

namespace ASPCollegeBooking.Models
{
    public class CalendarDB
    {
        [Key()]
        public string EventID { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        public string delete { get; set; }






    }
}
