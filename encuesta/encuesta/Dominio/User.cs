
using System;
namespace encuesta
{
    public class User : BaseItem
    {
        public User() { }

        public User(int _id, string _username, string _password, int _classID, string _name, int _zoneID)
        {
            ID = _id;
            Username = _username;
            Password = _password;
            ClassID = _classID;
            ZoneID = _zoneID;
            Name = _name;
        }

        public User(string _username, string _password, int _classID, string _name, int _zoneID)
        {
            Username = _username;
            Password = _password;
            ClassID = _classID;
            ZoneID = _zoneID;
            Name = _name;
        }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ClassID { get; set; }
        public int ZoneID { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Username}";
        }
    }
}