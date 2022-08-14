namespace GarageV3.Core.ViewModels
{

    public enum AltSearch { NONE, Vehicle, MemberShip, Owner }

#nullable disable
    public class SearchViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public IEnumerable<OwnerViewModel> Owners { get; set; }

        //ToDo: Add MemberShipViewModel
        //public IEnumerable<MemberShipViewModel> MemberShips { get; set; }

        public AltSearch AltSearch { get; set; }


        public string HeadLine { get; set; }
        public string SubTitle { get; set; }
    }
}
