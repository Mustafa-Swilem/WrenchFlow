using System.ComponentModel.DataAnnotations;

namespace WrenchFlow.BLL.DTOs.Specializations;

public class CreateSpecializationDto
{
    [Required, StringLength(50)]
    public string SpecializationName { get; set; } = string.Empty;
}