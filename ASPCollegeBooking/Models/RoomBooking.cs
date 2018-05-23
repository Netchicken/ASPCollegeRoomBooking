using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCollegeBooking.Models
{
    public class RoomBooking
    {
        [Key()]
        public string BookingID { get; set; }
        public int ClientIDFK { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        public int Days { get; set; }
        public int Weeks { get; set; }
        [ForeignKey("Rooms")]
        public Rooms Room { get; set; }



    }
}
