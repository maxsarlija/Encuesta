﻿
using SQLite;
using System;
namespace encuesta
{
    public class Survey : BaseItem
    {
        public Survey() { }

        public Survey(int _id, string _name, string _notes, int _scope, int _zoneID)
        {
            ID = _id;
            Name = _name;
            Notes = _notes;
            Scope = _scope;
            ZoneID = _zoneID;
        }

        public Survey(string _name, string _notes, int _scope, int _zoneID)
        {
            Name = _name;
            Notes = _notes;
            Scope = _scope;
            ZoneID = _zoneID;
        }

        public string Name { get; set; }
        public string Notes { get; set; }
        public int Scope { get; set; }
        public int ZoneID { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}";
        }
    }
}