using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class ActorMovieConfig : IEntityTypeConfiguration<ActorMovie>
    {
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasKey(prop =>
             new { prop.MovieId, prop.ActorId });

            builder.HasOne(pa => pa.Actor)
                    .WithMany(a => a.ActorsMovies)
                    .HasForeignKey(pa => pa.ActorId);

            builder.HasOne(pa => pa.Movie)
                    .WithMany(p => p.ActorsMovies)
                    .HasForeignKey(pa => pa.MovieId);

            builder.Property(prop => prop.Character)
                .HasMaxLength(150);
        }
    }
}
