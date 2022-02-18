using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Entitites
{
    public class Genre : AuditableEntity
    {
        public int Identifier { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public HashSet<Movie> Movies { get; set; }
        public string Example { get; set; }
    }
}
