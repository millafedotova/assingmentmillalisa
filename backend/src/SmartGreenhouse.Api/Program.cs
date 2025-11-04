using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Application.Control;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Application.Abstractions;
using SmartGreenhouse.Application.Events;
using SmartGreenhouse.Application.Events.Observers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Port=5432;Database=greenhouse;Username=postgres;Password=lisa";

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cs));

builder.Services.AddScoped<IReadingPublisher, ReadingPublisher>();
builder.Services.AddScoped<IReadingObserver, LogObserver>();
builder.Services.AddScoped<IReadingObserver, AlertRuleObserver>();
builder.Services.AddScoped<ReadingService>();

builder.Services.AddSingleton<HysteresisCoolingStrategy>();
builder.Services.AddSingleton<MoistureTopUpStrategy>();
builder.Services.AddScoped<ControlStrategySelector>();
builder.Services.AddScoped<ControlService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

await app.RunAsync();
