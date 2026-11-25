using Microsoft.EntityFrameworkCore;
using WrenchFlow.DAL.Data;
using WrenchFlow.DAL.Entities;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.DAL.Repositories.Implementations;

public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context) { }

    private IQueryable<Vehicle> WithOwner()
        => _dbSet.Include(v => v.Customer).ThenInclude(c => c.Person);

    public async Task<Vehicle?> GetByIdWithOwnerAsync(int id)
        => await WithOwner().FirstOrDefaultAsync(v => v.VehicleId == id);

    public async Task<List<Vehicle>> GetAllWithOwnerAsync()
        => await WithOwner().ToListAsync();

    public async Task<List<Vehicle>> GetByCustomerIdAsync(int customerId)
        => await WithOwner().Where(v => v.CustomerId == customerId).ToListAsync();

    public async Task<bool> PlateExistsAsync(string plateNumber, int? excludeVehicleId = null)
        => await _dbSet.AnyAsync(v => v.PlateNumber == plateNumber
            && (excludeVehicleId == null || v.VehicleId != excludeVehicleId));
}