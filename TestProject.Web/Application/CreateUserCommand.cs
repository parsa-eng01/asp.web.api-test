using MediatR;
using TestProject.Web.Core;

namespace TestProject.Web.Application;

public sealed record CreateUserCommand(string Name, string Email, string password) : IRequest<Result<User>>;
