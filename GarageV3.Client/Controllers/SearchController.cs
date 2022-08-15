using AutoMapper;
using GarageV3.Core.Models;
using GarageV3.Core.ViewModels;
using GarageV3.Data;
using GarageV3.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace GarageV3.Client.Controllers
{
    public class SearchController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;


        private int _garageCapacity;
        private int _ticketBasePrice;
        private string _currency;


        public SearchController(GarageDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(context);
            _configuration = configuration;

            _garageCapacity = int.Parse(configuration["GarageCapacity"]);
            _ticketBasePrice = int.Parse(_configuration["TicketBasePrice"]);
            _currency = _configuration["Currency"];
        }

        [HttpGet]
        [ActionName("Load")]
        public async Task<IActionResult> LoadAsync()
        {
            var model = SetLoadOption(new SearchViewModel());

            return await Task.FromResult(View("../Search/SearchMain", model));
        }

        [HttpPost]
        [ActionName("FindVehicle")]
        public async Task<IActionResult> FindVehicleAsync(SearchViewModel model)
        {

            IQueryable<VehicleViewModel> result;

            if (string.IsNullOrWhiteSpace(model.SearchOption))
            {
                result = _mapper.ProjectTo<VehicleViewModel>(_unitOfWork.VehicleRepo.GetAll());
            }
            else
            {
                Expression<Func<Vehicle, bool>> predicate = q =>
                    q.RegNr.ToLower().Contains(model.SearchOption.ToLower()) ||
                    q.Brand.ToLower().Contains(model.SearchOption.ToLower()) ||
                    q.Model.ToLower().Contains(model.SearchOption.ToLower()) ||
                    q.VehicleType.VType.ToLower().Contains(model.SearchOption.ToLower());

                result = _mapper.ProjectTo<VehicleViewModel>(_unitOfWork.VehicleRepo.Find(predicate));
            }

            var userInfo = result.Any() ? "" : "Inga poster funna";

            model.AltSearch = AltSearch.Vehicle;
            var _model = SetLoadOption(model, userInfo);

            model.Vehicles = result;


            return await Task.FromResult(View("../Search/SearchMain", model));


            throw new NotImplementedException();
        }



        //[HttpPost]
        //[ActionName("FindVehicle")]
        //public async Task<IActionResult> FindVehicleAsync(string findTarget)
        //{

        //    Expression<Func<Vehicle, bool>> predicate = q =>
        //        q.RegNr.ToLower().Contains(findTarget.ToLower()) ||
        //        q.Brand.ToLower().Contains(findTarget.ToLower()) ||
        //        q.Model.ToLower().Contains(findTarget.ToLower());


        //    //Expression<Func<Vehicle, bool>> predicate = q => q.RegNr.ToLower().Contains(model.Vehicle.RegNr);


        //    var result = _mapper.ProjectTo<VehicleViewModel>(_unitOfWork.VehicleRepo.Find(predicate));

        //    var userInfo = result.Any() ? "" : "Inga poster funna";

        //    model.AltSearch = AltSearch.Vehicle;
        //    var _model = SetLoadOption(model, userInfo);

        //    model.Vehicles = result;


        //    return await Task.FromResult(View("../Search/SearchMain", model));


        //    throw new NotImplementedException();
        //}


        [HttpPost]
        [ActionName("SelectOption")]
        public async Task<IActionResult> SelectOptionAsync(SearchViewModel model)
        {
            var _model = SetLoadOption(model);

            _model.SubTitle = "Utför sökningar baserat på dina kriterier";

            return await Task.FromResult(View("../Search/SearchMain", _model));
        }


        private SearchViewModel SetLoadOption(SearchViewModel _model, string userInfo = "")
        {
            switch (_model.AltSearch)
            {
                case AltSearch.MemberShip:
                    _model.HeadLine = "Sök i medlemsregistret";
                    break;
                case AltSearch.Owner:
                    _model.HeadLine = "Sök bland ägare";
                    break;
                case AltSearch.Vehicle:
                    _model.HeadLine = "Sök i garaget";
                    break;
                default:
                    _model.HeadLine = "Sök i databasen";
                    break;
            }

            _model.UserInfo = userInfo;

            return _model;
        }
    }
}
