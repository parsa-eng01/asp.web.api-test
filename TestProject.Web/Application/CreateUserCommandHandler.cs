using MediatR;
using TestProject.Web.Core;

namespace TestProject.Web.Application;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User>>
{
  private readonly IUserRepository _userRepository;

  public CreateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }


  public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    try
    {
      await _userRepository.CreateAsync(request.Name, request.Email, request.password);
      var user = await _userRepository.LoginAsync(request.Email, request.password);
      return user!;
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
  }
}
