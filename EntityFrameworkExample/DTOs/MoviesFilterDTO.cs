namespace EntityFrameworkExample.DTOs
{
    public class MoviesFilterDTO
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool OnCinemas { get; set; }
        public bool NextReleases { get; set; }
    }
}
