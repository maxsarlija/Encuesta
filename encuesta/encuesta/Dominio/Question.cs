
using System;
namespace encuesta
{
    public class Question : BaseItem
    {
        public Question() { }

        public Question(int _id, string _details, string _when, int _score, string _shortDescription)
        {
            ID = _id;
            Details = _details;
            Moment = _when;
            Score = _score;
            ShortDescription = _shortDescription;
        }

        public Question(string _details, string _when, int _score, string _shortDescription)
        {
            Details = _details;
            Moment = _when;
            Score = _score;
            ShortDescription = _shortDescription;
        }


        public string Details { get; set; }
        public string Moment { get; set; }
        public int Score { get; set; }
        public string ShortDescription { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Details}, {Moment}, {Score}";
        }
    }
}