using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Business
{
    public static class DayWeeksAllDayMods
    {

        public static IEnumerable<Events> EventCalc(Events booking)
        {
            //need to change to a list instead of a DB set to add in new data
            var newbookings = new List<Events>();

            //add the original before modifying it
            newbookings.Add(booking);

            //work out if its full day or not
            if (booking.IsFullDay == true)
            {
                // create a start and end time for the dates saved
                DateTime startdate = booking.Start;
                TimeSpan tsStart = new TimeSpan(9, 0, 0); //9am
                booking.Start = startdate.Date + tsStart;

                DateTime enddate = booking.End;
                TimeSpan tsEnd = new TimeSpan(17, 0, 0); //5pm
                booking.End = enddate.Date + tsEnd;

            }
            //pass the datetime to local datetime
            DateTime StartDate = booking.Start;
            DateTime EndDate = booking.End;

            if (booking.Days > 0)
            {
                // create new bookings for the amount of days - account for weekends

                for (int i = 0; i < booking.Days; i++)
                {
                    Events singleBooking = new Events();
                    singleBooking.Start = StartDate.AddDays(i + 1);
                    singleBooking.End = EndDate.AddDays(i + 1);

                    //if the booking is a saturday add 2 days to make it a monday
                    if (booking.Start.DayOfWeek == DayOfWeek.Saturday)
                    { //move it to Monday
                        singleBooking.Start = singleBooking.Start.AddDays(2);
                        singleBooking.End = singleBooking.End.AddDays(2);
                    }
                    //add the new booking repeat for each day

                    LoadNewBookingElements(booking, singleBooking);

                    newbookings.Add(singleBooking);
                }
            }

            if (booking.Weeks > 0)
            {


                // create new bookings for the amount of weeks 
                for (int i = 0; i < booking.Weeks; i++)
                {
                    Events singleBooking = new Events();
                    double weeks = 7 * (i + 1);
                    singleBooking.Start = StartDate.AddDays(weeks);
                    singleBooking.End = EndDate.AddDays(weeks);

                    LoadNewBookingElements(booking, singleBooking);

                    newbookings.Add(singleBooking);
                }
            }
            return newbookings;
        }

        private static void LoadNewBookingElements(Events booking, Events singleBooking)
        {
            singleBooking.Days = booking.Days;
            singleBooking.Weeks = booking.Weeks;
            singleBooking.EventColor = booking.EventColor;
            singleBooking.IsFullDay = booking.IsFullDay;
            singleBooking.ResourceId = booking.ResourceId;
            singleBooking.Room = booking.Room;
            singleBooking.Title = booking.Title;
        }
    }
}
