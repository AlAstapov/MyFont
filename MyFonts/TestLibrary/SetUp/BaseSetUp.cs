using System;
using FrameworkCore.WebDriver;
using MyFonts.Elements;
using MyFonts.WorkWithFile;
using NUnit.Framework;

using OpenQA.Selenium.Support.UI;


namespace TestsLibrary.SetUp
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class BaseSetUp
    {
        private string _browser;


        public BaseSetUp(string browser)
        {
            _browser = browser;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DriverClass.SetDriver(WebDriverProvider.GetDriver(_browser));
            DriverClass.SetWait(5);
            DriverClass.MaximizeWindow();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            FileWriter.DeleteFile();
            DriverClass.Dispose();
        }
    }
}
