namespace TestProject.Web.Application;

public static class UserServiceErrors
{
  public static string UserNotFound => "Email or password is incorrect.";
  public static string InvalidEmail => "It seems that the email is not valid. :))";
  public static string ShortName => "The valid name must be more than 4 characters.";
  public static string ShortPassword => "Please use longer password.";
}
