using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ZoesBlog.Tests.Intergration
{
	public class TestContext
	{
		public static IWebDriver Driver { get; set; }
		[OneTimeSetUp]
		public void Setup()
		{
			Driver = new ChromeDriver(@"C:\chromedriver_win32");
			Driver.Manage().Window.Maximize();
		}

		
	}
}