namespace GarageV3.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository VehicleRepo { get; }
        IVehicleTypeRepository VehicleTypeRepo { get; }

        Task<int> CompleteAsync(bool stopTracker = false);

    }
}
