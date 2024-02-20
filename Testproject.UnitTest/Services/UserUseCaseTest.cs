using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Web.Application;
using TestProject.Web.Core;

namespace TestProject.UnitTest.Services;

public class UserUseCaseTest : TestBase
{
  private readonly ISender _sender;
  public UserUseCaseTest(WebAppFixture appFixture) : base(appFixture)
  {
    _sender = ServiceProvider.GetService<ISender>()!;
  }

  [Fact]
  public async Task Register_User_Command_Should_Success()
  {
    var result = await _sender.Send(new CreateUserCommand("Parsa", "register-command@test.local", "123456"));

    result.IsSuccess.Should().BeTrue(result.Error);
    result.Value!.Name.Should().Be("Parsa");
  }

  [Fact]
  public async Task Login_Query_Should_Success()
  {
    var email = "login-command@test.local";
    var password = "123456";
    var result = await _sender.Send(new CreateUserCommand("Parsa", email, password));
    result.IsSuccess.Should().BeTrue(result.Error);

    result = await _sender.Send(new LoginQuery(email, password));

    result.IsSuccess.Should().BeTrue(result.Error);
    result.Value!.Name.Should().Be("Parsa");
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaaa")]
  [InlineData("aaa.a.a")]
  public async Task Register_User_Command_Should_Failed_By_Invalid_Email(string email)
  {
    var result = await _sender.Send(new CreateUserCommand("Parsa", email, "123456"));

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().Be(UserServiceErrors.InvalidEmail);
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaa")]
  public async Task Register_User_Command_Should_Failed_By_Short_Name(string name)
  {
    var result = await _sender.Send(new CreateUserCommand(name, "email@test.local", "123456"));

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().Be(UserServiceErrors.ShortName);
  }

  [Theory]
  [InlineData("")]
  [InlineData("xxx")]
  public async Task Register_User_Command_Should_Failed_By_Short_Password(string password)
  {
    var result = await _sender.Send(new CreateUserCommand("parsa", "email@test.local", password));

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().Be(UserServiceErrors.ShortPassword);
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaaa")]
  [InlineData("aaa.a.a")]
  public async Task Login_Query_Should_Failed_By_Invalid_Email(string email)
  {
    var result = await _sender.Send(new LoginQuery(email, "123456"));

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().Be(UserServiceErrors.InvalidEmail);
  }

  [Theory]
  [InlineData("")]
  [InlineData("xxx")]
  public async Task Login_Query_Should_Failed_By_Short_Password(string password)
  {
    var result = await _sender.Send(new LoginQuery("email@test.local", password));

    result.IsSuccess.Should().BeFalse();
    result.Error.Should().Be(UserServiceErrors.ShortPassword);
  }


}
