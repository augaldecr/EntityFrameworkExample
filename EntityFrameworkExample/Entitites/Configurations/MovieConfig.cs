using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(prop => prop.Title)
             .HasMaxLength(250)
             .IsRequired();

            builder.Property(prop => prop.PosterURL)
                .HasMaxLength(500)
                .IsUnicode(false);

            //builder.HasMany(p => p.Genres)
            //    .WithMany(g => g.Movies)
            //    .UsingEntity(j => 
            //        j.ToTable("GenresMovies")
            //        .HasData(new { MoviesId = 1, GenresIdentifier = 7 })
            //        );
        }
    }
}
