using WrenchFlow.BLL.DTOs.Specializations;
using WrenchFlow.DAL.Entities;

namespace WrenchFlow.BLL.Mapping;

public static class SpecializationMappings
{
    public static SpecializationDto ToDto(this Specialization specialization) => new()
    {
        SpecializationId = specialization.SpecializationId,
        SpecializationName = specialization.SpecializationName
    };
}