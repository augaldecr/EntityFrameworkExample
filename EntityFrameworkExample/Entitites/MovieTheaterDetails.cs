using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Entitites
{
    public class MovieTheaterDetails
    {
        public int Id { get; set; }
        [Required]
        public string History { get; set; }
        public string Values { get; set; }
        public string Mision { get; set; }
        public string EthicsCode { get; set; }
        public MovieTheater MovieTheater { get; set; }
    }
}
