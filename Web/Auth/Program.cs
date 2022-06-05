using Auth;
using Infrastructure.Dependencies;
using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(opt =>
    {
        opt.AddDefaultPolicy(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    })
    .AddAuthenticationAndJwt()
    .AddAuthorization()
    .AddDbContext(builder.Configuration)
    .AddIdentity()
    .ConfigureIdentityOptions()
    .AddOpenIddictServer(builder.Environment)
    .AddEndpoints()
    .AddCoreServices();

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseCors();

app.MapEndpoints();

app.Run();
