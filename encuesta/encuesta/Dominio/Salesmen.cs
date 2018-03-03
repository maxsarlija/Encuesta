
using SQLite;
using System;
namespace encuesta
{
    public class Salesmen : BaseItemAutoIncrement
    {
        public Salesmen() { }

        public Salesmen(int _id, int _supervisorID, int _salesmanID)
        {
            ID = _id;
            SupervisorID = _supervisorID;
            SalesmanID = _salesmanID;
        }

        public Salesmen(int _supervisorID, int _salesmanID)
        {
            SupervisorID = _supervisorID;
            SalesmanID = _salesmanID;
        }

        public int SupervisorID { get; set; }
        public int SalesmanID { get; set; }

        public override string ToString()
        {
            return $"{ID}";
        }
    }
}