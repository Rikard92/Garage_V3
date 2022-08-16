using AutoMapper;
using GarageV3.Core.Models;
using GarageV3.Data;
using GarageV3.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarageV3.Client.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly GarageDBContext _context;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork;

        public MembershipsController(GarageDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            //var garageV3ClientContext = _context.MemberShips.Include(m => m.Owner);
            //return View(await garageV3ClientContext.ToListAsync());
            var viewModel = await GetMembers().ConfigureAwait(false);
            //var viewModel = await _unitOfWork.MembershipRepo.GetMembers().ConfigureAwait(false);
            return View(viewModel);
        }

        // GET: Memberships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberShips == null)
            {
                return NotFound();
            }

            var membership = await _context.MemberShips
                .Include(m => m.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // GET: Memberships/Create
        public async Task<IActionResult> CreateAsync()
        {
            //ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "Id", "FirstName");

            //var vtypes = await _unitOfWork.VehicleTypeRepo.GetAll().ToListAsync();

            var memcat = await _unitOfWork.MembershipRepo.GetAll().Select(x => x.MembershipCategory).Distinct().ToListAsync();
            ViewData["MembershipCategory"] = new SelectList(memcat);
            return View();
        }

        // POST: Memberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember([Bind("Id,MembershipCategory,PersonNumber,FirstName,LastName")] MemberShipsViewModel membershipInfo)
        {

            var vtypes = await _unitOfWork.VehicleTypeRepo.GetAll().ToListAsync();

            if (ModelState.IsValid)
            {
                Owner Owner = new Owner
                {
                    PersonNumber = membershipInfo.PersonNumber,
                    FirstName = membershipInfo.FirstName,
                    LastName = membershipInfo.LastName,
                    Vehicles = new List<Vehicle>()
                };

                Membership Membership = new Membership
                {
                    MembershipCategory = membershipInfo.MembershipCategory,
                    Owner = Owner
                };
                //Membership.OwnerId = membershipInfo.OwnerId;

                Owner.Membership = Membership;

                //_context.Add(Owner);
                //_context.Add(Membership);
                //await _context.SaveChangesAsync();

                _unitOfWork.OwnerRepo.Add(Owner);
                _unitOfWork.MembershipRepo.Add(Membership);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "Id", "FirstName", membership.OwnerId);

            return View("Create", membershipInfo);
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberShips == null)
            {
                return NotFound();
            }

            var membership = await _context.MemberShips.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "Id", "FirstName", membership.OwnerId);
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MembershipCategory,OwnerId")] Membership membership)
        {
            if (id != membership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "Id", "FirstName", membership.OwnerId);
            return View(membership);
        }

        // GET: Memberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberShips == null)
            {
                return NotFound();
            }

            var membership = await _context.MemberShips
                .Include(m => m.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberShips == null)
            {
                return Problem("Entity set 'GarageV3ClientContext.Membership'  is null.");
            }
            var membership = await _context.MemberShips.FindAsync(id);
            if (membership != null)
            {
                _context.MemberShips.Remove(membership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(int id)
        {
            return (_context.MemberShips?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<List<MemberShipsViewModel>> GetMembers()
        {
            //var result01 = _unitOfWork.MembershipRepo.GetAll();
            //return await _mapper.ProjectTo<MemberShipsViewModel>(result01).ToListAsync();

            return await _context.MemberShips.Select(e => new MemberShipsViewModel
            {
                MemberId = e.Id,
                OwnerId = e.OwnerId,
                FirstName = e.Owner.FirstName,
                LastName = e.Owner.LastName,
                PersonNumber = e.Owner.PersonNumber,
                Vehicles = e.Owner.Vehicles,
                VehiclesNum = e.Owner.Vehicles.Count(),
                MembershipCategory = e.MembershipCategory

            }).OrderBy(x => x.FirstName).ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> IndexFilter(string SearchInput)
        {
            var vehicles = string.IsNullOrWhiteSpace(SearchInput) ?
                                    _context.Vehicles :
                                    _context.Vehicles.Where(m => m.RegNr.ToLower()!.StartsWith(SearchInput.ToLower()));

            return View(nameof(Index), await _mapper.ProjectTo<MemberShipsViewModel>(vehicles).ToListAsync());
        }
    }
}
