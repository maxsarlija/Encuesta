
using System;
namespace encuesta
{
    public class User : BaseItem
    {
        public User() { }

        public User(int _id, string _username, string _password, int _classID)
        {
            ID = _id;
            Username = _username;
            Password = _password;
            ClassID = _classID;
        }

        public User(string _username, string _password, int _classID)
        {
            Username = _username;
            Password = _password;
            ClassID = _classID;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public int ClassID { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Username}";
        }
    }
}