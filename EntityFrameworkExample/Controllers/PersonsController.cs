using EntityFrameworkExample.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            return await context.Persons
                        .Include(p => p.SendedMessages)
                        .Include(p => p.ReceivedMessages)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
