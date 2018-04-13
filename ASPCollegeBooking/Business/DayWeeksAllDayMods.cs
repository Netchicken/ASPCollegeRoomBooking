using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Business
{
    public static class DayWeeksAllDayMods
    {
        public static Dictionary<Events, Events> BookingClashDic = new Dictionary<Events, Events>();
        public static bool WeHaveAClash = false;

        public static IEnumerable<Events> EventCalc(Events booking)
        {

            //need to change to a list instead of a DB set to add in new data
            var originalbookings = new List<Events>();

            //add the original before modifying it
            originalbookings.Add(booking);

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

                    DoTheDatesOverlap(originalbookings, singleBooking);
                    // add the new booking repeat for each day



                    if (WeHaveAClash == false)
                    {
                        LoadNewBookingElements(booking, singleBooking);
                    }

                    originalbookings.Add(singleBooking);
                }
            }

            if (booking.Weeks > 0)
            {


                //  create new bookings for the amount of weeks 
                for (int i = 0; i < booking.Weeks; i++)
                {
                    Events singleBooking = new Events();
                    double weeks = 7 * (i + 1);
                    singleBooking.Start = StartDate.AddDays(weeks);
                    singleBooking.End = EndDate.AddDays(weeks);

                    //check booking against database



                    DoTheDatesOverlap(originalbookings, singleBooking);

                    //show the error message


                    LoadNewBookingElements(booking, singleBooking);

                    originalbookings.Add(singleBooking);

                    //add check here
                }
            }
            return originalbookings;
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


        /// <summary>
        ///     Return a list of events that clash
        ///  </summary>
        /// <param name="ExistingBookings">from the DB</param>
        /// <param name="NewBooking">Just being made</param>
        public static void DoTheDatesOverlap(List<Events> ExistingBookings, Events NewBooking)
        {
            WeHaveAClash = false;
            foreach (var ExistingBooking in ExistingBookings)
            {
                //we have a clash
                if ((NewBooking.Start >= ExistingBooking.Start) && (NewBooking.Start < ExistingBooking.End)
                    ||
                    (NewBooking.End >= ExistingBooking.Start) && (NewBooking.End < ExistingBooking.End) && (NewBooking.ResourceId == ExistingBooking.ResourceId)
                    ||
                                                                                                            (NewBooking.Start <= ExistingBooking.Start) && (NewBooking.End > ExistingBooking.End)
                    )
                {
                    WeHaveAClash = true;
                    //if (BookingClashDic.ContainsKey(ExistingBooking))
                    //{
                    //    NewBooking.Title += " copy";
                    //}
                    BookingClashDic.Add(ExistingBooking, NewBooking);
                }
                else
                {
                    //we don't have a clash
                    WeHaveAClash = false;
                }

            }


            //return dateToCheck >= startDate && dateToCheck < endDate;

        }

    }
}
