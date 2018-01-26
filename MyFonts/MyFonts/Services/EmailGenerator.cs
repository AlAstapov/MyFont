using System;
using System.Text;


namespace MyFonts.Services
{
   public static class EmailGenerator
   {
        private static int incrementEmail = 5;
       private static string emailPrefix = "@mailinator.com";



       public static string GenerateEmail()
        {
            incrementEmail++;
            return "Al"+new Random().Next(1,1000000) + incrementEmail + emailPrefix ;
       }
   }
}
