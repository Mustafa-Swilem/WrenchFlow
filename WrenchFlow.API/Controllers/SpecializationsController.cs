using Microsoft.AspNetCore.Mvc;
using WrenchFlow.BLL.DTOs.Specializations;
using WrenchFlow.BLL.Services.Interfaces;

namespace WrenchFlow.API.Controllers;

[ApiController]
[Route("api/specializations")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _service;

    public SpecializationsController(ISpecializationService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<SpecializationDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SpecializationDto>> GetById(int id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<SpecializationDto>> Create(CreateSpecializationDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.SpecializationId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SpecializationDto>> Update(int id, UpdateSpecializationDto dto)
        => Ok(await _service.UpdateAsync(id, dto));
}