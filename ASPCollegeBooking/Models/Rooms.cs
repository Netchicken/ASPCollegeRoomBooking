using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASPCollegeBooking.Models
{
    public class Rooms
    {
        [Key()]
        public string ID { get; set; }
        public string Title { get; set; }
        public string EventColor { get; set; }
        public bool IsBookable { get; set; }

    }
}
