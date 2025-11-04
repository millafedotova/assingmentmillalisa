using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Api.Contracts;           
using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Application.Control;
using System.Text.Json;

namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/control")]
public class ControlController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ControlService _service;

    public ControlController(AppDbContext db, ControlService service)
    {
        _db = db;
        _service = service;
    }

    [HttpPost("profile")]
    public async Task<IActionResult> SetProfile([FromBody] SetControlProfileRequest request)
    {
        var profile = await _db.ControlProfiles.FirstOrDefaultAsync(p => p.DeviceId == request.DeviceId);

        if (profile == null)
        {
            profile = new ControlProfile
            {
                DeviceId = request.DeviceId,
                StrategyKey = request.StrategyKey,
                ParametersJson = request.Parameters != null ? JsonSerializer.Serialize(request.Parameters) : null
            };
            _db.ControlProfiles.Add(profile);
        }
        else
        {
            profile.StrategyKey = request.StrategyKey;
            profile.ParametersJson = request.Parameters != null ? JsonSerializer.Serialize(request.Parameters) : null;
        }

        await _db.SaveChangesAsync();
        return Ok(profile);
    }

    [HttpPost("evaluate")]
    public async Task<IActionResult> Evaluate([FromBody] EvaluateControlRequest request)
    {
        var commands = await _service.EvaluateAsync(request.DeviceId);
        return Ok(commands);
    }
}
