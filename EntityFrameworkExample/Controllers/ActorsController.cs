using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkExample.DTOs;
using EntityFrameworkExample.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get()
        {
            return await _context.Actors
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreateDTO actorCreacionDTO)
        {
            var actor = _mapper.Map<Actor>(actorCreacionDTO);
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActorCreateDTO actorCreacionDTO, int id)
        {
            var actorDB = await _context.Actors.AsTracking().FirstOrDefaultAsync(a => a.Id == id);

            if (actorDB is null)
            {
                return NotFound();
            }

            actorDB = _mapper.Map(actorCreacionDTO, actorDB);
            var entry = _context.Entry(actorDB);
            //await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("desconected/{id:int}")]
        public async Task<ActionResult> PutDesconectado(ActorCreateDTO actorCreateDTO, int id)
        {
            var actorExist = await _context.Actors.AnyAsync(a => a.Id == id);

            if (!actorExist)
            {
                return NotFound();
            }

            var actor = _mapper.Map<Actor>(actorCreateDTO);
            actor.Id = id;

            //context.Update(actor);

            _context.Entry(actor).Property(a => a.Name).IsModified = true;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
