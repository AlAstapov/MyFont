using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using MyFonts.BusinessObjects;
using MyFonts.HttpUtils;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;


namespace MyFonts
{
    public class HttpLogin : BaseHttp
    {
        public void LogInWithUser(IWebDriver driver, User user)
        {
            PostUrl = "https://www.myfonts.com/widgets/dropdown_login/login.php";
            GetUrl = "http://www.myfonts.com/widgets/dropdown_login/dropdown_login.php?success=1";
            RefererUrl = "http://www.myfonts.com/widgets/dropdown_login/dropdown_login.php?https=";
            Token = "ltoken";
            CookieParamsToLogin = CreateCookieString(driver);
            ParametrsToRequest = (string.Format("https=0&username={0}&password={1}", user.Email, user.Password));
            HttpWebRequest postRequest = ExecutePostRequest(ParametrsToRequest, new Dictionary<string, string>
                 {
                     {"Cookie", CookieParamsToLogin}
                 },
                new Dictionary<string, string>
                {
                     {"url", PostUrl},
                     {"ContentType", "application/x-www-form-urlencoded"},
                     {"Referer", RefererUrl},
                     {"AllowAutoRedirect", "false"}
                });
            var postResponce = (HttpWebResponse)postRequest.GetResponse();
            string tokenValue = GetTokenFromResponce(Token, postResponce.Headers["Set-Cookie"]);
            CookieParamsToLogin += string.Format("loggedIn=true; {0}={1}", Token, tokenValue);
            driver.Manage().Cookies.AddCookie(new Cookie(Token, tokenValue));
        }

        public string GetTokenFromResponce(string tokenName, string Cookie)
        {
            string token = "";
            string pattern = tokenName + "=([a-z0-9]*)";
            Regex regex = new Regex(pattern);
            MatchCollection matchCollection = regex.Matches(Cookie);
            if (matchCollection.Count > 0) token = matchCollection[0].Groups[1].Value;
            return token;
        }
    }
}

