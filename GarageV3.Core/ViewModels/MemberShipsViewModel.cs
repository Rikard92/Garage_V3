namespace GarageV3.Client.Controllers
{
    using GarageV3.Core.Models;
    using System.ComponentModel.DataAnnotations;
    public class MemberShipsViewModel
    {
        public int MemberId { get; set; }

        [Display(Name = "Medlemskategori")]
        public string? MembershipCategory { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public double PersonNumber { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; }

        public int VehiclesNum { get; set; }

        public int OwnerId { get; set; }
    }
}