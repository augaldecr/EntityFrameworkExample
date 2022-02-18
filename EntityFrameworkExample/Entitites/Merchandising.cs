namespace EntityFrameworkExample.Entitites
{
    public class Merchandising: Product
    {
        public bool OnInventory { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public bool Clothes { get; set; }
        public bool Colectionable { get; set; }
    }
}
