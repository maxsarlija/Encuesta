
using SQLite;
using System;
namespace encuesta
{
    public class Survey : BaseItem
    {
        public Survey() { }

        public Survey(int _id, string _name, string _notes, int _scope, int _zoneID, int _planGold)
        {
            ID = _id;
            Name = _name;
            Notes = _notes;
            Scope = _scope;
            ZoneID = _zoneID;
            PlanGold = _planGold;
        }

        public Survey(string _name, string _notes, int _scope, int _zoneID, int _planGold)
        {
            Name = _name;
            Notes = _notes;
            Scope = _scope;
            ZoneID = _zoneID;
            PlanGold = _planGold;
        }

        public string Name { get; set; }
        public string Notes { get; set; }
        public int Scope { get; set; }
        public int ZoneID { get; set; }
        public int PlanGold { get; set; }

        public bool PlanGoldBool
        {
            get { return PlanGold == 1 ? true : false; }
        }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}