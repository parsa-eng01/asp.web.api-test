using System.ComponentModel.DataAnnotations;

namespace TestProject.Web;

public record RegisterUserViewModel(string Name,[EmailAddress]string Email, string Password);
