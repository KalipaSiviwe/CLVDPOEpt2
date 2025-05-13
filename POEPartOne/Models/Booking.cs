using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POEPartOne.Models;

public partial class Booking
{
    [Key]
    [Column("BookingID")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Booking ID")]
    [Required(ErrorMessage = "Booking ID is required")]
    public int BookingId { get; set; }

    [Column("UserID")]
    [Display(Name = "User ID")]
    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    [Column("EventID")]
    [Display(Name = "Event")]
    [Required(ErrorMessage = "Event is required")]
    public int EventId { get; set; }

    [Required(ErrorMessage = "Booking date is required")]
    [Display(Name = "Booking Date")]
    [DataType(DataType.DateTime)]
    public DateTime BookingsDate { get; set; }

    [Column("VenueID")]
    [Display(Name = "Venue")]
    [Required(ErrorMessage = "Venue is required")]
    public int VenueId { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Bookings")]
    public virtual Event? Event { get; set; }

    [ForeignKey("VenueId")]
    [InverseProperty("Bookings")]
    public virtual Venue? Venue { get; set; }
}