using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly AppDbContext _db;
    public DevicesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var dtos = await _db.Devices.AsNoTracking()
            .Select(d => new DeviceDto(d.Id, d.DeviceName, d.DeviceType, d.CreatedAt))
            .ToListAsync();
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Device device)
    {
        _db.Devices.Add(device);
        await _db.SaveChangesAsync();
        return Ok(device);
    }
}
