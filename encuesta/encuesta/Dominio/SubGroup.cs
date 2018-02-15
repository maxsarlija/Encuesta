
using SQLite;
using System;
namespace encuesta
{
    public class SubGroup : BaseItem
    {
        public SubGroup() { }

        public SubGroup(int _id, string _name, int _score, int _groupID)
        {
            ID = _id;
            Name = _name;
            Score = _score;
            GroupID = _groupID;
        }

        public SubGroup(string _name, int _score, int _groupID)
        {
            Name = _name;
            Score = _score;
            GroupID = _groupID;
        }

        public string Name { get; set; }
        public int Score { get; set; }
        public int GroupID { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}