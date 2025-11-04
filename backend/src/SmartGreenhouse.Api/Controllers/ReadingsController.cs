using Microsoft.AspNetCore.Mvc;
using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Domain.Enums;
using SmartGreenhouse.Api.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingsController : ControllerBase
{
    private readonly CaptureReadingService _captureService;
    private readonly ReadingService _readingService;

    public ReadingsController(CaptureReadingService capture, ReadingService reading)
    {
        _captureService = capture;
        _readingService = reading; 
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? deviceId, [FromQuery] SensorTypeEnum? sensorType)
    {
        var readings = await _readingService.QueryAsync(deviceId, sensorType);
        var dtos = readings.Select(r => new ReadingDto(r.Id, r.DeviceId, r.SensorType, r.Value, r.Unit, r.Timestamp));
        return Ok(dtos);
    }

    [HttpPost("capture")]
    public async Task<IActionResult> Capture([FromBody] SmartGreenhouse.Api.Contracts.CaptureReadingRequest req)
    {
        var r = await _captureService.CaptureAsync(req.DeviceId, req.SensorType);
        return Ok(new ReadingDto(r.Id, r.DeviceId, r.SensorType, r.Value, r.Unit, r.Timestamp));
    }
}
