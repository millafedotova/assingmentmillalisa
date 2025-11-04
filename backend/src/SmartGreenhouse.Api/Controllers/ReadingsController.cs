using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Api.Contracts;           
using SmartGreenhouse.Application.Services;  
using SmartGreenhouse.Application.Control;  


namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/readings")]
public class ReadingsController : ControllerBase
{
    private readonly ReadingService _readingService;
    private readonly AppDbContext _db;

    public ReadingsController(ReadingService readingService, AppDbContext db)
    {
        _readingService = readingService;
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Query([FromQuery] int? deviceId, [FromQuery] string? sensorType)
    {
        var items = await _readingService.QueryAsync(deviceId, sensorType);
        return Ok(items);
    }

    [HttpPost("capture")]
    public async Task<IActionResult> Capture([FromBody] SensorReading reading)
    {
        
        var saved = await _readingService.AddAsync(reading);
        return Ok(saved);
    }
}
