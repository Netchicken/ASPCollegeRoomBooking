using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCollegeBooking.DTO
{
    public class EventDTO
    {
        // { id: '1', resourceId: 'b', start: '2017-10-07T02:00:00', end: '2017-10-07T07:00:00', title: 'event 1' },

      
        public string ID { get; set; }
        public string ResourceId { get; set; }
        public string EventColor { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Title { get; set; }
    }
}
