using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {

            builder.ToTable(name: "Genres", options =>
            {
                options.IsTemporal();
            });

            builder.Property<DateTime>("PeriodStart").HasColumnType("datetime2");
            builder.Property<DateTime>("PeriodEnd").HasColumnType("datetime2");

            builder.HasKey(prop => prop.Identifier);
            builder.Property(prop => prop.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasQueryFilter(g => !g.Deleted);

            builder.HasIndex(g => g.Name).IsUnique().HasFilter("Deleted = 'false'");

            builder.Property<DateTime>("CreationDate").HasDefaultValueSql("GetDate()").HasColumnType("datetime2");
        }
    }
}
