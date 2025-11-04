using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Api.Contracts;           
using SmartGreenhouse.Application.Services;  
using SmartGreenhouse.Application.Control; 

namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/alertrules")]
public class AlertRulesController : ControllerBase
{
    private readonly AppDbContext _db;

    public AlertRulesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rules = await _db.AlertRules.ToListAsync();
        return Ok(rules);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertAlertRuleRequest request)
    {
        var rule = new AlertRule
        {
            DeviceId = request.DeviceId,
            SensorType = request.SensorType,
            OperatorSymbol = request.OperatorSymbol,
            Threshold = request.Threshold,
            IsActive = request.IsActive
        };

        _db.AlertRules.Add(rule);
        await _db.SaveChangesAsync();
        return Ok(rule);
    }
}


