using MyFonts;
using MyFonts.BusinessObjects;
using NUnit.Framework;
using TestsLibrary.SetUp;
using MyFonts.WebDriver;

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
            User user = new User("FTBTestUser31", "Password01");
            new HttpLogin().LogInWithUser(DriverClass.GetDriver(), user);
			  			bool isLoggined = false;
            Assert.True(isLoggined, "User haven't been loggined");
        }    
    }
}
