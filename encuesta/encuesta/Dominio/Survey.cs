
using SQLite;
using System;
namespace encuesta
{
    public class Survey : BaseItem
    {
        public Survey() { }

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