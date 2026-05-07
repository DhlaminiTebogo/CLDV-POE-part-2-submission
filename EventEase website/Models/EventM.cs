using System.ComponentModel.DataAnnotations;

namespace EventEase_website.Models
{
    public class EventM
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public string? EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string? Description { get; set; }

        public int? VenueID { get; set; }
        public VenueM? Venue { get; set; }
    }
}
