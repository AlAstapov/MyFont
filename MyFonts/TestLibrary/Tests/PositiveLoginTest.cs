using System.Configuration;
using MyFonts;
using MyFonts.BusinessObjects;
using MyFonts.Pages;
using MyFonts.Pages.Email;
using MyFonts.Services;
using NUnit.Framework;
using TestsLibrary.SetUp;

namespace TestLibrary.Tests
{
        [TestFixture("Chrome")]
        
    class PositiveSearchTests : BaseSetUp
        {
           private static EmailProvider emailProvider = new EmailProvider();
           private static string gmailForGenerate = "aliaksandrastapau";
            private static string myFontEmail = "hello@myfonts.com";



        public PositiveSearchTests(string browser) :base(browser)
        {
        }
         
            static object[] Users =
            {   new object[] { "user1","astapov@mail.ru","zxcvasdfqwer123"},
                new object[] { "user1", EmailGenerator.GenerateEmail(),"zxcvasdfqwer123"},
                new object[] { "user2", EmailGenerator.GenerateEmail(), "zxcvasdfqwer123"},
                new object[] { "user3", EmailGenerator.GenerateEmail(), "zxcvasdfqwer123"}
            };

    [Test, TestCaseSource("Users")]
        public void PositiveLoginWithHttpRequest(string name, string email, string password)
        {
            User user = new User(name, email, password);
            string responce = new HttpLogin().LogInWithUser(Driver, user);
            Assert.True(responce.Contains("success"),"User haven't been loginned");
        }

        [Test,Order(1),TestCaseSource("Users")]
        public void RegisterNewUser(string name,string email, string password)
        {
            User user = new User(name, email, password);
            bool isRegistred;

            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["loginPageUrl"]);

            UserRegistartionPage userRegistartionPage = new UserRegistartionPage(Driver);
            YourAccountPage yourAccountPage =  userRegistartionPage.RegisterNewUser(user,out isRegistred);

            if(isRegistred) yourAccountPage.SignOut();
            Assert.True(isRegistred,"User haven't been registred");
        }
           /*[Test,TestCaseSource("Users")]
            public void TestEmailSentFromMyFont(string name, string email, string password)
            {
                User user = new User(name, email, password);
                MailInator mailInator = new MailInator(Driver);
                EmailsWindow emailsWindow = mailInator.OpenEmailsWindow(user.Email);
                Assert.True(emailsWindow.IsMessageFromEmailPresentOnPage(myFontEmail));
            }*/
            [Test]
            public void TestEmailSentFromMyFont()
            {
                string emailll = "astapov95@mailinator.com";
                MailInator mailInator = new MailInator(Driver);
                EmailsWindow emailsWindow = mailInator.OpenEmailsWindow(emailll);
                Assert.True(emailsWindow.IsMessageFromEmailPresentOnPage("al_astapov@mail.ru"));
            }
    }
}
