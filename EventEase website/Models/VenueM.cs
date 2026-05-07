using System.ComponentModel.DataAnnotations;

namespace EventEase_website.Models
{
    public class VenueM
    {
        [Key]
        public int VenueID { get; set; }

        [Required]

        public string? VenueName { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
