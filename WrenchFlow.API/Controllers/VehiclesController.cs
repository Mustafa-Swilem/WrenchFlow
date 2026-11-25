using Microsoft.AspNetCore.Mvc;
using WrenchFlow.BLL.DTOs.Vehicles;
using WrenchFlow.BLL.Services.Interfaces;

namespace WrenchFlow.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _service;

    public VehiclesController(IVehicleService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<VehicleDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleDto>> GetById(int id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpGet("by-customer/{customerId:int}")]
    public async Task<ActionResult<List<VehicleDto>>> GetByCustomer(int customerId)
        => Ok(await _service.GetByCustomerAsync(customerId));

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> Create(CreateVehicleDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.VehicleId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<VehicleDto>> Update(int id, UpdateVehicleDto dto)
        => Ok(await _service.UpdateAsync(id, dto));
}