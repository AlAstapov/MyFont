using OpenQA.Selenium;

namespace MyFonts.BusinessObjects
{
   public class Letter : AbstractPage
    {
        public By DeleteEmaeilButtonLocator = By.XPath("//i[@class='fa fa-trash fa-stack-1x fa-inverse']");
        public  By contentTypeSelectLocator = By.XPath("//select[@id='contenttypeselect']");
        public By jsonContentTypeLocator = By.XPath("//option[@value='json']");
        public  By subjectLocator = By.XPath("//div[@class='ng-binding']");
        /*public By LetterTextLocator = By.XPath("//iframe[@id='msg_body']//pre");*/
        public By LetterTextLocator = By.XPath("//pre");

        private  IWebElement contentTypeSelectElement => FindElement(contentTypeSelectLocator);
        private IWebElement jsonContentTypeElement => FindElement(jsonContentTypeLocator);
        private IWebElement letterTextElement => FindElement(LetterTextLocator);

    

        public Letter(IWebDriver driver)
        {
            Driver = driver;
        }

        public void ChoseJsonMessageFromat()
        {
           contentTypeSelectElement.Click();
           jsonContentTypeElement.Click();
        }

        public void DeleteLetter()
        {
            Driver.FindElement(DeleteEmaeilButtonLocator).Click();
        }

        public string GetLetterText()
        {
            return letterTextElement.Text;
        }

    }
}
