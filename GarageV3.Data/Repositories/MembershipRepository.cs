using GarageV3.Core.Models;
using GarageV3.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MKDevx.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GarageV3.Data.Repositories
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public MembershipRepository(GarageDBContext context) : base(context)
        {
            
        }
        public override void Add(Membership entity)
        {
            base.Add(entity);
        }

        public override void Update(Membership entity)
        {
            Context.Update(entity);
        }

        public override void Remove(Membership entity)
        {
            Context.Remove(entity);
        }

        public async override Task<Membership?> GetAsync(string id, bool asNoTracking = false)
        {
            var isId = int.TryParse(id, out int idd);
            try
            {
                

                return await AppDbContext!.MemberShips
                     .Include(x => x.Owner)
                     .AsSplitQuery()
                    .FirstOrDefaultAsync(a => a.Id == int.Parse(id));

            }
            catch (Exception e)
            {

                throw;
            }
        }

        //public virtual IQueryable<Membership?> Find(Expression<Func<Vehicle, bool>> predicate, bool asNotracking = true) =>
        //  AppDbContext!.MemberShips
        //        .Include(v => v.Owner)
        //        .AsSplitQuery()
        //        .Where(predicate);

        public override IQueryable<Membership> GetAll(string sortAlt = "")
        {
            return AppDbContext!.MemberShips
                .Include(v => v.Owner)
                .AsSplitQuery();
        }
        public GarageDBContext AppDbContext
        {
            get { return Context; }
        }

    }
}
