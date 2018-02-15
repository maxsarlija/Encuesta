
using SQLite;
using System;
namespace encuesta
{
    public class Objective : BaseItem
    {
        public Objective() { }

        public Objective(int _id, string _name, int _groupID, string _notes)
        {
            ID = _id;
            Name = _name;
            GroupID = _groupID;
            Notes = _notes;
        }

        public Objective(string _name, int _groupID, string _notes)
        {
            Name = _name;
            GroupID = _groupID;
            Notes = _notes;
        }

        public string Name { get; set; }
        public int GroupID { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}