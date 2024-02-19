using Microsoft.Extensions.DependencyInjection;

namespace TestProject.UnitTest;

public class TestBase : IClassFixture<WebAppFixture>
{
  private readonly WebAppFixture _factory;
  protected readonly HttpClient _client;
  protected IServiceProvider ServiceProvider => _factory.Services.CreateScope().ServiceProvider;

  public TestBase(WebAppFixture appFixture)
  {
    _factory = appFixture;
    _client = _factory.CreateClient();
  }
  
}
