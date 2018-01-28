﻿
using encuesta.Dominio.Enum;
using SQLite;
using System;
namespace encuesta
{
    public class Answer : BaseItem
    {
        public Answer() { }
        public Answer(int _id, int _customerAnswerID, int _questionID)
        {
            ID = _id;
            CustomerAnswerID = _customerAnswerID;
            QuestionID = _questionID;
            Option = AnswerOptions.PENDING;
        }

        public Answer(int _customerAnswerID, int _questionID)
        {
            CustomerAnswerID = _customerAnswerID;
            QuestionID = _questionID;
            Option = AnswerOptions.PENDING;
        }

        [Indexed]
        public int CustomerAnswerID { get; set; }
        public int QuestionID { get; set; }
        public string Option { get; set; }
        public int Score { get; set; }
        
        public override string ToString()
        {
            return $"{ID}, {CustomerAnswerID}, {QuestionID}, {Option}, {Score}";
        }
    }
}