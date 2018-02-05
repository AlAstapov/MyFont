using MyFonts.BusinessObjects;
using MyFonts.Elements;
using MyFonts.WorkWithFile;
using OpenQA.Selenium;


namespace MyFonts.Pages
{
    public class UserRegistartionPage : AbstractPage
    {
        private TextBox YourNameBox = new TextBox(By.XPath("//input[@name='newinfo[name]']"), "YourNameBox");
        private TextBox EmailAdressBox = new TextBox(By.XPath("//input[@name='newinfo[emailAddress]']"), "EmailAdressBox");
        private TextBox PasswordBox = new TextBox(By.XPath("//input[@name='newinfo[password]']"), "PasswordBox");
        private TextBox PasswordBoxAgain = new TextBox(By.XPath("//input[@name='newinfo[confirmPassword]']"), "PasswordBoxAgain");
        private Button CreateAccountButton = new Button(By.XPath("//input[@value='Create Account']"), "CreateAccountButton");


        public UserRegistartionPage()
        {

        }

        public YourAccountPage RegisterNewUser(User user)
        {
            bool isRegistred = false;
            YourNameBox.SendKeys(user.Name);
            EmailAdressBox.SendKeys(user.Email);
            PasswordBox.SendKeys(user.Password);
            PasswordBoxAgain.SendKeys(user.Password);
            CreateAccountButton.Click();

            YourAccountPage yourAccountPage = null;
            try
            {
                yourAccountPage = new YourAccountPage();
                isRegistred = yourAccountPage.IsUserLoggined(user);
            }
            catch (WebDriverTimeoutException e)
            {
                isRegistred = false;
            }
            if (isRegistred)
            {
                FileWriter.WriteUser(user);
            }
            return yourAccountPage;
        }


    }
}
