using System.ComponentModel.DataAnnotations;

namespace ASPCollegeBooking.Models
{
    public class Rooms
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string EventColor { get; set; }
        public bool IsBookable { get; set; }
        public int MaxOccupancy { get; set; }

    }
}
