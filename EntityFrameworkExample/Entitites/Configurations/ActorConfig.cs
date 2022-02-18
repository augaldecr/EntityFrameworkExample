using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(prop => prop.Name)
               .HasMaxLength(150)
               .IsRequired();

            builder.Property(x => x.Name).HasField("_name");

            //builder.Ignore(a => a.Age);
            //builder.Ignore(a => a.Address);

            builder.OwnsOne(a => a.HomeAddress, dir =>
            {
                dir.Property(d => d.Street).HasColumnName("Street");
                dir.Property(d => d.Province).HasColumnName("Province");
                dir.Property(d => d.Country).HasColumnName("Country");
            });
        }
    }
}
