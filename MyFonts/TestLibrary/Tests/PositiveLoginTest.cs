using System.Configuration;
using System.Threading;
using MyFonts;
using MyFonts.BusinessObjects;
using MyFonts.Elements;
using MyFonts.Pages;
using MyFonts.Pages.Email;
using MyFonts.Services;
using NUnit.Framework;
using TestsLibrary.SetUp;

namespace TestLibrary.Tests
{
    [TestFixture("Chrome")]

    class PositiveLoginTest : BaseSetUp
    {
        private static string myFontEmail = "hello@myfonts.com";
        public PositiveLoginTest(string browser) : base(browser)
        {
        }
        static object[] Users =
            {    new object[] { "Alex","astapov@mail.ru","zxcvasdfqwer123"},
                new object[] { "user1", EmailGenerator.GenerateEmail(),"zxcvasdfqwer123"},
                new object[] { "user2", EmailGenerator.GenerateEmail(), "zxcvasdfqwer123"},
                new object[] { "user3", EmailGenerator.GenerateEmail(), "zxcvasdfqwer123"}
            };

        [Test, TestCaseSource("Users")]
        public void PositiveLoginWithHttpRequest(string name, string email, string password)
        {
            DriverClass.GoToUrl("http://www.myfonts.com/");
            User user = new User(name, email, password);
            new HttpLogin().LogInWithUser(DriverClass.GetDriver(), user);
            DriverClass.GoToUrl("http://www.myfonts.com/");
            YourAccountPage yourAccountPage = new YourAccountPage();
            bool isLoggined = yourAccountPage.IsUserLoggined(user);
            if (isLoggined) yourAccountPage.SignOut();
            Assert.True(isLoggined, "User haven't been loggined");
        }

        [Test, Order(1), TestCaseSource("Users")]
        public void RegisterNewUser(string name, string email, string password)
        {
            DriverClass.GoToUrl("http://www.myfonts.com/");
            User user = new User(name, email, password);
            MainPage mainPage = new MainPage();
            YourAccountPage yourAccountPage =  mainPage.OpenDropMenu().SingUp().RegisterNewUser(user);
            bool isUserLoggined = yourAccountPage.IsUserLoggined(user);
            if (isUserLoggined) yourAccountPage.SignOut();
            Assert.True(isUserLoggined, "User haven't been registred");
        }

        [Test, TestCaseSource("Users")]
        public void TestEmailSentFromMyFont(string name, string email, string password)
        {
            User user = new User(name, email, password);
            DriverClass.GoToUrl(MailInator.url);
            MailInator mailInator = new MailInator();
            EmailsWindow emailsWindow = mailInator.OpenEmailsWindow(user.Email);
            Assert.True(emailsWindow.IsMessageFromEmailPresentOnPage(myFontEmail), "There are no registration confirm email");
        }
        /*  [Test]
          public void TestEmailSentFromMyFont()
          {
              string emailll = "astapov95@mailinator.com";
              DriverClass.GoToUrl(MailInator.url);
              MailInator mailInator = new MailInator();
              EmailsWindow emailsWindow = mailInator.OpenEmailsWindow(emailll);
              Assert.True(emailsWindow.IsMessageFromEmailPresentOnPage("al_astapov@mail.ru"), "There are no registration email");
          }*/
        [Test]
        public void LoGinHttpLego()
        {

        }

    }
}
