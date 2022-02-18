using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkExample.DTOs;
using EntityFrameworkExample.Entitites;
using EntityFrameworkExample.Entitites.Keyless;
using EntityFrameworkExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/movietheaters")]
    public class MovieTheatersController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUpdaterObservableCollection _updaterObservableCollection;

        public MovieTheatersController(ApplicationDbContext context, IMapper mapper, 
            IUpdaterObservableCollection actualizadorObservableCollection)
        {
            _context = context;
            _mapper = mapper;
            _updaterObservableCollection = actualizadorObservableCollection;
        }

        [HttpGet("locationless")]
        public async Task<IEnumerable<LocationlessMovieTheater>> GetLocationlessMovieTheaters()
        {
            //return await context.Set<LocationlessMovieTheater>().ToListAsync();
            return await _context.LocationlessMovieTheaters.ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<MovieTheaterDTO>> Get()
        {
            return await _context.MovieTheaters.ProjectTo<MovieTheaterDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("nearby")]
        public async Task<ActionResult> Get(double latitud, double longitud)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitud, latitud));
            var maxDistanceMTS = 2000;

            var movieTheaters = await _context.MovieTheaters
                .OrderBy(c => c.Location.Distance(myLocation))
                .Where(c => c.Location.IsWithinDistance(myLocation, maxDistanceMTS))
                .Select(c => new
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                }).ToListAsync();

            return Ok(movieTheaters);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var movieTheaterLocation = geometryFactory.CreatePoint(new Coordinate(-69.896979, 18.476276));

            var movieTheater = new MovieTheater()
            {
                Name = "My movietheater with details",
                Location = movieTheaterLocation,
                MovieTheaterDetails = new MovieTheaterDetails()
                {
                    History = "History...",
                    EthicsCode = "Ethics...",
                    Mision = "Mision..."
                },
                MovieTheaterOffer = new MovieTheaterOffer()
                {
                    Discount = 5,
                    Begins = DateTime.Today,
                    Ends = DateTime.Today.AddDays(7)
                },
                Cinemas = new ObservableCollection<Cinema>()
                {
                    new Cinema()
                    {
                        Price = 200,
                        Currency = Currency.Euro,
                        CinemaType = CinemaType.TwoD
                    },
                    new Cinema()
                    {
                        Price = 350,
                        Currency = Currency.Dollar,
                        CinemaType = CinemaType.ThreeD
                    }
                }
            };

            _context.Add(movieTheater);
            await _context.SaveChangesAsync();
            return Ok();
        }
    
        [HttpPost("usingDTO")]
        public async Task<ActionResult> Post(MovieTheaterCreateDTO movieTheaterCreateDTO)
        {
            var movieTheater = _mapper.Map<MovieTheater>(movieTheaterCreateDTO);
            _context.Add(movieTheater);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            //var movieTheaterDB = await context.MovieTheaters.AsTracking()
            //               .Include(c => c.Cinemas)
            //               .Include(c => c.MovieTheaterOffer)
            //               .Include(c => c.MovieTheaterDetails)
            //               .FirstOrDefaultAsync(c => c.Id == id);

            var movieTheaterDB = await _context.MovieTheaters
                            .FromSqlInterpolated($"Select * FROM MovieTheaters WHERE Id = {id}")
                            .Include(c => c.Cinemas)
                            .Include(c => c.MovieTheaterOffer)
                            .Include(c => c.MovieTheaterDetails)
                            .FirstOrDefaultAsync();

            if (movieTheaterDB is null)
            {
                return NotFound();
            }

            movieTheaterDB.Location = null;
            return Ok(movieTheaterDB);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(MovieTheaterCreateDTO movieTheaterCreateDTO, int id)
        {
            var movieTheaterDB = await _context.MovieTheaters.AsTracking()
                            .Include(c => c.Cinemas)
                            .Include(c => c.MovieTheaterOffer)
                            .FirstOrDefaultAsync(c => c.Id == id);

            if (movieTheaterDB is null)
            {
                return NotFound();
            }

            movieTheaterDB = _mapper.Map(movieTheaterCreateDTO, movieTheaterDB);
            _updaterObservableCollection.Update(movieTheaterDB.Cinemas, movieTheaterCreateDTO.Cinemas);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("movieTheaterOffer")]
        public async Task<ActionResult> PutCineOferta(MovieTheaterOffer movieTheaterOffer)
        {
            _context.Update(movieTheaterOffer);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieTheater = await _context.MovieTheaters
                .Include(c => c.Cinemas)
                .Include(c => c.MovieTheaterOffer).FirstOrDefaultAsync(x => x.Id == id);

            if (movieTheater is null)
            {
                return NotFound();
            }

            _context.RemoveRange(movieTheater.Cinemas);
            await _context.SaveChangesAsync();

            _context.Remove(movieTheater);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
