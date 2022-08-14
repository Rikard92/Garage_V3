using GarageV3.Data.Repositories.Interfaces;
using MKDevx.Data.Repositories;

namespace GarageV3.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GarageDBContext _context;


        public IVehicleRepository VehicleRepo { get; private set; }
        public IVehicleTypeRepository VehicleTypeRepo { get; private set; }

        public IOwnerTempRepository OwnerTempRepo { get; private set; }



        public UnitOfWork(GarageDBContext _context)
        {
            this._context = _context;

            VehicleRepo = new VehicleRepository(_context);
            VehicleTypeRepo = new VehicleTypeRepository(_context);
            OwnerTempRepo = new OwnerTempRepository(_context);

        }

        public async Task<int> CompleteAsync(bool stopTracker = false)
        {
            try
            {
                var save = await _context.SaveChangesAsync();
                if (stopTracker) { _context.ChangeTracker.Clear(); }
                return save;
            }
            catch (Exception e)
            {
                return -99;
                throw;
            }

        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
