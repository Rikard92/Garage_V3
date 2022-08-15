using GarageV3.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace GarageV3.Core.ViewModels
{
#nullable disable
    public class ParkCarViewModel
    {



        public IEnumerable<VehicleTypeViewModel> VehicleTypes { get; set; }

        public VehicleType VehicleType { get; set; } = new();
        public Vehicle Vehicle { get; set; }

        public OwnerViewModel Owner { get; set; }

        public IEnumerable<OwnerViewModel> Owners { get; set; } = new List<OwnerViewModel>();

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Regnr")]
        [RegularExpression(@"^[a-zA-Z-0-9 ]+$", ErrorMessage = "Endast siffror och text är tillåtet")]
        public string RegNr { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Färg")]
        public string Color { get; set; }

        [Range(0, 6)]
        [Display(Name = "Antal Hjul")]
        public int Wheels { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Modell")]
        public string CarModel { get; set; }


        [Display(Name = "Ankomsttid")]
        public DateTime ArrivalTime { get; set; } = DateTime.MinValue;


        public int GarageCapacity { get; set; }

        public int CurrentGarageCount { get; set; }

    }
}
