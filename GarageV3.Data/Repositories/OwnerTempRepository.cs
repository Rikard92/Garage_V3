using GarageV3.Core.Models;
using GarageV3.Data;
using GarageV3.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MKDevx.Data.Repositories
{
    public class OwnerTempRepository : Repository<Owner>, IOwnerTempRepository
    {
        public OwnerTempRepository(GarageDBContext context) : base(context)
        {
        }

        public override void Add(Owner entity)
        {
            AppDbContext.Entry(entity.Membership).State = EntityState.Unchanged;
            AppDbContext.Entry(entity.Vehicles).State = EntityState.Unchanged;

            base.Add(entity);
        }

        public override void Update(Owner entity)
        {
            AppDbContext.Entry(entity.Membership).State = EntityState.Unchanged;
            AppDbContext.Entry(entity.Vehicles).State = EntityState.Unchanged;

            Context.Update(entity);
        }

        public override void Remove(Owner entity)
        {
            //AppDbContext.Entry(entity.Membership).State = EntityState.Modified;

            Context.Remove(entity);
        }


        public async override Task<Owner?> GetAsync(string id, bool asNoTracking = false)
        {
            var isId = int.TryParse(id, out int idd);
            try
            {
                return await AppDbContext!.Owners
                    .Include(i => i.Id)
                    .Include(i => i.Vehicles)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(a => a.Id == int.Parse(id) || a.PersonNumber == int.Parse(id));

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public virtual IQueryable<Owner?> Find(Expression<Func<Owner, bool>> predicate, bool asNotracking = true) =>
          AppDbContext!.Owners
                .Include(i => i.Membership)
                .Include(i => i.Vehicles)
                .Where(predicate);

        public override IQueryable<Owner> GetAll(string sortAlt = "")
        {
            return AppDbContext!.Owners
                .Include(i => i.Membership)
                .Include(i => i.Vehicles)
                .OrderBy(o => o.PersonNumber);
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
