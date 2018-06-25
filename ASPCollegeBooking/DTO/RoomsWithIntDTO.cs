using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCollegeBooking.DTO
{
    public class RoomsWithIntDTO
    {
        [Key()]
        public int ID { get; set; }
        public string Title { get; set; }
        public string EventColor { get; set; }
        public bool IsBookable { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
