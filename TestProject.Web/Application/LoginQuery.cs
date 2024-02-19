using MediatR;
using TestProject.Web.Core;

namespace TestProject.Web.Application;

public sealed record LoginQuery(string Email, string password): IRequest<Result<User>>;
