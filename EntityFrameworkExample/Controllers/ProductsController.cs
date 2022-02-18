using EntityFrameworkExample.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await context.Products.ToListAsync();
        }

        [HttpGet("Merchs")]
        public async Task<ActionResult<IEnumerable<Merchandising>>> GetMerchs()
        {
            return await context.Set<Merchandising>().ToListAsync();
        }

        [HttpGet("Rentals")]
        public async Task<ActionResult<IEnumerable<RentableMovie>>> GetRentals()
        {
            return await context.Set<RentableMovie>().ToListAsync();
        }
    }
}
