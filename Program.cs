using IbadanGadgetAPI.Data;
using IbadanGadgetAPI.Entities;
using IbadanGadgetAPI.Interfaces;
using IbadanGadgetAPI.Repositories;
using IbadanGadgetAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------
// Add services to the container
// ------------------------------------------------------
builder.Services.AddControllers();

// Swagger / OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------
// Database configuration (SQL Server)
// ------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
	;

builder.Services.AddDbContext<IbadanGadgetDbContext>(options =>
	options.UseNpgsql(connectionString));

// ------------------------------------------------------
// Dependency Injection setup
// ------------------------------------------------------
builder.Services.AddScoped<IbadanGadgetAPI.Interfaces.IGadgetRepository, GadgetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGadgetService, GadgetService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// ------------------------------------------------------
// Build and configure the app
// ------------------------------------------------------
var app = builder.Build();

// ------------------------------------------------------
// Database Initialization (Development only)
// ------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<IbadanGadgetDbContext>();
	db.Database.EnsureCreated(); // Creates DB if it doesn’t exist
}

// ------------------------------------------------------
// Middleware pipeline
// ------------------------------------------------------


	app.UseSwagger();
	app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
