
using encuesta.Dominio.Enum;
using SQLite;
using System;
namespace encuesta
{
    public class CustomerAnswer : BaseItem
    {
        public CustomerAnswer() { }
        public CustomerAnswer(int _customerID, int _surveyID)
        {
            CustomerID = _customerID;
            SurveyID = _surveyID;
            Status = SurveyStatus.PENDING;
        }


        [Indexed]
        public int CustomerID { get; set; }
        public int SurveyID { get; set; }
        public DateTime DateCompleted { get; set; }
        public string Status { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {CustomerID}, {SurveyID}, {DateCompleted}, {Status}";
        }
    }
}