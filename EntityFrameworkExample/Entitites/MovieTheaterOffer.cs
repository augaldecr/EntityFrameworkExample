namespace EntityFrameworkExample.Entitites
{
    public class MovieTheaterOffer
    {
        public int Id { get; set; }
        public DateTime Begins { get; set; }
        public DateTime Ends { get; set; }
        public decimal Discount { get; set; }
        public int? MovieTheaterId { get; set; }
    }
}
