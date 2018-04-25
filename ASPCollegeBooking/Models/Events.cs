using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace ASPCollegeBooking.Models
{
    public class Events
    {

        // { id: '1', resourceId: 'b', start: '2017-10-07T02:00:00', end: '2017-10-07T07:00:00', title: 'event 1' },

        [Key()]
        public string ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInc { get; set; }

        public Rooms Room { get; set; }

        public string ResourceId { get; set; }
        public string EventColor { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public bool IsFullDay { get; set; }
        public int Days { get; set; }
        public int Weeks { get; set; }

    }
}
