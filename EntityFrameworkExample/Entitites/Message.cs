namespace EntityFrameworkExample.Entitites
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public Person Sender { get; set; }
        public int ReceptorId { get; set; }
        public Person Receptor { get; set; }
    }
}
