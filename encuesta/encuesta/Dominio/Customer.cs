
using System;
namespace encuesta
{
    public class Customer : BaseItem
    {
        public Customer()
        {

        }

        public Customer(int _id, string _name, string _address, int _planGold, int _salesmanID, int _zoneID)
        {
            ID = _id;
            Name = _name;
            Address = _address;
            PlanGold = _planGold;
            SalesmanID = _salesmanID;
            ZoneID = _zoneID;
        }

        public Customer(string _name, string _address, int _planGold, int _salesmanID, int _zoneID)
        {
            Name = _name;
            Address = _address;
            PlanGold = _planGold;
            SalesmanID = _salesmanID;
            ZoneID = _zoneID;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public int PlanGold { get; set; }
        public int SalesmanID { get; set; }
        public int ZoneID { get; set; }

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