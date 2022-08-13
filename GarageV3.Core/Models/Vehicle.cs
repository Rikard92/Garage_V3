using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageV3.Core.Models
{

#nullable disable
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string Model { get; set; }

        public VehicleType VehicleType { get; set; }

        public int VehicleTypeId { get; set; }

        public Owner Owner { get; set; }

        public int OwnerId { get; set; }

        [Display(Name = "Ankomsttid")]
        public DateTime ArrivalTime { get; set; } = DateTime.MinValue;

    }
}
