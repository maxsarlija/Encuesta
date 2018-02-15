
using SQLite;
using System;
namespace encuesta
{
    public class SubGroupQuestion : BaseItemAutoIncrement
    {
        public SubGroupQuestion() { }

        public SubGroupQuestion(int _id, int _subGroupID, int _questionID, int _score, int _questionNumber)
        {
            ID = _id;
            SubGroupID = _subGroupID;
            QuestionID = _questionID;
            Score = _score;
            QuestionOrder = _questionNumber;
        }

        public SubGroupQuestion(int _subGroupID, int _questionID, int _score, int _questionNumber)
        {
            SubGroupID = _subGroupID;
            QuestionID = _questionID;
            Score = _score;
            QuestionOrder = _questionNumber;
        }

        public int SubGroupID { get; set; }
        public int QuestionID { get; set; }
        public int Score { get; set; }
        public int QuestionOrder { get; set; }

        public override string ToString()
        {
            return $"{ID}";
        }
    }
}