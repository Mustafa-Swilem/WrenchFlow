using WrenchFlow.DAL.Entities;

namespace WrenchFlow.DAL.Repositories.Interfaces;

public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    Task<Vehicle?> GetByIdWithOwnerAsync(int id);
    Task<List<Vehicle>> GetAllWithOwnerAsync();
    Task<List<Vehicle>> GetByCustomerIdAsync(int customerId);
    Task<bool> PlateExistsAsync(string plateNumber, int? excludeVehicleId = null);
}