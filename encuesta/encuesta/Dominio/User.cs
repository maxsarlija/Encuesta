
using System;
namespace encuesta
{
    public class User : BaseItem
    {
        public User() { }

        public User(string _username, string _password)
        {
            Username = _username;
            Password = _password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Username}";
        }
    }
}