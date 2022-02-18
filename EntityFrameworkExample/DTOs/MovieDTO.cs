namespace EntityFrameworkExample.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public ICollection<MovieTheaterDTO> MovieTheaters { get; set; }
        public ICollection<ActorDTO> Actors { get; set; }
    }
}
