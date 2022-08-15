namespace GarageV3.Core.ViewModels
{

    using System.ComponentModel.DataAnnotations;

#nullable disable
    public class VehicleViewModel
    {
        public int Id { get; set; }


        [Key]
        [StringLength(10)]
        [Display(Name = "Registrerings nummer")]
        [RegularExpression(@"^[a-zA-Z-0-9 ]+$", ErrorMessage = "Endast siffror och text är tillåtet")]
        public string RegNr { get; set; }

        public string VType { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        [Display(Name = "Parkeringsdatum")]
        [DataType(DataType.Date)]
        public DateTime ArrivalTime { get; init; }

        [Display(Name = "Färg")]
        public string Color { get; set; }

        [Display(Name = "Antal hjul")]
        public int Wheels { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }



        public VehicleTypeViewModel VehicleTypeVM { get; set; } = new();


        /// <summary>
        /// HeadLine
        /// </summary>
        public string HeadLine { get; set; }

        /// <summary>
        /// Message shown to the parking user - CRUD operations
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// Flag indicating error or success - CRUD Operations
        /// </summary>
        public bool IsSucceed { get; set; } = true;

    }
}