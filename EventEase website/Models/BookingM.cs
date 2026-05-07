using System.ComponentModel.DataAnnotations;

namespace EventEase_website.Models
{
    public class BookingM
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int EventID { get; set; }
        public EventM? Event { get; set; }

        [Required]
        public int VenueID { get; set; }
        public VenueM? Venue { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
