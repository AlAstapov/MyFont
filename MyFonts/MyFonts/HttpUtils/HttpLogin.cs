using System.Collections.Generic;
using MyFonts.BusinessObjects;
using MyFonts.HttpUtils;
using OpenQA.Selenium;


namespace MyFonts
{
    public  class HttpLogin : BaseHttp
    {
        
        public  string LogInWithUser(IWebDriver driver, User user)
        {
        PostUrl = "https://www.myfonts.com/widgets/dropdown_login/login.php";
        GetUrl = "http://www.myfonts.com/widgets/dropdown_login/dropdown_login.php?success=1";
        RefererUrl = "http://www.myfonts.com/widgets/dropdown_login/dropdown_login.php?https=0";
        CookieParamsToLogin = CreateCookieString(driver);
        ParametrsToRequest = (string.Format("https=0&username={0}&password={1}", user.Email, user.Password));
      string responceFromPost =   GetResponce(ExecutePostRequest(ParametrsToRequest, new Dictionary<string, string>
                {
                    {"Cookie",CookieParamsToLogin }
                },
                new Dictionary<string, string>
                {
                    {"url",PostUrl },
                    {"ContentType","application/x-www-form-urlencoded" },
                    {"Referer",RefererUrl},
                    {"AllowAutoRedirect","true" }
                }));
         
         return responceFromPost;
        }
    }
}
