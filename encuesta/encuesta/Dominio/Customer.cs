
using System;
namespace encuesta
{
    public class Customer : BaseItem
    {
        public Customer()
        {

        }

        public Customer(int _id, string _name, string _address)
        {
            ID = _id;
            Name = _name;
            Address = _address;
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