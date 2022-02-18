namespace EntityFrameworkExample.Entitites
{
    public class MovieRentable
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime RentEndDate { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public Movie Movie { get; set; }
    }
}
