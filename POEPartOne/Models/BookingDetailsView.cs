using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POEPartOne.Models
{
    [Table("vw_BookingDetails")]
    public class BookingDetailsView
    {
        [Key]
        public int BookingId { get; set; }

        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "Booking Date")]
        [DataType(DataType.DateTime)]
        public DateTime BookingsDate { get; set; }

        [Display(Name = "Event ID")]
        public int EventId { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Display(Name = "Venue ID")]
        public int VenueId { get; set; }

        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Display(Name = "Venue Location")]
        public string VenueLocation { get; set; }

        [Display(Name = "Venue Capacity")]
        public int VenueCapacity { get; set; }
    }
}