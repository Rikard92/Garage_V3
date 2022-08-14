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
            return await Task.FromResult(View("../Search/SearchMain", new SearchViewModel()));
        }

        [HttpPost]
        [ActionName("FindVehicle")]
        public async Task<IActionResult> FindVehicleAsync(VehicleViewModel model)
        {

            Expression<Func<Vehicle, bool>> predicate = q => q.Brand.ToLower().Contains(model.Brand);

            var result = _mapper.ProjectTo<VehicleViewModel>(_unitOfWork.VehicleRepo.Find(predicate));


            //return View


            throw new NotImplementedException();
        }



        [HttpPost]
        [ActionName("SelectOption")]
        public async Task<IActionResult> SelectOptionAsync(SearchViewModel model)
        {
            return await Task.FromResult(View(model));
        }

    }
}
