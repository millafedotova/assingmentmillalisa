using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Api.Contracts;           
using SmartGreenhouse.Application.Services;  
using SmartGreenhouse.Application.Control; 

namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/devices")]
public class DevicesController : ControllerBase
{
    private readonly AppDbContext _db;

    public DevicesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _db.Devices.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Device dto)
    {
        _db.Devices.Add(dto);
        await _db.SaveChangesAsync();
        return Ok(dto);
    }
}
