using MediatR;
using TestProject.Web.Core;

namespace TestProject.Web.Application;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, Result<User>>
{
  private readonly IUserRepository _userRepository;

  public LoginQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }
  
  public async Task<Result<User>> Handle(LoginQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var user = await _userRepository.LoginAsync(request.Email, request.password);
      return user is null
        ? UserServiceErrors.UserNotFound
        : user;
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
  }
}
