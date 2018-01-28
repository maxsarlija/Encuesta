

using System;
namespace encuesta
{
    public class QuestionOption : BaseItem
    {
        public int QuestionID { get; set; }
        public int OptionNumber { get; set; }
        public string OptionText { get; set; }

        public QuestionOption() { }

        public QuestionOption(int _id, int _questionID, int _optionNumber, string _optionText)
        {
            ID = _id;
            QuestionID = _questionID;
            OptionNumber = _optionNumber;
            OptionText = _optionText;
        }

        public override string ToString()
        {
            return $"{ID}, {QuestionID}, {OptionNumber}, {OptionText}";
        }
    }
}