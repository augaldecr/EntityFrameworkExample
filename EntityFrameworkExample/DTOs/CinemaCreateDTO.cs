using EntityFrameworkExample.Entitites;

namespace EntityFrameworkExample.DTOs
{
    public class CinemaCreateDTO: IId
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CinemaType CinemaType { get; set; }
    }
}
