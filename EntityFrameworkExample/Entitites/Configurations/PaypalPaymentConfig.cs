using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class PaypalPaymentConfig : IEntityTypeConfiguration<PaypalPayment>
    {
        public void Configure(EntityTypeBuilder<PaypalPayment> builder)
        {
            builder.Property(p => p.Email).HasMaxLength(150).IsRequired();

            var pago1 = new PaypalPayment()
            {
                Id = 3,
                Date = new DateTime(2022, 1, 7),
                Amount = 157,
                PaymentType = PaymentType.Paypal,
                Email = "felipe@hotmail.com"
            };

            var pago2 = new PaypalPayment()
            {
                Id = 4,
                Date = new DateTime(2022, 1, 7),
                Amount = 9.99m,
                PaymentType = PaymentType.Paypal,
                Email = "claudia@hotmail.com"
            };

            builder.HasData(pago1, pago2);

        }
    }
}
