using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomatedTests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        // Test data from the manual case
        private const string Username = "democsr2";
        private const string Password = "<PASSWORD>"; // TODO: replace with the actual valid password

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        // TC-001: Login with valid username and password
        [Test]
        public void TC_001_LoginWithValidUsernameAndPassword()
        {
            // Preconditions: navigate to the login page
            driver.Navigate().GoToUrl("https://leaftaps.com/opentaps/control/login");

            // Step 1: Enter "democsr2" in the "Username" field
            var usernameField = wait.Until(d => d.FindElement(By.Id("username")));
            usernameField.Clear();
            usernameField.SendKeys(Username);
            // Expected: Username is displayed in the field
            Assert.That(usernameField.GetAttribute("value"), Is.EqualTo(Username), "Username field should contain the entered value.");

            // Step 2: Enter the valid password in the "Password" field
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.Clear();
            passwordField.SendKeys(Password);
            // Expected: Password characters are masked
            // TODO: Verify masking of password characters (cannot be validated via Selenium without additional info)

            // Step 3: Click the "Login" button
            var loginButton = driver.FindElement(By.CssSelector("#login > p:nth-of-type(3) > input"));
            loginButton.Click();
            // Expected: Login request is submitted
            // (Implicitly verified by subsequent navigation)

            // Step 4: Verify that the user is redirected to the My Home page
            wait.Until(d => d.Title == "My Home | opentaps CRM");
            Assert.That(driver.Title, Is.EqualTo("My Home | opentaps CRM"), "Page title should indicate successful login to My Home.");

            // ExpectedResult: User is logged in and lands on the My Home dashboard
            // (Already validated by title check)
        }
    }
}
