using MediatR;
using TestProject.Web.Core;

namespace TestProject.Web.Application;

public class UserValidation<TRequest, TResponse>
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<Result<User>>
  where TResponse : Result<User>
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if (request is LoginQuery login)
    {
      if (login.Email.Length < 4 || !login.Email.Contains('@'))
        return (TResponse)UserServiceErrors.InvalidEmail;
      if (login.password.Length < 6)
        return (TResponse)UserServiceErrors.ShortPassword;
    }

    if (request is CreateUserCommand create)
    {
      if (create.Email.Length < 4 || !create.Email.Contains('@'))
        return (TResponse)UserServiceErrors.InvalidEmail;
      if (create.password.Length < 6)
        return (TResponse)UserServiceErrors.ShortPassword;
      if (create.Name.Length < 4)
        return (TResponse)UserServiceErrors.ShortName;
    }

    return await next();
  }

}
