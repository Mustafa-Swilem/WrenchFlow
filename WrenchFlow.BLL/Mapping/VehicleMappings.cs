using WrenchFlow.BLL.DTOs.Vehicles;
using WrenchFlow.DAL.Entities;

namespace WrenchFlow.BLL.Mapping;

public static class VehicleMappings
{
    public static VehicleDto ToDto(this Vehicle vehicle) => new()
    {
        VehicleId = vehicle.VehicleId,
        CustomerId = vehicle.CustomerId,
        OwnerName = $"{vehicle.Customer.Person.FirstName} {vehicle.Customer.Person.LastName}",
        PlateNumber = vehicle.PlateNumber,
        Make = vehicle.Make,
        Model = vehicle.Model,
        Year = vehicle.Year,
        Color = vehicle.Color,
        CreatedDate = vehicle.CreatedDate
    };

    public static Vehicle ToEntity(this CreateVehicleDto dto) => new()
    {
        CustomerId = dto.CustomerId,
        PlateNumber = dto.PlateNumber.Trim(),
        Make = dto.Make.Trim(),
        Model = dto.Model.Trim(),
        Year = dto.Year,
        Color = dto.Color
    };

    public static void ApplyTo(this UpdateVehicleDto dto, Vehicle vehicle)
    {
        vehicle.PlateNumber = dto.PlateNumber.Trim();
        vehicle.Make = dto.Make.Trim();
        vehicle.Model = dto.Model.Trim();
        vehicle.Year = dto.Year;
        vehicle.Color = dto.Color;
    }
}