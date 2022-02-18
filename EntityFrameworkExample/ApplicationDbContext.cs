using EntityFrameworkExample.Entitites;
using EntityFrameworkExample.Entitites.Functions;
using EntityFrameworkExample.Entitites.Keyless;
using EntityFrameworkExample.Entitites.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EntityFrameworkExample
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection", options =>
                {
                    options.UseNetTopologySuite();
                }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (!Database.IsInMemory())
            {
                MoviesSeeding.Seed(modelBuilder);
                MessagesSeeding.Seed(modelBuilder);
                BillsSeeding.Seed(modelBuilder);
            }

            Escalars.RegisterFunctions(modelBuilder);

            modelBuilder.HasSequence<int>("BillNumber", "bill");

            //modelBuilder.Entity<Log>().Property(l => l.Id).ValueGeneratedNever();
            //modelBuilder.Ignore<Address>();

            modelBuilder.Entity<LocationlessMovieTheater>()
                .HasNoKey().ToSqlQuery("Select Id, Name FROM MovieTheaters").ToView(null);

            modelBuilder.Entity<MovieWithStats>().HasNoKey().ToTable(name: null);

            modelBuilder.HasDbFunction(() => MovieWithStats(0));

            foreach (var tipoEntidad in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in tipoEntidad.GetProperties())
                {
                    if (property.ClrType == typeof(string) && property.Name.Contains("URL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        property.SetIsUnicode(false);
                        property.SetMaxLength(500);
                    }
                }
            }

            modelBuilder.Entity<Merchandising>().ToTable("Merchandising");
            modelBuilder.Entity<RentableMovie>().ToTable("RentableMovie");

            var movie1 = new RentableMovie()
            {
                Id = 1,
                Name = "Spider-Man",
                MovieId = 1,
                Price = 5.99m
            };

            var merch1 = new Merchandising()
            {
                Id = 2,
                OnInventory = true,
                Clothes = true,
                Name = "T-Shirt One Piece",
                Weight = 1,
                Volume = 1,
                Price = 11
            };

            modelBuilder.Entity<Merchandising>().HasData(merch1);
            modelBuilder.Entity<RentableMovie>().HasData(movie1);

        }

        [DbFunction]
        public int BillDetailsTotal(int billId)
        {
            return 0;
        }

        public IQueryable<MovieWithStats> MovieWithStats(int movieId)
        {
            return FromExpression(() => MovieWithStats(movieId));
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<MovieTheaterOffer> MovieTheaterOffers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LocationlessMovieTheater> LocationlessMovieTheaters { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MovieTheaterDetails> MovieTheatersDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillsDetails { get; set; }
    }
}
