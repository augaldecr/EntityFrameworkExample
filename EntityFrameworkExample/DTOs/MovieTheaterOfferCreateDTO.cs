using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.DTOs
{
    public class MovieTheaterOfferCreateDTO
    {
        [Range(1, 100)]
        public double Discount { get; set; }
        public DateTime Begins { get; set; }
        public DateTime Ends { get; set; }
    }
}
