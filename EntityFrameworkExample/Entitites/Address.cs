using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Entitites
{
    [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
