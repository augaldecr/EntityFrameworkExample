using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Entitites
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int BillNumber { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
    }
}
