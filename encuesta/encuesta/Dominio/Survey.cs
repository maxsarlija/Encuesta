
using SQLite;
using System;
namespace encuesta
{
    public class Survey : BaseItem
    {
        public Survey() { }

        public Survey(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }

        public Survey(string _name)
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