
using SQLite;
using System;
namespace encuesta
{
    public class SurveyGroup : BaseItemAutoIncrement
    {
        public SurveyGroup() { }

        public SurveyGroup(int _id, int _surveyID, int _groupID)
        {
            ID = _id;
            SurveyID = _surveyID;
            GroupID = _groupID;
        }

        public SurveyGroup(int _surveyID, int _groupID)
        {
            SurveyID = _surveyID;
            GroupID = _groupID;
        }

        public int SurveyID { get; set; }
        public int GroupID { get; set; }

        public override string ToString()
        {
            return $"{ID}";
        }
    }
}