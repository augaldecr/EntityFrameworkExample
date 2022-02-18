using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable(name: "Bills", options =>
            {
                options.IsTemporal(t =>
                {
                    t.HasPeriodStart("From");
                    t.HasPeriodEnd("To");
                    t.UseHistoryTable(name: "BillsHistory");
                });
            });

            builder.Property<DateTime>("From").HasColumnType("datetime2");
            builder.Property<DateTime>("To").HasColumnType("datetime2");

            builder.HasMany(typeof(BillDetails)).WithOne();

            builder.Property(f => f.BillNumber)
                .HasDefaultValueSql("NEXT VALUE FOR bill.BillNumber");

            //builder.Property(f => f.Version).IsRowVersion();
        }
    }
}
