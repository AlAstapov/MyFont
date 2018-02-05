using System.Collections.Generic;
using System.IO;
using MyFonts.BusinessObjects;

namespace MyFonts.WorkWithFile
{
    public class FileWriter
    {
        private static FileStream fileStream;
        private static string filePath = @"d:\users.txt";


        public static void WriteUser(User user)
        {
            fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(user.Name + " " + user.Email + " " + user.Password, true);
            }
        }

        public static List<User> ReadUsers()
        {
            string currentLine = "";
            List<User> userList = new List<User>();
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                while (currentLine != null)
                {
                    currentLine = streamReader.ReadLine();
                    if (currentLine != null)
                    {
                        string[] userinfo = currentLine.Split(' ');
                        userList.Add(new User(userinfo[0], userinfo[1], userinfo[2]));
                    }
                }
            }
            return userList;
        }

        public static void DeleteFile()
        {
            if (File.Exists(filePath)) File.Delete(filePath);

        }
    }
}
