using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class MovieTheaterDetailsConfig : IEntityTypeConfiguration<MovieTheaterDetails>
    {
        public void Configure(EntityTypeBuilder<MovieTheaterDetails> builder)
        {
            builder.ToTable("MovieTheaters");
        }
    }
}
