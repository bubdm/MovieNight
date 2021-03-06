using System.Collections.Generic;

namespace MovieNight.Core.Models
{
    public class GenreResponse
    {
        //public Genres[] genres { get; set; }
        public List<Genres> genres { get; set; }
        public string status_message { get; set; }
        public int status_code { get; set; }
    }

    public class Genres
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
