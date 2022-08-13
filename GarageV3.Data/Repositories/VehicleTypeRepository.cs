using GarageV3.Core.Models;
using GarageV3.Data;
using GarageV3.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MKDevx.Data.Repositories
{
    public class VehicleTypeRepository : Repository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(GarageDBContext context) : base(context)
        {
        }

        public override void Add(VehicleType entity)
        {
            base.Add(entity);
        }

        public override void Update(VehicleType entity)
        {
            Context.Update(entity);
        }

        public override void Remove(VehicleType entity)
        {
            Context.Remove(entity);
        }


        public async override Task<VehicleType?> GetAsync(string id, bool asNoTracking = false)
        {
            var isId = int.TryParse(id, out int idd);
            try
            {
                if (isId)
                {
                    return await AppDbContext!.VehicleTypes
                        .FirstOrDefaultAsync(a => a.Id == int.Parse(id));

                }

                return await AppDbContext!.VehicleTypes
                    .FirstOrDefaultAsync(a => a.VType!.ToLower() == id.ToLower());

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public virtual IQueryable<VehicleType?> Find(Expression<Func<VehicleType, bool>> predicate, bool asNotracking = true) =>
          AppDbContext!.VehicleTypes
                .Where(predicate);

        public override IQueryable<VehicleType> GetAll(string sortAlt = "")
        {
            return AppDbContext!.VehicleTypes
                .OrderBy(o => o.VType);
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
