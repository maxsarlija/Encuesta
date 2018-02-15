
using SQLite;
using System;
using System.Collections.Generic;

namespace encuesta
{
    public class Group : BaseItem
    {
        public Group() { }

        public Group(int _id, string _name, int _score)
        {
            ID = _id;
            Name = _name;
            Score = _score;
        }

        public Group(string _name, int _score)
        {
            Name = _name;
            Score = _score;
        }

        public string Name { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}