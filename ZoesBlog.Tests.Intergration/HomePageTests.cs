using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ZoesBlog.Tests.Intergration
{
	[TestFixture]
	public class HomePageTests : TestContext
	{
		[Test, Category("HomePage")]
		public void Customer_Can_Go_To_Home_Page()
		{
			Driver.Navigate().GoToUrl("https://tt-test-frontend.azurewebsites.net/");
			Driver.FindElement(By.PartialLinkText(""));
			var signInButton = Driver.FindElement(By.LinkText("SIGN IN"));
			signInButton.Click();
			Assert.IsTrue(Driver.Url == "/signin");

			var userNameField = Driver.FindElement(By.Id("Email"));
			userNameField.SendKeys("test+2@razor.co.uk");

		}
	}
}
