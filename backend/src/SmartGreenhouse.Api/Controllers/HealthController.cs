using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Api.Contracts;           
using SmartGreenhouse.Application.Services;  
using SmartGreenhouse.Application.Control; 

namespace SmartGreenhouse.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "ok" });
}
