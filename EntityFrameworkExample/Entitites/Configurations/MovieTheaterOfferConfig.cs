using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class MovieTheaterOfferConfig : IEntityTypeConfiguration<MovieTheaterOffer>
    {
        public void Configure(EntityTypeBuilder<MovieTheaterOffer> builder)
        {
            builder.Property(prop => prop.Discount)
                .HasPrecision(precision: 5, scale: 2);
        }
    }
}
