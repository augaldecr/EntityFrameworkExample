using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityFrameworkExample.Entitites.Seeding
{
    public static class MoviesSeeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var acción = new Genre { Identifier = 1, Name = "Acción" };
            var animación = new Genre { Identifier = 2, Name = "Animación" };
            var comedia = new Genre { Identifier = 3, Name = "Comedia" };
            var cienciaFicción = new Genre { Identifier = 4, Name = "Ciencia ficción" };
            var drama = new Genre { Identifier = 5, Name = "Drama" };

            modelBuilder.Entity<Genre>().HasData(acción, animación, comedia, cienciaFicción, drama);

            var tomHolland = new Actor() { Id = 1, Name = "Tom Holland", Birthday = new DateTime(1996, 6, 1), Bio = "Thomas Stanley Holland (Kingston upon Thames, Londres; 1 de junio de 1996), conocido simplemente como Tom Holland, es un actor, actor de voz y bailarín británico." };
            var samuelJackson = new Actor() { Id = 2, Name = "Samuel L. Jackson", Birthday = new DateTime(1948, 12, 21), Bio = "Samuel Leroy Jackson (Washington D. C., 21 de diciembre de 1948), conocido como Samuel L. Jackson, es un actor y productor de movieTheater, televisión y teatro estadounidense. Ha sido candidato al premio Óscar, a los Globos de Oro y al Premio del Sindicato de Actores, así como ganador de un BAFTA al mejor actor de reparto." };
            var robertDowney = new Actor() { Id = 3, Name = "Robert Downey Jr.", Birthday = new DateTime(1965, 4, 4), Bio = "Robert John Downey Jr. (Nueva York, 4 de abril de 1965) es un actor, actor de voz, productor y cantante estadounidense. Inició su carrera como actor a temprana edad apareciendo en varios filmes dirigidos por su padre, Robert Downey Sr., y en su infancia estudió actuación en varias academias de Nueva York." };
            var chrisEvans = new Actor() { Id = 4, Name = "Chris Evans", Birthday = new DateTime(1981, 06, 13) };
            var laRoca = new Actor() { Id = 5, Name = "Dwayne Johnson", Birthday = new DateTime(1972, 5, 2) };
            var auliCravalho = new Actor() { Id = 6, Name = "Auli'i Cravalho", Birthday = new DateTime(2000, 11, 22) };
            var scarlettJohansson = new Actor() { Id = 7, Name = "Scarlett Johansson", Birthday = new DateTime(1984, 11, 22) };
            var keanuReeves = new Actor() { Id = 8, Name = "Keanu Reeves", Birthday = new DateTime(1964, 9, 2) };

            modelBuilder.Entity<Actor>().HasData(tomHolland, samuelJackson,
                            robertDowney, chrisEvans, laRoca, auliCravalho, scarlettJohansson, keanuReeves);
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var agora = new MovieTheater() { Id = 1, Name = "Agora Mall", Location = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233)) };
            var sambil = new MovieTheater() { Id = 2, Name = "Sambil", Location = geometryFactory.CreatePoint(new Coordinate(-69.911582, 18.482455)) };
            var megacentro = new MovieTheater() { Id = 3, Name = "Megacentro", Location = geometryFactory.CreatePoint(new Coordinate(-69.856309, 18.506662)) };
            var acropolis = new MovieTheater() { Id = 4, Name = "Acropolis", Location = geometryFactory.CreatePoint(new Coordinate(-69.939248, 18.469649)) };

            var agoraCineOferta = new MovieTheaterOffer { Id = 1, MovieTheaterId = agora.Id, Begins = DateTime.Today, Ends = DateTime.Today.AddDays(7), Discount = 10 };

            var salaDeCine2DAgora = new Cinema()
            {
                Id = 1,
                TheMovieTheater = agora.Id,
                Price = 220,
                CinemaType = CinemaType.TwoD
            };
            var salaDeCine3DAgora = new Cinema()
            {
                Id = 2,
                TheMovieTheater = agora.Id,
                Price = 320,
                CinemaType = CinemaType.ThreeD
            };

            var salaDeCine2DSambil = new Cinema()
            {
                Id = 3,
                TheMovieTheater = sambil.Id,
                Price = 200,
                CinemaType = CinemaType.TwoD
            };
            var salaDeCine3DSambil = new Cinema()
            {
                Id = 4,
                TheMovieTheater = sambil.Id,
                Price = 290,
                CinemaType = CinemaType.ThreeD
            };


            var salaDeCine2DMegacentro = new Cinema()
            {
                Id = 5,
                TheMovieTheater = megacentro.Id,
                Price = 250,
                CinemaType = CinemaType.TwoD
            };
            var salaDeCine3DMegacentro = new Cinema()
            {
                Id = 6,
                TheMovieTheater = megacentro.Id,
                Price = 330,
                CinemaType = CinemaType.ThreeD
            };
            var salaDeCineCXCMegacentro = new Cinema()
            {
                Id = 7,
                TheMovieTheater = megacentro.Id,
                Price = 450,
                CinemaType = CinemaType.CXC
            };

            var salaDeCine2DAcropolis = new Cinema()
            {
                Id = 8,
                TheMovieTheater = acropolis.Id,
                Price = 250,
                CinemaType = CinemaType.TwoD
            };

            var acropolisCineOferta = new MovieTheaterOffer { Id = 2, MovieTheaterId = acropolis.Id, Begins = DateTime.Today, Ends = DateTime.Today.AddDays(5), Discount = 15 };

            modelBuilder.Entity<MovieTheater>().HasData(acropolis, sambil, megacentro, agora);
            modelBuilder.Entity<MovieTheaterOffer>().HasData(acropolisCineOferta, agoraCineOferta);
            modelBuilder.Entity<Cinema>().HasData(salaDeCine2DMegacentro, salaDeCine3DMegacentro, salaDeCineCXCMegacentro, salaDeCine2DAcropolis, salaDeCine2DAgora, salaDeCine3DAgora, salaDeCine2DSambil, salaDeCine3DSambil);


            var avengers = new Movie()
            {
                Id = 1,
                Title = "Avengers",
                OnCinemas = false,
                ReleaseDate = new DateTime(2012, 4, 11),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Avengers_%282012_film%29_poster.jpg",
            };

            var entidadGeneroPelicula = "GeneroPelicula";
            var generoIdPropiedad = "GenerosIdentificador";
            var movieIdPropiedad = "PeliculasId";

            var entidadSalaDeCinePelicula = "PeliculaSalaDeCine";
            var salaDeCineIdPropiedad = "SalasDeCineId";

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdPropiedad] = acción.Identifier, [movieIdPropiedad] = avengers.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = cienciaFicción.Identifier, [movieIdPropiedad] = avengers.Id }
            );

            var coco = new Movie()
            {
                Id = 2,
                Title = "Coco",
                OnCinemas = false,
                ReleaseDate = new DateTime(2017, 11, 22),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/9/98/Coco_%282017_film%29_poster.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
               new Dictionary<string, object> { [generoIdPropiedad] = animación.Identifier, [movieIdPropiedad] = coco.Id }
           );

            var noWayHome = new Movie()
            {
                Id = 3,
                Title = "Spider-Man: No way home",
                OnCinemas = false,
                ReleaseDate = new DateTime(2021, 12, 17),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
               new Dictionary<string, object> { [generoIdPropiedad] = cienciaFicción.Identifier, [movieIdPropiedad] = noWayHome.Id },
               new Dictionary<string, object> { [generoIdPropiedad] = acción.Identifier, [movieIdPropiedad] = noWayHome.Id },
               new Dictionary<string, object> { [generoIdPropiedad] = comedia.Identifier, [movieIdPropiedad] = noWayHome.Id }
           );

            var farFromHome = new Movie()
            {
                Id = 4,
                Title = "Spider-Man: Far From Home",
                OnCinemas = false,
                ReleaseDate = new DateTime(2019, 7, 2),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
               new Dictionary<string, object> { [generoIdPropiedad] = cienciaFicción.Identifier, [movieIdPropiedad] = farFromHome.Id },
               new Dictionary<string, object> { [generoIdPropiedad] = acción.Identifier, [movieIdPropiedad] = farFromHome.Id },
               new Dictionary<string, object> { [generoIdPropiedad] = comedia.Identifier, [movieIdPropiedad] = farFromHome.Id }
           );

            var theMatrixResurrections = new Movie()
            {
                Id = 5,
                Title = "The Matrix Resurrections",
                OnCinemas = true,
                ReleaseDate = DateTime.Today,
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/5/50/The_Matrix_Resurrections.jpg",
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
              new Dictionary<string, object> { [generoIdPropiedad] = cienciaFicción.Identifier, [movieIdPropiedad] = theMatrixResurrections.Id },
              new Dictionary<string, object> { [generoIdPropiedad] = acción.Identifier, [movieIdPropiedad] = theMatrixResurrections.Id },
              new Dictionary<string, object> { [generoIdPropiedad] = drama.Identifier, [movieIdPropiedad] = theMatrixResurrections.Id }
          );

            modelBuilder.Entity(entidadSalaDeCinePelicula).HasData(
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine2DSambil.Id, [movieIdPropiedad] = theMatrixResurrections.Id },
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine3DSambil.Id, [movieIdPropiedad] = theMatrixResurrections.Id },
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine2DAgora.Id, [movieIdPropiedad] = theMatrixResurrections.Id },
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine3DAgora.Id, [movieIdPropiedad] = theMatrixResurrections.Id },
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine2DMegacentro.Id, [movieIdPropiedad] = theMatrixResurrections.Id },
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine3DMegacentro.Id, [movieIdPropiedad] = theMatrixResurrections.Id },
             new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCineCXCMegacentro.Id, [movieIdPropiedad] = theMatrixResurrections.Id }
         );


            var keanuReevesMatrix = new ActorMovie
            {
                ActorId = keanuReeves.Id,
                MovieId = theMatrixResurrections.Id,
                Order = 1,
                Character = "Neo"
            };

            var avengersChrisEvans = new ActorMovie
            {
                ActorId = chrisEvans.Id,
                MovieId = avengers.Id,
                Order = 1,
                Character = "Capitán América"
            };

            var avengersRobertDowney = new ActorMovie
            {
                ActorId = robertDowney.Id,
                MovieId = avengers.Id,
                Order = 2,
                Character = "Iron Man"
            };

            var avengersScarlettJohansson = new ActorMovie
            {
                ActorId = scarlettJohansson.Id,
                MovieId = avengers.Id,
                Order = 3,
                Character = "Black Widow"
            };

            var tomHollandFFH = new ActorMovie
            {
                ActorId = tomHolland.Id,
                MovieId = farFromHome.Id,
                Order = 1,
                Character = "Peter Parker"
            };

            var tomHollandNWH = new ActorMovie
            {
                ActorId = tomHolland.Id,
                MovieId = noWayHome.Id,
                Order = 1,
                Character = "Peter Parker"
            };

            var samuelJacksonFFH = new ActorMovie
            {
                ActorId = samuelJackson.Id,
                MovieId = farFromHome.Id,
                Order = 2,
                Character = "Samuel L. Jackson"
            };

            modelBuilder.Entity<Movie>().HasData(avengers, coco, noWayHome, farFromHome, theMatrixResurrections);
            modelBuilder.Entity<ActorMovie>().HasData(samuelJacksonFFH, tomHollandFFH, tomHollandNWH, avengersRobertDowney, avengersScarlettJohansson,
                avengersChrisEvans, keanuReevesMatrix);

        }
    }
}
