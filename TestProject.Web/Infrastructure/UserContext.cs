using Microsoft.EntityFrameworkCore;
using TestProject.Web.Core;

namespace TestProject.Web.Infrastructure;

public class UserContext : DbContext
{
  public UserContext(DbContextOptions<UserContext> options) : base(options)
  {

  }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    var user = modelBuilder.Entity<User>();
    user.HasKey(m => m.Id);
    user.Property(m => m.Name)
      .HasMaxLength(200);
  }
}
