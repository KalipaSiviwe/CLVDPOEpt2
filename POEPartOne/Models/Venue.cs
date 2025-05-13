using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POEPartOne.Models;

public partial class Venue
{
    [Key]
    [Column("VenueID")]
    [Required(ErrorMessage = "Venue ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Venue ID must be a positive number")]
    public int VenueId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Location is required")]
    [StringLength(250)]
    [Unicode(false)]
    public string Location { get; set; }

    [Required(ErrorMessage = "Capacity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
    public int Capacity { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    [Url(ErrorMessage = "Please enter a valid URL for the image")]
    public string? ImageUrl { get; set; }

    [InverseProperty("Venue")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Venue")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}