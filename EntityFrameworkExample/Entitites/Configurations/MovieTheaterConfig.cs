using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class MovieTheaterConfig : IEntityTypeConfiguration<MovieTheater>
    {
        public void Configure(EntityTypeBuilder<MovieTheater> builder)
        {

            builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
            
            builder.Property(prop => prop.Name)
              .HasMaxLength(150)
              .IsRequired();

            builder.HasOne(c => c.MovieTheaterOffer)
                .WithOne()
                .HasForeignKey<MovieTheaterOffer>(co => co.MovieTheaterId);

            builder.HasMany(c => c.Cinemas)
                .WithOne(s => s.MovieTheater)
                .HasForeignKey(s => s.TheMovieTheater)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.MovieTheaterDetails).WithOne(cd => cd.MovieTheater)
                .HasForeignKey<MovieTheaterDetails>(cd => cd.Id);

            builder.OwnsOne(c => c.Address, dir =>
            {
                dir.Property(d => d.Street).HasColumnName("Street");
                dir.Property(d => d.Province).HasColumnName("Province");
                dir.Property(d => d.Country).HasColumnName("Country");
            });
        }
    }
}
