namespace GarageV3.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository VehicleRepo { get; }
        IVehicleTypeRepository VehicleTypeRepo { get; }

        IOwnerTempRepository OwnerTempRepo { get; }

        IOwnerRepository OwnerRepo { get; }

        IMembershipRepository MembershipRepo { get; }

        Task<int> CompleteAsync(bool stopTracker = false);

    }
}
