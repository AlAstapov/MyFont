using System;
using FrameworkCore.WebDriver;
using MyFonts;
using MyFonts.WorkWithFile;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace TestsLibrary.SetUp
{
    [Parallelizable(ParallelScope.Fixtures)] 
    public class BaseSetUp
    {
        private string _browser;
        protected IWebDriver Driver;
        

        public BaseSetUp(string browser)
        {
            _browser = browser;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
          Driver = WebDriverProvider.GetDriver(_browser);
          AbstractPage.Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
          Driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            FileWriter.DeleteFile();
            Driver.Dispose();   
        }

    }
}
