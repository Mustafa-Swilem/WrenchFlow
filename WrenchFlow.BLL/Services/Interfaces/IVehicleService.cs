using WrenchFlow.BLL.DTOs.Vehicles;

namespace WrenchFlow.BLL.Services.Interfaces;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetAllAsync();
    Task<VehicleDto> GetByIdAsync(int id);
    Task<List<VehicleDto>> GetByCustomerAsync(int customerId);
    Task<VehicleDto> CreateAsync(CreateVehicleDto dto);
    Task<VehicleDto> UpdateAsync(int id, UpdateVehicleDto dto);
}