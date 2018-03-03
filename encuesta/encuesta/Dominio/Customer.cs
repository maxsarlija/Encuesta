
using System;
namespace encuesta
{
    public class Customer : BaseItem
    {
        public Customer()
        {

        }

        public Customer(int _id, string _name, string _address, int _planGold)
        {
            ID = _id;
            Name = _name;
            Address = _address;
            PlanGold = _planGold;
        }

        public Customer(string _name, string _address, int _planGold)
        {
            Name = _name;
            Address = _address;
            PlanGold = _planGold;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public int PlanGold { get; set; }

        public bool PlanGoldBool
        {
            get { return PlanGold == 1 ? true : false; }
        }

        public override string ToString()
        {
            return $"{ID}, {Name}, {Address}";
        }
    }
}