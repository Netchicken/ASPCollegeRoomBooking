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

        [Key] public int Id { get; set; }
        public Rooms Room { get; set; }

        [ValidateRoom(ErrorMessage = "You need a room")]
        [Required] public string ResourceId { get; set; }
        public string EventColor { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Start { get; set; }

        [DataType(DataType.DateTime)]
        [ValidateEndDate(ErrorMessage = "Start date  must be before End date")]
        [Required]
        public DateTime End { get; set; }

        [Required] public string Title { get; set; }
        public bool IsFullDay { get; set; }
        public int Days { get; set; }
        public int Weeks { get; set; }
        public string Organizer { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (End < Start)
        //    {
        //        yield return new ValidationResult(
        //            $"Start date {Start} must be before End date {End} Validation in class", new[] { "End" });
        //    }
        //    }
    }

}
