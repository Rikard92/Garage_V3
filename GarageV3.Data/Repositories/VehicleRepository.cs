using GarageV3.Core.Models;
using GarageV3.Data;
using GarageV3.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MKDevx.Data.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(GarageDBContext context) : base(context)
        {
        }

        public override void Add(Vehicle entity)
        {
            AppDbContext.Entry(entity.Owner).State = EntityState.Unchanged;
            AppDbContext.Entry(entity.VehicleType).State = EntityState.Unchanged;

            AppDbContext.Add(entity);
        }

        public override void Update(Vehicle entity)
        {
            AppDbContext.Entry(entity.Owner).State = EntityState.Unchanged;
            AppDbContext.Entry(entity.VehicleType).State = EntityState.Unchanged;
            AppDbContext.Update(entity);
        }

        public override void Remove(Vehicle entity)
        {
            AppDbContext.Entry(entity.Owner).State = EntityState.Unchanged;
            AppDbContext.Entry(entity.VehicleType).State = EntityState.Unchanged;

            AppDbContext.Remove(entity);
        }


        public async override Task<Vehicle?> GetAsync(string id, bool asNoTracking = false)
        {
            var isId = int.TryParse(id, out int idd);
            try
            {
                if (isId)
                {
                    return await AppDbContext!.Vehicles
                        .Include(x => x.Owner)
                        .Include(x => x.VehicleType)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(a => a.Id == int.Parse(id));

                }

                return await AppDbContext!.Vehicles
                     .Include(x => x.Owner)
                     .Include(x => x.VehicleType)
                     .AsSplitQuery()
                    .FirstOrDefaultAsync(a => a.RegNr.ToLower().Contains(id.ToLower()));

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public virtual IQueryable<Vehicle?> Find(Expression<Func<Vehicle, bool>> predicate, bool asNotracking = true) =>
          AppDbContext!.Vehicles
                .Include(v => v.Owner)
                .Include(v => v.VehicleType)
                .AsSplitQuery()
                .Where(predicate);

        public override IQueryable<Vehicle> GetAll(string sortAlt = "")
        {
            return AppDbContext!.Vehicles
                .Include(v => v.Owner)
                .Include(v => v.VehicleType)
                .AsSplitQuery();
        }


        /// <summary>
        /// Sets the generic context to its type
        /// </summary>
        public GarageDBContext AppDbContext
        {
            get { return Context; }
        }

    }
}
