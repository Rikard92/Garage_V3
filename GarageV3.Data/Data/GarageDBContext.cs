using GarageV3.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageV3.Data
{
    public class GarageDBContext : DbContext
    {
        public GarageDBContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();

        public DbSet<Membership> MemberShips => Set<Membership>();

        public DbSet<Owner> Owners => Set<Owner>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
