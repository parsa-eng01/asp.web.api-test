using System.ComponentModel.DataAnnotations;

namespace TestProject.Web;

public record LoginViewModel([EmailAddress] string Email, string Password);
