using OpenQA.Selenium;

namespace MyFonts.Elements
{
    class TextBox : BaseElement
    {
        public TextBox(By locator, string title) : base(locator, title)
        {

        }
        public void SendKeys(string text)
        {
            webElement.SendKeys(text);
        }
    }
}
