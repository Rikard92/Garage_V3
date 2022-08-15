using System.ComponentModel;

namespace GarageV3.Core.ViewModels
{
#nullable disable
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }


        [DisplayName("Fordonstyp")]
        public string VType { get; set; }

        public IEnumerable<VehicleTypeViewModel> VehicleTypes { get; set; }
    }
}
