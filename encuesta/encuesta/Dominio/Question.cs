
using System;
namespace encuesta
{
    public class Question : BaseItem
    {
        public Question() { }

        public Question(string _details, string _when, int _score)
        {
            Details = _details;
            When = _when;
            Score = _score;
        }

        public Question(string _details, When _when, int _score)
        {
            Details = _details;
            _when = this._when;
            Score = _score;
        }

        public string Details { get; set; }
        public string When { get; set; }
        public int Score { get; set; }

        private When _when;

        public override string ToString()
        {
            return $"{ID}, {Details}, {When}, {Score}";
        }
    }
}