using WrenchFlow.BLL.DTOs.Vehicles;
using WrenchFlow.BLL.Exceptions;
using WrenchFlow.BLL.Mapping;
using WrenchFlow.BLL.Services.Interfaces;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.BLL.Services.Implementations;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicles;
    private readonly ICustomerRepository _customers;

    public VehicleService(IVehicleRepository vehicles, ICustomerRepository customers)
    {
        _vehicles = vehicles;
        _customers = customers;
    }

    public async Task<List<VehicleDto>> GetAllAsync()
    {
        var list = await _vehicles.GetAllWithOwnerAsync();
        return list.Select(v => v.ToDto()).ToList();
    }

    public async Task<VehicleDto> GetByIdAsync(int id)
    {
        var vehicle = await _vehicles.GetByIdWithOwnerAsync(id)
            ?? throw new NotFoundException($"Vehicle {id} not found.");
        return vehicle.ToDto();
    }

    public async Task<List<VehicleDto>> GetByCustomerAsync(int customerId)
    {
        _ = await _customers.GetByIdAsync(customerId)
            ?? throw new NotFoundException($"Customer {customerId} not found.");

        var list = await _vehicles.GetByCustomerIdAsync(customerId);
        return list.Select(v => v.ToDto()).ToList();
    }

    public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
    {
        var customer = await _customers.GetByIdAsync(dto.CustomerId)
            ?? throw new NotFoundException($"Customer {dto.CustomerId} not found.");

        if (!customer.IsActive)
            throw new BusinessRuleException("This customer is not active.");

        if (await _vehicles.PlateExistsAsync(dto.PlateNumber.Trim()))
            throw new BusinessRuleException("A vehicle with this plate number already exists.");

        var vehicle = dto.ToEntity();
        await _vehicles.AddAsync(vehicle);
        await _vehicles.SaveChangesAsync();

        var saved = await _vehicles.GetByIdWithOwnerAsync(vehicle.VehicleId);
        return saved!.ToDto();
    }

    public async Task<VehicleDto> UpdateAsync(int id, UpdateVehicleDto dto)
    {
        var vehicle = await _vehicles.GetByIdWithOwnerAsync(id)
            ?? throw new NotFoundException($"Vehicle {id} not found.");

        if (await _vehicles.PlateExistsAsync(dto.PlateNumber.Trim(), id))
            throw new BusinessRuleException("A vehicle with this plate number already exists.");

        dto.ApplyTo(vehicle);
        await _vehicles.SaveChangesAsync();

        return vehicle.ToDto();
    }
}