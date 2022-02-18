namespace EntityFrameworkExample.DTOs
{
    public class MovieCreateDTO
    {
        public string Title { get; set; }
        public bool OnCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<int> Genres { get; set; }
        public List<int> Cinemas { get; set; }
        public List<ActorMovieCreateDTO> ActorsMovies { get; set; }
    }
}
