
using SQLite;
using System;
namespace encuesta
{
    public class Class : BaseItem
    {
        public Class() { }

        public Class(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }

        public string Name { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}