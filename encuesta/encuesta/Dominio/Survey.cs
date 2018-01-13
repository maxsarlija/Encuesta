
using SQLite;
using System;
namespace encuesta
{
    public class Survey : BaseItem
    {
        public DateTime Date { get; set; }
        public int Score { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {Date}, {Score}";
        }
    }
}