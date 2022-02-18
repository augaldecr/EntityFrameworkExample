using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExample.Entitites.Configurations
{
    public class CardPaymentConfig : IEntityTypeConfiguration<CardPayment>
    {
        public void Configure(EntityTypeBuilder<CardPayment> builder)
        {
            builder.Property(p => p.Last4Digits).HasColumnType("char(4)").IsRequired();

            var pago1 = new CardPayment()
            {
                Id = 1,
                Date = new DateTime(2022, 1, 6),
                Amount = 500,
                PaymentType = PaymentType.Card,
                Last4Digits = "0123"
            };

            var pago2 = new CardPayment()
            {
                Id = 2,
                Date = new DateTime(2022, 1, 6),
                Amount = 120,
                PaymentType = PaymentType.Card,
                Last4Digits = "1234"
            };

            builder.HasData(pago1, pago2);

        }
    }
}
