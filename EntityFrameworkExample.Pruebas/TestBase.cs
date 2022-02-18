using AutoMapper;
using EntityFrameworkExample.Services;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Test
{
    public class TestBase
    {
        protected ApplicationDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var userService = new UserService();

            var dbContext = new ApplicationDbContext(options, userService, eventosDbContext: null);
            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}
