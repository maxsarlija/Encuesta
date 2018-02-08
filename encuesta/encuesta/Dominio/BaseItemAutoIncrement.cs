using SQLite;

namespace encuesta
{
    public class BaseItemAutoIncrement
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}