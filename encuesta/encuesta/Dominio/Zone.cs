
using SQLite;
using System;
namespace encuesta
{
    public class Zone : BaseItem
    {
        public Zone() { }

        public Zone(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }

        public Zone(string _name)
        {
            Name = _name;
        }

        public string Name { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}