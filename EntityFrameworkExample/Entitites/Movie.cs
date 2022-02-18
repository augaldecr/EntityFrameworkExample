using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Entitites
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool OnCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        //[Unicode(false)]
        public string PosterURL { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
