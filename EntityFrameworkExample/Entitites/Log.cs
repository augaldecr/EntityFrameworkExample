using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkExample.Entitites
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Example { get; set; }
    }
}
