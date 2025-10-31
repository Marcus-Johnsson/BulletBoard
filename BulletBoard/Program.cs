using BulletBoard.Data;
using BulletBoard.EndPoints.Notes;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Localhost", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<BulletDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("Localhost");
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
UpdateNotes.MapEndPoints(app);
GetNotes.MapEndpoint(app);
DeleteNotes.MapEndpoint(app);
CreateNotes.MapEndpoint(app);
UpdateNotes.MapEndPoints(app);
GetNotes.MapEndpoint(app);

app.Run();
