namespace TestProject.Web.Core;

public interface IUserRepository
{
  Task CreateAsync(string name, string email, string password);
  Task<User?> LoginAsync(string email, string password);
}
