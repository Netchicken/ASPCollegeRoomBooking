using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Data
{
    public class ClashDTO
    {
        public Dictionary<Events, Events> BookingClashDic = new Dictionary<Events, Events>();

    }
}
