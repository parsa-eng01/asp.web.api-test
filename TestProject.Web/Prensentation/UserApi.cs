using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Web.Application;

namespace TestProject.Web;

public static class UserApi
{
  public static void AddUserEndpoints(this IEndpointRouteBuilder routeBuilder)
  {
    var api = routeBuilder.MapGroup("api/v1/users/");
    api.MapPost("register", RegisterUserAsync);
    api.MapPost("login", LoginAsync);
  }

  private static async Task<IResult> RegisterUserAsync(ISender sender, RegisterUserViewModel model, CancellationToken cancellation)
  {
    var result = await sender.Send(new CreateUserCommand(model.Name, model.Email, model.Password), cancellation);
    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
  }

  private static async Task<IResult> LoginAsync(ISender sender, LoginViewModel model, CancellationToken cancellation)
  {
    var result = await sender.Send(new LoginQuery(model.Email, model.Password), cancellation);
    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
  }
}
