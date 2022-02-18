using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkExample.Entitites
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [InverseProperty("Sender")]
        public List<Message> SendedMessages { get; set; }
        [InverseProperty("Receptor")]
        public List<Message> ReceivedMessages { get; set; }
    }
}
