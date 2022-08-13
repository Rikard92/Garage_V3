using System.ComponentModel;

namespace GarageV3.Core.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        /// <summary>
        /// Vehicle type
        /// </summary>
        /// 
        [DisplayName("Fordonstyp")]
        public string? VType { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}
