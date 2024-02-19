using Microsoft.EntityFrameworkCore;
using TestProject.Web.Core;

namespace TestProject.Web.Infrastructure;

public class UserRepository : IUserRepository
{
  private readonly UserContext _context;

  public UserRepository(UserContext context)
  {
    _context = context;
  }
  public async Task CreateAsync(string name, string email, string password)
  {
    var user = new User(name, email, password); // no hashing because it's not purpose of test 
    _context.Add(user);
    await _context.SaveChangesAsync();
  }

  public async Task<User?> LoginAsync(string email, string password)
  {
    var user = await _context.Users
      .SingleOrDefaultAsync(u => u.Email == email && u.Password == password);

    return user;
  }
}
