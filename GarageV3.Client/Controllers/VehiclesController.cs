using AutoMapper;
using GarageV3.Client.Filters;
using GarageV3.Core.Models;
using GarageV3.Core.ViewModels;
using GarageV3.Data;
using GarageV3.Data.Repositories.Interfaces;
using GarageV3.Util.Extensions;
using GarageV3.Util.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GarageV3.Controllers
{

    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;


        private int _garageSpace;
        private int _ticketBasePrice;
        private string _currency;

        public VehiclesController(GarageDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(context);
            _configuration = configuration;

            _garageSpace = int.Parse(configuration["GarageSpace"]);
            _ticketBasePrice = int.Parse(_configuration["TicketBasePrice"]);
            _currency = _configuration["Currency"];

        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await GetVehicles().ConfigureAwait(false);
            return View(viewModel);
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) { return NotFound(); }

            var vehicle = await GetVehicle(id);

            if (vehicle == null) { return NotFound(); }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }


        [HttpGet]
        public async Task<IActionResult> ParkCar()
        {
            var vtypes = await _unitOfWork.VehicleTypeRepo.GetAll().ToListAsync();

            var owners = _unitOfWork.OwnerTempRepo.GetAll();


            var parkCarVm = new ParkCarViewModel
            {
                Owner = new Owner(),
                VehicleTypes = vtypes,
                Owners = await _mapper.ProjectTo<OwnerViewModel>(owners).ToListAsync()

            };

            return View(parkCarVm);
        }


        [HttpPost]
        [ModelStateValidation]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ParkCar(ParkCarViewModel parkVM)
        {

            var _regNr = parkVM.RegNr.RemoveWhiteSpace().ToUpper();

            var isExist = await GetVehicle(_regNr);
            if (isExist is not null)
            {
                ViewData["HeadLine"] = "Meddelande";
                ViewData["UserMessage"] = $"Angivet registeringsnummer {_regNr} existerar redan vilket måste vara unikt";
                return View();
            }


            //Todo: Replace this with a _unitOfWork.OwnerRepo.GetAsync()

            var _owner = await _unitOfWork.OwnerTempRepo.GetAsync(parkVM.Owner.Id.ToString());

            //var owner = new Owner
            //{
            //    Id = 1,
            //    FirstName = "Pelle",
            //    LastName = "Jonsson",
            //    PersonNumber = 199005075012
            //};


            var vehicleType = await _unitOfWork.VehicleTypeRepo.GetAsync(parkVM.VehicleType.Id.ToString());

            parkVM.ArrivalTime = DateTime.Now;
            parkVM.RegNr = _regNr.ToUpper().RemoveWhiteSpace();
            parkVM.Color = parkVM.Color.TranslateColorLang();

            var vehicle = _mapper.Map<Vehicle>(parkVM);
            vehicle.ArrivalTime = DateTimeHelper.GetCurrentDate();
            vehicle.Owner = _owner;
            vehicle.VehicleType = vehicleType;


            _unitOfWork.VehicleRepo.Add(vehicle);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Details), this.ControllerContext.RouteData.Values["controller"].ToString(), new { id = vehicle.RegNr });
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ModelStateValidation]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegNr,Color,Wheels,Brand,Model,VehicleType")] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                ViewData["HeadLine"] = "Meddelande";
                ViewData["UserMessage"] = $"Välj fordonstyp i formuläret";
                return View();
            }

            var _regNr = vehicle.RegNr.RemoveWhiteSpace();

            var isExist = await GetVehicle(_regNr);
            if (isExist is not null)
            {
                ViewData["HeadLine"] = "Meddelande";
                ViewData["UserMessage"] = $"Angivet registeringsnummer {_regNr} existerar redan vilket måste vara unikt";
                return View();
            }

            vehicle.ArrivalTime = DateTime.Now;
            vehicle.RegNr = _regNr.ToUpper();
            vehicle.Color = vehicle.Color.TranslateColorLang();

            _unitOfWork.VehicleRepo.Add(vehicle);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Details), this.ControllerContext.RouteData.Values["controller"].ToString(), new { id = vehicle.RegNr });
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                ViewData["UserMessage"] = $"Regnummer {id} saknas";
                return NotFound();
            }

            var vehicle = await GetVehicleModel(id);

            if (vehicle == null) { return NotFound(); }

            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ModelStateValidation]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RegNr,Color,Wheels,Brand,Model,ArrivalTime,VehicleType")] Vehicle vehicle)
        {

            if (!ModelState.IsValid)
            {
                ViewData["UserMessage"] = $"Någonting gick fel. kontrollera att obligatoriska värden är ifyllda";
                return View();
            }


            if (vehicle.VehicleType.VType!.Contains("välj", StringComparison.OrdinalIgnoreCase))
            {
                ViewData["UserMessage"] = "Välj ett fordon i listan";
                return View();
            }


            if (id.ToUpper() != vehicle.RegNr.ToUpper())
            {
                ViewData["UserMessage"] = $"Registeringsnummer {id} saknas";
                return View();
            }


            _unitOfWork.VehicleRepo.Update(vehicle);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var vehicle = await GetVehicleModel(id);
            if (vehicle == null) { return NotFound(); }

            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> IndexFilter(string RegNr)
        {
            IQueryable<Vehicle> result;

            if (string.IsNullOrWhiteSpace(RegNr))
            {
                result = _unitOfWork.VehicleRepo.GetAll()!;
            }
            else
            {
                Expression<Func<Vehicle, bool>> predicate = m => m.RegNr.ToLower()!.StartsWith(RegNr.ToLower());
                result = _unitOfWork.VehicleRepo.Find(predicate)!;
            }

            return View(nameof(Index), await _mapper.ProjectTo<VehicleViewModel>(result).ToListAsync());
        }



        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            TicketViewModel voucher = new();

            var vehicle = await GetVehicleModel(id.ToString());

            if (vehicle != null)
            {
                voucher = CalculateVoucher(vehicle);
            }

            _unitOfWork.VehicleRepo.Remove(vehicle!);
            await _unitOfWork.CompleteAsync();

            voucher.Currency = _currency;

            return RedirectToAction(nameof(DeleteSucess), voucher);
        }


        public async Task<IActionResult> DeleteSucess(TicketViewModel kvitto)
        {
            return await Task.FromResult(View(kvitto));
        }


        [HttpPost]
        public async Task<IActionResult> Receit(TicketViewModel model)
        {

            //var receipt = _mapper.Map<ReceitViewModel>(model);


            ReceitViewModel receit = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                RegNr = model!.RegNr!,
                ArrivalTime = model.ArrivalTime,
                CheckOutTime = model.CheckOutTime,
                ParkTime = model.ParkTime,
                ParkingPrice = $"{model.Price} {_currency}",
                MinimumFee = $"{_ticketBasePrice} {_currency}",
                FeePerHour = $"{_ticketBasePrice} {_currency}"

            };

            return await Task.FromResult(View(receit));
        }



        private async Task<bool> VehicleExists(string id)
        {
            var isExist = await GetVehicleModel(id);
            return isExist != null;
        }

        [HttpGet]
        public async Task<IActionResult> SetVehicleType(string vehicleType)
        {
            return Json(vehicleType);
        }


        /// <summary>
        /// Get vehicle collection from db
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<VehicleViewModel>> GetVehicles()
        {
            var result01 = _unitOfWork.VehicleRepo.GetAll();

            return await _mapper.ProjectTo<VehicleViewModel>(result01)
                    .OrderByDescending(o => o.ArrivalTime).ToListAsync();
        }

        /// <summary>
        /// Gets single Vehicle from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<VehicleViewModel?> GetVehicle(string id)
        {
            var result = await _unitOfWork.VehicleRepo.GetAsync(id);

            return _mapper.Map<VehicleViewModel>(result);

        }


        /// <summary>
        /// Gets Vehicle pure viehicle model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Vehicle?> GetVehicleModel(string id)
        {
            return await _unitOfWork.VehicleRepo.GetAsync(id);

        }

        /// <summary>
        /// Calculates car parking price
        /// </summary>
        /// <param name="_vehicle"></param>
        /// <returns></returns>
        private TicketViewModel CalculateVoucher(Vehicle _vehicle)
        {
            if (_vehicle is null)
            {
                ArgumentNullException argumentNullException = new("Vehicle är null");
                throw argumentNullException;
            }

            float tempPrice;
            var dateTimeNow = DateTimeHelper.GetCurrentDate();

            var voucher = new TicketViewModel();

            try
            {
                voucher.FirstName = _vehicle.Owner.FirstName;
                voucher.LastName = _vehicle.Owner.LastName;
                voucher.ArrivalTime = _vehicle.ArrivalTime;
                voucher.CheckOutTime = DateTime.Now;
                voucher.RegNr = _vehicle.RegNr;
                voucher.ParkTime = dateTimeNow - _vehicle.ArrivalTime;

                tempPrice = (float)voucher.ParkTime.TotalHours * _ticketBasePrice;
                tempPrice = (float)Math.Round(tempPrice, 2);

                if (tempPrice < _ticketBasePrice) tempPrice = _ticketBasePrice;

                voucher.Price = $"{tempPrice.ToString()} {_currency}";
                // avgift = 12Kr/h

                return voucher;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
