
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject.UnitTest.UI;

public class UITestBase : IDisposable
{
  protected readonly IWebDriver _driver;

  public UITestBase()
    => _driver = new ChromeDriver();

  public void Dispose()
  {
    _driver.Quit();
    _driver.Dispose();
  }
}