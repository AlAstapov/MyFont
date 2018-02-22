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
            User user = new User("PTFTBTestUser08", "Password_01");
            string url = new HttpLogin().LogInWithUser("","demo",DriverClass.GetDriver(), user);
			  			bool isLoggined = false;
      
            Assert.True(isLoggined, "User haven't been loggined");
        }    
    }
}
