using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASPCollegeBooking.Models
{
    public class Rooms
    {  //some technical debt happening here, its not a guid, just an counter pretending to be a guid, should be an int.
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string EventColor { get; set; }
        public bool IsBookable { get; set; }

    }
}
