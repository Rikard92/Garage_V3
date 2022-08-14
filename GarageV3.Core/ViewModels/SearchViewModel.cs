namespace GarageV3.Core.ViewModels
{

#nullable disable
    public class SearchViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public IEnumerable<OwnerViewModel> Owners { get; set; }

        //ToDo: Add MemberShipViewModel
        //public IEnumerable<MemberShipViewModel> MemberShips { get; set; }


    }
}
