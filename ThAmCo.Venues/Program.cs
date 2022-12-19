using Microsoft.EntityFrameworkCore;
using ThAmCo.Venues.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:7004");
        });
});

// Register database context with the framework
builder.Services.AddDbContext<VenuesDbContext>();


var app = builder.Build();


    //app.UseRouting();
app.UseCors();
    //app.UseAuthorization();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
