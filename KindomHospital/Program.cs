using Serilog;
using KindomHospital.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Host.UseSerilog((context, services, lc) =>
    lc.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllers();

// DbContext uniquement pour tester la base
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
