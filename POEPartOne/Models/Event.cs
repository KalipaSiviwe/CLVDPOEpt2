using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POEPartOne.Models
{
    public partial class Event
    {
        [Key]
        [Required(ErrorMessage = "Event ID is required")]
        [Column("EventID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Venue ID is required")]
        [Column("VenueID")]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        public string? Description { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [ForeignKey("VenueId")]
        [InverseProperty("Events")]
        public virtual Venue? Venue { get; set; }
    }
}