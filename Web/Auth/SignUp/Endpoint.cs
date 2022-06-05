using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint;

namespace Auth.SignUp;

public class Endpoint : IEndpoint<IResult, Request>
{
    private UserManager<ApplicationUser> _userManager;

    public void AddRoute (IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.SignUp, async (Request request, UserManager<ApplicationUser> userManager) =>
        {
            _userManager = userManager;
            return await HandleAsync(request);
        });
    }

    public async Task<IResult> HandleAsync (Request request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result);
        }

        return Results.Ok();
    }
}
