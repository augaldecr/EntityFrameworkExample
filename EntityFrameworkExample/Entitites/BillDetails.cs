using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Entitites
{
    public class BillDetails
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string Product { get; set; }
        [Precision(18,2)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Precision(18,2)]
        public decimal Total { get; set; }
    }
}
