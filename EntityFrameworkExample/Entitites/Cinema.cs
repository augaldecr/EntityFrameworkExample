using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkExample.Entitites
{
    public class Cinema: IId
    {
        public int Id { get; set; }
        public CinemaType CinemaType { get; set; }
        public decimal Price { get; set; }
        public int TheMovieTheater { get; set; }
        [ForeignKey(nameof(TheMovieTheater))]
        public MovieTheater MovieTheater { get; set; }
        public HashSet<Movie> Movies { get; set; }
        public Currency Currency { get; set; }
    }
}
