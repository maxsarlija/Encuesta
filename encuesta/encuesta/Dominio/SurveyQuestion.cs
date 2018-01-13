
using System;
namespace encuesta
{
    public class SurveyQuestion : BaseItem
    {
        public SurveyQuestion() { }
        
        public SurveyQuestion(int _surveyID, int _questionID, int _questionNumber)
        {
            SurveyID = _surveyID;
            QuestionID = _questionID;
            QuestionNumber = _questionNumber;
        }

        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionNumber { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {SurveyID}, {QuestionID}, {QuestionNumber}";
        }
    }
}