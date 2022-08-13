using System.ComponentModel.DataAnnotations;

namespace GarageV3.Core.Models
{
    // Todo: Skapa ett filter
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Förnamn")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string? LastName { get; set; }

        public double PersonNumber { get; set; }
        public Membership? Membership { get; set; }



        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}