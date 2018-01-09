

using System;
namespace encuesta
{
    public class SurveyItem : BaseItem
    {
        public int Customer { get; set; }
        public int Survey { get; set; }
        public int Question { get; set; }
        public bool Answer { get; set; }
        public int Score { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {Customer}, {Survey}, {Question}, {Answer}, {Score}";
        }
    }
}