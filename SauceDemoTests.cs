using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.Tests
{
    public class LoginTests
    {
        private IWebDriver Driver;
        private LoginPage LoginPage;

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            LoginPage = new LoginPage(Driver);
        }

        [Test]
        public void UC1_EmptyCredentials_ShowsUsernameRequired()
        {
            LoginPage.EnterUsername("");
            LoginPage.EnterPassword("");
            LoginPage.ClickLogin();

            string errorMessage = LoginPage.GetErrorMessage();
            string usernameErrorMessage = "Epic sadface: Username is required";
            Assert.That(errorMessage.Contains(usernameErrorMessage), $"Expected '{usernameErrorMessage}' but found '{errorMessage}'.");
        }

        [Test]
        public void UC2_MissingPassword_ShowsPasswordRequired()
        {
            LoginPage.EnterUsername("standard_user");
            LoginPage.EnterPassword("");
            LoginPage.ClickLogin();

            string errorMessage = LoginPage.GetErrorMessage();
            string paswardErrorMessage = "Epic sadface: Password is required";
            Assert.That(errorMessage.Contains(paswardErrorMessage), $"Expected '{paswardErrorMessage}' but found '{errorMessage}'.");
        }

        [Test]
        public void UC3_ValidCredentials_NavigatesToDashboard()
        {
            LoginPage.EnterUsername("standard_user");
            LoginPage.EnterPassword("secret_sauce");
            LoginPage.ClickLogin();

            Assert.That(Driver.Title.Contains("Swag Labs"));
        }

        [TearDown]
        public void Teardown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}