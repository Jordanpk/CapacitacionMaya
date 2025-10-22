using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Movies
    {
        public int id { get; set; }
        public string title { get; set; }
        public string posterURL { get; set; }
        public string imdbId { get; set; }
        public MovieGenre? genero { get; set; }
    }
}
