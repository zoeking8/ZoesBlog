using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium;

namespace ZoesBlog.Tests.Intergration
{
	public class TestContext
	{
		public static IWebDriver Driver { get; set; }

		[OneTimeSetUp]
		public void Setup()
		{

			var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			Driver = new ChromeDriver(outputDirectory);
			Driver.Manage().Window.Maximize();
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			Driver = new ChromeDriver();
			Driver.Quit(); 
		}
	}
}