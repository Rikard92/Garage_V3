using GarageV3.Client.Controllers;
using GarageV3.Core.Models;

namespace GarageV3.Core.ViewModels
{

    public enum AltSearch { NONE, Vehicle, MemberShip, Owner }

#nullable disable
    public class SearchViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public VehicleViewModel Vehicle { get; set; } = new VehicleViewModel();

        public IEnumerable<OwnerViewModel> Owners { get; set; }

        public IEnumerable<MemberShipsViewModel> MemberShips { get; set; }

        public MemberShipsViewModel MemberShipVM { get; set; } = new();


        public Membership MemberShip { get; set; }

        public OwnerViewModel Owner { get; set; } = new();

        public string SearchOption { get; set; }


        public AltSearch AltSearch { get; set; }

        public string UserInfo { get; set; }

        public string HeadLine { get; set; }
        public string SubTitle { get; set; }

        public bool IsSort { get; set; }

        public bool IsExactMatch { get; set; }

    }
}
