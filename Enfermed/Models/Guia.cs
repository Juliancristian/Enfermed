using SQLite;

namespace Enfermed.Models
{
    public class Guia
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

}