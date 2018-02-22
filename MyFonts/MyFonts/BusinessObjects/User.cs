namespace MyFonts.BusinessObjects
{
    public class User
    {
        public string Name { get; set; }
        
        public string Password { get; set; }

        public User(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
    }
}
