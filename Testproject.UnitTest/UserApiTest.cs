using System.Net.Http.Json;
using FluentAssertions;
using TestProject.Web;
using TestProject.Web.Application;

namespace TestProject.UnitTest;

public class UserApiTest : TestBase
{
  public UserApiTest(WebAppFixture appFixture) : base(appFixture)
  {
  }

  [Fact]
  public async Task Register_User_Should_Success()
  {
    var model = new RegisterUserViewModel("Parsa", "register-api@test.local", "123456");
    var response = await _client.PostAsJsonAsync("api/v1/users/register", model);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
  }

  [Fact]
  public async Task Login_Should_Success()
  {
    var email = "login-api@test.local";
    var password = "123456";
    var model = new RegisterUserViewModel("Parsa", email, password);
    var response = await _client.PostAsJsonAsync("api/v1/users/register", model);
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

    var login = new LoginViewModel(email, password);
    response = await _client.PostAsJsonAsync("api/v1/users/login", login);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaaa")]
  [InlineData("aaa.a.a")]
  public async Task Register_User_Should_Failed_By_Invalid_Email(string email)
  {
    var model = new RegisterUserViewModel("Parsa", email, "123456");
    var response = await _client.PostAsJsonAsync("api/v1/users/register", model);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    var body = await response.Content.ReadFromJsonAsync<string>();
    body.Should().Be(UserServiceErrors.InvalidEmail);
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaa")]
  public async Task Register_User_Should_Failed_By_Short_Name(string name)
  {
    var model = new RegisterUserViewModel(name, "email@test.local", "123456");
    var response = await _client.PostAsJsonAsync("api/v1/users/register", model);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    var body = await response.Content.ReadFromJsonAsync<string>();
    body.Should().Be(UserServiceErrors.ShortName);
  }

  [Theory]
  [InlineData("")]
  [InlineData("xxx")]
  public async Task Register_User_Should_Failed_By_Short_Password(string password)
  {
    var model = new RegisterUserViewModel("Parsa", "email@test.local", password);
    var response = await _client.PostAsJsonAsync("api/v1/users/register", model);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    var body = await response.Content.ReadFromJsonAsync<string>();
    body.Should().Be(UserServiceErrors.ShortPassword);
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaaa")]
  [InlineData("aaa.a.a")]
  public async Task Login_Should_Failed_By_Invalid_Email(string email)
  {
    var login = new LoginViewModel(email, "123456");
    var response = await _client.PostAsJsonAsync("api/v1/users/login", login);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    var body = await response.Content.ReadFromJsonAsync<string>();
    body.Should().Be(UserServiceErrors.InvalidEmail);
  }

  [Theory]
  [InlineData("")]
  [InlineData("xxx")]
  public async Task Login_Should_Failed_By_Short_Password(string password)
  {
    var login = new LoginViewModel("email@test.local", password);
    var response = await _client.PostAsJsonAsync("api/v1/users/login", login);

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    var body = await response.Content.ReadFromJsonAsync<string>();
    body.Should().Be(UserServiceErrors.ShortPassword);
  }

}
