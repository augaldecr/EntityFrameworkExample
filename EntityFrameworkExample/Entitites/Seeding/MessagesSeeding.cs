using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Entitites.Seeding
{
    public static class MessagesSeeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var felipe = new Person() { Id = 1, Name = "Felipe" };
            var claudia = new Person() { Id = 2, Name = "Claudia" };

            var mensaje1 = new Message() { Id = 1, Content = "Hola, Claudia!", SenderId = felipe.Id, ReceptorId = claudia.Id };
            var mensaje2 = new Message() { Id = 2, Content = "Hola, Felipe, ¿Cómo te va?", SenderId = claudia.Id, ReceptorId = felipe.Id };
            var mensaje3 = new Message() { Id = 3, Content = "Todo bien, ¿Y tú?", SenderId = felipe.Id, ReceptorId = claudia.Id };
            var mensaje4 = new Message() { Id = 4, Content = "Muy bien :)", SenderId = claudia.Id, ReceptorId = felipe.Id };

            modelBuilder.Entity<Person>().HasData(felipe, claudia);
            modelBuilder.Entity<Message>().HasData(mensaje1, mensaje2, mensaje3, mensaje4);
        }
    }
}
