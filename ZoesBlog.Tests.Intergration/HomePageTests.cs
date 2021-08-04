using NUnit.Framework;
using OpenQA.Selenium;

namespace ZoesBlog.Tests.Intergration
{
	[TestFixture]
	public class HomePageTests : TestContext
	{
		[Test, Category("HomePage")]
		public void Admin_Can_Login()
		{
			
			Driver.Navigate().GoToUrl("https://localhost:44335");
			Driver.FindElement(By.Id("login nav link")).Click();

			var emailInput = Driver.FindElement(By.Id("email"));
			emailInput.SendKeys("zoe.king@razor.co.uk");

			var passwordInput = Driver.FindElement(By.Id("password"));
			passwordInput.SendKeys("Password123!");

			var loginButton = Driver.FindElement(By.Id("login button"));
			loginButton.Click();

			var adminTable = Driver.FindElement(By.Id("admin table"));

			Assert.IsTrue(adminTable.Displayed);

		}
	}
}
