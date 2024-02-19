
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestProject.Web.Infrastructure;

namespace TestProject.UnitTest;

public class WebAppFixture : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      services.RemoveAll(typeof(DbContextOptions<UserContext>));
      services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("usersTestDb"));

    });
    builder.UseEnvironment("Development");
    base.ConfigureWebHost(builder);
  }
}
