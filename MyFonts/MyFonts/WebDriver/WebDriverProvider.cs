using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;

namespace FrameworkCore.WebDriver
{
    public class WebDriverProvider
    {
        private WebDriverProvider()
        {
        }

        public static IWebDriver GetDriver(string browserName)
        {
            IWebDriver driver = null;
            BrowsersEnum browserType = (BrowsersEnum) Enum.Parse(typeof(BrowsersEnum), browserName);
            switch (browserType)
            {
                case BrowsersEnum.Chrome:
                {
                    driver = GetChromeDriver();
                      break;
                }
                case BrowsersEnum.FireFox:
                {
                    driver = GetFireFoxDriver();
                    break;
                }
                case BrowsersEnum.Edge:
                {
                    driver = GetEdgeDriver();
                        break;
                }
                case BrowsersEnum.Opera:
                {
                    driver = GetOperaDriver();
                        break;
                }
                case BrowsersEnum.RemoteFireFox:
                {
                    driver = GetRemoteFireFoxDriver();
                    break;   
                }
               default:
                {
                        //Write to log
                    driver = null;
                    Environment.Exit(0);
                    break;
                }
            }
           return driver;
        }

        private static IWebDriver GetChromeDriver()
        {
            
            IWebDriver driver = new ChromeDriver();
            return driver;
        }


        private static IWebDriver GetFireFoxDriver()
        {
            IWebDriver driver = new FirefoxDriver();
            return driver;
        }

        private static IWebDriver GetEdgeDriver()
        {
           IWebDriver driver = new EdgeDriver();
           return driver;
        }

        private static IWebDriver GetOperaDriver()
        {
            IWebDriver iWebDriver = new OperaDriver();
            return iWebDriver;
        }
        private static IWebDriver GetRemoteFireFoxDriver()
        {
            var options = new FirefoxOptions();
            var capabilities = options.ToCapabilities();
            return new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), capabilities);
            
        }
    }
}
