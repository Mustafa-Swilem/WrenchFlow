using WrenchFlow.BLL.DTOs.Specializations;
using WrenchFlow.BLL.Exceptions;
using WrenchFlow.BLL.Mapping;
using WrenchFlow.BLL.Services.Interfaces;
using WrenchFlow.DAL.Entities;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.BLL.Services.Implementations;

public class SpecializationService : ISpecializationService
{
    private readonly IGenericRepository<Specialization> _repo;

    public SpecializationService(IGenericRepository<Specialization> repo) => _repo = repo;

    public async Task<List<SpecializationDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.OrderBy(s => s.SpecializationName).Select(s => s.ToDto()).ToList();
    }

    public async Task<SpecializationDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Specialization {id} not found.");
        return entity.ToDto();
    }

    public async Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto)
    {
        var name = dto.SpecializationName.Trim();

        var duplicates = await _repo.FindAsync(s => s.SpecializationName == name);
        if (duplicates.Any())
            throw new BusinessRuleException("This specialization already exists.");

        var entity = new Specialization { SpecializationName = name };
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return entity.ToDto();
    }

    public async Task<SpecializationDto> UpdateAsync(int id, UpdateSpecializationDto dto)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Specialization {id} not found.");

        var name = dto.SpecializationName.Trim();

        var duplicates = await _repo.FindAsync(s => s.SpecializationName == name
            && s.SpecializationId != id);
        if (duplicates.Any())
            throw new BusinessRuleException("This specialization already exists.");

        entity.SpecializationName = name;
        await _repo.SaveChangesAsync();

        return entity.ToDto();
    }
}