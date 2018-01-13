

using System;
namespace encuesta
{
    public class QuestionOption : BaseItem
    {
        public int QuestionID { get; set; }
        public int OptionNumber { get; set; }
        public int OptionText { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {QuestionID}, {OptionNumber}, {OptionText}";
        }
    }
}