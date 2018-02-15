
using SQLite;
using System;
namespace encuesta
{
    public class Task : BaseItem
    {
        public Task() { }

        public Task(int _id, string _name, string _details, DateTime _date, DateTime _dateCompleted, string _status, int _userID)
        {
            ID = _id;
            Name = _name;
            Details = _details;
            Date = _date;
            DateCompleted = _dateCompleted;
            Status = _status;
            UserID = _userID;
        }

        public Task(string _name, string _details, DateTime _date, DateTime _dateCompleted, string _status, int _userID)
        {
            Name = _name;
            Details = _details;
            Date = _date;
            DateCompleted = _dateCompleted;
            Status = _status;
            UserID = _userID;
        }

        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCompleted { get; set; }
        public string Status { get; set; }
        public int UserID { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}