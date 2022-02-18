using EntityFrameworkExample.Entitites.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class CinemaConfig : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(prop => prop.Price)
                .HasPrecision(precision: 9, scale: 2);

            builder.Property(prop => prop.CinemaType)
                .HasDefaultValue(CinemaType.TwoD)
                .HasConversion<string>();

            builder.Property(prop => prop.Currency)
                .HasConversion<CurrencyToSymbolConverter>();
        }
    }
}
