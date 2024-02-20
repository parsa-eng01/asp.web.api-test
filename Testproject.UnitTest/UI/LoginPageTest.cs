using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Web.Application;

namespace TestProject.UnitTest.UI;

public class LoginPageTest : UITestBase
{
  [Fact]
  public void Load_Page_Success()
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("login")));
    var button = _driver.FindElement(By.Id("login"));

    button.TagName.Should().Be("button");
    button.Text.Should().Be("Login");
  }

  [Fact]
  public async Task Login_Should_Success(){
    _driver.Navigate().GoToUrl("http://localhost:5235/");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("login")));

    _driver.FindElement(By.Id("email"))
      .SendKeys("register-ui@test.local");
    _driver.FindElement(By.Id("password"))
      .SendKeys("123456");
    _driver.FindElement(By.Id("login"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("success")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be("You entred!");
  }

  [Theory]
  [InlineData("")]
  [InlineData("aaaa")]
  [InlineData("aaa.a.a")]
  public async Task Login_Should_Failed_By_Invalid_Email(string email)
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("login")));

    _driver.FindElement(By.Id("email"))
      .SendKeys(email);
    _driver.FindElement(By.Id("password"))
      .SendKeys("123456");
    _driver.FindElement(By.Id("login"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("error")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be(UserServiceErrors.InvalidEmail);
  }

  [Theory]
  [InlineData("")]
  [InlineData("xxx")]
  public async Task Login_Should_Failed_By_Invalid_Password(string password)
  {
    _driver.Navigate().GoToUrl("http://localhost:5235/");
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
    wait.Until(w => w.FindElement(By.Id("login")));

    _driver.FindElement(By.Id("email"))
      .SendKeys("register-ui@test.local");
    _driver.FindElement(By.Id("password"))
      .SendKeys(password);
    _driver.FindElement(By.Id("login"))
      .Click();

    await Task.Delay(TimeSpan.FromSeconds(1));
    var message = _driver.FindElement(By.Id("error")).Text;
    message.Should().NotBeEmpty();
    message.Should().Be(UserServiceErrors.ShortPassword);
  }

}
