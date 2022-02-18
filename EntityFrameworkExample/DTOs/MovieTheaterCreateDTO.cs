using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.DTOs
{
    public class MovieTheaterCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public MovieTheaterOfferCreateDTO MovieTheaterOffer { get; set; }
        public CinemaCreateDTO[] Cinemas { get; set; }
    }
}
