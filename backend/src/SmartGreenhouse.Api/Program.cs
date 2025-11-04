using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSingleton<SimulatedDeviceFactory>();
builder.Services.AddSingleton<IDeviceFactoryResolver, DeviceFactoryResolver>();
builder.Services.AddScoped<CaptureReadingService>();
builder.Services.AddScoped<ReadingService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
