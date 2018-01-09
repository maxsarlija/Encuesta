

using System;
namespace encuesta
{
    public class Survey : BaseItem
    {
        public int Customer { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {Customer}, {Date}, {Score}";
        }
    }
}