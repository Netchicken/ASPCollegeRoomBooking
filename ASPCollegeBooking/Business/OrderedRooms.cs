using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.DTO;

using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCollegeBooking.Business
{
    public class OrderedRooms : Controller
    {

        private readonly BookingContext _context;

        public OrderedRooms(BookingContext context)
        {
            _context = context;
        }

        public IEnumerable GetOrderedRooms()
        {
            //we need to convert the rom ID from a string to an INT. So that when it gets ordered it is done with an int not a string
            List<Rooms> allRooms = new List<Rooms>();
            List<RoomsWithIntDTO> allRoomswithInt = new List<RoomsWithIntDTO>();
            //display ordered by ID
            allRooms.AddRange(_context.Rooms.OrderBy(r => r.ID).ThenBy(r => r.Title));

            foreach (var room in allRooms)
            {
                RoomsWithIntDTO newrooms = new RoomsWithIntDTO();
                newrooms.ID = Convert.ToInt32(room.ID);
                newrooms.Title = room.Title;
                newrooms.EventColor = room.EventColor;
                newrooms.IsBookable = room.IsBookable;
                allRoomswithInt.Add(newrooms);
            }


            return allRoomswithInt.Where(r => r.IsBookable).OrderBy(r => r.ID);
        }



    }
}
