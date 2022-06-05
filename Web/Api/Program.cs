using Infrastructure.Dependencies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddApplicationDbContext(connectionString);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
