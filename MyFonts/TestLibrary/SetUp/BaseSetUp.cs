
using FrameworkCore.WebDriver;


using NUnit.Framework;

using MyFonts.WebDriver;

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
           
            DriverClass.Dispose();
        }
    }
}
