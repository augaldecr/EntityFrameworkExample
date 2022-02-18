using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkExample.DTOs;
using EntityFrameworkExample.Entitites;
using EntityFrameworkExample.Entitites.Keyless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("moviesWithStats/{id:int}")]
        public async Task<ActionResult<MovieWithStats>> GetMoviesWithStats(int id)
        {
            var res = await context.MovieWithStats(id).FirstOrDefaultAsync();

            if (res is null)
            {
                return NotFound();
            }

            return res;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await context.Movies
                .Include(p => p.Genres.OrderByDescending(g => g.Name))
                .Include(p => p.Cinemas)
                    .ThenInclude(s => s.MovieTheater)
                .Include(p => p.ActorsMovies.Where(pa => pa.Actor.Birthday.Value.Year >= 1980))
                    .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.MovieTheaters = movieDTO.MovieTheaters.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> Filter(
                [FromQuery] MoviesFilterDTO moviesFiltroDTO)
        {
            var moviesQueryable = context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(moviesFiltroDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(p => p.Title.Contains(moviesFiltroDTO.Title));
            }

            if (moviesFiltroDTO.OnCinemas)
            {
                moviesQueryable = moviesQueryable.Where(p => p.OnCinemas);
            }

            if (moviesFiltroDTO.NextReleases)
            {
                var hoy = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(p => p.ReleaseDate > hoy);
            }

            if (moviesFiltroDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(p => 
                    p.Genres.Select(g => g.Identifier)
                            .Contains(moviesFiltroDTO.GenreId));
            }

            var movies = await moviesQueryable.Include(p => p.Genres).ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovieCreateDTO movieCreacionDTO)
        {
            var movie = mapper.Map<Movie>(movieCreacionDTO);
            movie.Genres.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            movie.Cinemas.ForEach(s => context.Entry(s).State = EntityState.Unchanged);

            if (movie.ActorsMovies is not null)
            {
                for (int i = 0; i < movie.ActorsMovies.Count; i++)
                {
                    movie.ActorsMovies[i].Order = i + 1;
                }
            }

            context.Add(movie);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
