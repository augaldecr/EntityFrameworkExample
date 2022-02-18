using AutoMapper;
using EntityFrameworkExample.DTOs;
using EntityFrameworkExample.Entitites;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityFrameworkExample.Services
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();

            CreateMap<GenreUpdateDTO, Genre>();

            CreateMap<MovieTheater, MovieTheaterDTO>()
                .ForMember(dto => dto.Latitud, ent => ent.MapFrom(prop => prop.Location.Y))
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Location.X));

            CreateMap<Genre, GenreDTO>();

            // Sin projectTo
            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.MovieTheaters, ent => ent.MapFrom(prop => prop.Cinemas.Select(s => s.MovieTheater)))
                .ForMember(dto => dto.Actors, ent =>
                    ent.MapFrom(prop => prop.ActorsMovies.Select(pa => pa.Actor)));

            // Con ProjectTo
            //CreateMap<Pelicula, PeliculaDTO>()
            //    .ForMember(dto => dto.Genres, ent => ent.MapFrom(prop => 
            //        prop.Genres.OrderByDescending(g => g.Nombre)))
            //    .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.SalasDeCine.Select(s => s.Cine)))
            //    .ForMember(dto => dto.Actores, ent =>
            //        ent.MapFrom(prop => prop.PeliculasActores.Select(pa => pa.Actor)));

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<MovieTheaterCreateDTO, MovieTheater>()
                .ForMember(ent => ent.Cinemas, options => options.Ignore())
                .ForMember(ent => ent.Location, 
                    dto => dto.MapFrom(field => 
                        geometryFactory.CreatePoint(new Coordinate(field.Longitud, field.Latitud))));

            CreateMap<MovieTheaterOfferCreateDTO, MovieTheaterOffer>();
            CreateMap<CinemaCreateDTO, Cinema>();

            CreateMap<MovieCreateDTO, Movie>()
                .ForMember(ent => ent.Genres,
                    dto => dto.MapFrom(field => field.Genres.Select(id => new Genre() { Identifier = id })))
                .ForMember(ent => ent.Cinemas,
                    dto => dto.MapFrom(field => field.Cinemas.Select(id => new Cinema() { Id = id })));

            CreateMap<ActorMovieCreateDTO, ActorMovie>();

            CreateMap<ActorCreateDTO, Actor>();
        }
    }
}
