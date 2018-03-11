
using SQLite;
using System;
namespace encuesta
{
    public class Tasks : BaseItem
    {
        public Tasks() { }

        public Tasks(int _id, string _name, string _details, DateTime _date, string _status, int _userID)
        {
            ID = _id;
            Name = _name;
            Details = _details;
            Date = _date;
            Status = _status;
            UserID = _userID;
            Notes = "";
            Time = _date;
        }

        public Tasks(string _name, string _details, DateTime _date, string _status, int _userID)
        {
            Name = _name;
            Details = _details;
            Date = _date;
            Status = _status;
            UserID = _userID;
            Notes = "";
            Time = _date;
        }

        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public string DateCompleted { get; set; }
        public string Status { get; set; }
        public int UserID { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}