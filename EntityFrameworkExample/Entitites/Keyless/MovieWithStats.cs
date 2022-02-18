namespace EntityFrameworkExample.Entitites.Keyless
{
    public class MovieWithStats
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenresCount { get; set; }
        public int MovieTheatersCount { get; set; }
        public int ActorCount { get; set; }
    }
}
