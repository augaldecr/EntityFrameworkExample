using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.DTOs
{
    public class GenreUpdateDTO
    {
        public int Identifier { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Original_Name { get; set; }
    }
}
