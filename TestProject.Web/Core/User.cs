namespace TestProject.Web.Core;

public record User(string Name, string Email, string Password)
{
  public int Id { get; private set; }
}
