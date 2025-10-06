using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Domain.Enums;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Api.Contracts;


[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    // Accept payload like: { "deviceName": "Greenhouse Pi", "deviceType": "Simulated" }
    // deviceType is optional; if missing or invalid we default to DeviceTypeEnum.Simulated
    public record CreateDeviceRequest(string DeviceName, string? DeviceType);

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
    public async Task<IActionResult> Create([FromBody] CreateDeviceRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.DeviceName))
            return BadRequest("deviceName is required.");

        // parse optional enum name, default to Simulated on missing/invalid
        var deviceType = DeviceTypeEnum.Simulated;
        if (!string.IsNullOrWhiteSpace(req.DeviceType)
            && Enum.TryParse<DeviceTypeEnum>(req.DeviceType, true, out var parsed))
        {
            deviceType = parsed;
        }

        var device = new Device
        {
            DeviceName = req.DeviceName,
            DeviceType = deviceType
        };

        _db.Devices.Add(device);
        await _db.SaveChangesAsync();

        var dto = new DeviceDto(device.Id, device.DeviceName, device.DeviceType, device.CreatedAt);
        return Ok(dto);
    }
}
