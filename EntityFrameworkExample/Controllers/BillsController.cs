using EntityFrameworkExample.Entitites;
using EntityFrameworkExample.Entitites.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/bills")]
    public class BillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BillsController> _logger;

        public BillsController(ApplicationDbContext context, ILogger<BillsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<IEnumerable<BillDetails>>> GetDetalle(int id)
        {
            return await _context.BillsDetails.Where(f => f.BillId == id)
                .OrderByDescending(f => f.Total).ToListAsync();
        }

        [HttpPost("Concurrency_Line")]
        public async Task<ActionResult> ConcurrencyLine()
        {
            var billId = 2;

            var bill = await _context.Bills.AsTracking().FirstOrDefaultAsync(f => f.Id == billId);
            bill.CreationDate = DateTime.Now;

            await _context.Database.ExecuteSqlInterpolatedAsync(
                                                     @$"UPDATE Bills SET CreationDate = GetDate()
                                                        WHERE Id = {billId}");

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Concurrency_Line_Handling_Error")]
        public async Task<ActionResult> ConcurrencyLineHandlingError()
        {
            var billId = 2;

            try
            {
                var bill = await _context.Bills.AsTracking().FirstOrDefaultAsync(f => f.Id == billId);
                bill.CreationDate = DateTime.Now.AddDays(-10);

                await _context.Database.ExecuteSqlInterpolatedAsync(
                                                         @$"UPDATE Bills SET CreationDate = GetDate()
                                                        WHERE Id = {billId}");
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();

                var billActual = await _context.Bills.AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == billId);

                foreach (var property in entry.Metadata.GetProperties())
                {
                    var triedValue = entry.Property(property.Name).CurrentValue;
                    var valueDBActual = _context.Entry(billActual).Property(property.Name).CurrentValue;
                    var prevValue = entry.Property(property.Name).OriginalValue;

                    if (valueDBActual.ToString() == triedValue.ToString())
                    {
                        // Esta property no fue modificada
                        continue;
                    }

                    _logger.LogInformation($"--- Property {property.Name} ---");
                    _logger.LogInformation($"Value tried {triedValue}");
                    _logger.LogInformation($"Value in DB {valueDBActual}");
                    _logger.LogInformation($"Prev value {prevValue}");

                    // hacer algo...
                }

                return BadRequest("The record could not be updated because it was modified by another person");
            }
           
        }


        [HttpGet("Functions_escalars")]
        public async Task<ActionResult> GetFunctionsEscalars()
        {
            var bills = await _context.Bills.Select(f => new
            {
                Id = f.Id,
                Total = _context.BillDetailsTotal(f.Id),
                Average = Escalars.BillDetailsAverage(f.Id)
            }).
            OrderByDescending(f => _context.BillDetailsTotal(f.Id))
            .ToListAsync();

            return Ok(bills);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var bill = new Bill()
                {
                    CreationDate = DateTime.Now
                };

                _context.Add(bill);
                await _context.SaveChangesAsync();

                throw new ApplicationException("This is a test");

                var billDeatils = new List<BillDetails>()
                        {
                            new BillDetails()
                            {
                                Product = "Product A",
                                Price = 123,
                                BillId = bill.Id
                            },
                            new BillDetails()
                            {
                                Product = "Product B",
                                Price = 456,
                                BillId = bill.Id
                            }
                        };

                _context.AddRange(billDeatils);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                // Handle excepción
                return BadRequest("An error");
            }
        }

        [HttpGet("GetBill")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var factura = await _context.Bills.FirstOrDefaultAsync(f => f.Id == id);

            if (factura is null)
            {
                return NotFound();
            }

            return factura;
        }

        [HttpPut("UpdateBill")]
        public async Task<ActionResult> UpdateBill(Bill bill)
        {
            _context.Update(bill);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
