using System.ComponentModel.DataAnnotations;

namespace WrenchFlow.BLL.DTOs.Vehicles;

public class UpdateVehicleDto
{
    [Required, StringLength(20)]
    public string PlateNumber { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Make { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int Year { get; set; }

    [StringLength(30)]
    public string? Color { get; set; }
}