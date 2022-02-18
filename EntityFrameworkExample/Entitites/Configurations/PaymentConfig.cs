using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasDiscriminator(p => p.PaymentType)
                .HasValue<PaypalPayment>(PaymentType.Paypal)
                .HasValue<CardPayment>(PaymentType.Card);

            builder.Property(p => p.Amount).HasPrecision(18, 2);
        }
    }
}
