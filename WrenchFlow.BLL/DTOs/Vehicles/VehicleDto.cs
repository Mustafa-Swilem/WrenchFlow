namespace WrenchFlow.BLL.DTOs.Vehicles;

public class VehicleDto
{
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string PlateNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedDate { get; set; }
}