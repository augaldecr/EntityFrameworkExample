using AutoMapper;
using EntityFrameworkExample.DTOs;
using EntityFrameworkExample.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenresController(ApplicationDbContext context, IMapper mapper,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _context = context;
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            using (var nuevoContext = _dbContextFactory.CreateDbContext())
            {
                nuevoContext.Logs.Add(new Log
                {
                    Id = Guid.NewGuid(),
                    Message = "Executing GenresController.Get"
                });
                await nuevoContext.SaveChangesAsync();
                return await nuevoContext.Genres.OrderByDescending(g => EF.Property<DateTime>(g, "CreationDate")).ToListAsync();
            }
        }

        [HttpGet("stored_procedure/{id:int}")]
        public async Task<ActionResult<Genre>> GetSP(int id)
        {
            var genres = _context.Genres
                        .FromSqlInterpolated($"EXEC Genres_GetById {id}")
                        .IgnoreQueryFilters()
                        .AsAsyncEnumerable();

            await foreach (var genre in genres)
            {
                return genre;
            }

            return NotFound();
        }

        [HttpPut("update_multiple_times")]
        public async Task<ActionResult> UpdateMultipleTimes()
        {
            var id = 3;
            var genre = await _context.Genres.AsTracking().FirstOrDefaultAsync(g => g.Identifier == id);

            genre.Name = "Comedia 2";
            await _context.SaveChangesAsync();
            await Task.Delay(2000);

            genre.Name = "Comedia 3";
            await _context.SaveChangesAsync();
            await Task.Delay(2000);

            genre.Name = "Comedia 4";
            await _context.SaveChangesAsync();
            await Task.Delay(2000);

            genre.Name = "Comedia 5";
            await _context.SaveChangesAsync();
            await Task.Delay(2000);

            genre.Name = "Comedia 6";
            await _context.SaveChangesAsync();
            await Task.Delay(2000);

            genre.Name = "Comedia Actual";
            await _context.SaveChangesAsync();
            await Task.Delay(2000);

            return Ok();
        }


        [HttpPost("Stored_Procedure")]
        public async Task<ActionResult> PostSP(Genre genre)
        {
            var genreWithProvidedNameExist = await _context.Genres.AnyAsync(g => g.Name == genre.Name);

            if (genreWithProvidedNameExist)
            {
                return BadRequest("An genre already exist using the name: " + genre.Name);
            }

            var outputId = new SqlParameter();
            outputId.ParameterName = "@id";
            outputId.SqlDbType = System.Data.SqlDbType.Int;
            outputId.Direction = System.Data.ParameterDirection.Output;

            await _context.Database
                .ExecuteSqlRawAsync("EXEC Genres_Insert @name = {0}, @id = {1} OUTPUT",
                genre.Name, outputId);

            var id = (int)outputId.Value;
            return Ok(id);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await _context.Genres.AsTracking().FirstOrDefaultAsync(g => g.Identifier == id);

            if (genre is null)
            {
                return NotFound();
            }

            var creationDate = _context.Entry(genre).Property<DateTime>("CreationDate").CurrentValue;
            var periodStart = _context.Entry(genre).Property<DateTime>("PeriodStart").CurrentValue;
            var periodEnd = _context.Entry(genre).Property<DateTime>("PeriodEnd").CurrentValue;

            return Ok(new
            {
                Id = genre.Identifier,
                Nombre = genre.Name,
                creationDate,
                periodStart,
                periodEnd
            });
        }

        [HttpGet("TempAll/{id:int}")]
        public async Task<ActionResult> GetTempAll(int id)
        {
            var genres = await _context.Genres.TemporalAll().AsTracking()
                .Where(g => g.Identifier == id)
                .Select(g => new
                {
                    Id = g.Identifier,
                    Nombre = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet("TempAsOf/{id:int}")]
        public async Task<ActionResult> GetTempAsOf(int id, DateTime date)
        {
            var genre = await _context.Genres.TemporalAsOf(date).AsTracking()
                .Where(g => g.Identifier == id)
                .Select(g => new
                {
                    Id = g.Identifier,
                    Nombre = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                }).FirstOrDefaultAsync();

            return Ok(genre);
        }

        [HttpGet("TempFromTo/{id:int}")]
        public async Task<ActionResult> GetTempFromTo(int id, DateTime from, DateTime to)
        {
            var genres = await _context.Genres.TemporalFromTo(from, to).AsTracking()
                .Where(g => g.Identifier == id)
                .Select(g => new
                {
                    Id = g.Identifier,
                    Nombre = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet("TempContainedIn/{id:int}")]
        public async Task<ActionResult> GetTempContainedIn(int id, DateTime from, DateTime to)
        {
            var genres = await _context.Genres.TemporalContainedIn(from, to).AsTracking()
                .Where(g => g.Identifier == id)
                .Select(g => new
                {
                    Id = g.Identifier,
                    Nombre = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet("TemporalBetween/{id:int}")]
        public async Task<ActionResult> GetTemporalBetween(int id, DateTime from, DateTime to)
        {
            var genres = await _context.Genres.TemporalBetween(from, to).AsTracking()
                .Where(g => g.Identifier == id)
                .Select(g => new
                {
                    Id = g.Identifier,
                    Nombre = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                })
                .ToListAsync();

            return Ok(genres);
        }


        [HttpPost]
        public async Task<ActionResult> Post(Genre genre)
        {
            var genreWithProvidedNameExist = await _context.Genres.AnyAsync(g => g.Name == genre.Name);

            if (genreWithProvidedNameExist)
            {
                return BadRequest("A record with that name already exists: " + genre.Name);
            }

            //context.Add(genre);
            //context.Entry(genre).State = EntityState.Added;

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            INSERT INTO Genres(Name)
            VALUES({genre.Name})");

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> Post(Genre[] genres)
        {
            _context.AddRange(genres);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(GenreUpdateDTO genreUpdateDTO)
        {
            var genre = _mapper.Map<Genre>(genreUpdateDTO);
            _context.Update(genre);
            _context.Entry(genre).Property(g => g.Name)
                    .OriginalValue = genreUpdateDTO.Original_Name;
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("add2")]
        public async Task<ActionResult> Add2(int id)
        {
            var genre = await _context.Genres.AsTracking().FirstOrDefaultAsync(g => g.Identifier == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.Name += " 2";
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Identifier == id);

            if (genre is null)
            {
                return NotFound();
            }

            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("softDelete/{id:int}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var genre = await _context.Genres.AsTracking().FirstOrDefaultAsync(g => g.Identifier == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.Deleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Restore/{id:int}")]
        public async Task<ActionResult> Restore(int id)
        {
            var genre = await _context.Genres.AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Identifier == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.Deleted = false;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("RestoredDeleted/{id:int}")]
        public async Task<ActionResult> RestoredDeleted(int id, DateTime date)
        {
            var genre = await _context.Genres.TemporalAsOf(date).AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Identifier == id);

            if (genre is null)
            {
                return NotFound();
            }

            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"

                SET IDENTITY_INSERT Genres ON;

                INSERT INTO Genres (Identifier, Name)
                VALUES ({genre.Identifier}, {genre.Name})

                SET IDENTITY_INSERT Genres OFF;");
            }
            finally
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Genres OFF;");
            }

            return Ok();
        }

        [HttpPost("concurrency_token")]
        public async Task<ActionResult> ConcurrencyToken()
        {
            var genreId = 1;

            var genre = await _context.Genres.AsTracking().FirstOrDefaultAsync(g => g.Identifier == genreId);
            genre.Name = "Felipe was here";

            // Claudia actualiza el registro en la BD.
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE Genres SET Name = 'Claudia was here' 
                                                        WHERE Identifier = {genre}");
            // Felipe intenta actualizar.
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
