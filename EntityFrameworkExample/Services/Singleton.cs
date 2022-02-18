using EntityFrameworkExample.Entitites;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Services
{
    public class Singleton
    {
        private readonly IServiceProvider serviceProvider;

        public Singleton(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<Genre>> ObtenerGeneros()
        {
            await using (var scope = serviceProvider.CreateAsyncScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return await context.Genres.ToListAsync();
            }
        }
    }
}
