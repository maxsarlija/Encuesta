
using encuesta.Dominio.Enum;
using SQLite;
using System;
namespace encuesta
{
    public class CustomerAnswer : BaseItemAutoIncrement
    {
        public CustomerAnswer() { }

        public CustomerAnswer(int _id, int _customerID, int _surveyID, string _userID)
        {
            ID = _id;
            CustomerID = _customerID;
            SurveyID = _surveyID;
            Status = SurveyStatus.PENDING;
            UserID = _userID;
        }
        public CustomerAnswer(int _customerID, int _surveyID, string _userID)
        {
            CustomerID = _customerID;
            SurveyID = _surveyID;
            Status = SurveyStatus.PENDING;
            UserID = _userID;
        }


        [Indexed]
        public int CustomerID { get; set; }
        public int SurveyID { get; set; }
        public DateTime DateCompleted { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {CustomerID}, {SurveyID}, {DateCompleted}, {Status}";
        }
    }
}