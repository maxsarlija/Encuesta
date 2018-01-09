
using System;
namespace encuesta
{
    public class Customer : BaseItem
    {
        public Customer()
        {

        }

        public Customer(string _name, string _address)
        {
            Name = _name;
            Address = _address;
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}, {Address}";
        }
    }
}