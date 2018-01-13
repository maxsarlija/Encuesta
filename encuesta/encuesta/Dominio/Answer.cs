
using SQLite;
using System;
namespace encuesta
{
    public class Answer : BaseItem
    {
        [Indexed]
        public int Customer { get; set; }
        [Indexed]
        public int QuestionID { get; set; }
        public bool OptionID { get; set; }
        public int Score { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {Customer}, {QuestionID}, {OptionID}, {Score}";
        }
    }
}