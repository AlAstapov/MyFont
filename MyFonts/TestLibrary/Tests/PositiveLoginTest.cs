using MyFonts;
using MyFonts.BusinessObjects;
using NUnit.Framework;
using TestsLibrary.SetUp;
using MyFonts.WebDriver;
using System;

namespace TestLibrary.Tests
{
    [TestFixture("Chrome")]

	
    class PositiveLoginTest : BaseSetUp
    {
        public PositiveLoginTest(string browser) : base(browser)
        {
        }
        [Test]
        public void PositiveLoginWithHttpRequest()
        {
            User user = new User("FCUKPool2Admin2", "Password_01");
            string url = new HttpLogin().LogInWithUser("Uk","qed",DriverClass.GetDriver(), user);
			  			bool isLoggined = false;
            DriverClass.GetDriver().Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(10000);
      
            Assert.True(isLoggined, "User haven't been loggined");
        }    
    }
}
