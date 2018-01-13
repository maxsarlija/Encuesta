
using System;
namespace encuesta
{
    public class Moment : BaseItem
    {
        public Moment() { }

        public Moment(string _category, string _description)
        {
            Category = _category;
            Description = _description;
        }

        public string Category { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Description}";
        }
    }
}