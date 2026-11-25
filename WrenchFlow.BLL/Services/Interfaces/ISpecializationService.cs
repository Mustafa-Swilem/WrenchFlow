using WrenchFlow.BLL.DTOs.Specializations;

namespace WrenchFlow.BLL.Services.Interfaces;

public interface ISpecializationService
{
    Task<List<SpecializationDto>> GetAllAsync();
    Task<SpecializationDto> GetByIdAsync(int id);
    Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto);
    Task<SpecializationDto> UpdateAsync(int id, UpdateSpecializationDto dto);
}