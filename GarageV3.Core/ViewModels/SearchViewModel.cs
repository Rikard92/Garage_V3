namespace GarageV3.Core.ViewModels
{

    public enum AltSearch { NONE, Vehicle, MemberShip, Owner }

#nullable disable
    public class SearchViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public VehicleViewModel Vehicle { get; set; } = new();

        public IEnumerable<OwnerViewModel> Owners { get; set; }

        public OwnerViewModel Owner { get; set; } = new();


        //ToDo: Add MemberShipViewModel
        //public IEnumerable<MemberShipViewModel> MemberShips { get; set; }




        public AltSearch AltSearch { get; set; }


        public string HeadLine { get; set; }
        public string SubTitle { get; set; }
    }
}
