using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Web.Application;

namespace TestProject.UnitTest.UI;

public class RegisterPageTest : UITestBase
{

  [Fact]
  public async Task Register_User_Should_Success()
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/register");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("register")));

    _driver.FindElement(By.Id("name"))
      .SendKeys("Parsa");
    _driver.FindElement(By.Id("email"))
      .SendKeys("register-ui@test.local");
    _driver.FindElement(By.Id("password"))
      .SendKeys("123456");
    _driver.FindElement(By.Id("register"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("success")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be("User has registered");
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaaa")]
  [InlineData("aaa.a.a")]
  public async Task Register_User_Command_Should_Failed_By_Invalid_Email(string email)
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/register");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("register")));

    _driver.FindElement(By.Id("name"))
      .SendKeys("Parsa");
    _driver.FindElement(By.Id("email"))
      .SendKeys(email);
    _driver.FindElement(By.Id("password"))
      .SendKeys("123456");
    _driver.FindElement(By.Id("register"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("error")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be(UserServiceErrors.InvalidEmail);
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaa")]
  public async Task Register_User_Command_Should_Failed_By_Short_Name(string name)
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/register");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("register")));

    _driver.FindElement(By.Id("name"))
      .SendKeys(name);
    _driver.FindElement(By.Id("email"))
      .SendKeys("register-ui@test.local");
    _driver.FindElement(By.Id("password"))
      .SendKeys("123456");
    _driver.FindElement(By.Id("register"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("error")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be(UserServiceErrors.ShortName);
  }

  [Theory]
  [InlineData("")]
  [InlineData("xxx")]
  public async Task Register_User_Command_Should_Failed_By_Short_Password(string password)
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/register");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("register")));

    _driver.FindElement(By.Id("name"))
      .SendKeys("Parsa");
    _driver.FindElement(By.Id("email"))
      .SendKeys("register-ui@test.local");
    _driver.FindElement(By.Id("password"))
      .SendKeys(password);
    _driver.FindElement(By.Id("register"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("error")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be(UserServiceErrors.ShortPassword);
  }

}
