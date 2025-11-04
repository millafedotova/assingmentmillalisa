using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Api.Contracts;           
using SmartGreenhouse.Application.Services;  
using SmartGreenhouse.Application.Control; 

namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/alerts")]
public class AlertsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AlertsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int deviceId)
    {
        var alerts = await _db.AlertNotifications
            .Where(a => a.DeviceId == deviceId)
            .OrderByDescending(a => a.Timestamp)
            .ToListAsync();

        return Ok(alerts);
    }
}
