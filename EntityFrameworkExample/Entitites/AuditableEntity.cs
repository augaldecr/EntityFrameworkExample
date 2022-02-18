using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Entitites
{
    public class AuditableEntity
    {
        [StringLength(150)]
        public string UsersCreation { get; set; }
        [StringLength(150)]
        public string UsersModification { get; set; }
    }
}
