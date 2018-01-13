
using System;
namespace encuesta
{
    public class Question : BaseItem
    {
        public Question() { }

        public Question(string _details, string _when, int _score)
        {
            Details = _details;
            Moment = _when;
            Score = _score;
        }

        public Question(string _details, Moment _when, int _score)
        {
            Details = _details;
            _when = this._moment;
            Score = _score;
        }

        public string Details { get; set; }
        public string Moment { get; set; }
        public int Score { get; set; }

        private Moment _moment;

        public override string ToString()
        {
            return $"{ID}, {Details}, {Moment}, {Score}";
        }
    }
}